/* 迹I柳燕
 * 
 * FileName:   SelectorHelper.cs
 * Version:    1.0
 * Date:       2014.03.27
 * Author:     Ji
 * 
 *========================================
 * @namespace  Jisons 
 * @class      SelectorHelper
 * @extends    
 *             
 *             WPF 扩展
 *             对于 Selector 的扩展方法
 * 
 *========================================
 * Hi,小喵喵...
 * Copyright © 迹I柳燕
 * 
 * 转载请保留...
 * 
 */

using System.Windows;
using System.Windows.Controls.Primitives;

namespace Jisons
{
    public static class  SelectorHelper
    {

        /// <summary> 在列表控件中中查询子元素 </summary>
        /// <typeparam name="T"> 欲查找的控件类型 </typeparam>
        /// <param name="selector"> 元数据列表 </param>
        /// <param name="data"> 控件的数据源 </param>
        /// <returns> 返回查找到的类型 T? </returns>
        public static T FindDependencyObjectInSelector<T>(this Selector selector, object data) where T : DependencyObject
        {
            var item = selector.ItemContainerGenerator.ContainerFromItem(data);
            if (item != null)
            {
                return item.FindVisualChild<T>();
            }
            return default(T);
        }

    }
}
