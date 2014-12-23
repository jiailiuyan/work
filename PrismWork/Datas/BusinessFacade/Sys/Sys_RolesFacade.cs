using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Transactions;
using Project.Common;
using Project.DataAccess;
using Project.Entities;

namespace Project.BusinessFacade 
{  
	/// <summary>
    /// 项目角色业务外观类
    /// 开发人员: 
    /// 生成日期: 2014年10月 27日 20:45
    ///</summary>
    public partial class Sys_RolesFacade
    {
	    /// <summary>
	    /// 更新Sys_Roles
	    /// </summary>
	    /// <param name="sys_Roles">实体类</param>
	    /// <param name="logEntity">日志类</param>
	    /// <returns></returns>
	    public string UpdateSys_Roles(Sys_Roles sys_Roles,Log_Operate logEntity)
	    {
			string strResult = "";
			using (TransactionScope trans = new TransactionScope())
	        {
	             try
	             {
	                 this._sys_RolesDAO.UpdateSys_Roles(sys_Roles);
	                 Log_OperateFacade logFacade = new Log_OperateFacade();
	                 int intLog = logFacade.CreateLog_Operate(logEntity);
	                 trans.Complete();
	             }
	             catch (Exception ex)
	             {
	                 strResult = ex.Message;
	             }
	         }
	         return strResult;
	    }
    
	    /// <summary>
	    /// 创建Sys_Roles
	   	/// </summary>
	   	/// <param name="sys_Roles">实体类</param>
	    /// <param name="logEntity">日志类</param>
	    /// <param name="strResult">错误信息</param>
	    /// <returns></returns>
	    public int InsertSys_Roles(Sys_Roles sys_Roles,Log_Operate logEntity,ref string strResult)
	    {
			int intResult = 0;
			using (TransactionScope trans = new TransactionScope())
	        {
	             try
	             {
	                 this._sys_RolesDAO.InsertSys_Roles(sys_Roles);
	                 Log_OperateFacade logFacade = new Log_OperateFacade();
	                 logEntity.OperateFunction = "新增_sys_Roles表ID为" + intResult.ToString() + "的数据";
	                 int intLog = logFacade.CreateLog_Operate(logEntity);
	                 trans.Complete();
	             }
	             catch (Exception ex)
	             {
	                 strResult = ex.Message;
	             }
	         }
	         return intResult;
	    }
    
	    /// <summary>
	    /// 删除Sys_Roles
	    /// </summary>
	    /// <param name="Sys_RolesId">主码</param>
	    /// <param name="logEntity">日志类</param>
	    /// <returns></returns>
	    public string DeleteSys_Roles(int keyId,Log_Operate logEntity)
	    {
			string strResult = "";
			using (TransactionScope trans = new TransactionScope())
			{
				try
				{
					this._sys_RolesDAO.DeleteSys_Roles(keyId);
					Log_OperateFacade logFacade = new Log_OperateFacade();
					int intLog = logFacade.CreateLog_Operate(logEntity);
					trans.Complete();
				}
				catch (Exception ex)
				{
					strResult = ex.Message;
				}
			}
			 return strResult;
	    }

        #region V1
        /// <summary>
        /// 自动获取RoleCode
        /// </summary>
        public string AutoRoleCode(string projid)
        {
            string strRoleCode = "";
            int iStartCode = 0;
            iStartCode = DataParser.ParseInt(_sys_RolesDAO.GetMaxRoleCode(projid));
            if (iStartCode == 0)
                strRoleCode = "1000";
            else
                strRoleCode = (iStartCode + 10).ToString();
            return strRoleCode;
        }

        /// <summary>
        /// 返回满足查询条件的角色基本信息数据表
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <returns>角色基本信息数据表</returns>
        public DataTable GetRolesData(string strWhere)
        {
            return this._sys_RolesDAO.GetRolesData(strWhere);
        }
        /// <summary>
        /// 获取角色功能模块
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public DataSet GetRoleModules(int projectID, int roleID)
        {
            return _sys_RolesDAO.GetRoleModules(projectID, roleID);
        }

        /// <summary>
        /// 保存角色、角色功能、角色用户数据
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="project">所属系统</param>
        /// <param name="role">角色</param>
        /// <param name="roleModules">角色功能</param>
        /// <param name="roleUsers">角色用户</param>
        public void SaveRole(int user, int project, Sys_Roles role, Sys_RoleModules[] roleModules, Sys_UserRole[] roleUsers)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (role.KeyId == 0)
                {
                    role.ProjID = project;
                    role.RoleCode = AutoRoleCode(project.ToString());
                    role.CreatedBy = user;
                    role.CreatedOn = DateTime.Now;
                    role.KeyId = _sys_RolesDAO.InsertSys_Roles(role);
                }
                else
                {
                    role.ModifiedBy = user;
                    role.ModifiedOn = DateTime.Now;
                    _sys_RolesDAO.UpdateSys_Roles(role);
                }

                var roleModulesDao = new Sys_RoleModulesDAO();
                roleModulesDao.SaveRoleModules(roleModules, role.KeyId, user, project);

                var roleUsersDao = new Sys_UserRoleDAO();
                roleUsersDao.SaveRoleUserTable(roleUsers, role.KeyId, user, project);

                scope.Complete();
            }
        }

        /// <summary>
        /// 获取角色用户
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public DataSet GetRoleUsers(int projectID, int roleID)
        {
            return _sys_RolesDAO.GetRoleUsers(projectID, roleID);
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
            return _sys_RolesDAO.GetUsersOfRole(_regioncode, _stationid, role);
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public string DeleteRole(int roleId)
        {
            return _sys_RolesDAO.DeleteRole(roleId);
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
            return _sys_RolesDAO.GetDataList_V2(where);
        }

        /// <summary>
        /// 获取角色功能模块
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public DataSet GetRoleModules_V2(int projectID, int roleID)
        {
            return _sys_RolesDAO.GetRoleModules_V2(projectID, roleID);
        } 
        #endregion
    }
}  
