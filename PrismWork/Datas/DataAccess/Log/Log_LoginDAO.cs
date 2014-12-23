using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Project.Common;
using Project.Entities;

namespace Project.DataAccess 
{
    /// <summary>
    /// Log_LoginDAO Log_Login数据存取类
    /// 开发人员:
    /// 开发日期: 2014年10月 28日
    /// </summary>
    public partial class Log_LoginDAO
    {
        /// <summary>
        /// 获取用户最近登录信息
        /// </summary>
        /// <param name="personid"></param>
        /// <returns></returns>
        public int GetUserLoginLogTop(int personid)
        {
            string sql = "select top 1 keyid from Log_Login where PersonId = " + personid + " and LogoutTime is null order by LoginTime desc";
            object keyid = this.ExecuteScalar(sql);
            if (keyid is DBNull)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(keyid);
            }
        }

        /// <summary>
        /// 更新退出时间
        /// </summary>
        /// <param name="keyid"></param>
        public void UpdateLogoutTime(int keyid)
        {
            string sql = "update Log_Login set LogoutTime = getDate() where keyid = " + keyid;
            this.ExecuteSql(sql);
        }
    }
}