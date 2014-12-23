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
    /// Sys_PersonDeskTopConfigDAO Sys_PersonDeskTopConfig数据存取类
    /// 开发人员:
    /// 开发日期: 2014年11月 20日
    /// </summary>
    public partial class Sys_PersonDeskTopConfigDAO
    {
        /// <summary>
        /// 更新桌面设置
        /// </summary>
        /// <param name="personId">用户ID</param>
        /// <param name="name">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public int UpdateSys_PersonDeskTopConfig(int personId, string name, string value)
        {
            try
            {
                string sql = @"UPDATE dbo.Sys_PersonDeskTopConfig SET ConfigValue = @ConfigValue, ModifiedDate = getDate() WHERE PersonId = @PersonId and ConfigName = @ConfigName";

                Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
                DbCommand command = db.GetSqlStringCommand(sql);

                db.AddInParameter(command, "PersonId", DbType.Int32, personId);
                db.AddInParameter(command, "ConfigName", DbType.String, name);
                db.AddInParameter(command, "ConfigValue", DbType.String, value);

                return db.ExecuteNonQuery(command);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 判断桌面设置键是否存在
        /// </summary>
        /// <param name="personId">用户ID</param>
        /// <param name="name">键</param>
        /// <returns></returns>
        public int CheckPersonConfigName(int personId, string name)
        {
            try
            {
                string sql = @"SELECT count(*) FROM dbo.Sys_PersonDeskTopConfig where PersonId = @PersonId and ConfigName = @ConfigName";
                Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
                DbCommand command = db.GetSqlStringCommand(sql);

                db.AddInParameter(command, "PersonId", DbType.Int32, personId);
                db.AddInParameter(command, "ConfigName", DbType.String, name);

                return int.Parse(db.ExecuteScalar(command).ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}