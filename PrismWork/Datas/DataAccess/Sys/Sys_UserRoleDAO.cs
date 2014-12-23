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
    /// Sys_UserRoleDAO 用户角色对应关系数据存取类
    /// 开发人员:
    /// 开发日期: 2014年10月 27日
    /// </summary>
    public partial class Sys_UserRoleDAO
    {
        #region V1
        /// <summary>
        /// 获取角色用户信息
        /// </summary>
        /// <param name="RoleId">角色Id</param>
        /// <param name="PersonName">用户名称</param>
        /// <param name="ProjID">项目Id</param>
        /// <param name="BusiUnitId">单位代码</param>
        /// <returns></returns>
        public DataTable GetRolesUserTable(int RoleId, string PersonName, int ProjID, string BusiUnitId)
        {
            string sqlstr = string.Format("p_GetRoleUser {0},'{1}',{2},'{3}'", RoleId.ToString(), PersonName, ProjID.ToString(), BusiUnitId);
            return GetDataTable(sqlstr);
        }
        /// <summary>
        /// 根据项目内码、角色内码删除角色用户信息
        /// </summary>
        /// <param name="roleid"></param>
        /// <param name="projid"></param>
        /// <returns></returns>
        public string DeleteRolesUser(string roleid,string projid)
        {
            string strResult = string.Empty;
            string sql = "delete Sys_UserRole where projid=" + projid + " and roleid=" + roleid;
            strResult=ExecuteSql(sql).ToString();
            return strResult;
        }
        /// <summary>
        /// 保存角色用户数据
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="RoleId"></param>
        /// <param name="SystemUserId"></param>
        /// <param name="ProjID"></param>
        /// <returns></returns>
        public string SaveRoleUserTable(DataTable dt, int RoleId, int SystemUserId, int ProjID)
        {
            string strResult = string.Empty;
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    string del_sqlstr = "delete Sys_UserRole where projid=" + ProjID + " and roleid=" + RoleId;
                    this.ExecuteSql(del_sqlstr);
                    foreach (DataRow dr in dt.Rows)
                    {
                        if ((bool)dr["Status"])
                        {
                            string sql = "insert into Sys_UserRole(projid,personid,roleid,status,createdby,createdon)";
                            sql += " values(" + ProjID.ToString() + "," + dr["PersonID"].ToString() + "," + RoleId.ToString() + ",1," + SystemUserId.ToString() + ",getdate())";
                            this.ExecuteSql(sql);
                        }
                    }
                    scope.Complete();
                    strResult = "";
                }
                catch (Exception ex)
                {
                    strResult = ex.Message;
                    throw (ex);
                }
            }
            return strResult;
        }
        /// <summary>
        /// 根据人员ID获取人员角色信息
        /// </summary>
        /// <param name="PersonID">人员id</param>
        /// <returns></returns>
        public DataTable GetRoleByPersonId(string PersonId)
        {
            string sql = "SELECT * FROM dbo.Sys_UserRole WHERE PersonID=" + PersonId;
            return GetDataTable(sql);
        }

        public void SaveRoleUserTable(Sys_UserRole[] roleUsers, int roleID, int user, int project)
        {
            string query = "DELETE Sys_UserRole WHERE RoleID = @RoleID AND ProjID = @Project";
            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(query);
            db.AddInParameter(command, "RoleID", DbType.String, roleID);
            db.AddInParameter(command, "Project", DbType.String, project);
            db.ExecuteNonQuery(command);
            if (roleUsers != null) {
                Array.ForEach(roleUsers, roleUser => {
                    roleUser.CreatedBy = user;
                    roleUser.CreatedOn = DateTime.Now;
                    roleUser.RoleId = roleID;
                    roleUser.ProjID = project;
                    roleUser.Status = true;

                    roleUser.KeyId = InsertSys_UserRole(roleUser);
                });
            }
        }
        #endregion
    }
}