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
    /// Sys_ProjInfoDAO 项目基本信息数据存取类
    /// 开发人员:
    /// 开发日期: 2014年10月 27日
    /// </summary>
    public partial class Sys_ProjInfoDAO
    {
        /// <summary>
        /// 根据项目代码获取项目的全称
        /// </summary>
        /// <param name="projcode"></param>
        /// <returns></returns>
        public string GetProjNameByCode(string projcode)
        {
            string sql = @"select projfullname from sys_projinfo where projcode='" + projcode + "'";
            DataTable dt = GetDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
                return dt.Rows[0]["projfullname"].ToString();
            else
                return "";
        }

        /// <summary>
        /// 获取一个项目的项目内码
        /// </summary>
        /// <param name="projcode"></param>
        /// <returns></returns>
        public string GetProjIDByProjCode(string projcode)
        {
            string sql = "select KeyId from sys_projinfo where projcode='" + projcode + "'";
            DataTable dt = GetDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
                return dt.Rows[0][0].ToString();
            else
                return "";
        }

        /// <summary>
        /// 返回满足查询条件的项目基本信息数据表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>项目基本信息数据表</returns>
        public DataTable GetProjInfoTable(QueryParameter param)
        {
            string sql = @"SELECT KeyId, ProjName, ProjFullName, ProjCode, ProjDetails, OnlineDate, Director, DBIpAdd, DBName, DBUser, DBPwd, Status, Remark, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn FROM Sys_ProjInfo";
            return GetDataTable(sql, param);
        }
    }
}