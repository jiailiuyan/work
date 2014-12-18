#region File and License Information
/*
<File>
	<License Type="BSD">
		Copyright © 2009 - 2012, Daniel Vaughan. All rights reserved.
	
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
using System.Collections.Generic;

using Work.UndoManager.TaskModel;

namespace Work.UndoManager
{
    /// <summary>
    /// This interface describes a service that is able to execute <see cref="ITask"/>s.
    /// </summary>
    public interface ITaskService
    {
        /// <summary>
        /// Executes the specified task.
        /// </summary>
        /// <param name="task">The command to execute.</param>
        /// <param name="argument">The argument passed to the task on execution.</param>
        /// <param name="contextKey">A key representing the owner of the task. 
        /// This might be, for example, a text editor.
        /// This allows for a set of tasks to be associated with a control. 
        /// Can be <c>null</c>, in which case the task is deemed to be global.</param>
        /// <returns>The result of performing the task.</returns>
        TaskResult PerformTask<T>(TaskBase<T> task, T argument, object contextKey = null);

        ///// <summary>
        ///// Executes the specified task.
        ///// </summary>
        ///// <param name="task">The command to execute.</param>
        ///// <param name="argument">The argument passed to the task on execution.</param>
        ///// <param name="ownerKey">A key representing the owner of the task. 
        ///// This might be, for example, a text editor.
        ///// This allows for a set of tasks to be associated with a control. 
        ///// Can be <c>null</c>, in which case the task is deemed to be global.</param>
        ///// <returns>The result of performing the task.</returns>
        TaskResult PerformTask<T>(UndoableTaskBase<T> task, T argument, object ownerKey = null);

        /// <summary>
        /// Gets a value indicating whether this instance can undo an task.
        /// </summary>
        /// <param name="ownerKey">A key representing the owner of the task. 
        /// This might be, for example, a text editor.
        /// This allows for a set of tasks to be associated with a control. 
        /// Can be <c>null</c>, in which case the task is deemed to be global.</param>
        /// <value><c>true</c> if this instance can undo; otherwise, <c>false</c>.</value>
        bool CanUndo(object ownerKey = null);

        /// <summary>
        /// Undoes the last task.
        /// </summary>
        /// <param name="ownerKey">A key representing the owner of the task. 
        /// This might be, for example, a text editor.
        /// This allows for a set of tasks to be associated with a control. 
        /// Can be <c>null</c>, in which case the task is deemed to be global.</param>
        /// <returns>The result of the task. <see cref="TaskResult"/></returns>
        /// <exception cref="InvalidOperationException">
        /// Occurs if there are no previously executed tasks to undo.</exception>
        /// <returns>The result of undoing the task.</returns>
        TaskResult Undo(object ownerKey = null);

        /// <summary>Undoes the last number of tasks. 
        /// If any single task does not complete the process is halted.
        /// </summary>
        /// <param name="ownerKey">A key representing the owner of the task. 
        /// This might be, for example, a text editor.
        /// This allows for a set of tasks to be associated with a control. 
        /// Can be <c>null</c>, in which case the task is deemed to be global.</param>
        /// <returns>The result of the task. <see cref="TaskResult"/></returns>
        /// <exception cref="InvalidOperationException">
        /// Occurs if there are no previously executed tasks to undo.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Occurs if the list of undoable tasks 
        /// is smaller in length than the specified undoCount.</exception>
        /// <returns>The result of undoing the task.</returns>
        TaskResult Undo(int undoCount, object ownerKey = null);

        /// <summary>
        /// Gets a value indicating whether an task can be redone, 
        /// after it has been undone.
        /// </summary>
        /// <param name="ownerKey">A key representing the owner of the task. 
        /// This might be, for example, a text editor.
        /// This allows for a set of tasks to be associated with a control. 
        /// Can be <c>null</c>, in which case the task is deemed to be global.</param>
        /// <value><c>true</c> if this instance can redo the last task; 
        /// otherwise, <c>false</c>.</value>
        bool CanRedo(object ownerKey = null);

        /// <summary>
        /// Executes the task that was previously undone.
        /// </summary>
        /// <param name="ownerKey">A key representing the owner of the task. 
        /// This might be, for example, a text editor.
        /// This allows for a set of tasks to be associated with a control. 
        /// Can be <c>null</c>, in which case the task is deemed to be global.</param>
        /// <returns>The result of redoing the task.</returns>
        TaskResult Redo(object ownerKey = null);

        /// <summary>
        /// Causes the last <see cref="ITask"/> that was performed to be performed again.
        /// </summary>
        /// <param name="ownerKey">A key representing the owner of the task. 
        /// This might be, for example, a text editor.
        /// This allows for a set of tasks to be associated with a control. 
        /// Can be <c>null</c>, in which case the task is deemed to be global.</param>
        /// <returns>The result of repeating the last task.</returns>
        TaskResult Repeat(object ownerKey = null);

        /// <summary>
        /// Gets a value indicating whether this instance can execute the last task executed.
        /// </summary>
        /// <param name="ownerKey">A key representing the owner of the task. 
        /// This might be, for example, a text editor.
        /// This allows for a set of tasks to be associated with a control. 
        /// Can be <c>null</c>, in which case the task is deemed to be global.</param>
        /// <value>
        /// 	<c>true</c> if this instance can execute the last task executed; otherwise, <c>false</c>.
        /// </value>
        bool CanRepeat(object ownerKey = null);

        /// <summary>
        /// Gets the tasks which are deemed undoable.
        /// </summary>
        /// <param name="ownerKey">The owner associated with a set of tasks. 
        /// For example, a text editor. Can be <c>null</c>. 
        /// If <c>null</c> those tasks not associated with an ownerKey (global tasks) are returned.</param>
        /// <returns>The undoable tasks.</returns>
        IEnumerable<ITask> GetUndoableTasks(object ownerKey = null);

        /// <summary>
        /// Gets the tasks which are deemed redoable.
        /// </summary>
        /// <param name="ownerKey">The owner associated with a set of tasks. 
        /// For example, a text editor. Can be <c>null</c>. 
        /// If <c>null</c> those tasks not associated with an ownerKey (global tasks) are returned.</param>
        /// <returns>The redoable tasks.</returns>
        IEnumerable<ITask> GetRedoableTasks(object ownerKey = null);

        /// <summary>
        /// Gets the tasks which are deemed repeatable.
        /// </summary>
        /// <param name="ownerKey">The owner associated with a set of tasks. 
        /// For example, a text editor. Can be <c>null</c>. 
        /// If <c>null</c> those tasks not associated with an ownerKey (global tasks) are returned.</param>
        /// <returns>The undoable tasks.</returns>
        IEnumerable<ITask> GetRepeatableTasks(object ownerKey = null);

        /// <summary>
        /// Clears the task list for a particular owner.
        /// </summary>
        /// <param name="ownerKey">The owner associated with a set of tasks. 
        /// For example, a text editor. Can be <c>null</c>. 
        /// If <c>null</c> those tasks not associated with an ownerKey (global tasks) are cleared.</param>
        void Clear(object ownerKey = null);

        /// <summary>
        /// Limits the number of tasks that can be undone to the specified value.
        /// This can help to reduce memory usage in some scenarios.
        /// </summary>
        /// <param name="count">The maximum number of undo tasks to be retained.</param>
        /// <param name="ownerKey">The context key. Can be <c>null</c>.</param>
        void SetMaximumUndoCount(int count, object ownerKey = null);

        /// <summary>
        /// 执行操作,操作执行前触发
        /// </summary>
        event EventHandler<CancellableTaskServiceEventArgs> Executing;
        /// <summary>
        /// 执行操作,操作执行结束后触发
        /// </summary>
        event EventHandler<TaskServiceEventArgs> Executed;
        /// <summary>
        /// 撤销操作,操作执行前触发
        /// </summary>
        event EventHandler<CancellableTaskServiceEventArgs> Undoing;
        /// <summary>
        /// 执行操作,操作执行结束后触发
        /// </summary>
        event EventHandler<TaskServiceEventArgs> Undone;
        /// <summary>
        /// 重做操作,操作执行前触发
        /// </summary>
        event EventHandler<CancellableTaskServiceEventArgs> Redoing;
        /// <summary>
        /// 重做操作,操作执行结束后触发
        /// </summary>
        event EventHandler<TaskServiceEventArgs> Redone;
        /// <summary>
        /// 清除历史记录操作,操作完成后触发
        /// </summary>
        event EventHandler<EventArgs> Cleared;

        /// <summary>
        /// 当前撤销回退过程是否可用
        /// </summary>
        bool Enable { get; set; }
        /// <summary>
        /// 是否正在执行撤销回退
        /// </summary>
        bool IsUndoing { get; }
    }
}