#region File and License Information
/*
<File>
	<License Type="BSD">
		Copyright ?2009 - 2012, Daniel Vaughan. All rights reserved.
	
		This file is part of Calcium (http://calciumsdk.net).

		Redistribution and use in source and binary forms, with or without
		modification, are permitted provided that the following conditions are met:
			* Redistributions of source code must retain the above copyright
			  notice, this list of conditions and the following disclaimer.
			* Redistributions in binary form must reproduce the above copyright
			  notice, this list of conditions and the following disclaimer in the
			  documentation and/or other materials provided with the distribution.
			* Neither the name of the <organization> nor the
			  names of its contributors may be used to endorse or promote products
			  derived from this software without specific prior written permission.

		THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
		ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
		WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
		DISCLAIMED. IN NO EVENT SHALL <COPYRIGHT HOLDER> BE LIABLE FOR ANY
		DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
		(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
		LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
		ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
		(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
		SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
	</License>
	<Owner Name="Daniel Vaughan" Email="Work.UndoManager@outcoder.com" />
	<CreationDate>2010-01-23 17:21:10Z</CreationDate>
</File>
*/
#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Work.UndoManager.TaskModel
{
    /// <summary>
    /// Provides for execution, undoing, and redoing
    /// of <see cref="ITask"/> instances.
    /// </summary>
    public class TaskService : ITaskService, IInternalTaskService
    {
        readonly Dictionary<object, TaskCollection<IInternalTask>> repeatableDictionary = new Dictionary<object, TaskCollection<IInternalTask>>();
        readonly Dictionary<object, TaskCollection<IUndoableTask>> redoableDictionary = new Dictionary<object, TaskCollection<IUndoableTask>>();
        readonly Dictionary<object, TaskCollection<IUndoableTask>> undoableDictionary = new Dictionary<object, TaskCollection<IUndoableTask>>();

        readonly TaskCollection<IInternalTask> globallyRepeatableTasks = new TaskCollection<IInternalTask>();
        readonly TaskCollection<IUndoableTask> globallyRedoableTasks = new TaskCollection<IUndoableTask>();
        readonly TaskCollection<IUndoableTask> globallyUndoableTasks = new TaskCollection<IUndoableTask>();

        /// <summary>
        /// 是否正在执行回退操作,在执行回退操作的过程中,不再响应外部的操作输入
        /// </summary>
        public bool IsUndoing { get; private set; }

        #region PerformTask
        /// <summary>Executes the specified task.</summary>
        /// <param name="task">The command to execute.</param>
        /// <param name="argument">The argument passed to the task on execution.</param>
        public TaskResult PerformTask<T>(TaskBase<T> task, T argument, object ownerKey = null)
        {
            if (Enable == false || IsUndoing)
                return TaskResult.NoEnable;

            ArgumentValidator.AssertNotNull(task, "task");

            if (ownerKey == null)
            {
                return PerformTask(task, argument);
            }

            var eventArgs = new CancellableTaskServiceEventArgs(task);
            OnExecuting(eventArgs);

            if (eventArgs.Cancel)
            {
                return TaskResult.Cancelled;
            }

            /* Clear the undoable tasks for this context. */
            undoableDictionary.Remove(ownerKey);
            redoableDictionary.Remove(ownerKey);

            TaskCollection<IInternalTask> repeatableTasks;
            if (!repeatableDictionary.TryGetValue(ownerKey, out repeatableTasks))
            {
                repeatableTasks = new TaskCollection<IInternalTask>();
                repeatableDictionary[ownerKey] = repeatableTasks;
            }
            repeatableTasks.AddLast(task);

            var result = task.PerformTask(argument, TaskMode.FirstTime);

            TrimIfRequired(ownerKey);

            OnExecuted(new TaskServiceEventArgs(task));
            return result;
        }

        TaskResult PerformTask<T>(TaskBase<T> task, T argument)
        {
            var eventArgs = new CancellableTaskServiceEventArgs(task);
            OnExecuting(eventArgs);

            if (eventArgs.Cancel)
            {
                return TaskResult.Cancelled;
            }

            globallyRedoableTasks.Clear();
            globallyUndoableTasks.Clear();

            globallyRepeatableTasks.AddLast(task);

            TaskResult result = task.PerformTask(argument, TaskMode.FirstTime);

            TrimIfRequired();

            OnExecuted(new TaskServiceEventArgs(task));
            return result;
        }

        /// <summary>Executes the specified task.</summary>
        /// <param name="task">The command to execute.</param>
        /// <param name="argument">The argument passed to the task on execution.</param>
        public TaskResult PerformTask<T>(UndoableTaskBase<T> task, T argument, object ownerKey = null)
        {
            if (Enable == false || IsUndoing)
                return TaskResult.NoEnable;

            ArgumentValidator.AssertNotNull(task, "task");

            if (ownerKey == null)
            {
                return PerformTask(task, argument);
            }

            var eventArgs = new CancellableTaskServiceEventArgs(task) { OwnerKey = ownerKey };
            OnExecuting(eventArgs);
            if (eventArgs.Cancel)
            {
                return TaskResult.Cancelled;
            }

            redoableDictionary.Remove(ownerKey);

            TaskCollection<IInternalTask> repeatableTasks;
            if (!repeatableDictionary.TryGetValue(ownerKey, out repeatableTasks))
            {
                repeatableTasks = new TaskCollection<IInternalTask>();
                repeatableDictionary[ownerKey] = repeatableTasks;
            }
            repeatableTasks.AddLast(task);

            TaskCollection<IUndoableTask> undoableTasks;
            if (!undoableDictionary.TryGetValue(ownerKey, out undoableTasks))
            {
                undoableTasks = new TaskCollection<IUndoableTask>();
                undoableDictionary[ownerKey] = undoableTasks;
            }
            undoableTasks.AddLast(task);

            TaskResult result = task.PerformTask(argument, TaskMode.FirstTime);

            TrimIfRequired(ownerKey);

            OnExecuted(new TaskServiceEventArgs(task));
            return result;
        }

        TaskResult PerformTask<T>(UndoableTaskBase<T> task, T argument)
        {
            var eventArgs = new CancellableTaskServiceEventArgs(task);
            OnExecuting(eventArgs);
            if (eventArgs.Cancel)
            {
                return TaskResult.Cancelled;
            }

            globallyRedoableTasks.Clear();
            globallyRepeatableTasks.AddLast(task);
            globallyUndoableTasks.AddLast(task);

            TaskResult result = task.PerformTask(argument, TaskMode.FirstTime);

            TrimIfRequired();

            OnExecuted(new TaskServiceEventArgs(task));
            return result;
        }

        #endregion

        public bool CanUndo(object ownerKey = null)
        {
            if (ownerKey == null)
            {
                return globallyUndoableTasks.Count > 0;
            }

            TaskCollection<IUndoableTask> undoableTasks;
            if (undoableDictionary.TryGetValue(ownerKey, out undoableTasks))
            {
                return undoableTasks.Count > 0;
            }
            return false;
        }

        /// <summary>
        /// Undoes the execution of a previous <see cref="ITask"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Occurs if there are no previously executed tasks to undo.</exception>
        public TaskResult Undo(object ownerKey = null)
        {
            if (ownerKey == null)
            {
                return Undo();
            }

            TaskCollection<IUndoableTask> undoableTasks;
            if (!undoableDictionary.TryGetValue(ownerKey, out undoableTasks))
            {
                throw new InvalidOperationException("No undoable tasks for the specified owner key.");
            }

            IUndoableTask undoableTask = undoableTasks.Pop();

            TaskCollection<IInternalTask> repeatableTasks;
            if (!repeatableDictionary.TryGetValue(ownerKey, out repeatableTasks))
            {
                throw new InvalidOperationException("No repeatable tasks for the specified owner key.");
            }

            repeatableTasks.RemoveLast();

            var eventArgs = new CancellableTaskServiceEventArgs(undoableTask) { OwnerKey = ownerKey };
            OnUndoing(eventArgs);
            if (eventArgs.Cancel)
            {
                undoableTasks.AddLast(undoableTask);
                return TaskResult.Cancelled;
            }

            TaskCollection<IUndoableTask> redoableTasks;
            if (!redoableDictionary.TryGetValue(ownerKey, out redoableTasks))
            {
                redoableTasks = new TaskCollection<IUndoableTask>();
                redoableDictionary[ownerKey] = redoableTasks;
            }
            redoableTasks.AddLast(undoableTask);

            var result = undoableTask.Undo();

            TrimIfRequired(ownerKey);

            OnUndone(new TaskServiceEventArgs(undoableTask));
            return result;
        }

        TaskResult Undo()
        {
            if (globallyRepeatableTasks.Count < 1)
            {
                throw new InvalidOperationException("No task to undo.");
            }

            IUndoableTask undoableTask = globallyUndoableTasks.Pop();
            IInternalTask repeatableTask = globallyRepeatableTasks.Pop();

            var eventArgs = new CancellableTaskServiceEventArgs(undoableTask);
            OnUndoing(eventArgs);
            if (eventArgs.Cancel)
            {
                globallyUndoableTasks.AddLast(undoableTask);
                globallyRepeatableTasks.AddLast(repeatableTask);
                return TaskResult.Cancelled;
            }

            globallyRedoableTasks.AddLast(undoableTask);

            TaskResult result = undoableTask.Undo();

            OnUndone(new TaskServiceEventArgs(undoableTask));
            return result;
        }

        public TaskResult Undo(int undoCount, object ownerKey = null)
        {
            ArgumentValidator.AssertGreaterThan(undoCount, 0, "undoCount");
            if (ownerKey == null)
            {
                return Undo(undoCount);
            }

            for (int i = 0; i < undoCount; i++)
            {
                var iterationResult = Undo(ownerKey);
                if (iterationResult != TaskResult.Completed)
                {
                    return iterationResult;
                }
            }
            return TaskResult.Completed;
        }

        TaskResult Undo(int undoCount)
        {
            for (int i = 0; i < undoCount; i++)
            {
                var iterationResult = Undo();
                if (iterationResult != TaskResult.Completed)
                {
                    return iterationResult;
                }
            }
            return TaskResult.Completed;
        }

        public bool CanRedo(object ownerKey = null)
        {
            TaskCollection<IUndoableTask> redoableTasks;

            if (ownerKey == null)
            {
                return CanRedo();
            }

            if (redoableDictionary.TryGetValue(ownerKey, out redoableTasks))
            {
                return redoableTasks.Count > 0;
            }
            return false;
        }

        bool CanRedo()
        {
            return globallyRedoableTasks.Count > 0;
        }

        /// <summary>
        /// Performs the execution of a <see cref="ITask"/>
        /// instance that has been undone, then places it back
        /// into the command stack.
        /// </summary>
        public TaskResult Redo(object ownerKey = null)
        {
            if (ownerKey == null)
            {
                return Redo();
            }

            TaskCollection<IUndoableTask> redoableTasks;
            if (!redoableDictionary.TryGetValue(ownerKey, out redoableTasks))
            {
                throw new InvalidOperationException("No tasks to be redone for the specified owner key.");
            }
            IUndoableTask task = redoableTasks.Pop();

            var eventArgs = new CancellableTaskServiceEventArgs(task);
            OnRedoing(eventArgs);

            if (eventArgs.Cancel)
            {
                redoableTasks.AddLast(task);
                return TaskResult.Cancelled;
            }

            var internalTask = (IInternalTask)task;

            TaskCollection<IInternalTask> repeatableTasks;
            if (!repeatableDictionary.TryGetValue(ownerKey, out repeatableTasks))
            {
                repeatableTasks = new TaskCollection<IInternalTask>();
            }
            repeatableTasks.AddLast(internalTask);

            TaskCollection<IUndoableTask> undoableTasks;
            if (!undoableDictionary.TryGetValue(ownerKey, out undoableTasks))
            {
                undoableTasks = new TaskCollection<IUndoableTask>();
            }
            undoableTasks.AddLast(task);

            TaskResult result = internalTask.PerformTask(internalTask.Argument, TaskMode.Redo);

            TrimIfRequired(ownerKey);

            OnRedone(new TaskServiceEventArgs(task));
            return result;
        }

        TaskResult Redo()
        {
            if (globallyRedoableTasks.Count < 1)
            {
                throw new InvalidOperationException("No task to redo."); /* TODO: Make localizable resource. */
            }

            var task = globallyRedoableTasks.Pop();
            var eventArgs = new CancellableTaskServiceEventArgs(task);
            OnRedoing(eventArgs);

            if (eventArgs.Cancel)
            {
                globallyRedoableTasks.AddLast(task);
                return TaskResult.Cancelled;
            }

            var internalTask = (IInternalTask)task;

            globallyRepeatableTasks.AddLast(internalTask);
            globallyUndoableTasks.AddLast(task);

            var result = internalTask.PerformTask(internalTask.Argument, TaskMode.Redo);

            TrimIfRequired();

            OnRedone(new TaskServiceEventArgs(task));
            return result;
        }

        public TaskResult Repeat(object ownerKey = null)
        {
            if (ownerKey == null)
            {
                return Repeat();
            }

            TaskCollection<IInternalTask> repeatableTasks;
            if (!repeatableDictionary.TryGetValue(ownerKey, out repeatableTasks))
            {
                throw new InvalidOperationException("No tasks to be redone for the specified owner key.");
            }
            var task = repeatableTasks.Peek();
            if (!task.Repeatable)
            {
                return TaskResult.NoTask;
            }

            var eventArgs = new CancellableTaskServiceEventArgs(task) { OwnerKey = ownerKey };
            OnExecuting(eventArgs);
            if (eventArgs.Cancel)
            {
                return TaskResult.Cancelled;
            }

            repeatableTasks.AddLast(task);

            TaskCollection<IUndoableTask> undoableTasks;
            if (!undoableDictionary.TryGetValue(ownerKey, out undoableTasks))
            {
                undoableTasks = new TaskCollection<IUndoableTask>();
                undoableDictionary[ownerKey] = undoableTasks;
            }

            var undoableTask = task as IUndoableTask;
            if (undoableTask != null)
            {
                undoableTasks.AddLast(undoableTask);
            }
            else
            {
                /* It's not undoable so we clear the list of undoable tasks. 
                 * This is because this task may cause the previous 
                 * undo activities to be rendered invalid. */
                undoableDictionary[ownerKey] = null;
                redoableDictionary[ownerKey] = null;
            }

            TaskResult result = task.PerformTask(task.Argument, TaskMode.Repeat);

            TrimIfRequired(ownerKey);

            OnExecuted(new TaskServiceEventArgs(task));
            return result;
        }

        Dictionary<object, int> taskCountMaximums = new Dictionary<object, int>();

        void TrimIfRequired(object ownerKey = null)
        {
            TaskCollection<IUndoableTask> undoableTasks;
            TaskCollection<IInternalTask> repeatableTasks;
            TaskCollection<IUndoableTask> redoableTasks;
            long maximumTaskCount = taskCountMax;

            if (ownerKey != null)
            {
                int tempMaximum;
                if (taskCountMaximums.TryGetValue(ownerKey, out tempMaximum))
                {
                    maximumTaskCount = tempMaximum;
                }

                if (maximumTaskCount == long.MaxValue)
                {
                    return;
                }

                undoableDictionary.TryGetValue(ownerKey, out undoableTasks);
                repeatableDictionary.TryGetValue(ownerKey, out repeatableTasks);
                redoableDictionary.TryGetValue(ownerKey, out redoableTasks);
            }
            else
            {
                if (taskCountMax == long.MaxValue)
                {
                    return; /* Nothing to do. */
                }

                undoableTasks = globallyUndoableTasks;
                repeatableTasks = globallyRepeatableTasks;
                redoableTasks = globallyRedoableTasks;
            }

            int undoableTaskCount = undoableTasks != null ? undoableTasks.Count : 0;
            int repeatableTaskCount = repeatableTasks != null ? repeatableTasks.Count : 0;
            int redoableTaskCount = redoableTasks != null ? redoableTasks.Count : 0;

            long undoableTaskExcess = undoableTaskCount - maximumTaskCount;
            long repeatableTaskExcess = repeatableTaskCount - maximumTaskCount;
            long redoableTaskExcess = redoableTaskCount - maximumTaskCount;

            for (long i = 0; i < undoableTaskExcess; i++)
            {
                undoableTasks.RemoveFirst();
            }

            for (long i = 0; i < repeatableTaskExcess; i++)
            {
                repeatableTasks.RemoveFirst();
            }

            for (long i = 0; i < redoableTaskExcess; i++)
            {
                redoableTasks.RemoveFirst();
            }
        }

        internal enum TaskType
        {
            Undoable,
            Redoable,
            Repeatable
        }

        TaskResult Repeat()
        {
            var task = globallyRepeatableTasks.Peek();
            if (!task.Repeatable)
            {
                return TaskResult.NoTask;
            }
            var eventArgs = new CancellableTaskServiceEventArgs(task);
            OnExecuting(eventArgs);
            if (eventArgs.Cancel)
            {
                return TaskResult.Cancelled;
            }

            globallyRedoableTasks.Clear();
            globallyRepeatableTasks.AddLast(task);

            var undoableTask = task as IUndoableTask;
            if (undoableTask != null)
            {
                globallyUndoableTasks.AddLast(undoableTask);
            }
            else
            {
                globallyUndoableTasks.Clear();
                globallyRedoableTasks.Clear();
            }

            var result = task.PerformTask(task.Argument, TaskMode.Repeat);

            OnExecuted(new TaskServiceEventArgs(task));
            return result;
        }

        public bool CanRepeat(object ownerKey = null)
        {
            TaskCollection<IInternalTask> tasks;

            if (ownerKey == null)
            {
                tasks = globallyRepeatableTasks;
            }
            else
            {
                if (!repeatableDictionary.TryGetValue(ownerKey, out tasks))
                {
                    return false;
                }
            }

            return tasks.Count > 0 && tasks.Peek().Repeatable;
        }

        public IEnumerable<ITask> GetUndoableTasks(object ownerKey = null)
        {
            if (ownerKey == null)
            {
                return new List<ITask>(globallyUndoableTasks.Cast<ITask>());
            }

            TaskCollection<IUndoableTask> tasks;
            if (!undoableDictionary.TryGetValue(ownerKey, out tasks))
            {
                return new List<ITask>();
            }
            return new List<ITask>(tasks.Cast<ITask>());
        }

        public IEnumerable<ITask> GetRedoableTasks(object ownerKey = null)
        {
            if (ownerKey == null)
            {
                return new List<ITask>(globallyRedoableTasks.Cast<ITask>());
            }

            TaskCollection<IUndoableTask> tasks;
            if (!redoableDictionary.TryGetValue(ownerKey, out tasks))
            {
                return new List<ITask>();
            }
            return new List<ITask>(tasks.Cast<ITask>());
        }

        public IEnumerable<ITask> GetRepeatableTasks(object ownerKey = null)
        {
            List<ITask> result;
            if (ownerKey == null)
            {
                result = globallyRepeatableTasks.Where(task => task.Repeatable).Cast<ITask>().ToList();
                return result;
            }

            TaskCollection<IInternalTask> tasks;
            if (!repeatableDictionary.TryGetValue(ownerKey, out tasks))
            {
                return new List<ITask>();
            }
            result = tasks.Where(task => task.Repeatable).Cast<ITask>().ToList();
            return result;
        }

        long taskCountMax = long.MaxValue;

        public void SetMaximumUndoCount(int count, object ownerKey = null)
        {
            ArgumentValidator.AssertGreaterThan(count, 0, "count");
            if (ownerKey == null)
            {
                taskCountMax = count;
            }
            else
            {
                taskCountMaximums[ownerKey] = count;
            }
        }

        /// <summary>
        /// Clears the undo and redo stacks.
        /// </summary>
        public void Clear(object ownerKey = null)
        {
            if (ownerKey == null)
            {
                globallyRepeatableTasks.Clear();
                globallyUndoableTasks.Clear();
                globallyRedoableTasks.Clear();
                OnCleared(EventArgs.Empty);
                return;
            }

            TaskCollection<IInternalTask> repeatableTasks;
            if (repeatableDictionary.TryGetValue(ownerKey, out repeatableTasks))
            {
                repeatableTasks.Clear();
            }

            TaskCollection<IUndoableTask> undoableTasks;
            if (undoableDictionary.TryGetValue(ownerKey, out undoableTasks))
            {
                undoableTasks.Clear();
            }

            TaskCollection<IUndoableTask> redoableTasks;
            if (redoableDictionary.TryGetValue(ownerKey, out redoableTasks))
            {
                redoableTasks.Clear();
            }

            OnCleared(EventArgs.Empty);
        }

        /// <summary>
        /// 为了便于断点，特此拆分
        /// </summary>
        private bool enable;
        /// <summary>
        /// 控制当前撤销与回退管理是否可用
        /// </summary>
        public bool Enable
        {
            get { return enable; }
            set
            {
                enable = value;
            }
        }

        #region event Executing

        event EventHandler<CancellableTaskServiceEventArgs> executing;

        public event EventHandler<CancellableTaskServiceEventArgs> Executing
        {
            add
            {
                executing += value;
            }
            remove
            {
                executing -= value;
            }
        }

        void OnExecuting(CancellableTaskServiceEventArgs e)
        {
            if (executing != null)
            {
                executing(this, e);
            }
        }

        #endregion

        #region event Executed

        event EventHandler<TaskServiceEventArgs> executed;

        public event EventHandler<TaskServiceEventArgs> Executed
        {
            add
            {
                executed += value;
            }
            remove
            {
                executed -= value;
            }
        }

        void OnExecuted(TaskServiceEventArgs e)
        {
            if (executed != null)
            {
                executed(this, e);
            }
        }

        #endregion

        #region event Undoing

        event EventHandler<CancellableTaskServiceEventArgs> undoing;

        public event EventHandler<CancellableTaskServiceEventArgs> Undoing
        {
            add
            {
                undoing += value;
            }
            remove
            {
                undoing -= value;
            }
        }

        protected virtual void OnUndoing(CancellableTaskServiceEventArgs e)
        {
            IsUndoing = true;
            if (undoing != null)
            {
                undoing(this, e);
            }
        }

        #endregion

        #region event Undone

        event EventHandler<TaskServiceEventArgs> undone;

        public event EventHandler<TaskServiceEventArgs> Undone
        {
            add
            {
                undone += value;
            }
            remove
            {
                undone -= value;
            }
        }

        protected virtual void OnUndone(TaskServiceEventArgs e)
        {
            IsUndoing = false;
            if (undone != null)
            {
                undone(this, e);
            }
        }

        #endregion

        #region event Redoing

        event EventHandler<CancellableTaskServiceEventArgs> redoing;

        public event EventHandler<CancellableTaskServiceEventArgs> Redoing
        {
            add
            {
                redoing += value;
            }
            remove
            {
                redoing -= value;
            }
        }

        protected virtual void OnRedoing(CancellableTaskServiceEventArgs e)
        {
            IsUndoing = true;
            if (redoing != null)
            {
                redoing(this, e);
            }
        }

        #endregion

        #region event Redone

        event EventHandler<TaskServiceEventArgs> redone;

        public event EventHandler<TaskServiceEventArgs> Redone
        {
            add
            {
                redone += value;
            }
            remove
            {
                redone -= value;
            }
        }

        protected virtual void OnRedone(TaskServiceEventArgs e)
        {
            IsUndoing = false;
            if (redone != null)
            {
                redone(this, e);
            }
        }

        #endregion

        #region event Cleared

        event EventHandler<EventArgs> cleared;

        public event EventHandler<EventArgs> Cleared
        {
            add
            {
                cleared += value;
            }
            remove
            {
                cleared -= value;
            }
        }

        void OnCleared(EventArgs e)
        {
            if (cleared != null)
            {
                cleared(this, e);
            }
        }

        #endregion

        void IInternalTaskService.NotifyTaskRepeatableChanged(IInternalTask task)
        {
            /* Here we should notify listeners that a task has changed its repeatable property. 
             * This will prompt repopulation of any Repeatable task lists etc. */
        }

        class TaskCollection<T> : LinkedList<T>
        {
            public T Pop()
            {
                T result = Last.Value;
                RemoveLast();
                return result;
            }

            public T Peek()
            {
                var last = Last;
                return last != null ? last.Value : default(T);
            }
        }

        internal int GetTaskCount(TaskType taskType, object ownerKey = null)
        {
            if (taskType == TaskType.Undoable)
            {
                if (ownerKey == null)
                {
                    return globallyUndoableTasks.Count;
                }

                TaskCollection<IUndoableTask> tasks;
                if (!undoableDictionary.TryGetValue(ownerKey, out tasks))
                {
                    return 0;
                }
                return tasks.Count;
            }
            else if (taskType == TaskType.Repeatable)
            {
                if (ownerKey == null)
                {
                    return globallyRepeatableTasks.Count;
                }

                TaskCollection<IInternalTask> tasks;
                if (!repeatableDictionary.TryGetValue(ownerKey, out tasks))
                {
                    return 0;
                }
                return tasks.Count;
            }
            else if (taskType == TaskType.Redoable)
            {
                if (ownerKey == null)
                {
                    return globallyRedoableTasks.Count;
                }

                TaskCollection<IUndoableTask> tasks;
                if (!redoableDictionary.TryGetValue(ownerKey, out tasks))
                {
                    return 0;
                }
                return tasks.Count;
            }
            else
            {
                throw new InvalidOperationException("Unknown task type: " + taskType);
            }
        }
    }
}
