/* 迹I柳燕
 * 
 * FileName:   FindVisualHelper.cs
 * Version:    1.0
 * Date:       2014.03.18
 * Author:     Ji
 * 
 *========================================
 * @namespace  Jisons 
 * @class      FindVisualHelper
 * @extends    
 *             
 *             WPF 扩展
 *             对于Visual 元素的查询方法扩展
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
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Jisons
{
    public static class FindVisualHelper
    {

        /// <summary> 向上查找指定类型的逻辑容器 </summary>
        /// <typeparam name="T">欲查找的控件类型</typeparam>
        /// <param name="descendent">查找的子容器</param>
        /// <param name="isContainsSelf">查找是否包含自身</param>
        /// <returns>返回第一个查找到的父控件 T?</returns>
        public static T FindVisualParent<T>(this DependencyObject descendent, bool isContainsSelf = true) where T : class
        {
            T ancestor = null;
            DependencyObject scan = isContainsSelf ? descendent : VisualTreeHelper.GetParent(descendent);
            ancestor = null;
            while (scan != null && ((ancestor = scan as T) == null))
            {
                scan = VisualTreeHelper.GetParent(scan);
            }
            return ancestor;
        }

        public static bool? FindFirstVisualParent<T, R>(this DependencyObject descendent, bool isContainsSelf = true)
            where T : class
            where R : class
        {
            T ancestorT = null;
            R ancestorR = null;
            DependencyObject scan = isContainsSelf ? descendent : VisualTreeHelper.GetParent(descendent);

            while (scan != null && (((ancestorT) == null) && ((ancestorR) == null)))
            {
                scan = VisualTreeHelper.GetParent(scan);
                if (scan != null)
                {
                    var ir = scan.GetType().GetInterface(typeof(T).Name, true);
                    if (ir != null)
                    {
                        return true;
                    }
                    var it = scan.GetType().GetInterface(typeof(R).Name, true);
                    if (it != null)
                    {
                        return false;
                    }
                }
            }
            return null;
        }

        /// <summary> 查找指定类型的子物体 </summary>
        /// <typeparam name="T">欲查找的控件类型</typeparam>
        /// <param name="descendent">查找的根容器</param>
        /// <param name="isContainsSelf">查找是否包含自身</param>
        /// <returns>返回第一个查找到的子控件 T?</returns>
        public static T FindVisualChild<T>(this DependencyObject descendent, bool isContainsSelf = true) where T : DependencyObject
        {
            T found = null;
            if (descendent is T && isContainsSelf)
            {
                found = descendent as T;
            }
            else
            {
                var childlen = VisualTreeHelper.GetChildrenCount(descendent);
                for (int i = 0; i < childlen; i++)
                {
                    var child = VisualTreeHelper.GetChild(descendent, i);
                    var target = child as T;
                    if (target == null)
                    {
                        found = FindVisualChild<T>(child, false);
                        if (found != null)
                        {
                            break;
                        }
                    }
                    else
                    {
                        found = (T)child;
                        break;
                    }
                }
            }
            return found;
        }

        /// <summary> 查找指定类型的子物体 </summary>
        /// <typeparam name="T">欲查找的控件类型</typeparam>
        /// <param name="descendent">查找的根容器</param>
        /// <param name="isContainsSelf">查找是否包含自身</param>
        /// <returns>返回所有查找到的子控件 T?</returns>
        public static IEnumerable<T> FindAllVisualChild<T>(this DependencyObject descendent, bool isContainsSelf = true) where T : DependencyObject
        {
            T found = null;
            if (descendent is T && isContainsSelf)
            {
                found = descendent as T;
            }
            else
            {
                var childlen = VisualTreeHelper.GetChildrenCount(descendent);
                for (int i = 0; i < childlen; i++)
                {
                    var child = VisualTreeHelper.GetChild(descendent, i);
                    var target = child as T;
                    if (target == null)
                    {
                        FindAllVisualChild<T>(child, false);
                    }
                    else
                    {
                        yield return (T)child;
                    }
                }
            }
            yield return found;
        }

        /// <summary> 在列表控件中中查询子元素 </summary>
        /// <typeparam name="T">欲查找的控件类型</typeparam>
        /// <param name="selector">元数据列表</param>
        /// <param name="data">控件的数据源</param>
        /// <returns>返回查找到的类型 T?</returns>
        public static T FindControlInSelector<T>(this Selector selector, object data) where T : DependencyObject
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
