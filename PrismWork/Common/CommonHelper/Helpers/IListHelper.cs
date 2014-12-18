/* 迹I柳燕
 * 
 * FileName:   IListHelper.cs
 * Version:    1.0
 * Date:       2014.03.18
 * Author:     Ji
 * 
 *========================================
 * @namespace  Jisons 
 * @class      IListHelper
 * @extends    
 * 
 *             对于 IList 的Linq扩展查询
 * 
 *========================================
 * Hi,小喵喵...
 * Copyright © 迹I柳燕
 * 
 * 转载请保留...
 * 
 */

using System.Collections.Generic;
using System.Linq;

namespace Jisons
{
    public static class IListHelper
    {

        /// <summary> 执行删除所有子项，且忽略锁定检查 
        /// 此项看源代码后发现比直接使用 Clear 会效率影响很多，建议在有此特殊需求时再使用 </summary>
        /// <typeparam name="T"> 遍历的子类型 </typeparam>
        /// <param name="items"> 欲清空的列表,继承 IList </param>
        public static void DeleteAllItems<T>(this IList<T> items)
        {
            if (items != null)
            {
                var count = items.Count();
                if (count > 0)
                {
                    for (int i = count - 1; i >= 0; i--)
                    {
                        items.RemoveAt(i);
                    }
                }
            }
        }

    }
}
