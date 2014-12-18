using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Work.UndoManager.Recorder;
using Work.UndoManager.TaskModel;

namespace Work.UndoManager
{
    /// <summary>
    /// 集合操作记录类
    /// </summary>
    public class UndoTask : UndoableTaskBase<object>
    {
        #region 字段
        /// <summary>
        /// 是否执行
        /// </summary>
        protected Predicate<object> canExecute;
        /// <summary>
        /// 执行Redo调用
        /// </summary>
        protected Action<object> execute;
        /// <summary>
        /// 执行Undo调用
        /// </summary>
        protected Action<object> unExecute;
        #endregion

        #region 属性
        /// <summary>
        /// 用户描述信息
        /// </summary>
        public override string DescriptionForUser
        {
            get { return "UndoTask"; }
        }
        /// <summary>
        /// 任务分组名称,支持历史记录分组
        /// </summary>
        public string TaskGroupName
        {
            get
            {
                if (this.Recorder == null)
                    return null;
                return this.Recorder.TaskGroupName;
            }
        }
        /// <summary>
        /// 撤销操作
        /// </summary>
        public EnumUndoAction Action { get; protected set; }
        /// <summary>
        /// 监视器
        /// </summary>
        internal BaseRecorder Recorder { get; private set; }
        #endregion

        #region 构造函数
        /// <summary>
        /// 记录器
        /// </summary>
        /// <param name="recorder"></param>
        public UndoTask(BaseRecorder recorder)
        {
            this.Recorder = recorder;
            this.Init();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="recorder">回退记录器</param>
        /// <param name="actionType"></param>
        /// <param name="execute">执行</param>
        /// <param name="unExecute">反向执行,即Undo操作</param>
        /// <param name="canExecute">能否执行</param>
        public UndoTask(BaseRecorder recorder, EnumUndoAction actionType, Action<object> execute, Action<object> unExecute,
                   Predicate<object> canExecute = null)
        {
            Contract.Requires(execute != null);

            this.Recorder = recorder;
            this.Init();

            this.execute = execute;
            this.Action = actionType;
            this.unExecute = unExecute;
            this.canExecute = canExecute;
            this.Repeatable = false;
        }
        #endregion

        #region 方法
        /// <summary>
        /// 初始化任务回退类
        /// </summary>
        private void Init()
        {
            this.Repeatable = false;
            this.RegisterEventHandle();
        }

        /// <summary>
        /// 注册撤销回退事件回调
        /// </summary>
        private void RegisterEventHandle()
        {
            base.Execute += OnExecute;
            base.Undo += OnUndo;
        }

        /// <summary>
        /// 执行,即Redo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnExecute(object sender, TaskEventArgs<object> e)
        {
            if (e.TaskMode == TaskMode.Redo)
            {
                if (Recorder != null)
                {
                    RecorderTaskEventArgs args = new RecorderTaskEventArgs(this);
                    Recorder.Redoing(args);
                    if (args.Enabled)
                        execute(null);
                    Recorder.Redone(args);
                }
                else
                    execute(null);
            }
        }

        /// <summary>
        /// 撤销
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnUndo(object sender, TaskEventArgs<object> e)
        {
            if (Recorder != null)
            {
                RecorderTaskEventArgs args = new RecorderTaskEventArgs(this);
                Recorder.Undoing(args);
                if (args.Enabled)
                    unExecute(null);
                Recorder.Undone(args);
            }
            else
                unExecute(null);
        }
        #endregion

    }
}
