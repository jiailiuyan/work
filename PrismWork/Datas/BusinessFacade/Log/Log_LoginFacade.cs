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
    /// Log_Login业务外观类
    /// 开发人员: 
    /// 生成日期: 2014年10月 28日 14:36
    ///</summary>
    public partial class Log_LoginFacade
    {
	    /// <summary>
	    /// 更新Log_Login
	    /// </summary>
	    /// <param name="log_Login">实体类</param>
	    /// <param name="logEntity">日志类</param>
	    /// <returns></returns>
	    public string UpdateLog_Login(Log_Login log_Login,Log_Operate logEntity)
	    {
			string strResult = "";
			using (TransactionScope trans = new TransactionScope())
	        {
	             try
	             {
	                 this._log_LoginDAO.UpdateLog_Login(log_Login);
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
	    /// 创建Log_Login
	   	/// </summary>
	   	/// <param name="log_Login">实体类</param>
	    /// <param name="logEntity">日志类</param>
	    /// <param name="strResult">错误信息</param>
	    /// <returns></returns>
	    public int InsertLog_Login(Log_Login log_Login,Log_Operate logEntity,ref string strResult)
	    {
			int intResult = 0;
			using (TransactionScope trans = new TransactionScope())
	        {
	             try
	             {
	                 this._log_LoginDAO.InsertLog_Login(log_Login);
	                 Log_OperateFacade logFacade = new Log_OperateFacade();
	                 logEntity.OperateFunction = "新增_log_Login表ID为" + intResult.ToString() + "的数据";
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
	    /// 删除Log_Login
	    /// </summary>
	    /// <param name="Log_LoginId">主码</param>
	    /// <param name="logEntity">日志类</param>
	    /// <returns></returns>
	    public string DeleteLog_Login(int keyId,Log_Operate logEntity)
	    {
			string strResult = "";
			using (TransactionScope trans = new TransactionScope())
			{
				try
				{
					this._log_LoginDAO.DeleteLog_Login(keyId);
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
        /// 获取用户最近登录信息
        /// </summary>
        /// <param name="personid"></param>
        /// <returns></returns>
        public int GetUserLoginLogTop(int personid)
        {
            return _log_LoginDAO.GetUserLoginLogTop(personid);
        }

        /// <summary>
        /// 更新退出时间
        /// </summary>
        /// <param name="keyid"></param>
        public void UpdateLogoutTime(int keyid)
        {
            _log_LoginDAO.UpdateLogoutTime(keyid);
        }
    }
}  
