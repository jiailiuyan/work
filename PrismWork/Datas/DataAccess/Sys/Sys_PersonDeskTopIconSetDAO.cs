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
    /// Sys_PersonDeskTopIconSetDAO Sys_PersonDeskTopIconSet数据存取类
    /// 开发人员:
    /// 开发日期: 2014年11月 20日
    /// </summary>
    public partial class Sys_PersonDeskTopIconSetDAO
    {
        /// <summary>
        /// 删除PersonDeskTopIconSet记录
        /// </summary>
        /// <param name="moduleId">ModuleId</param> /// <param name="personId">PersonId</param> 
        /// <returns>受影响的记录数</returns>
        public int DeletePersonDeskTopIconSet(int moduleId, int personId)
        {
            string sql = @"DELETE FROM dbo.Sys_PersonDeskTopIconSet WHERE ModuleId = @ModuleId AND PersonId = @PersonId";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);

            db.AddInParameter(command, "ModuleId", DbType.Int32, moduleId);

            db.AddInParameter(command, "PersonId", DbType.Int32, personId);

            return db.ExecuteNonQuery(command);
        }
    }
}