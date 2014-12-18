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
	/// Used to communicated with implementations of the <see cref="TaskService"/> class.
	/// </summary>
	public class TaskServiceEventArgs : EventArgs
	{
		/// <summary>
		/// Gets or sets the task the is being executed/undone etc.
		/// </summary>
		/// <value>The task.</value>
		public ITask Task { get; private set; }

		public TaskServiceEventArgs()
		{
			/* Intentionally left blank. */
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TaskServiceEventArgs"/> class.
		/// </summary>
		/// <param name="task">The task. Can be <c>null</c>.</param>
		public TaskServiceEventArgs(ITask task)
		{
			Task = task;
		}
	}

	/// <summary>
	/// Used to communicated with implementations of the <see cref="TaskService"/> class,
	/// and to provide the means to cancel an operation.
	/// </summary>
	public class CancellableTaskServiceEventArgs : TaskServiceEventArgs
	{
		bool cancelled;

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="CancellableTaskServiceEventArgs"/> 
		/// has been cancelled by a handler. This means that the operation will not proceed, 
		/// e.g., the task will not be executed.
		/// </summary>
		/// <value><c>true</c> if cancel; otherwise, <c>false</c>.</value>
		public bool Cancel
		{
			get
			{
				return cancelled;
			}
			set
			{
				/* We don't allow a handler to 'uncancel'. 
				 * Once it's cancelled, that's it. */
				if (cancelled)
				{
					return;
				}
				cancelled = value;
			}
		}

		internal object OwnerKey { get; set; }

		public CancellableTaskServiceEventArgs()
		{
			/* Intentionally left blank. */
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TaskServiceEventArgs"/> class.
		/// </summary>
		/// <param name="task">The task. Can be <c>null</c>.</param>
		public CancellableTaskServiceEventArgs(ITask task) :  base(task)
		{
			/* Intentionally left blank. */
		}
	}
}