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
    /// Sys_BusiUnitTypeDAO 业务单位类型,该表不做显示维护数据存取类
    /// 开发人员:
    /// 开发日期: 2014年10月 27日
    /// </summary>
    public partial class Sys_BusiUnitTypeDAO
    {
        /// <summary>
        /// 获得业务单位类型的表的全部数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetBusiUnitType()
        {
            string sql = "SELECT KeyId AS BusiUnitTypeID ,BusiUnitTypeCode ,BusiUnitTypeName ,BusiUnitTypeShortName ,ParentID ,Status, Remark,isnull(ParentID,0) as pid from Sys_BusiUnitType where status=1";
            return GetDataTable(sql);
        }
        /// <summary>
        /// 获得业务单位类型的表的全部数据
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable GetBusiUnitType(string strWhere)
        {
            string sql = "select KeyId AS BusiUnitTypeID,BusiUnitTypeCode,BusiUnitTypeName ,BusiUnitTypeShortName,ParentID,(CASE Status WHEN 1 THEN '是' ELSE '否' END) AS Status, Remark,isnull(parentid,0) as parentid from Sys_BusiUnitType where status=1";
            if (strWhere != "")
                sql += strWhere;
            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);

            DataSet ds = db.ExecuteDataSet(command);
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            else
                return null;
        }

        /// <summary>
        /// 根据条件获取所有菜单
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <returns></returns>
        public DataTable GetAllBusiUnitType(string strWhere)
        {
            //            string sql = @"SELECT a.BusiUnitTypeID,a.BusiUnitTypeCode,(CASE WHEN a.ParentID IS NULL THEN 0 ELSE a.ParentID END ) AS ParentID,
            //                         (CASE WHEN a.ParentID IS NULL THEN 1 WHEN (SELECT COUNT(*) FROM dbo.BusiUnitType b WHERE BusiUnitTypeID = a.ParentID AND ParentID IS NULL)=1 THEN 2 WHEN 
            //                          (SELECT COUNT(*) FROM dbo.BusiUnitType c WHERE BusiUnitTypeID=(SELECT ParentID FROM b WHERE BusiUnitTypeID = a.ParentID) AND ParentID IS NULL) THEN 3 ELSE 4 END) AS Levels
            //                         FROM dbo.BusiUnitType a WHERE Status = 1 ";
            string sql = @"SELECT KeyId AS BusiUnitTypeID,BusiUnitTypeCode,
                            len(BusiUnitTypeCode) as levels from Sys_BusiUnitType where status=1 ";
            if (strWhere != "")
            {
                sql += strWhere;
            }
            sql += " Order by BusiUnitTypeCode desc";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);

            DataSet ds = db.ExecuteDataSet(command);
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            else
                return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetRegion()
        {
            string sql = "select RegionCode ,RegionName,isnull(parentid,0) as ParentId from region";
            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);

            DataSet ds = db.ExecuteDataSet(command);
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            else
                return null;
        }
    }
}