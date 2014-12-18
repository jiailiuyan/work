/* 迹I柳燕
 * 
 * FileName:   IconHelper.cs
 * Version:    1.0
 * Date:       2014.03.18
 * Author:     Ji
 * 
 *========================================
 * @namespace  Jisons 
 * @class      IconHelper
 * @extends    
 * 
 *             对于Icon的处理
 * 
 *========================================
 * Hi,小喵喵...
 * Copyright © 迹I柳燕
 * 
 * 转载请保留...
 * 
 */

using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Jisons
{
    public static class IconHelper
    {

        /// <summary> 通过进程的句柄获取此句柄的Icon </summary>
        /// <param name="handle"> 进程的句柄 </param>
        /// <returns> 获取到的 ImageSource </returns>
        public static ImageSource GetIconOfImageSource(this IntPtr handle)
        {
            var iconData = GetIconOfBytes(handle);
            if (iconData != null)
            {
                using (var ms = new MemoryStream(iconData))
                {
                    var bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = ms;
                    bitmapImage.EndInit();
                    bitmapImage.Freeze();
                    return bitmapImage;
                }
            }
            return null;

        }

        /// <summary> 通过进程的句柄获取此句柄的Icon </summary>
        /// <param name="handle"> 进程的句柄 </param>
        /// <returns> 获取到的 byte[] </returns>
        public static byte[] GetIconOfBytes(this IntPtr handle)
        {
            if (handle != IntPtr.Zero)
            {
                var process = Process.GetProcessById((int)handle);
                if (process.MainModule != null)
                {
                    IntPtr hIcon = NativeMethods.ExtractIcon(IntPtr.Zero, process.MainModule.FileName, 0);
                    if (hIcon != IntPtr.Zero)
                    {
                        var icon = Icon.FromHandle(hIcon);
                        var bitmap = icon.ToBitmap();
                        using (var ms = new MemoryStream())
                        {
                            bitmap.Save(ms, ImageFormat.Png);
                            icon.Save(ms);
                            return ms.ToArray();
                        }
                    }
                }
            }
            return null;
        }

    }
}
