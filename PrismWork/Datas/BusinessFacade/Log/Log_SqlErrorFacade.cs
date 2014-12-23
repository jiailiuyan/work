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
    /// Log_SqlError业务外观类
    /// 开发人员: 
    /// 生成日期: 2014年10月 27日 20:39
    ///</summary>
    public partial class Log_SqlErrorFacade
    {
	    /// <summary>
	    /// 更新Log_SqlError
	    /// </summary>
	    /// <param name="log_SqlError">实体类</param>
	    /// <param name="logEntity">日志类</param>
	    /// <returns></returns>
	    public string UpdateLog_SqlError(Log_SqlError log_SqlError,Log_Operate logEntity)
	    {
			string strResult = "";
			using (TransactionScope trans = new TransactionScope())
	        {
	             try
	             {
	                 this._log_SqlErrorDAO.UpdateLog_SqlError(log_SqlError);
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
	    /// 创建Log_SqlError
	   	/// </summary>
	   	/// <param name="log_SqlError">实体类</param>
	    /// <param name="logEntity">日志类</param>
	    /// <param name="strResult">错误信息</param>
	    /// <returns></returns>
	    public int InsertLog_SqlError(Log_SqlError log_SqlError,Log_Operate logEntity,ref string strResult)
	    {
			int intResult = 0;
			using (TransactionScope trans = new TransactionScope())
	        {
	             try
	             {
	                 this._log_SqlErrorDAO.InsertLog_SqlError(log_SqlError);
	                 Log_OperateFacade logFacade = new Log_OperateFacade();
	                 logEntity.OperateFunction = "新增_log_SqlError表ID为" + intResult.ToString() + "的数据";
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
	    /// 删除Log_SqlError
	    /// </summary>
	    /// <param name="Log_SqlErrorId">主码</param>
	    /// <param name="logEntity">日志类</param>
	    /// <returns></returns>
	    public string DeleteLog_SqlError(int keyId,Log_Operate logEntity)
	    {
			string strResult = "";
			using (TransactionScope trans = new TransactionScope())
			{
				try
				{
					this._log_SqlErrorDAO.DeleteLog_SqlError(keyId);
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
    }
}  
