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
    /// 项目基本信息业务外观类
    /// 开发人员: 
    /// 生成日期: 2014年10月 27日 20:33
    ///</summary>
    public partial class Sys_ProjInfoFacade
    {
	    /// <summary>
	    /// 更新Sys_ProjInfo
	    /// </summary>
	    /// <param name="sys_ProjInfo">实体类</param>
	    /// <param name="logEntity">日志类</param>
	    /// <returns></returns>
	    public string UpdateSys_ProjInfo(Sys_ProjInfo sys_ProjInfo,Log_Operate logEntity)
	    {
			string strResult = "";
			using (TransactionScope trans = new TransactionScope())
	        {
	             try
	             {
	                 this._sys_ProjInfoDAO.UpdateSys_ProjInfo(sys_ProjInfo);
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
	    /// 创建Sys_ProjInfo
	   	/// </summary>
	   	/// <param name="sys_ProjInfo">实体类</param>
	    /// <param name="logEntity">日志类</param>
	    /// <param name="strResult">错误信息</param>
	    /// <returns></returns>
	    public int InsertSys_ProjInfo(Sys_ProjInfo sys_ProjInfo,Log_Operate logEntity,ref string strResult)
	    {
			int intResult = 0;
			using (TransactionScope trans = new TransactionScope())
	        {
	             try
	             {
	                 this._sys_ProjInfoDAO.InsertSys_ProjInfo(sys_ProjInfo);
	                 Log_OperateFacade logFacade = new Log_OperateFacade();
	                 logEntity.OperateFunction = "新增_sys_ProjInfo表ID为" + intResult.ToString() + "的数据";
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
	    /// 删除Sys_ProjInfo
	    /// </summary>
	    /// <param name="Sys_ProjInfoId">主码</param>
	    /// <param name="logEntity">日志类</param>
	    /// <returns></returns>
	    public string DeleteSys_ProjInfo(int keyId,Log_Operate logEntity)
	    {
			string strResult = "";
			using (TransactionScope trans = new TransactionScope())
			{
				try
				{
					this._sys_ProjInfoDAO.DeleteSys_ProjInfo(keyId);
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
        /// 根据项目代码获取项目的全称
        /// </summary>
        /// <param name="projcode"></param>
        /// <returns></returns>
        public string GetProjNameByCode(string projcode)
        {
            return this._sys_ProjInfoDAO.GetProjNameByCode(projcode);
        }

        /// <summary>
        /// 获取一个项目的项目内码
        /// </summary>
        /// <param name="projcode"></param>
        /// <returns></returns>
        public string GetProjIDByProjCode(string projcode)
        {
            return _sys_ProjInfoDAO.GetProjIDByProjCode(projcode);
        }

        /// <summary>
        /// 返回满足查询条件的项目基本信息数据表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>
        /// 项目基本信息数据表</returns>
        public DataTable GetProjInfoTable(QueryParameter param)
        {
            return this._sys_ProjInfoDAO.GetProjInfoTable(param);
        }
    }
}  
