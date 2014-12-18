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
	<CreationDate>2010-02-06 18:16:45Z</CreationDate>
</File>
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Work.UndoManager.TaskModel
{
	/// <summary>
	/// Provides the ability to group tasks for execution
	/// either sequentially or in parallel. <see cref="CompositeTask.Parallel"/>
	/// </summary>
	public class CompositeTask<T> : TaskBase<T>
	{
		readonly string descriptionForUser;
		readonly Dictionary<TaskBase<T>, T> taskDictionary;

		public CompositeTask(IDictionary<TaskBase<T>, T> tasks, string descriptionForUser)
		{
			ArgumentValidator.AssertNotNull(descriptionForUser, "descriptionForUser");
			ArgumentValidator.AssertNotNull(tasks, "tasks");
			this.descriptionForUser = descriptionForUser;
			taskDictionary = new Dictionary<TaskBase<T>, T>(tasks);
			Execute += OnExecute;

			/* Determine repeatable status. */
			bool repeatable = taskDictionary.Keys.Count > 0;
			foreach (IInternalTask key in tasks.Keys)
			{
				if (!key.Repeatable)
				{
					repeatable = false;
				}
			}
			Repeatable = repeatable;
		}

		void OnExecute(object sender, TaskEventArgs<T> e)
		{
			ExecuteInternal(taskDictionary, e.TaskMode);
		}

		protected internal virtual void ExecuteInternal(
			Dictionary<TaskBase<T>, T> taskDictionary, TaskMode taskMode)
		{
			if (Parallel)
			{
				ExecuteInParallel(taskDictionary, taskMode);
			}
			else
			{
				ExecuteInSequence(taskDictionary, taskMode);
			}
		}

		public override string DescriptionForUser
		{
			get
			{
				return descriptionForUser;
			}
		}

		#region Parallel Execution
		/// <summary>
		/// Gets or sets a value indicating whether the composite tasks
		/// are to be performed sequentially, one after the other, or in parallel.
		/// </summary>
		/// <value><c>true</c> if parallel; otherwise, <c>false</c>.</value>
		public bool Parallel { get; set; }

		static void ExecuteInSequence(Dictionary<TaskBase<T>, T> taskDictionary, TaskMode taskMode)
		{
			foreach (KeyValuePair<TaskBase<T>, T> pair in taskDictionary)
			{
				var task = (IInternalTask)pair.Key;
				task.PerformTask(pair.Value, taskMode);
			}
		}

		static void ExecuteInParallel(Dictionary<TaskBase<T>, T> taskDictionary, TaskMode taskMode)
		{
			/* When we move to .NET 4 we may use System.Threading.Parallel for the Desktop CLR. */
			var performedTasks = new List<TaskBase<T>>();
			object performedTasksLock = new object();
			var exceptions = new List<Exception>();
			object exceptionsLock = new object();
			var events = taskDictionary.ToDictionary(x => x, x => new AutoResetEvent(false));

			foreach (KeyValuePair<TaskBase<T>, T> pair in taskDictionary)
			{
				var autoResetEvent = events[pair];
				var task = (IInternalTask)pair.Key;
				var undoableTask = pair.Key;
				var arg = pair.Value;

				ThreadPool.QueueUserWorkItem(
					delegate
					{
						try
						{
							task.PerformTask(arg, taskMode);
							lock (performedTasksLock)
							{
								performedTasks.Add(undoableTask);
							}
						}
						catch (Exception ex)
						{
							/* TODO: improve this to capture undone task errors. */
							lock (exceptionsLock)
							{
								exceptions.Add(ex);
							}
						}
						autoResetEvent.Set();
					});

			}

			foreach (var autoResetEvent in events.Values)
			{
				autoResetEvent.WaitOne();
			}

			if (exceptions.Count > 0)
			{
				throw new CompositeException("Unable to undo tasks", exceptions);
			}
		}
		#endregion
	}
}
