using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Work.UndoManager.TaskModel;

namespace Work.UndoManager
{
    /// <summary>
    /// 组合任务服务接口
    /// </summary>
    public interface ICompositeTaskService
    {
        /// <summary>
        /// 当前组合任务名称
        /// </summary>
        string CurrentCompositeTaskName { get; }
        /// <summary>
        /// 是否正在执行打包组合任务
        /// </summary>
        bool IsRunningCompositeTask { get; }


        /// <summary>
        /// 执行用户给定的操作,并将操作期间产生的所有记录组合为一个单独的操作
        /// </summary>
        /// <param name="taskName">任务名称</param>
        /// <param name="aciton">需要执行的操作</param>
        /// <param name="isMergePropertyChange">是否合并重复的属性更改,默认值为True</param>
        void RunAsCompositeTask(string taskName, Action aciton, bool isMergePropertyChange = true);
        /// <summary>
        /// 执行用户给定的操作,并将操作期间产生的所有记录组合为一个单独的操作
        /// </summary>
        /// <param name="taskName">任务名称</param>
        /// <param name="aciton">执行操作</param>
        /// <param name="isMergePropertyChange">是否合并重复的属性更改,默认值为True</param>
        void RunAsCompositeTask(string taskName, Action<object> aciton, object parameter, bool isMergePropertyChange = true);
        /// <summary>
        /// 开始组合任务
        /// </summary>
        /// <param name="taskName">组合任务</param>
        /// <param name="isMergePropertyChange">是否合并重复的属性更改,默认值为True</param>
        void BeginCompositeTask(string taskName, bool isMergePropertyChange = true);
        /// <summary>
        /// 结束组合任务
        /// </summary>
        void EndCompositeTask();
        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="task"></param>
        void AddRecord(UndoTask task);
    }
}
