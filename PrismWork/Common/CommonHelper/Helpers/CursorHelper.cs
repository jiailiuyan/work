/* 迹I柳燕
 * 
 * FileName:   CursorHelper.cs
 * Version:    1.0
 * Date:       2014.03.18
 * Author:     Ji
 * 
 *========================================
 * @namespace  Jisons 
 * @class      CursorHelper
 * @extends    
 * 
 *             对于Cursor的处理
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
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Jisons
{
    public static class CursorHelper
    {

        /// <summary> 把Stream转换为Cursor </summary>
        /// <param name="ms"> 传入的Stream </param>
        /// <returns> 中内存流中读取的Cursor </returns>
        public static Cursor ConvertToCursor(this Stream ms)
        {
            return new Cursor(ms);
        }

    }
}
