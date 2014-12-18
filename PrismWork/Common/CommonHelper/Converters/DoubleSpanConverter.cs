/* 迹I柳燕
 * 
 * FileName:   DoubleSpanConverter.cs
 * Version:    1.0
 * Date:       2014.03.18
 * Author:     Ji
 * 
 *========================================
 * @namespace  Jisons 
 * @class      DoubleSpanConverter
 * @extends    IValueConverter
 *             
 *             WPF 扩展
 *             执行对于 ConverterParmeter 的 double 求和
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
using System.Windows.Data;

namespace Jisons
{
    public class DoubleSpanConverter : IValueConverter
    {
        public static readonly IValueConverter Converter = new DoubleSpanConverter();

        /// <summary> 执行对于 ConverterParmeter 的 double 求和 </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double span = 0, length = 0;

            if (parameter != null && value != null && double.TryParse(value.ToString(), out length) && double.TryParse(parameter.ToString(), out span))
            {
                return length > span ? length - span : 0;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
