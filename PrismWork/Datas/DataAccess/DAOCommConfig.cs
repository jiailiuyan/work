using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.DataAccess
{
    public class DAOCommConfig
    {
        protected enum DBLink
        {
            /// <summary>
            /// 系统库
            /// </summary>
            SysDBLink = 1,
            /// <summary>
            /// 业务库
            /// </summary>
            BusiDBLink =2
        }
        /// <summary>
        /// 链接服务器地址
        /// </summary>
        protected static string LinkServerADD = System.Configuration.ConfigurationManager.AppSettings["LinkServerADD"].ToString();
    }
}
