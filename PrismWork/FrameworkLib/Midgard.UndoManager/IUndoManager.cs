using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Work.UndoManager.TaskModel;

namespace Work.UndoManager
{
    /// <summary>
    /// 撤销回退管理器
    /// </summary>
    public interface IUndoManager : ITaskService
    {
        /// <summary>
        /// 当前组合任务名称
        /// </summary>
        string CurrentCompositeTaskName { get; }
        /// <summary>
        /// 当前正在回退的任务
        /// </summary>
        string DoingTaskName { get; }
        /// <summary>
        /// 是否正在执行打包任务
        /// </summary>
        bool IsRunningCompositeTask { get; }
        /// <summary>
        /// 开始组合任务
        /// </summary>
        /// <param name="taskName">组合任务名称</param>
        ///// <param name="isMergePropertyChange">是否属性更改合并,默认值为false</param>
        void BeginCompositeTask(string taskName);
        /// <summary>
        /// 结束组合任务
        /// </summary>
        void EndCompositeTask();
    }
}
