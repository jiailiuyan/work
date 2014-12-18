using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Work.UndoManager.Recorder
{
    /// <summary>
    /// 撤销回退操作,回调接口
    /// </summary>
    public interface IRecordableCallback
    {
        /// <summary>
        /// 在执行重做前,进行回调处理
        /// </summary>
        void Redoing(RecorderTaskEventArgs args);
        /// <summary>
        /// 在执行撤销前,进行回调处理
        /// </summary>
        void Undoing(RecorderTaskEventArgs args);
        /// <summary>
        /// 执行重做后,进行回调处理
        /// </summary>
        void Redone(RecorderTaskEventArgs args);
        /// <summary>
        /// 执行撤销后,进行回调处理
        /// </summary>
        void Undone(RecorderTaskEventArgs args);
    }
}
