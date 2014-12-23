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
    /// Sys_RolesDAO 项目角色数据存取类
    /// 开发人员:
    /// 开发日期: 2014年10月 27日
    /// </summary>
    public partial class Sys_RolesDAO
    {
        #region V1
        /// <summary>
        /// 获得最大角色
        /// </summary>
        /// <param name="projid"></param>
        /// <returns></returns>
        public string GetMaxRoleCode(string projid)
        {
            string sql = "select isnull(max(rolecode),1000) from Sys_Roles where projid=" + projid;
            return GetDataTable(sql).Rows[0][0].ToString();
        }
        /// <summary>
        /// 返回满足查询条件的角色基本信息数据表
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <returns>角色基本信息数据表</returns>
        public DataTable GetRolesData(string strWhere)
        {
            string sql = @" SELECT a.KeyId AS RoleId,a.ProjID,a.RoleName,a.RoleCode,a.CreatedBy,a.CreatedOn,a.ModifiedBy,a.ModifiedOn,
                            case when len(a.Remark)>20 then left(cast(a.Remark as varchar(max)),20)+'.....' else a.Remark end as Remark, 
                            b.ProjName,(CASE a.Status WHEN 1 THEN '是' ELSE '否' END) AS DBStatus 
                            FROM dbo.Sys_Roles a LEFT JOIN dbo.Sys_ProjInfo b ON b.KeyId =a.ProjID WHERE b.Status =1 ";

            if (strWhere != "")
            {
                sql += strWhere;
            }

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);

            DataSet ds = db.ExecuteDataSet(command);
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            else
                return null;
        }

        /// <summary>
        /// 获取角色功能模块
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public DataSet GetRoleModules(int projectID, int roleID)
        {
            string query = @"WITH ModulesData AS (
	SELECT KeyId AS  ModuleID, ModuleName, DisplayPrivilegeMask, NULL AS ParentID
			, 1 AS LevelCode
			, ROW_NUMBER() OVER(ORDER BY DisplayOrder) * POWER(10, 8) AS PathCode
		FROM dbo.Sys_Modules 
		WHERE ParentId IS NULL AND [Status] = 1 AND ProjID = @ProjectID
	UNION ALL
		SELECT m.KeyId AS ModuleID, m.ModuleName, m.DisplayPrivilegeMask, m.ParentId
				, p.LevelCode + 1 AS LevelCode
				, p.PathCode + ROW_NUMBER() OVER(ORDER BY m.DisplayOrder) * POWER(10, 8 - p.LevelCode * 2) AS PathCode
			FROM dbo.Sys_Modules m JOIN ModulesData p
				ON m.ParentId = p.ModuleID AND m.ProjID = @ProjectID
			WHERE m.[Status] = 1
)
SELECT m.ModuleID, m.ModuleName, ISNULL(m.ParentID, -1) AS ParentID, m.DisplayPrivilegeMask AS Mask

		, CASE WHEN (DisplayPrivilegeMask & rm.PrivilegeMask) & 1  > 0 THEN 1 END AS [Select]
		, CASE WHEN (DisplayPrivilegeMask & rm.PrivilegeMask) & 2  > 0 THEN 1 END AS [Create]
		, CASE WHEN (DisplayPrivilegeMask & rm.PrivilegeMask) & 4  > 0 THEN 1 END AS [Update]
		, CASE WHEN (DisplayPrivilegeMask & rm.PrivilegeMask) & 8  > 0 THEN 1 END AS [Delete]
		, CASE WHEN (DisplayPrivilegeMask & rm.PrivilegeMask) & 16 > 0 THEN 1 END AS [Print]
		, CASE WHEN (DisplayPrivilegeMask & rm.PrivilegeMask) & 32 > 0 THEN 1 END AS [Export]
		, CASE WHEN (DisplayPrivilegeMask & rm.PrivilegeMask) & 64 > 0 THEN 1 END AS [Approve]
        , CASE WHEN (DisplayPrivilegeMask & rm.PrivilegeMask) & 128 > 0 THEN 1 END AS [Submit]
	FROM ModulesData m                                       
		LEFT JOIN dbo.Sys_RoleModules rm ON rm.ProjID = @ProjectID AND rm.RoleID = @RoleID AND m.ModuleID = rm.ModulesID
	ORDER BY PathCode";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(query);
            db.AddInParameter(command, "ProjectID", DbType.Int32, projectID);
            db.AddInParameter(command, "RoleID", DbType.Int32, roleID);

            return db.ExecuteDataSet(command);
        }

        /// <summary>
        /// 获取角色用户
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public DataSet GetRoleUsers(int projectID, int roleID)
        {
            string query = @"WITH BusiUnitData AS (
	SELECT 1 AS LevelCode, BusiUnitName, KeyId AS BusiUnitId, NULL AS UserID
			, CONVERT(VARCHAR(100), NULL) AS UserAccount, NULL AS Parent
			, POWER(10.0, 7) AS LevelPath, 0 AS TypeCode/*单位*/
		FROM dbo.Sys_BusiUnit 
		--WHERE ParentId IS NULL
    UNION ALL 
		SELECT 0, CONVERT(VARCHAR(100), p.PersonName), -p.KeyId, p.KeyId, p.DomainAcc
				, u.BusiUnitId, u.LevelPath, 1 AS TypeCode/*用户*/
			FROM BusiUnitData u
				JOIN dbo.Sys_PersonBusiUnit pu ON u.BusiUnitId = pu.BusiUnitID
				JOIN dbo.Sys_Person p ON pu.PersonID = p.KeyId
			WHERE u.TypeCode = 0  AND p.[Status] = 1
	UNION ALL
		SELECT p.LevelCode + 1 AS LevelCode, u.BusiUnitName, u.KeyId, NULL, NULL, u.ParentId
				, ROW_NUMBER() OVER(ORDER BY OrderId) * POWER(10.0, 7 - 2 * p.LevelCode) + p.LevelPath
				, 0 AS TypeCode/*单位*/
			FROM BusiUnitData p
				JOIN Sys_BusiUnit u ON p.BusiUnitId = u.ParentId   
			WHERE u.[Status] = 1 AND p.TypeCode = 0
)  
SELECT BusiUnitName AS [Text], BusiUnitId AS ID, a.Parent AS PID
		, UserID, UserAccount, LevelPath, TypeCode
		, CASE WHEN ur.KeyId > 0 THEN 1 END AS RoleUser
	FROM BusiUnitData a
		LEFT JOIN dbo.Sys_UserRole ur
			ON ur.RoleId = @RoleID AND ur.ProjID = @ProjectID AND ur.PersonID = a.UserID
	ORDER BY LevelPath, TypeCode, BusiUnitName";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(query);
            db.AddInParameter(command, "RoleID", DbType.Int32, roleID);
            db.AddInParameter(command, "ProjectID", DbType.Int32, projectID);

            return db.ExecuteDataSet(command);
        }

        /// <summary>
        /// 获取流程角色对应可能的用户
        /// </summary>
        /// <param name="_regioncode"></param>
        /// <param name="_stationid"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public DataTable GetUsersOfRole(string _regioncode, string _stationid, int role)
        {
            string query = @"SELECT TPerson.PersonID
                              FROM (SELECT " + role.ToString() + @" roleid,'' code 
                                   UNION ALL
                                    SELECT " + role.ToString() + @",'" + _regioncode + @"' 
                                   UNION ALL
                                    SELECT " + role.ToString() + @", '" + _stationid + @"'
                                    ) AS TRole,
                                   (SELECT p.KeyId AS PersonID,
                                           r.RoleId,
                                           SUBSTRING(isnull(u.BusiUnitCode, ''), 5, 4) unitCode
                                      FROM dbo.Sys_UserRole r
                                      JOIN dbo.Sys_Person p
                                        ON r.PersonID = p.KeyId
                                      JOIN dbo.Sys_PersonBusiUnit pu
                                        ON p.KeyId = pu.PersonID
                                       AND pu.IsMaster = '1'
                                      JOIN dbo.Sys_BusiUnit u
                                        ON pu.BusiUnitID = u.KeyId) AS TPerson
                             WHERE TRole.roleid = TPerson.RoleId
                               AND TRole.code = TPerson.unitCode";
            return this.GetDataTable(query);
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public string DeleteRole(int roleId)
        {
            string countsql1 = "select count(*) from Sys_UserRole where RoleId = @RoleId";
            string countsql2 = "select count(*) from Sys_RoleModules where RoleId = @RoleId";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command1 = db.GetSqlStringCommand(countsql1);
            DbCommand command2 = db.GetSqlStringCommand(countsql2);
            db.AddInParameter(command1, "RoleId", DbType.Int32, roleId);
            db.AddInParameter(command2, "RoleId", DbType.Int32, roleId);

            int count1 = db.ExecuteScalar<int>(command1);
            int count2 = db.ExecuteScalar<int>(command2);

            if (count1 == 0 && count2 == 0)
            {
                string delsql = "delete from Sys_Roles where KeyId = @RoleId";
                DbCommand delcommand = db.GetSqlStringCommand(delsql);
                db.AddInParameter(delcommand, "RoleId", DbType.Int32, roleId);
                db.ExecuteNonQuery(delcommand);
                return string.Empty;
            }
            else
            {
                return "角色被其他用户或模块使用，不能删除!";
            }
        } 
        #endregion        

        #region V2
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public DataTable GetDataList_V2(string where)
        {
            string sql = @"SELECT a.KeyId,a.ProjID,a.RoleName,a.RoleCode,a.Status,a.Remark,a.CreatedBy,a.CreatedOn,a.ModifiedBy,a.ModifiedOn,	 
	                                    b.ProjName,(CASE a.Status WHEN 1 THEN '是' ELSE '否' END) AS StatusName 
                                    FROM dbo.Sys_Roles a 
                                    LEFT JOIN dbo.Sys_ProjInfo b ON b.KeyId =a.ProjID 
                                    WHERE b.Status =1 ";
            if (!string.IsNullOrWhiteSpace(where))
                sql += where;
            return GetDataTable(sql);
        }

        /// <summary>
        /// 获取角色功能模块
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public DataSet GetRoleModules_V2(int projectID, int roleID)
        {
            string query = @"WITH ModulesData AS (
	                        SELECT KeyId AS ModuleID, ModuleName, DisplayPrivilegeMask, NULL AS ParentID
			                        , 1 AS LevelCode
			                        , ROW_NUMBER() OVER(ORDER BY DisplayOrder) * POWER(10, 8) AS PathCode
		                        FROM dbo.Sys_Modules 
		                        WHERE ParentId IS NULL AND [Status] = 1 AND ProjID = @ProjectID
	                        UNION ALL
		                        SELECT m.KeyId AS ModuleID, m.ModuleName, m.DisplayPrivilegeMask, m.ParentId
				                        , p.LevelCode + 1 AS LevelCode
				                        , p.PathCode + ROW_NUMBER() OVER(ORDER BY m.DisplayOrder) * POWER(10, 8 - p.LevelCode * 2) AS PathCode
			                        FROM dbo.Sys_Modules m JOIN ModulesData p
				                        ON m.ParentId = p.ModuleID AND m.ProjID = @ProjectID
			                        WHERE m.[Status] = 1
                        )
                        SELECT m.ModuleID, m.ModuleName, ISNULL(m.ParentID, -1) AS ParentID

		                        , '{'+'''action'':''add'',''name'':''选择'',''checked'':'+
		                        CASE WHEN (DisplayPrivilegeMask & rm.PrivilegeMask) & 1  > 0 THEN 'true' ELSE 'false' END +'},'
		                        +'{'+'''action'':''create'',''name'':''新增'',''checked'':'+		                        
		                        CASE WHEN (DisplayPrivilegeMask & rm.PrivilegeMask) & 2  > 0 THEN 'true' ELSE 'false' END +'},'
		                        +'{'+'''action'':''update'',''name'':''修改'',''checked'':'+
		                        CASE WHEN (DisplayPrivilegeMask & rm.PrivilegeMask) & 4  > 0 THEN 'true' ELSE 'false' END +'},'
		                        +'{'+'''action'':''delete'',''name'':''删除'',''checked'':'+
		                        CASE WHEN (DisplayPrivilegeMask & rm.PrivilegeMask) & 8  > 0 THEN 'true' ELSE 'false' END +'},'
		                        +'{'+'''action'':''print'',''name'':''打印'',''checked'':'+
		                        CASE WHEN (DisplayPrivilegeMask & rm.PrivilegeMask) & 16 > 0 THEN 'true' ELSE 'false' END +'},'
		                        +'{'+'''action'':''export'',''name'':''导出'',''checked'':'+
		                        CASE WHEN (DisplayPrivilegeMask & rm.PrivilegeMask) & 32 > 0 THEN 'true' ELSE 'false' END +'},'
		                        +'{'+'''action'':''approve'',''name'':''审核'',''checked'':'+
		                        CASE WHEN (DisplayPrivilegeMask & rm.PrivilegeMask) & 64 > 0 THEN 'true' ELSE 'false' END +'}'
		                        AS functions
	                        FROM ModulesData m                                       
		                        LEFT JOIN dbo.Sys_RoleModules rm ON rm.ProjID = @ProjectID AND rm.RoleID = @RoleID AND m.ModuleID = rm.ModulesID
	                        ORDER BY PathCode ";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(query);
            db.AddInParameter(command, "ProjectID", DbType.Int32, projectID);
            db.AddInParameter(command, "RoleID", DbType.Int32, roleID);

            return db.ExecuteDataSet(command);
        } 
        #endregion
    }
}