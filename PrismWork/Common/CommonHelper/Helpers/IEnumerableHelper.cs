/* 迹I柳燕
 * 
 * FileName:   IEnumerableHelper.cs
 * Version:    1.0
 * Date:       2014.03.18
 * Author:     Ji
 * 
 *========================================
 * @namespace  Jisons 
 * @class      IEnumerableHelper
 * @extends    
 * 
 *             对于 IEnumerable 的Linq扩展查询
 * 
 *========================================
 * Hi,小喵喵...
 * Copyright © 迹I柳燕
 * 
 * 转载请保留...
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jisons
{
    public static class IEnumerableHelper
    {

        /// <summary> 具有锁定检查的遍历执行函数体 </summary>
        /// <typeparam name="T"> 集合的泛型类型 </typeparam>
        /// <param name="items"> 欲执行循环的列表,继承 IEnumerable </param>
        /// <param name="action"> 执行循环的函数体 </param>
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
            {
                action.Invoke(item);
            }
        }

        /// <summary> 执行具有锁定检查并具有类型槛车的遍历执行函数体 </summary>
        /// <typeparam name="T"> 集合的泛型类型 </typeparam>
        /// <typeparam name="R"> 执行遍历时的类型判断参数 </typeparam>
        /// <param name="items"> 欲执行循环的列表,继承 IEnumerable </param>
        /// <param name="action"> 执行循环的函数体 </param>
        public static void ForEachWithTypeMatch<T, R>(this IEnumerable<T> items, Action<R> action) where R : class
        {
            foreach (var item in items)
            {
                var data = item as R;
                if (data != null)
                {
                    action.Invoke(data);
                }
            }
        }

    }
}
