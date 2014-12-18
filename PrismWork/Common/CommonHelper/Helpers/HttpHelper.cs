/* 迹I柳燕
 * 
 * FileName:   JisonsHttp.cs
 * Version:    1.0
 * Date:       2014.03.18
 * Author:     Ji
 * 
 *========================================
 * @namespace  Jisons 
 * @class      JisonsHttp
 * @extends    
 * 
 *             对于 Http 的数据包装
 * 
 *========================================
 * Hi,小喵喵...
 * Copyright © 迹I柳燕
 * 
 * 转载请保留...
 * 
 */

using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Jisons
{

    /// <summary> 基于Http的数据请求 </summary>
    public static class HttpHelper
    {

        /// <summary> 默认设置当前的标识为IE7 </summary>
        public const string IE7 = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; InfoPath.2; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022; .NET4.0C; .NET4.0E)";

        /// <summary> 设置Cookie容器 此项能在需要的时候保存Cookies </summary>
        public static CookieContainer CookieContainers = new CookieContainer();

        public static Dictionary<string, string> Cookies = new Dictionary<string, string>();

        /// <summary> 设置需要证书请求的时候默认为true </summary>
        static HttpHelper()
        {
            ServicePointManager.ServerCertificateValidationCallback += (se, cert, chain, sslerror) => { return true; };
        }

        /// <summary> 向HTTP流中添加数据头 </summary>
        /// <param name="url"> 请求的URL </param>
        /// <param name="method"> 请求使用的方法 GET、POST </param>
        /// <returns> 返回创建的 HttpWebRequest </returns>
        private static HttpWebRequest CreatRequest(this string url, string method)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

            //优化多线程超时响应
            req.KeepAlive = false;
            System.Net.ServicePointManager.DefaultConnectionLimit = 50;

            req.Method = method.ToUpper();
            req.AllowAutoRedirect = true;
            req.CookieContainer = CookieContainers;
            req.ContentType = "application/x-www-form-urlencoded";

            req.UserAgent = IE7;
            req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            req.Timeout = 50000;

            return req;
        }

        /// <summary> 根据URL获取回传的 Stream 无编码格式的确认 </summary>
        /// <param name="url"> 请求的URL </param>
        /// <returns> 返回的数据流 </returns>
        public static Stream GetStreamResponse(this string url, string method = "get", string data = "")
        {
            try
            {
                var req = CreatRequest(url, method);

                if (method.ToUpper() == "POST" && data != null)
                {
                    var postBytes = new ASCIIEncoding().GetBytes(data);
                    req.ContentLength = postBytes.Length;
                    Stream st = req.GetRequestStream();
                    st.Write(postBytes, 0, postBytes.Length);
                    st.Close();
                }

                HttpWebResponse res = (HttpWebResponse)req.GetResponse();

                var stream = res.GetResponseStream();
                {
                    //优化多线程内存流的释放
                    MemoryStream ms = new MemoryStream();
                    stream.CopyTo(ms);
                    //接收到的数据流需要重新设置读取起始位
                    ms.Position = 0;

                    //优化多线程多个实例时的端口占用
                    res.Close();
                    req.Abort();

                    return ms;
                }
            }
            catch
            {
                return null;
            }
        }


        /// <summary> 以字符串形式获取返回值 </summary>
        /// <param name="url"> 请求的URl </param>
        /// <param name="method"> 传递方法 </param>
        /// <param name="data"> 传递数据 </param>
        /// <returns> 返回的字符串 UTF-8 编码 </returns>
        public static string GetStringResponse(this string url, string method = "get", string data = "")
        {
            return GetStringResponse(url, Encoding.UTF8, method, data);
        }

        /// <summary> 以字符串形式获取返回值 </summary>
        /// <param name="url"> 请求的URl </param>
        /// <param name="encoding"> 编码格式 </param>
        /// <param name="method"> 传递方法 </param>
        /// <param name="data"> 传递数据 </param>
        /// <returns> 返回指定编码的字符串 </returns>
        public static string GetStringResponse(this string url, Encoding encoding, string method = "get", string data = "")
        {
            try
            {
                var req = CreatRequest(url, method);

                if (method.ToUpper() == "POST" && data != null)
                {
                    var postBytes = new ASCIIEncoding().GetBytes(data);
                    req.ContentLength = postBytes.Length;
                    Stream st = req.GetRequestStream();
                    st.Write(postBytes, 0, postBytes.Length);
                    st.Close();
                }

                HttpWebResponse res = (HttpWebResponse)req.GetResponse();

                foreach (Cookie cookie in res.Cookies)
                {
                    Cookies[cookie.Name] = cookie.Value;
                    CookieContainers.Add(cookie);
                }

                //优化多线程内存流的释放
                using (var stream = res.GetResponseStream())
                {
                    var sr = new StreamReader(stream, encoding).ReadToEnd();

                    //优化多线程多个实例时的端口占用
                    res.Close();
                    req.Abort();
                    return sr;
                }
            }
            catch
            {
                return string.Empty;
            }
        }

    }
}
