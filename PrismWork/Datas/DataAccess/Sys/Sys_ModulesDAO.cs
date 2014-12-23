using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Project.Common;
using Project.Entities;
using System.Transactions;

namespace Project.DataAccess 
{
    /// <summary>
    /// Sys_ModulesDAO 项目模块数据存取类
    /// 开发人员:
    /// 开发日期: 2014年10月 27日
    /// </summary>
    public partial class Sys_ModulesDAO
    {
        /// <summary>
        /// 获取用户功能权限清单
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="projID"></param>
        /// <param name="moduleEntry"></param>
        /// <returns></returns>
        public int? GetUserModulePrivilege(int userID, int projID, string moduleEntry)
        {
            string query = @"SELECT rm.PrivilegeMask & m.DisplayPrivilegeMask
	FROM dbo.Sys_Modules m
		JOIN dbo.Sys_RoleModules rm ON m.ModuleId = rm.ModulesID AND m.ProjID = rm.ProjID
		JOIN dbo.Sys_UserRole ur ON rm.ProjID = ur.ProjID AND rm.RoleId = ur.RoleId
	WHERE (m.ModuleEntry = @ModuleEntry OR m.ModuleEntry LIKE @ModuleEntry + '?%')
        AND m.ProjID = @ProjectID
		AND ur.KeyId = @UserID";
            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(query);

            db.AddInParameter(command, "UserID", DbType.Int32, userID);
            db.AddInParameter(command, "ProjectID", DbType.Int32, projID);
            db.AddInParameter(command, "ModuleEntry", DbType.String, moduleEntry);

            return db.ExecuteScalar<int?>(command);
        }

        /// <summary>
        /// 获取用户功能权限清单
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="projID"></param>
        /// <param name="moduleEntry"></param>
        /// <returns></returns>
        public int? GetUserModuleEntry(int userID, int projID, string moduleEntry)
        {
            string query = @"SELECT m.KeyId
	FROM dbo.Sys_Modules m
		JOIN dbo.Sys_RoleModules rm ON m.KeyId = rm.ModulesID AND m.ProjID = rm.ProjID
		JOIN dbo.Sys_UserRole ur ON rm.ProjID = ur.ProjID AND rm.RoleId = ur.RoleId
	WHERE (m.ModuleEntry = @ModuleEntry OR m.ModuleEntry LIKE @ModuleEntry + '?%')
        AND m.ProjID = @ProjectID
		AND ur.KeyId = @UserID";
            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(query);

            db.AddInParameter(command, "UserID", DbType.Int32, userID);
            db.AddInParameter(command, "ProjectID", DbType.Int32, projID);
            db.AddInParameter(command, "ModuleEntry", DbType.String, moduleEntry);

            return db.ExecuteScalar<int?>(command);
        }

