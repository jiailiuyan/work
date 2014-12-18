using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Work.UndoManager.TaskModel;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;

namespace Work.UndoManager
{
    /// <summary>
    /// 撤销重做管理单实例类
    /// 提供撤销重做处理的统一接口
    /// </summary>
    [Export(typeof(IUndoManager))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class TaskServiceSingleton : TaskService, IUndoManager
    {
        #region 单例
        /// <summary>
        /// 当前对象唯一实例
        /// </summary>
        private static TaskServiceSingleton instance;
        /// <summary>
        /// 私有构造函数
        /// </summary>
        private TaskServiceSingleton()
        {
        }
        /// <summary>
        /// 获取当前对象唯一实例
        /// </summary>
        /// <returns></returns>
        public static IUndoManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = (TaskServiceSingleton)ServiceLocator.Current.GetInstance<IUndoManager>();
                }
                return instance;
            }

        }
        #endregion

        protected override void OnUndoing(CancellableTaskServiceEventArgs e)
        {
            this.DoingTaskName = e.Task.DescriptionForUser;
            base.OnUndoing(e);
        }

        protected override void OnUndone(TaskServiceEventArgs e)
        {
            base.OnUndone(e);
            this.DoingTaskName = null;
        }

        protected override void OnRedoing(CancellableTaskServiceEventArgs e)
        {
            this.DoingTaskName = e.Task.DescriptionForUser;
            base.OnRedoing(e);
        }

        protected override void OnRedone(TaskServiceEventArgs e)
        {
            base.OnRedone(e);
            this.DoingTaskName = null;
        }

        /// <summary>
        /// 开始打包处理,将监视到的任务组合
        /// </summary>
        /// <param name="taskName"></param>
        ///// <param name="isMergePropertyChange">暂时屏蔽该接口,效率太低.是否启用属性合并,默认值为True</param>
        public void BeginCompositeTask(string taskName)
        {
            //屏蔽原有自动合并接口,通过Recorder的Start和Stop接口,实现自动检测属性改变.
            CompositeTaskManager.Instance.BeginCompositeTask(taskName, false);
        }
        /// <summary>
        /// 结束打包处理
        /// </summary>
        public void EndCompositeTask()
        {
            CompositeTaskManager.Instance.EndCompositeTask();
        }

        /// <summary>
        /// 是否正在执行打包任务
        /// </summary>
        public bool IsRunningCompositeTask
        {
            get { return CompositeTaskManager.Instance.IsRunningCompositeTask; }
        }

        /// <summary>
        /// 当前正在执行打包的任务名称
        /// </summary>
        public string CurrentCompositeTaskName
        {
            get { return CompositeTaskManager.Instance.CurrentCompositeTaskName; }
        }

        /// <summary>
        /// 当前正在执行的任务
        /// </summary>
        public string DoingTaskName { get; private set; }
    }
}
