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
    /// Sys_PersonDeskTopIconSet业务外观类
    /// 开发人员: 
    /// 生成日期: 2014年11月 20日 18:47
    ///</summary>
    public partial class Sys_PersonDeskTopIconSetFacade
    {
	    /// <summary>
	    /// 更新Sys_PersonDeskTopIconSet
	    /// </summary>
	    /// <param name="sys_PersonDeskTopIconSet">实体类</param>
	    /// <param name="logEntity">日志类</param>
	    /// <returns></returns>
	    public string UpdateSys_PersonDeskTopIconSet(Sys_PersonDeskTopIconSet sys_PersonDeskTopIconSet,Log_Operate logEntity)
	    {
			string strResult = "";
			using (TransactionScope trans = new TransactionScope())
	        {
	             try
	             {
	                 this._sys_PersonDeskTopIconSetDAO.UpdateSys_PersonDeskTopIconSet(sys_PersonDeskTopIconSet);
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
	    /// 创建Sys_PersonDeskTopIconSet
	   	/// </summary>
	   	/// <param name="sys_PersonDeskTopIconSet">实体类</param>
	    /// <param name="logEntity">日志类</param>
	    /// <param name="strResult">错误信息</param>
	    /// <returns></returns>
	    public int InsertSys_PersonDeskTopIconSet(Sys_PersonDeskTopIconSet sys_PersonDeskTopIconSet,Log_Operate logEntity,ref string strResult)
	    {
			int intResult = 0;
			using (TransactionScope trans = new TransactionScope())
	        {
	             try
	             {
	                 this._sys_PersonDeskTopIconSetDAO.InsertSys_PersonDeskTopIconSet(sys_PersonDeskTopIconSet);
	                 Log_OperateFacade logFacade = new Log_OperateFacade();
	                 logEntity.OperateFunction = "新增_sys_PersonDeskTopIconSet表ID为" + intResult.ToString() + "的数据";
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
	    /// 删除Sys_PersonDeskTopIconSet
	    /// </summary>
	    /// <param name="Sys_PersonDeskTopIconSetId">主码</param>
	    /// <param name="logEntity">日志类</param>
	    /// <returns></returns>
	    public string DeleteSys_PersonDeskTopIconSet(int keyId,Log_Operate logEntity)
	    {
			string strResult = "";
			using (TransactionScope trans = new TransactionScope())
			{
				try
				{
					this._sys_PersonDeskTopIconSetDAO.DeleteSys_PersonDeskTopIconSet(keyId);
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
        /// 删除PersonDeskTopIconSet记录
        /// </summary>
        /// <param name="moduleId">ModuleId</param> /// <param name="personId">PersonId</param> 
        /// <returns>受影响的记录数</returns>
        public int DeletePersonDeskTopIconSet(int moduleId, int personId)
        {
            return this._sys_PersonDeskTopIconSetDAO.DeletePersonDeskTopIconSet(moduleId, personId);
        } 
    }
}  
