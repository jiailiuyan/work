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

namespace Work.UndoManager.TaskModel
{
	/// <summary>
	/// This class is used during the evaluation 
	/// of an <see cref="IUndoableTask"/>'s CanUndo property.
	/// </summary>
	public class UndoableTaskEventArgs<TArgument> : TaskEventArgs<TArgument>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UndoableTaskEventArgs{TArgument}"/> class.
		/// </summary>
		/// <param name="argument">The argument used by the concrete
		/// <code>TaskBase</code> implementation, which is propagated during 
		/// the can and perform events.</param>
		public UndoableTaskEventArgs(TArgument argument)
			: base(argument)
		{
			/* Intentionally left blank. */
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="UndoableTaskEventArgs{TArgument}"/> class.
		/// </summary>
		/// <param name="argument">The argument used by the concrete
		/// <code>TaskBase</code> implementation, which is propagated during 
		/// the can and perform events.</param>
		/// <param name="taskMode">Indicates whether this task is being performed 
		/// for the fist time, if it is being redone, or if it is being repeated.</param>
		internal UndoableTaskEventArgs(TArgument argument, TaskMode taskMode)
			: base(argument, taskMode)
		{
			/* Intentionally left blank. */
		}

		bool enabled = true;

		/// <summary>
		/// Gets or sets a value indicating whether this instance can undo or redo.
		/// </summary>
		/// <value><c>true</c> if this instance can undo; otherwise, <c>false</c>. 
		/// Default is <c>true</c></value>
		public bool Enabled
		{
			get
			{
				return enabled;
			}
			set
			{
				enabled = value;
			}
		}
	}
}