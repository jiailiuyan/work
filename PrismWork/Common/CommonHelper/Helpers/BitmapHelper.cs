/* 迹I柳燕
 * 
 * FileName:   BitmapHelper.cs
 * Version:    1.0
 * Date:       2014.03.18
 * Author:     Ji
 * 
 *========================================
 * @namespace  Jisons 
 * @class      BitmapHelper
 * @extends    
 * 
 *             对于 Bitmap 的转换函数 
 * 
 *========================================
 * Hi,小喵喵...
 * Copyright © 迹I柳燕
 * 
 * 转载请保留...
 * 
 */

using System.Drawing;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Jisons
{
    public static class  BitmapHelper
    {

        /// <summary> 从 ImageSource 转换到 Bitmap </summary>
        /// <param name="imageSource"> 将要转换的 ImageSource </param>
        /// <returns> 返回转换后的 Bitmap </returns>
        public static Bitmap ConvertToBitmap(this ImageSource imageSource)
        {
            var bitmapSource = imageSource as BitmapSource;
            if (bitmapSource != null)
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
                using (MemoryStream stm = new MemoryStream())
                {
                    encoder.Save(stm);
                    return new Bitmap(stm);
                }
            }
            return null;
        }

    }
}
