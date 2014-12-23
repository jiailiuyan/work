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
    /// 业务单位类型,该表不做显示维护业务外观类
    /// 开发人员: 
    /// 生成日期: 2014年10月 27日 21:07
    ///</summary>
    public partial class Sys_BusiUnitTypeFacade
    {
	    /// <summary>
	    /// 更新Sys_BusiUnitType
	    /// </summary>
	    /// <param name="sys_BusiUnitType">实体类</param>
	    /// <param name="logEntity">日志类</param>
	    /// <returns></returns>
	    public string UpdateSys_BusiUnitType(Sys_BusiUnitType sys_BusiUnitType,Log_Operate logEntity)
	    {
			string strResult = "";
			using (TransactionScope trans = new TransactionScope())
	        {
	             try
	             {
	                 this._sys_BusiUnitTypeDAO.UpdateSys_BusiUnitType(sys_BusiUnitType);
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
	    /// 创建Sys_BusiUnitType
	   	/// </summary>
	   	/// <param name="sys_BusiUnitType">实体类</param>
	    /// <param name="logEntity">日志类</param>
	    /// <param name="strResult">错误信息</param>
	    /// <returns></returns>
	    public int InsertSys_BusiUnitType(Sys_BusiUnitType sys_BusiUnitType,Log_Operate logEntity,ref string strResult)
	    {
			int intResult = 0;
			using (TransactionScope trans = new TransactionScope())
	        {
	             try
	             {
	                 this._sys_BusiUnitTypeDAO.InsertSys_BusiUnitType(sys_BusiUnitType);
	                 Log_OperateFacade logFacade = new Log_OperateFacade();
	                 logEntity.OperateFunction = "新增_sys_BusiUnitType表ID为" + intResult.ToString() + "的数据";
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
	    /// 删除Sys_BusiUnitType
	    /// </summary>
	    /// <param name="Sys_BusiUnitTypeId">主码</param>
	    /// <param name="logEntity">日志类</param>
	    /// <returns></returns>
	    public string DeleteSys_BusiUnitType(int keyId,Log_Operate logEntity)
	    {
			string strResult = "";
			using (TransactionScope trans = new TransactionScope())
			{
				try
				{
					this._sys_BusiUnitTypeDAO.DeleteSys_BusiUnitType(keyId);
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
        /// 获得业务单位类型的表的全部数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetBusiUnitType()
        {
            return _sys_BusiUnitTypeDAO.GetBusiUnitType();
        }
        /// <summary>
        /// 根据条件查询业务类型表的数据
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable GetBusiUnitType(string strWhere)
        {
            return _sys_BusiUnitTypeDAO.GetBusiUnitType(strWhere);
        }

        /// <summary>
        /// 根据条件获取所有菜单
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <returns></returns>
        public DataTable GetAllBusiUnitType(string strWhere)
        {
            return _sys_BusiUnitTypeDAO.GetAllBusiUnitType(strWhere);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetRegion()
        {
            return _sys_BusiUnitTypeDAO.GetRegion();
        }
    }
}  
