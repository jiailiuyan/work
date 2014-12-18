using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Work.UndoManager
{
    /// <summary>
    /// 状态改变事件参数,提供给撤销回退历史记录
    /// </summary>
    public class StateChangedEventArgs : EventArgs
    {
        /// <summary>
        /// 属性名称
        /// </summary>
        public string PropertyName { get; private set; }
        /// <summary>
        /// 旧的值
        /// </summary>
        public object OldValue { get; private set; }
        /// <summary>
        ///  新的值
        /// </summary>
        public object NewValue { get; private set; }
        /// <summary>
        /// 是否由调用者提供值,在提供oldValue和newValue时,为True
        /// 默认值为False
        /// </summary>
        public bool IsProvideValue { get; private set; }

        /// <summary>
        /// 默认构造,只提供属性名称,
        /// Recorder监视器利用自身记录的旧的属性值,生成撤销回退记录
        /// </summary>
        /// <param name="propertyName">更改的属性名称</param>
        public StateChangedEventArgs(string propertyName)
        {
            this.PropertyName = propertyName;
        }

        /// <summary>
        /// 构造函数,指定更改的属性名称,以及原始值和新的值
        /// </summary>
        /// <param name="propertyName">更改的属性名称</param>
        /// <param name="oldValue">原始属性值</param>
        /// <param name="newValue">新的值</param>
        public StateChangedEventArgs(string propertyName, object oldValue, object newValue)
        {
            this.PropertyName = propertyName;
            this.OldValue = oldValue;
            this.NewValue = newValue;
            this.IsProvideValue = true;
        }
    }
}
