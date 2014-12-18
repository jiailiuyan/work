using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Jisons
{
    public static class BitmapSourceHelper
    {

        /// <summary> 把 BitmapSource 转换成 Bitmap </summary>
        /// <param name="bs"></param>
        /// <returns></returns>
        public static Bitmap ConvertToBitmap(this BitmapSource bs)
        {
            return ConvertToBitmap(bs, 0, 0, bs.PixelWidth, bs.PixelHeight);
        }

        /// <summary> 把 BitmapSource 转换成指定起始点和宽高的 Bitmap </summary>
        /// <param name="bs"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Bitmap ConvertToBitmap(this BitmapSource bs, int x, int y, int width, int height)
        {
            var bmp = new Bitmap(width, height, PixelFormat.Format32bppPArgb);
            var bmpdata = bmp.LockBits(new Rectangle(System.Drawing.Point.Empty, bmp.Size), ImageLockMode.WriteOnly, PixelFormat.Format32bppPArgb);
            bs.CopyPixels(new Int32Rect(x, y, width, height), bmpdata.Scan0, bmpdata.Height * bmpdata.Stride, bmpdata.Stride);
            bmp.UnlockBits(bmpdata);
            return bmp;
        }

        /// <summary> 把 BitmapSource 转换成 byte[] </summary>
        /// <param name="bs"></param>
        /// <returns></returns>
        public static byte[] ConvertToBytes(this BitmapSource bs)
        {
            return ConvertToBytes(bs, 0, 0, (int)bs.Width, (int)bs.Height);
        }

        /// <summary> 把 BitmapSource 转换成指定起始点和宽高的 byte[] </summary>
        /// <param name="bs"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static byte[] ConvertToBytes(this BitmapSource bs, int x, int y, int width, int height)
        {
            var rect = new Int32Rect(x, y, width, height);
            var stride = bs.Format.BitsPerPixel * rect.Width / 8;
            byte[] data = new byte[rect.Height * stride];
            bs.CopyPixels(rect, data, stride, 0);
            return data;
        }

        public static BitmapSource ClipBitmapSource(this BitmapSource bs, int x, int y, int width, int height)
        {
            var rect = new Int32Rect(x, y, width, height);
            var stride = bs.Format.BitsPerPixel * rect.Width / 8;
            byte[] data = new byte[rect.Height * stride];
            bs.CopyPixels(rect, data, stride, 0);
            return BitmapSource.Create(width, height, 0, 0, System.Windows.Media.PixelFormats.Bgra32, null, data, stride);
        }

        public static Bitmap ConvertToBitmap(this byte[] data, int width, int height)
        {
            var bmp = new Bitmap(width, height);
            for (int w = 0; w < width; w++)
            {
                for (int h = 0; h < height; h++)
                {
                    int index = h * width * 4 + w * 4;

                    int B = data[index];
                    int G = data[index + 1];
                    int R = data[index + 2];
                    int A = data[index + 3];

                    bmp.SetPixel(w, h, System.Drawing.Color.FromArgb(A, R, G, B));
                }
            }
            return bmp;
        }

        public static BitmapSource ConvertToBitmapSource(this Bitmap source)
        {
            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(source.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }

    }
}
