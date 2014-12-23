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
    /// Sys_PersonBusiUnitDAO 一个人可能属于多个单位数据存取类
    /// 开发人员:
    /// 开发日期: 2014年10月 27日
    /// </summary>
    public partial class Sys_PersonBusiUnitDAO
    {
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public DataTable GetDataList(string where)
        {
            string sql = @"SELECT a.KeyId, PersonID, BusiUnitID, IsMaster, BeginDate, EndDate, a.CreatedBy, a.CreatedOn, a.ModifiedBy
                        , a.ModifiedOn, sp.PersonName, sbu.BusiUnitName
                        FROM dbo.Sys_PersonBusiUnit a
                        LEFT JOIN dbo.Sys_Person sp ON a.PersonID=sp.KeyId
                        LEFT JOIN dbo.Sys_BusiUnit sbu ON a.BusiUnitID=sbu.KeyId
                        WHERE 1=1 ";
            if (!string.IsNullOrWhiteSpace(where))
                sql += where;
            return GetDataTable(sql);
        }

        /// <summary>
        /// 根据用户id删除记录
        /// </summary>
        /// <param name="personid"></param>
        public void DeleteByPerson(string personid)
        {
            string sql = @"DELETE FROM dbo.Sys_PersonBusiUnit WHERE PersonID='" + personid + "'";
            ExecuteSql(sql);
        }
    }
}