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
    /// 项目模块业务外观类
    /// 开发人员: 
    /// 生成日期: 2014年10月 27日 21:50
    ///</summary>
    public partial class Sys_ModulesFacade
    {
	    /// <summary>
	    /// 更新Sys_Modules
	    /// </summary>
	    /// <param name="sys_Modules">实体类</param>
	    /// <param name="logEntity">日志类</param>
	    /// <returns></returns>
	    public string UpdateSys_Modules(Sys_Modules sys_Modules,Log_Operate logEntity)
	    {
			string strResult = "";
			using (TransactionScope trans = new TransactionScope())
	        {
	             try
	             {
	                 this._sys_ModulesDAO.UpdateSys_Modules(sys_Modules);
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
	    /// 创建Sys_Modules
	   	/// </summary>
	   	/// <param name="sys_Modules">实体类</param>
	    /// <param name="logEntity">日志类</param>
	    /// <param name="strResult">错误信息</param>
	    /// <returns></returns>
	    public int InsertSys_Modules(Sys_Modules sys_Modules,Log_Operate logEntity,ref string strResult)
	    {
			int intResult = 0;
			using (TransactionScope trans = new TransactionScope())
	        {
	             try
	             {
                     intResult = this._sys_ModulesDAO.InsertSys_Modules(sys_Modules);
	                 Log_OperateFacade logFacade = new Log_OperateFacade();
	                 logEntity.OperateFunction = "新增_sys_Modules表ID为" + intResult.ToString() + "的数据";
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
	    /// 删除Sys_Modules
	    /// </summary>
	    /// <param name="Sys_ModulesId">主码</param>
	    /// <param name="logEntity">日志类</param>
	    /// <returns></returns>
	    public string DeleteSys_Modules(int keyId,Log_Operate logEntity)
	    {
			string strResult = "";
			using (TransactionScope trans = new TransactionScope())
			{
				try
				{
					this._sys_ModulesDAO.DeleteSys_Modules(keyId);
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

        public int? GetUserModulePrivilege(string moduleEntry, int userID, int projectID)
        {
            return _sys_ModulesDAO.GetUserModulePrivilege(userID, projectID, moduleEntry);
        }

        public int? GetUserModuleEntry(string moduleEntry, int userID, int projectID)
        {
            return _sys_ModulesDAO.GetUserModuleEntry(userID, projectID, moduleEntry);
        }

        public DataTable GetParentModulesData(int projectID, string logsystemname)
        {
            return _sys_ModulesDAO.GetParentModulesData(projectID, logsystemname);
        }

        /// <summary>
        /// 返回满足查询条件的实体列表
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <returns>实体列表</returns>
        public DataTable GetModulesTable(string strWhere)
        {
            return this._sys_ModulesDAO.GetModulesTable(strWhere);
        }

        /// <summary>
        /// 删除模块
        /// </summary>
        /// <param name="modulesid"></param>
        /// <returns></returns>
        public string DeleteSingleModules(int modulesid)
        {
            return _sys_ModulesDAO.DeleteSingleModules(modulesid);
        }

        /// <summary>
        /// 返回满足查询条件的项目模块数据表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>
        /// 项目模块数据表</returns>
        public DataTable GetModulesTable(QueryParameter param)
        {
            return this._sys_ModulesDAO.GetModulesTable(param);
        }
    }
}  
