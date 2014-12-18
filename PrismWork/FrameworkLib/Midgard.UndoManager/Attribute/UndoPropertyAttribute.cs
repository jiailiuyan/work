using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Work.UndoManager
{
    /// <summary>
    /// 用来指定一个属性,是否需要被监视
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class UndoPropertyAttribute : Attribute
    {
    }
}
