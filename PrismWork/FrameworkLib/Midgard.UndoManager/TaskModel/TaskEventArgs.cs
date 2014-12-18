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
	/// Used during task execution.
	/// </summary>
	public class TaskEventArgs<TArgument> : EventArgs
	{
		/// <summary>
		/// Gets or sets the argument used by the concrete 
		/// <seealso cref="TaskBase{T}"/> implementation.
		/// </summary>
		/// <value>The argument.</value>
		public TArgument Argument { get; set; }

		/// <summary>
		/// Gets or sets the task result.
		/// </summary>
		/// <value>The task result.</value>
		public TaskResult TaskResult { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="TaskEventArgs{TArgument}"/> class.
		/// </summary>
		/// <param name="argument">The argument used by the concrete 
		/// <code>TaskBase</code> implementation.</param>
		public TaskEventArgs(TArgument argument)
		{
			Argument = argument;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TaskEventArgs{TArgument}"/> class.
		/// </summary>
		/// <param name="argument">The argument used by the concrete 
		/// <code>TaskBase</code> implementation.</param>
		/// <param name="taskMode">Indicates whether this task is being performed 
		/// for the fist time, if it is being redone, or if it is being repeated.</param>
		internal TaskEventArgs(TArgument argument, TaskMode taskMode) : this(argument)
		{
			TaskMode = taskMode;
		}

		public TaskMode TaskMode { get; private set; }
	}

	/// <summary>
	/// Indicates why a task is being performed.
	/// </summary>
	public enum TaskMode
	{
		Unknown,
		FirstTime,
		Redo,
		Repeat,
	}
}