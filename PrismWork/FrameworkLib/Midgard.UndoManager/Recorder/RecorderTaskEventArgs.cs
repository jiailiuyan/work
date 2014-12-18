using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Work.UndoManager.TaskModel;

namespace Work.UndoManager.Recorder
{
    /// <summary>
    /// 记录器任务,事件参数
    /// </summary>
    public class RecorderTaskEventArgs : UndoableTaskEventArgs<UndoTask>
    {
        /// <summary>
        /// 构造函数,指定本次任务内部的UndoTask
        /// </summary>
        /// <param name="undoTask"></param>
        public RecorderTaskEventArgs(UndoTask undoTask)
            : base(undoTask)
        {
        }
    }
}
