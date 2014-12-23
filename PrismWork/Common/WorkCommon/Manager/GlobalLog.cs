using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WorkCommon.Manager
{
    public class GlobalLog
    {
        private static GlobalLog instance;
        public static GlobalLog Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GlobalLog();
                }
                return instance;
            }
        }

        public string UserCustomerConfigFloder
        {
            get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs"); }
        }

        /// <summary> 日志输出接口 </summary>
        public static ICSLog Logger { get; private set; }

        /// <summary> Output输出,在界面中显示,并同时输出到日志中 </summary>
        public static ICSLog Output { get; private set; }

        /// <summary> 静态构造函数 </summary>
        static GlobalLog()
        {
            Logger = new CSLogger(false);
            Output = new CSLogger(true);
        }

    }
}
