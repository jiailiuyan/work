using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Work.UndoManager.Recorder;
using Work.UndoManager.TaskModel;
using Work.UndoManager.TaskModel.UndoableTask;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.ServiceLocation;
using System.Diagnostics;

namespace Work.UndoManager
{
    /// <summary>
    /// 组合任务管理
    /// 作为所有属性检测信息的统一管理者
    /// 接收所有属性更改的通知
    /// </summary>
    [Export(typeof(ICompositeTaskService))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class CompositeTaskManager : ICompositeTaskService
    {
        #region 字段,属性
        /// <summary>
        /// 任务管理器
        /// </summary>
        private IUndoManager taskService;
        /// <summary>
        /// 任务集合字典
        /// </summary>
        private List<UndoableTaskBase<object>> taskList;
        /// <summary>
        /// 当前任务分组
        /// </summary>
        private string currentTaskGroupName;
        /// <summary>
        /// 是否合并属性更改
        /// </summary>
        public bool IsMergePropertyChange { get; private set; }
        /// <summary>
        /// 缓存的Recorder集合
        /// </summary>
        private HashSet<BaseRecorder> tempRecorderList;
        /// <summary>
        /// 当前组合任务名称
        /// </summary>
        public string CurrentCompositeTaskName { get; private set; }
        /// <summary>
        /// 指示是否正在执行组合任务,控制是否直接打包任务
        /// </summary>
        public bool IsRunningCompositeTask { get; private set; }
        #endregion

        #region 单例
        /// <summary>
        /// 当前对象唯一实例
        /// </summary>
        private static CompositeTaskManager instance;
        /// <summary>
        /// 私有构造函数
        /// </summary>
        private CompositeTaskManager()
        {
            taskList = new List<UndoableTaskBase<object>>();
            taskService = TaskServiceSingleton.Instance;
            tempRecorderList = new HashSet<BaseRecorder>();
        }
        /// <summary>
        /// 获取当前对象唯一实例
        /// </summary>
        /// <returns></returns>
        public static ICompositeTaskService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = (CompositeTaskManager)ServiceLocator.Current.GetInstance<ICompositeTaskService>();
                }
                return instance;
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 压入组合任务到管理器
        /// </summary>
        private void PushCompositeTask(string taskName, List<UndoableTaskBase<object>> tasks)
        {
            //在此认为如果组合列表里面没有操作的话就不压入撤销栈
            if (tasks.Count <= 0)
            {
                Debug.Assert(false, "There is no tasks in the composite task");
                return;
            }

            SequentiallyCompositeUndoableTask<object> compositeTask = new SequentiallyCompositeUndoableTask<object>(tasks, taskName);
            taskService.PerformTask<object>(compositeTask, null, currentTaskGroupName);
        }
        /// <summary>
        /// 添加并更新任务
        /// </summary>
        /// <param name="task"></param>
        private void CheckSubTask(UndoTask task)
        {
            if (IsMergePropertyChange)//通过缓存的Recorder列表进行判断
            {
                CheckTaskGroupName(task, tempRecorderList.Count);
            }
            else//通过已经存储的任务列表进行判断
            {
                CheckTaskGroupName(task, taskList.Count);
            }
        }
        /// <summary>
        /// 检测当前任务的分组名称,保证同时只允许一个堆栈任务执行
        /// 通过给定的任务集合,如果任务个数大于0,则说明已经存在第一个任务了,
        /// 直接判断后来的任务分组名称是否相同
        /// </summary>
        /// <param name="task">当前任务</param>
        /// <param name="listCount">当前需要检测是任务集合个数</param>
        private void CheckTaskGroupName(UndoTask task, int listCount)
        {
            if (listCount > 0)
            {
                if (task.TaskGroupName != currentTaskGroupName)
                    throw new InvalidOperationException("Can run only one composite task at one time. All tasks should in the same group task.");
            }
            else//对于第一个任务,根据第一个任务设置任务分组名称
            {
                currentTaskGroupName = task.TaskGroupName;
            }
        }

        /// <summary>
        /// 在执行组合任务时,添加子任务
        /// </summary>
        /// <param name="task"></param>
        private void AddSubTask(UndoTask task)
        {
            if (IsMergePropertyChange)//缓存Recorder,用于后续搜集子任务
            {
                tempRecorderList.Add(task.Recorder);
                task.Recorder.BeginedCompositeTask(task);
            }
            else
            {
                taskList.Add(task);
            }
        }
        /// <summary>
        /// 收集子任务,通知当前所有
        /// </summary>
        private List<UndoableTaskBase<object>> CollectSubTasks()
        {
            List<UndoableTaskBase<object>> list = new List<UndoableTaskBase<object>>();
            foreach (var item in tempRecorderList)
            {
                if (item != null)
                {
                    var subTaskList = item.EndingCompositeTask();
                    if (subTaskList != null)
                    {
                        list.AddRange(subTaskList);
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="task"></param>
        public void AddRecord(UndoTask task)
        {
            if (taskService.Enable == false)
                return;
            if (IsRunningCompositeTask)
            {
                CheckSubTask(task);
                AddSubTask(task);
            }
            else
                taskService.PerformTask<object>(task, null, task.TaskGroupName);
        }
        /// <summary>
        /// 执行操作,并作为组合任务进行打包
        /// </summary>
        /// <param name="taskName"></param>
        /// <param name="aciton"></param>
        public void RunAsCompositeTask(string taskName, Action aciton, bool isMergePropertyChange = true)
        {
            BeginCompositeTask(taskName, isMergePropertyChange);
            aciton();
            EndCompositeTask();
        }
        /// <summary>
        /// 执行操作,并作为组合任务进行打包
        /// </summary>
        /// <param name="taskName"></param>
        /// <param name="aciton"></param>
        public void RunAsCompositeTask(string taskName, Action<object> aciton, object parameter, bool isMergePropertyChange = true)
        {
            RunAsCompositeTask(taskName, delegate { aciton(parameter); }, isMergePropertyChange);
        }
        /// <summary>
        /// 开始组合任务,后续发生的所有任务必须是同一个任务堆栈
        /// </summary>
        /// <param name="taskName"></param>
        public void BeginCompositeTask(string taskName, bool isMergePropertyChange = true)
        {

            if (IsRunningCompositeTask)
            {
#if DEBUG
                throw new InvalidOperationException("Begin composite task twice.One composite task is running.");
#else
                LogConfig.Logger.Error("Begin composite task twice.One composite task is running.");
                return;
#endif
            }

            IsRunningCompositeTask = true;
            CurrentCompositeTaskName = taskName;
            this.IsMergePropertyChange = isMergePropertyChange;
        }
        /// <summary>
        /// 结束组合任务
        /// </summary>
        public void EndCompositeTask()
        {
            if (IsRunningCompositeTask == false)
            {
#if DEBUG
                throw new InvalidOperationException("Not running composite task. Cann't end composite task.");
#else
                LogConfig.Logger.Error("Not running composite task. Cann't end composite task.");
                return;
#endif
            }

            if (IsMergePropertyChange)//如果启用了属性合并,则收集子任务进行打包
            {
                var subTasks = CollectSubTasks();
                PushCompositeTask(CurrentCompositeTaskName, subTasks);
                tempRecorderList.Clear();
                IsMergePropertyChange = false;
            }
            else
            {
                PushCompositeTask(CurrentCompositeTaskName, taskList);
                taskList.Clear();
            }

            IsRunningCompositeTask = false;
            CurrentCompositeTaskName = null;
        }
        #endregion
    }
}
