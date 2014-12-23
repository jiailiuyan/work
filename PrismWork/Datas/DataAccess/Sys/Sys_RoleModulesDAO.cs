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
    /// Sys_RoleModulesDAO 角色功能对应表数据存取类
    /// 开发人员:
    /// 开发日期: 2014年10月 27日
    /// </summary>
    public partial class Sys_RoleModulesDAO
    {
        #region V1
        /// <summary>
        /// 返回模块权限
        /// </summary>
        /// <param name="role_id"></param>
        /// <param name="parent_role_id"></param>
        /// <param name="module_id"></param>
        /// <param name="ProjID"></param>
        /// <returns></returns>
        public DataTable GetRoleModulesTable(int role_id, int parent_role_id, int module_id, int ProjID)
        {
            string sqlstr = string.Format("p_GetRoleModules {0},{1},{2},{3} ", role_id.ToString(), parent_role_id.ToString(), module_id.ToString(), ProjID.ToString());
            return GetDataTable(sqlstr);
        }
        /// <summary>
        /// 保存角色拥有的模块权限
        /// </summary>
        /// <param name="mod_dt"></param>
        /// <param name="role_id"></param>
        /// <param name="user_id"></param>
        /// <param name="Proj_id"></param>
        public void SaveRoleModules(DataTable mod_dt, string role_id, string user_id, int Proj_id)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    foreach (DataRow mod_row in mod_dt.Rows)
                    {
                        if (mod_row["ModuleId"] + "" != "")
                        {
                            string module_id = mod_row["ModuleId"] + "";
                            string del_sqlstr = string.Format(" delete Sys_RoleModules where ModulesID in (select KeyId from Sys_Modules where ParentId = {0} or KeyId = {0}) and RoleId = {1} ", module_id, role_id);

                            ExecuteSql(del_sqlstr);

                            string sqlstr = "insert Sys_RoleModules (ProjID,RoleId, ModulesID, PrivilegeMask, Status, CreatedBy, CreatedOn) "
                                        + " select {4},{0} as RoleId,KeyId,{1}, 1, 1,'{3}' from Sys_Modules where ProjID = {4} and (KeyId = {5} or ParentId = {5}) ";

                            string add_sqlstr = string.Format(sqlstr, role_id, mod_row["PrivilegeMask"] + "", user_id, DateTime.Now.ToString(), Proj_id.ToString(), module_id);
                            ExecuteSql(add_sqlstr);
                        }
                    }
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
        }

        public void SaveRoleModules(Sys_RoleModules[] roleModules, int roleID, int user, int project)
        {
            string query = "DELETE Sys_RoleModules WHERE RoleID = @RoleID AND ProjID = @Project";
            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(query);
            db.AddInParameter(command, "RoleID", DbType.String, roleID);
            db.AddInParameter(command, "Project", DbType.String, project);
            db.ExecuteNonQuery(command);
            if (roleModules != null) {
                Array.ForEach(roleModules, roleModule => {
                    roleModule.CreatedBy = user;
                    roleModule.CreatedOn = DateTime.Now;
                    roleModule.RoleId = roleID;
                    roleModule.ProjID = project;
                    roleModule.Status = true;

                    roleModule.KeyId = InsertSys_RoleModules(roleModule);
                });
            }
        }
        #endregion
    }
}