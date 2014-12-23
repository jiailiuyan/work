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
    /// Sys_BusiUnitDAO 业务单位信息数据存取类
    /// 开发人员:
    /// 开发日期: 2014年10月 27日
    /// </summary>
    public partial class Sys_BusiUnitDAO
    {       
        #region V1
        /// <summary>
        /// 获取一个单位下的单位信息（包含自己及下属单位）
        /// </summary>
        /// <param name="busiid"></param>
        /// <param name="strbusitype"></param>
        /// <returns></returns>
        public DataTable GetBusiUnitOwnAndChildren(int busiid,string strbusitype)
        {
            string sql = "EXEC p_GetBusiUnitById "+busiid.ToString()+",'"+strbusitype+"'";
            return this.GetDataTable(sql);
        }

        /// <summary>
        /// 取唯一编码最大值
        /// </summary>
        /// <returns>唯一编码最大值</returns>
        public int GetMaxBusiUnitCode(int beginValue)
        {
            string sql = @"select isnull(max(cast(BusiUnitCode as int))," + beginValue + ") as maxBusiUnitCode FROM Sys_BusiUnit ";
            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);
            return int.Parse(db.ExecuteScalar(command).ToString());
        }

        /// <summary>
        /// 返回状态为true的所有非叶子节点单位信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetParentBusiUnitForQuery()
        {
            string sql = @"SELECT DISTINCT b.KeyId AS ParentId,b.BusiUnitName AS ParentName,b.ParentId AS ParentPId FROM dbo.Sys_BusiUnit a
                           JOIN dbo.Sys_BusiUnit b ON a.ParentId=b.KeyId
                            WHERE a.Status=1 ";
            return GetDataTable(sql);
        }

        /// <summary>
        /// 返回状态为true的所有单位信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetBusiUnit()
        {
            string sql = @"SELECT * FROM dbo.Sys_BusiUnit WHERE Status=1 ";
            return GetDataTable(sql);
        }

        /// <summary>
        /// 返回满足查询条件的单位信息
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable GetBusiUnitByWhere(string strWhere)
        {
            string sql = @"SELECT * FROM dbo.Sys_BusiUnit WHERE Status=1 ";
            if (!string.IsNullOrWhiteSpace(strWhere))
            {
                sql += strWhere;
            }
            return GetDataTable(sql);
        }

        /// <summary>
        /// 根据父节点id获得所有子节点
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public DataTable GetBusiUnitByParent(string parentId)
        {
            string sql = @"SELECT a.KeyId AS BusiUnitId, a.BusiUnitTypeID, a.RegionCode, a.BusiUnitCode, a.BusiUnitName
				--, a.ERPCode
                , CASE WHEN(a.ShortName IS NULL) THEN a.BusiUnitName ELSE a.ShortName END AS ShortName, a.HelperCode, a.OrderId, a.Address, a.ParentId, a.WebSiteUrl, a.FtpSiteUrl, a.Telephone1, a.Telephone2
                , a.Fax, a.E_Mail, a.X, a.Y, a.Status, a.Remark, a.CreatedBy, a.CreatedOn, a.ModifiedBy, a.ModifiedOn,CASE WHEN a.Status=1 THEN '√' ELSE '×' END AS StatusDescribe,b.BusiUnitTypeName,c.BusiUnitName AS ParentName 
                FROM dbo.Sys_BusiUnit a
                     LEFT JOIN dbo.Sys_BusiUnitType b ON a.BusiUnitTypeID=b.KeyId
                     LEFT JOIN dbo.Sys_BusiUnit c ON a.ParentId=c.KeyId ";
            if (!string.IsNullOrWhiteSpace(parentId))
            {
                sql += " WHERE a.ParentId=" + parentId;
            }
            else
            {
                sql += " WHERE a.ParentId IS NUll";
            }
            return GetDataTable(sql);
        }

        /// <summary>
        /// 删除单位
        /// </summary>
        /// <param name="unitid"></param>
        /// <returns></returns>
        public string DeleteUnit(int unitid)
        {
            try
            {
                string sql = "delete from Sys_BusiUnit where KeyId = @KeyID";
                Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
                DbCommand command = db.GetSqlStringCommand(sql);

                db.AddInParameter(command, "KeyID", DbType.Int32, unitid);
                db.ExecuteNonQuery(command);
                return string.Empty;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("REFERENCE 约束"))
                {
                    return "当前单位被其它模块使用，不能删除!";
                }
                return ex.Message.Replace('\'', '‘');
            }
        }
        #endregion
    }
}