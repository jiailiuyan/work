using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Work.UndoManager
{
    /// <summary>
    /// 通知当前对象,状态改变
    /// </summary>
    public interface INotifyStateChanged : INotifyPropertyChanged
    {
        /// <summary>
        /// 指明当前对象状态改变
        /// </summary>
        event EventHandler<StateChangedEventArgs> StateChanged;
        /// <summary>
        /// 是否触发属性更改,提供给Recorder使用,
        /// 在关闭监视器时,直接控制对象不再触发状态更改事件
        /// </summary>
        bool IsRaiseStateChanged { get; set; }
    }
}
