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
    /// 一个人可能属于多个单位业务外观类
    /// 开发人员: 
    /// 生成日期: 2014年10月 27日 21:08
    ///</summary>
    public partial class Sys_PersonBusiUnitFacade
    {
	    /// <summary>
	    /// 更新Sys_PersonBusiUnit
	    /// </summary>
	    /// <param name="sys_PersonBusiUnit">实体类</param>
	    /// <param name="logEntity">日志类</param>
	    /// <returns></returns>
	    public string UpdateSys_PersonBusiUnit(Sys_PersonBusiUnit sys_PersonBusiUnit,Log_Operate logEntity)
	    {
			string strResult = "";
			using (TransactionScope trans = new TransactionScope())
	        {
	             try
	             {
	                 this._sys_PersonBusiUnitDAO.UpdateSys_PersonBusiUnit(sys_PersonBusiUnit);
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
	    /// 创建Sys_PersonBusiUnit
	   	/// </summary>
	   	/// <param name="sys_PersonBusiUnit">实体类</param>
	    /// <param name="logEntity">日志类</param>
	    /// <param name="strResult">错误信息</param>
	    /// <returns></returns>
	    public int InsertSys_PersonBusiUnit(Sys_PersonBusiUnit sys_PersonBusiUnit,Log_Operate logEntity,ref string strResult)
	    {
			int intResult = 0;
			using (TransactionScope trans = new TransactionScope())
	        {
	             try
	             {
	                 this._sys_PersonBusiUnitDAO.InsertSys_PersonBusiUnit(sys_PersonBusiUnit);
	                 Log_OperateFacade logFacade = new Log_OperateFacade();
	                 logEntity.OperateFunction = "新增_sys_PersonBusiUnit表ID为" + intResult.ToString() + "的数据";
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
	    /// 删除Sys_PersonBusiUnit
	    /// </summary>
	    /// <param name="Sys_PersonBusiUnitId">主码</param>
	    /// <param name="logEntity">日志类</param>
	    /// <returns></returns>
	    public string DeleteSys_PersonBusiUnit(int keyId,Log_Operate logEntity)
	    {
			string strResult = "";
			using (TransactionScope trans = new TransactionScope())
			{
				try
				{
					this._sys_PersonBusiUnitDAO.DeleteSys_PersonBusiUnit(keyId);
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
        /// 获取数据
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public DataTable GetDataList(string where)
        {
            return _sys_PersonBusiUnitDAO.GetDataList(where);
        }
    }
}  
