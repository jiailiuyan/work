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
    /// 用户角色对应关系业务外观类
    /// 开发人员: 
    /// 生成日期: 2014年10月 27日 21:08
    ///</summary>
    public partial class Sys_UserRoleFacade
    {
	    /// <summary>
	    /// 更新Sys_UserRole
	    /// </summary>
	    /// <param name="sys_UserRole">实体类</param>
	    /// <param name="logEntity">日志类</param>
	    /// <returns></returns>
	    public string UpdateSys_UserRole(Sys_UserRole sys_UserRole,Log_Operate logEntity)
	    {
			string strResult = "";
			using (TransactionScope trans = new TransactionScope())
	        {
	             try
	             {
	                 this._sys_UserRoleDAO.UpdateSys_UserRole(sys_UserRole);
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
	    /// 创建Sys_UserRole
	   	/// </summary>
	   	/// <param name="sys_UserRole">实体类</param>
	    /// <param name="logEntity">日志类</param>
	    /// <param name="strResult">错误信息</param>
	    /// <returns></returns>
	    public int InsertSys_UserRole(Sys_UserRole sys_UserRole,Log_Operate logEntity,ref string strResult)
	    {
			int intResult = 0;
			using (TransactionScope trans = new TransactionScope())
	        {
	             try
	             {
	                 this._sys_UserRoleDAO.InsertSys_UserRole(sys_UserRole);
	                 Log_OperateFacade logFacade = new Log_OperateFacade();
	                 logEntity.OperateFunction = "新增_sys_UserRole表ID为" + intResult.ToString() + "的数据";
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
	    /// 删除Sys_UserRole
	    /// </summary>
	    /// <param name="Sys_UserRoleId">主码</param>
	    /// <param name="logEntity">日志类</param>
	    /// <returns></returns>
	    public string DeleteSys_UserRole(int keyId,Log_Operate logEntity)
	    {
			string strResult = "";
			using (TransactionScope trans = new TransactionScope())
			{
				try
				{
					this._sys_UserRoleDAO.DeleteSys_UserRole(keyId);
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
        Sys_BusiUnitFacade busiUnitFacade = new Sys_BusiUnitFacade();
        /// <summary>
        /// 获取角色用户信息
        /// </summary>
        /// <param name="RoleId">角色Id</param>
        /// <param name="PersonName">用户名称</param>
        /// <param name="ProjID">项目Id</param>
        /// <param name="BusiUnitId">单位代码</param>
        /// <returns></returns>
        public DataTable GetRolesUserTable(int RoleId, string PersonName, int ProjID, int BusiUnitId)
        {
            string orgaList = BusiUnitId != 0 ? BusiUnitId.ToString() : "";
            string orgaListChild = new Sys_BusiUnitFacade().GetBusiUnitOwnAndChildrenToStr(BusiUnitId, "");
            if (orgaListChild != "")
                orgaList += "," + orgaListChild;

            return this._sys_UserRoleDAO.GetRolesUserTable(RoleId, PersonName, ProjID, orgaList);
        }
        /// <summary>
        /// 保存角色用户信息
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <param name="RoleId">角色Id</param>
        /// <param name="SystemUserId">用户Id</param>
        /// <param name="ProjID">项目Id</param>
        public string SaveRoleUserTable(DataTable dt, int RoleId, int SystemUserId, int ProjID)
        {
            return this._sys_UserRoleDAO.SaveRoleUserTable(dt, RoleId, SystemUserId, ProjID);
        }

        /// <summary>
        /// 根据人员ID获取人员角色信息
        /// </summary>
        /// <param name="PersonID">人员id</param>
        /// <returns></returns>
        public DataTable GetRoleByPersonId(string PersonId)
        {
            return _sys_UserRoleDAO.GetRoleByPersonId(PersonId);
        }
        #endregion
    }
}  
