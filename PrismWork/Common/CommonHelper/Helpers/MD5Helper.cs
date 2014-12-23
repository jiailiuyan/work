using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Jisons
{
    public static class MD5Helper
    {

        public static string GetMD5(this string fileName)
        {
            string str = string.Empty;
            try
            {
                using (var file = new FileStream(fileName, FileMode.Open))
                {
                    str = GetMD5(file);
                }
            }
            catch { }
            return str;
        }

        public static string GetMD5(this Stream stream)
        {
            StringBuilder sb = new StringBuilder();
            byte[] retVal = (new System.Security.Cryptography.MD5CryptoServiceProvider()).ComputeHash(stream);
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }

    }
}
