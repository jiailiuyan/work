using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Work.UndoManager.Recorder
{
    /// <summary>
    /// 撤销回退操作,执行操作类型枚举
    /// </summary>
    public enum EnumUndoAction
    {
        /// <summary>
        /// 没有指定类型,作为默认值
        /// </summary>
        None = 0,
        /// <summary>
        /// 设置属性
        /// </summary>
        SetProperty,
        /// <summary>
        /// 添加对象
        /// </summary>
        AddObject,
        /// <summary>
        /// 删除对象
        /// </summary>
        RemoveObject,
        /// <summary>
        /// 移动对象
        /// </summary>
        MoveObject,
        /// <summary>
        /// 替换对象
        /// </summary>
        ReplaceObject
    }
}
