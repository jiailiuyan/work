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

namespace Work.UndoManager.TaskModel
{
	/// <summary>
	/// The base class for <see cref="ITask"/>s.
	/// An task performs an application task.
	/// An task may be a command behaviour, that is, it may encapsulate 
	/// the logic performed when a command is initiated.
	/// </summary>
	public abstract class TaskBase<T> : IInternalTask
	{
		public bool Undoable { get; protected internal set; }

		#region event PerformTask

		event EventHandler<TaskEventArgs<T>> execute;

		/// <summary>
		/// Occurs when the task is being performed. 
		/// This is the event to handler for your task logic.
		/// </summary>
		protected event EventHandler<TaskEventArgs<T>> Execute
		{
			add
			{
				execute += value;
			}
			remove
			{
				execute -= value;
			}
		}

		void OnExecute(TaskEventArgs<T> e)
		{
			if (execute != null)
			{
				execute(this, e);
			}
		}

		#endregion

		internal T Argument { get; private set; }

		object IInternalTask.Argument
		{
			get
			{
				return Argument;
			}
		}

		TaskResult IInternalTask.PerformTask(object argument, TaskMode taskMode)
		{
			Argument = (T)argument;

			var eventArgs = new TaskEventArgs<T>(Argument, taskMode);
			OnExecute(eventArgs);
			return eventArgs.TaskResult;
		}

		internal TaskResult PerformTask(object argument, TaskMode taskMode)
		{
			var internalTask = (IInternalTask)this;
			return internalTask.PerformTask(argument, taskMode);
		}

		internal TaskResult Repeat()
		{
			var eventArgs = new TaskEventArgs<T>(Argument, TaskMode.Repeat);
			OnExecute(eventArgs);
			return eventArgs.TaskResult;
		}

		public abstract string DescriptionForUser { get; }

//		bool IInternalTask.Repeatable
//		{
//			get
//			{
//				return repeatable;
//			}
//		}

		bool repeatable;

		public bool Repeatable
		{
			get
			{
				return repeatable;
			}
			protected set
			{
				if (repeatable != value)
				{
					repeatable = value;
					if (TaskService != null)
					{
						TaskService.NotifyTaskRepeatableChanged(this);
					}
				}
			}
		}

		internal IInternalTaskService TaskService { get; set; }
	}
}
