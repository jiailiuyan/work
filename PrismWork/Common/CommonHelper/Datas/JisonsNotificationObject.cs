/* 迹I柳燕
 * 
 * FileName:   JisonsNotificationObject.cs
 * Version:    1.0
 * Date:       2014.03.19
 * Author:     Ji
 * 
 *========================================
 * @namespace  Jisons 
 * @class      JisonsNotificationObject
 *             JisonsINotifyPropertyChanged
 * @extends    
 * 
 *             对于实现了 INotifyPropertyChanged 接口的类，可以直接扩展通知当前的线程通知
 *             
 * 
 *========================================
 * Hi,小喵喵...
 * Copyright © 迹I柳燕
 * 
 * 转载请保留...
 * 
 */

using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Jisons
{
    public static class JisonsNotificationObject
    {

        /// <summary> 实现了 INotifyPropertyChanged 接口的类 </summary>
        public abstract class JisonsINotifyPropertyChanged : INotifyPropertyChanged
        {
            /// <summary> 在更改属性值时触发此事件 </summary>
            public event PropertyChangedEventHandler PropertyChanged;

            /// <summary> 当前类型的单项线程通知函数 </summary>
            /// <param name="propertyName"> 进行线程通知的属性名称 </param>
            protected virtual void RaisePropertyChanged(string propertyName)
            {
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }

            /// <summary> 当前类型的多项线程通知函数 </summary>
            /// <param name="propertyNames"> 进行线程通知的属性名称集合 </param>
            protected void RaisePropertyChanged(params string[] propertyNames)
            {
                if (propertyNames == null)
                {
                    propertyNames.ForEach(propertyName => this.RaisePropertyChanged(propertyName));
                }
            }
        }

        /// <summary> 增加对与 实现 INotifyPropertyChanged 接口的扩展线程通知 </summary>
        /// <typeparam name="D">当前被绑定的类型</typeparam>
        /// <typeparam name="T">当前发送线程通知的类型</typeparam>
        /// <param name="data">当前被绑定的数据</param>
        /// <param name="propertyExpression">反射获取类型名称的委托</param>
        public static void RaisePropertyChanged<D, T>(this D data, Expression<Func<T>> propertyExpression) where D : INotifyPropertyChanged
        {
            if (data != null)
            {
                var multicastDelegateFieldInfo = data.GetType().FindField("PropertyChanged");
                if (multicastDelegateFieldInfo != null)
                {
                    var multicastDelegateValue = multicastDelegateFieldInfo.GetValue(data);
                    if (multicastDelegateValue != null)
                    {
                        var multicastDelegate = (MulticastDelegate)multicastDelegateValue;
                        Delegate[] delegates = multicastDelegate.GetInvocationList();
                        var propertyName = ExtractPropertyName<T>(propertyExpression);
                        if (delegates.Count() > 0 && !string.IsNullOrWhiteSpace(propertyName))
                        {
                            var propertyChangedEventArgs = new PropertyChangedEventArgs(propertyName);
                            foreach (Delegate delegateItem in delegates)
                            {
                                //在此动态的调用其方法
                                //因此需要需要保证其方法参数的正确性
                                var propertyChangedEventHandler = delegateItem as PropertyChangedEventHandler;
                                if (propertyChangedEventHandler != null)
                                {
                                    propertyChangedEventHandler.DynamicInvoke(data, propertyChangedEventArgs);
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary> 获取当前的委托名称 </summary>
        /// <typeparam name="T">当前发送线程通知的类型</typeparam>
        /// <param name="propertyExpression">反射获取类型名称的委托</param>
        /// <returns> 当前的委托名称 </returns>
        public static string ExtractPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression != null)
            {
                var memberExpression = propertyExpression.Body as MemberExpression;
                var propertyInfo = memberExpression.Member as PropertyInfo;
                var getMethod = propertyInfo.GetGetMethod(true);
                return memberExpression.Member.Name;
            }
            return null;
        }

    }
}
