using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Work.UndoManager.Recorder
{
    /// <summary>
    /// 所有记录器的基类
    /// </summary>
    public abstract class BaseRecorder : IDisposable
    {
        #region 属性,字段
        /// <summary>
        /// 是否合并属性更改,
        /// 在当前Recorder第一次被CompositeTask反向通知时,设置为True
        /// </summary>
        protected bool IsMergePropertyChange { get; private set; }
        /// <summary>
        /// 当前监视器,监视的对象
        /// </summary>
        protected INotifyStateChanged objectItem;
        /// <summary>
        /// 是否正在执行撤销回退
        /// </summary>
        public static bool IsUndoing
        {
            get { return TaskServiceSingleton.Instance.IsUndoing; }
        }
        /// <summary>
        ///  任务分组名称
        /// </summary>
        public string TaskGroupName { get; private set; }

        /// <summary>
        /// 控制当前记录器,是否自动记录
        /// </summary>
        private bool isAutoRecord;
        public bool IsAutoRecord
        {
            get
            {
                return isAutoRecord;
            }
            set
            {
                isAutoRecord = value;
            }
        }

        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="objectItem">指定需要监视的对象</param>
        /// <param name="taskGroupName">指定当前监视器,需要将记录发送到那个堆栈</param>
        public BaseRecorder(INotifyStateChanged objectItem, string taskGroupName = null)
        {
            IsAutoRecord = true;
            this.objectItem = objectItem;
            this.TaskGroupName = taskGroupName;
        }
        #endregion

        #region 内部方法
        /// <summary>
        /// 提供给外部回调,在重做前执行
        /// </summary>
        /// <param name="args"></param>
        internal void Redoing(RecorderTaskEventArgs args)
        {
            if (objectItem == null)
                return;
            IRecordableCallback callback = objectItem as IRecordableCallback;
            if (callback != null)
            {
                callback.Redoing(args);
            }
        }
        /// <summary>
        /// 提供给外部回调,在重做完成后执行
        /// </summary>
        /// <param name="args"></param>
        internal void Redone(RecorderTaskEventArgs args)
        {
            if (objectItem == null)
                return;
            IRecordableCallback callback = objectItem as IRecordableCallback;
            if (callback != null)
            {
                callback.Redone(args);
            }
        }
        /// <summary>
        ///  提供给外部回调,在撤销前执行
        /// </summary>
        /// <param name="args">事件参数</param>
        internal void Undoing(RecorderTaskEventArgs args)
        {
            if (objectItem == null)
                return;
            IRecordableCallback callback = objectItem as IRecordableCallback;
            if (callback != null)
            {
                callback.Undoing(args);
            }
        }
        /// <summary>
        /// 提供给外部回调,在撤销完成后执行
        /// </summary>
        /// <param name="args">事件参数</param>
        internal void Undone(RecorderTaskEventArgs args)
        {
            if (objectItem == null)
                return;
            IRecordableCallback callback = objectItem as IRecordableCallback;
            if (callback != null)
            {
                callback.Undone(args);
            }
        }
        /// <summary>
        /// 开始执行组合任务,
        /// 在开启执行组合任务后,第一次添加记录时,会反向调用该方法
        /// </summary>
        /// <param name="task">Undo任务</param>
        internal void BeginedCompositeTask(UndoTask task)
        {
            IsMergePropertyChange = true;
            OnBeginedCompositeTask(task);
        }
        /// <summary>
        /// 在完成组合任务之前,会反向调用该方法
        /// 用于生成最新
        /// </summary>
        internal List<UndoTask> EndingCompositeTask()
        {
            if (IsMergePropertyChange == false)
                throw new InvalidOperationException("Current recorder does not merge property change. Please check if you call BeginCompositeTask.");

            var list = OnEndingCompositeTask();

            IsMergePropertyChange = false;
            return list;
        }
        /// <summary>
        /// 开始执行组合任务,
        /// 在开启执行组合任务后,第一次添加记录时,会反向调用该方法
        /// </summary>
        protected virtual void OnBeginedCompositeTask(UndoTask task)
        {
        }
        /// <summary>
        /// 结束组合任务,
        /// 收集在执行过程中,产生的任务,并返回
        /// </summary>
        /// <returns></returns>
        protected virtual List<UndoTask> OnEndingCompositeTask()
        {
            return null;
        }
        /// <summary>
        /// 打开监视器时调用,提供子类重载接口
        /// </summary>
        /// <param name="isCreateRecorder">在重新开启时,是否自动生成记录</param>
        protected virtual void OnStart(bool isCreateRecorder)
        { }
        /// <summary>
        /// 关闭监视,
        /// </summary>
        protected virtual void OnStop()
        { }
        #endregion

        #region 公开方法
        /// <summary>
        /// 添加一次历史记录,推送到组合任务中
        /// 在组合任务管理器中,进行处理
        /// </summary>
        /// <param name="undoTask">任务</param>
        public void AddRecord(UndoTask undoTask)
        {
            CompositeTaskManager.Instance.AddRecord(undoTask);
        }
        /// <summary>
        /// 开启监视,并控制是否检测属性的更改生成记录.
        /// </summary>
        /// <param name="isCreateRecorder">在重新开启时,是否自动生成记录,默认值为True</param>
        public void Start(bool isCreateRecorder = true)
        {
            if (IsAutoRecord)
            {
#if DEBUG
                throw new InvalidOperationException("This recorder is already opened.");
#else
                LogConfig.Logger.Error("This recorder is already opened.");
                return;
#endif
            }
            if (IsMergePropertyChange && isCreateRecorder)
                throw new InvalidOperationException("Can't open merge property and create recorder at the same time.");

            IsAutoRecord = true;
            objectItem.IsRaiseStateChanged = true;
            OnStart(isCreateRecorder);
        }
        /// <summary>
        /// 关闭监视,并
        /// </summary>
        public void Stop()
        {
            if (IsAutoRecord == false)
            {
#if DEBUG
                throw new InvalidOperationException("This recorder is not opened.");
#else
                LogConfig.Logger.Error("This recorder is not opened.");
                return;
#endif
            }

            IsAutoRecord = false;
            objectItem.IsRaiseStateChanged = false;
            OnStop();
        }
        /// <summary>
        /// 清理当前对象保留的引用
        /// </summary>
        public virtual void Dispose()
        {
            objectItem = null;
        }
        #endregion
    }
}
