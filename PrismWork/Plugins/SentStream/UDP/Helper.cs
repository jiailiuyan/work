using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Windows.Media.Imaging;

namespace JI
{
   public static class Helper
    {
       public static byte[] StringToByte(string message)
        {
            return Encoding.Unicode.GetBytes(message);
        }

       public static string Search()
       {
           string filPath = string.Empty;
           OpenFileDialog openFileDialog1 = new OpenFileDialog();
           openFileDialog1.Filter = "图片文件(*.jpg,*.jpeg,*.png,*.bmp,*.ico)|*.jpg;*.jpeg;*.png;*.bmp;*.ico";
           openFileDialog1.FilterIndex = 0;
           openFileDialog1.RestoreDirectory = true;
           if (openFileDialog1.ShowDialog() == DialogResult.OK)
           {
               filPath = openFileDialog1.FileName;
           }
           else
           { }
           return filPath;
       }

       public static void SaveImageCapture(BitmapSource bitmap)
       {
           JpegBitmapEncoder encoder = new JpegBitmapEncoder();
           encoder.Frames.Add(BitmapFrame.Create(bitmap));
           encoder.QualityLevel = 100;

           // Configure save file dialog box
           Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
           dlg.FileName = "JI-Image"; // Default file name
           dlg.DefaultExt = ".Jpg"; // Default file extension
           dlg.Filter = "JI-Image (.jpg)|*.jpg"; // Filter files by extension

           // Show save file dialog box
           Nullable<bool> result = dlg.ShowDialog();

           // Process save file dialog box results
           if (result == true)
           {
               // Save Image
               string filename = dlg.FileName;
               FileStream fstream = new FileStream(filename, FileMode.Create);
               encoder.Save(fstream);
               fstream.Close();
           }

       }

    }
}
