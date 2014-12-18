/* 迹I柳燕
 * 
 * FileName:   ImageSourceHelper.cs
 * Version:    1.0
 * Date:       2014.03.18
 * Author:     Ji
 * 
 *========================================
 * @namespace  Jisons 
 * @class      ImageSourceHelper
 * @extends    
 *             
 *             WPF 扩展
 *             对于JisonsImageSource的处理
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
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Jisons
{
    public static class ImageSourceHelper
    {

        /// <summary> 转换 Icon 到 ImageSource </summary>
        /// <param name="icon">将要转换的 Icon </param>
        /// <returns>转换后的 ImageSource</returns>
        public static ImageSource ConvertToImageSource(this Icon icon)
        {
            return icon != null ? Imaging.CreateBitmapSourceFromHIcon(icon.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions()) : null;
        }

        /// <summary> 转换 Bitmap 到 ImageSource 
        /// Imaging.CreateBitmapSourceFromHBitmap 具有相同的功能，此项是通过数据流进行转换</summary>
        /// <param name="bitmap">将要转换的 Bitmap </param>
        /// <returns>转换后的 ImageSource </returns>
        public static ImageSource ConvertImageSource(this Bitmap bitmap)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                ImageBrush imageBrush = new ImageBrush();
                ImageSourceConverter imageSourceConverter = new ImageSourceConverter();
                return (ImageSource)imageSourceConverter.ConvertFrom(stream);
            }
        }

        /// <summary> 从 FrameworkElement 中截取 ImageSource 当前 FrameworkElement 需已经渲染 </summary>
        /// <param name="frameworkElement">将要截取图像的 FrameworkElement </param>
        /// <returns>截取后的 ImageSource </returns>
        public static ImageSource GetImageSource(this FrameworkElement frameworkElement)
        {
            int Height = (int)frameworkElement.ActualHeight;
            int Width = (int)frameworkElement.ActualWidth;
            RenderTargetBitmap renderBMP = new RenderTargetBitmap(Width, Height, 96, 96, PixelFormats.Pbgra32);
            renderBMP.Render(frameworkElement);
            return renderBMP as ImageSource;
        }

        /// <summary> 从文件中读取 ImageSource </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>读取到的 ImageSource </returns>
        public static ImageSource GetImageSource(this string filePath)
        {
            try
            {
                using (FileStream fs = File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    BinaryReader binReader = new BinaryReader(fs);
                    byte[] bytes = binReader.ReadBytes((int)fs.Length);
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.CreateOptions = BitmapCreateOptions.IgnoreImageCache;

                    bitmap.BeginInit();
                    bitmap.StreamSource = new MemoryStream(bytes);
                    bitmap.EndInit();

                    return bitmap;
                }
            }
            catch { }
            return null;
        }

    }
}
