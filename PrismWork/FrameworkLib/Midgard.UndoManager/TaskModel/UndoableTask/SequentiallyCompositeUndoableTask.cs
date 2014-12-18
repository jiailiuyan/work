using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Work.UndoManager.TaskModel.UndoableTask
{
    /// <summary>
    /// 顺序执行的组合任务,与默认的组合任务不同
    /// 不使用字典类保存,使用List集合进行处理
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SequentiallyCompositeUndoableTask<T> : UndoableTaskBase<T>
    {
        /// <summary>
        /// 任务列表
        /// </summary>
        private List<UndoableTaskBase<T>> taskList;

        private string descriptionForUser;
        /// <summary>
        /// 用户描述信息
        /// </summary>
        public override string DescriptionForUser
        {
            get { return descriptionForUser; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="taskList">任务列表</param>
        /// <param name="descriptionForUser">描述信息</param>
        public SequentiallyCompositeUndoableTask(List<UndoableTaskBase<T>> taskList, string descriptionForUser)
        {
            ArgumentValidator.AssertNotNull(taskList, "tasks");

            this.descriptionForUser = descriptionForUser;
            //这里需要重新复制一个列表,外部会直接清理该列表
            this.taskList = taskList.ToList();
            Execute += OnExecute;
            Undo += OnUndo;
        }

        void OnExecute(object sender, TaskEventArgs<T> e)
        {
            ExecuteInternal(taskList, e.TaskMode);
        }

        protected internal virtual void ExecuteInternal(List<UndoableTaskBase<T>> taskList, TaskMode taskMode)
        {
            var performedTasks = new List<UndoableTaskBase<T>>();
            foreach (var item in taskList)
            {
                try
                {
                    item.PerformTask(null, taskMode);
                    performedTasks.Add(item);
                }
                catch (Exception)
                {
                    /* TODO: improve this to capture undone task errors. */
                    SafelyUndoTasks(performedTasks.Cast<IUndoableTask>());
                    throw;
                }
            }
        }

        static void SafelyUndoTasks(IEnumerable<IUndoableTask> undoableTasks)
        {
            try
            {
                foreach (var undoableTask in undoableTasks)
                {
                    try
                    {
                        undoableTask.Undo();
                    }
                    catch (Exception ex)
                    {
                        Debug.Assert(false, "SafelyUndoTasks failed " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        void OnUndo(object sender, TaskEventArgs<T> e)
        {
            /* Undo sequentially. */
            //这里需要注意的:在进行回退操作时,必须倒序进行处理
            //即模拟栈操作
            for (int i = taskList.Count - 1; i >= 0; i--)
            {
                ((IUndoableTask)taskList[i]).Undo();
            }
        }
    }
}