        public DataTable GetParentModulesData(int projectID, string logsystemname)
        {
            string query = string.Format(@"
WITH Level1Modules AS(
	SELECT -1 AS KeyId,'" + logsystemname + @"' AS ModuleName,NULL AS ParentId,0 AS DisplayOrder
	FROM dbo.Sys_Modules
	UNION 
	SELECT KeyId, ModuleName, -1 AS ParentId, DisplayOrder
		FROM dbo.Sys_Modules
		WHERE ParentId IS NULL AND ProjID = @ProjectID AND [Status] = 1
)
SELECT * FROM Level1Modules
	UNION ALL 
		SELECT i.KeyId, i.ModuleName, i.ParentId, i.DisplayOrder
			FROM dbo.Sys_Modules i, Level1Modules j
			WHERE i.ParentId = j.KeyId AND [Status] = 1
	ORDER BY ParentID, DisplayOrder");
            var db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            var command = db.GetSqlStringCommand(query);
            db.AddInParameter(command, "ProjectID", DbType.String, projectID);
            return db.ExecuteDataSet(command).Tables[0];
        }

        /// <summary>
        /// 返回满足查询条件的实体列表
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <returns>实体列表</returns>
        public DataTable GetModulesTable(string strWhere)
        {
            string sql = @" SELECT a.KeyId, a.KeyId as ProjID, a.ModuleCode, a.ModuleName, a.ShortName, a.ParentId,isnull(a.ParentId,0) as ParentValue, a.UrlString, 
                    a.Status, a.Hint, a.DisplayOrder, a.DisplayPrivilegeMask, a.HelpUrlString, a.ModuleIconS, a.ModuleIconB, a.IsShowInDeskTop, a.OpenType, a.Remark, 
                    b.ProjName, ISNULL(c.ModuleName,'模块目录') AS ParentName, a.moduleentry,
                    (CASE a.Status WHEN 1 THEN '√' WHEN 0 THEN '×' END) AS DBStatus
                    FROM dbo.Sys_Modules a LEFT JOIN dbo.Sys_ProjInfo b ON b.KeyId = a.ProjID 
                    LEFT JOIN dbo.Sys_Modules c ON c.KeyId = a.ParentId Where 1=1 ";
            if (strWhere != "")
            {
                sql += strWhere;
            }
            sql += " order by DisplayOrder";
            return this.GetDataTable(sql);
        }

        /// <summary>
        /// 删除模块
        /// </summary>
        /// <param name="modulesid"></param>
        /// <returns></returns>
        public string DeleteSingleModules(int modulesid)
        {
            string countsql1 = " select count(*) from Sys_RoleModules rm where rm.ModulesID = @ModulesId or rm.ModulesID in (select m.KeyId from Sys_Modules m where m.ParentId = rm.ModulesID) or rm.ModulesID in (select ms.KeyId from Sys_Modules ms where ms.ParentId in (select KeyId from Sys_Modules where ParentId = rm.ModulesID))";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command1 = db.GetSqlStringCommand(countsql1);
            db.AddInParameter(command1, "ModulesId", DbType.Int32, modulesid);

            int count1 = db.ExecuteScalar<int>(command1);

            if (count1 == 0)
            {
                using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required))
                {
                    string delsqllevel1 = " delete from Sys_Modules where KeyId = @ModuleId";
                    string delsqllevel2 = " delete from Sys_Modules where ParentId = @ModuleId";
                    string delsqllevel3 = " delete from Sys_Modules where ParentId in (select KeyId from Sys_Modules where ParentId = @ModuleId)";
                    //string delsql = delsqllevel1 + delsqllevel2 + delsqllevel3;
                    DbCommand delcommand1 = db.GetSqlStringCommand(delsqllevel1);
                    DbCommand delcommand2 = db.GetSqlStringCommand(delsqllevel2);
                    DbCommand delcommand3 = db.GetSqlStringCommand(delsqllevel3);

                    db.AddInParameter(delcommand3, "ModuleId", DbType.Int32, modulesid);
                    db.AddInParameter(delcommand2, "ModuleId", DbType.Int32, modulesid);
                    db.AddInParameter(delcommand1, "ModuleId", DbType.Int32, modulesid);
                    db.ExecuteNonQuery(delcommand3);//ExecuteScalar<int>(command);
                    db.ExecuteNonQuery(delcommand2);
                    db.ExecuteNonQuery(delcommand1);

                    transaction.Complete();
                }
                return string.Empty;
            }
            else
            {
                return "该模块已分配给其他角色，不能删除!";
            }
        }

        /// <summary>
        /// 返回满足查询条件的项目模块数据表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>项目模块数据表</returns>
        public DataTable GetModulesTable(QueryParameter param)
        {
            string sql = @"SELECT KeyId, ProjID, ModuleCode, ModuleName, ShortName, ParentId, UrlString, ModuleEntry, ModuleIconS, ModuleIconB, IsShowInDeskTop, OpenType, Status, Hint, DisplayOrder, DisplayPrivilegeMask, HelpUrlString, Remark, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn FROM dbo.Sys_Modules";
            return GetDataTable(sql, param);
        }
    }
}