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
	<CreationDate>2010-02-14 17:42:01Z</CreationDate>
</File>
*/
#endregion

namespace Work.UndoManager.TaskModel
{
	/// <summary>
	/// Provides functionality to perform and repeat a task within the <see cref="TaskService"/>.
	/// </summary>
	interface IInternalTask : ITask
	{
		/// <summary>
		/// Gets the argument that is used during task execution.
		/// </summary>
		/// <value>The argument used during execution.</value>
		object Argument
		{ 
			get;
		}

		//		bool Repeatable
//		{ 
//			get;
//		}
		/// <summary>
		/// Gets a value indicating whether this <see cref="IInternalTask"/> 
		/// can be executed more than once.
		/// </summary>
		/// <value><c>true</c> if repeatable; otherwise, <c>false</c>.</value>
		/// <summary>
		/// Performs the task, raising events to allowing user code to execute.
		/// </summary>
		/// <param name="argument">The argument used during execution.</param>
		/// <param name="taskMode">Indicates whether the task is being performed
		/// for the first time, whether it is being redone, or whether it is being repeated.</param>
		/// <returns></returns>
		TaskResult PerformTask(object argument, TaskMode taskMode);
	}
}