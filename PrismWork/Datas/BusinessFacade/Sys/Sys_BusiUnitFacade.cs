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
    /// 业务单位信息业务外观类
    /// 开发人员: 
    /// 生成日期: 2014年10月 27日 21:07
    ///</summary>
    public partial class Sys_BusiUnitFacade
    {
	    /// <summary>
	    /// 更新Sys_BusiUnit
	    /// </summary>
	    /// <param name="sys_BusiUnit">实体类</param>
	    /// <param name="logEntity">日志类</param>
	    /// <returns></returns>
	    public string UpdateSys_BusiUnit(Sys_BusiUnit sys_BusiUnit,Log_Operate logEntity)
	    {
			string strResult = "";
			using (TransactionScope trans = new TransactionScope())
	        {
	             try
	             {
	                 this._sys_BusiUnitDAO.UpdateSys_BusiUnit(sys_BusiUnit);
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
	    /// 创建Sys_BusiUnit
	   	/// </summary>
	   	/// <param name="sys_BusiUnit">实体类</param>
	    /// <param name="logEntity">日志类</param>
	    /// <param name="strResult">错误信息</param>
	    /// <returns></returns>
	    public int InsertSys_BusiUnit(Sys_BusiUnit sys_BusiUnit,Log_Operate logEntity,ref string strResult)
	    {
			int intResult = 0;
			using (TransactionScope trans = new TransactionScope())
	        {
	             try
	             {
                     intResult = this._sys_BusiUnitDAO.InsertSys_BusiUnit(sys_BusiUnit);
	                 Log_OperateFacade logFacade = new Log_OperateFacade();
	                 logEntity.OperateFunction = "新增_sys_BusiUnit表ID为" + intResult.ToString() + "的数据";
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
	    /// 删除Sys_BusiUnit
	    /// </summary>
	    /// <param name="Sys_BusiUnitId">主码</param>
	    /// <param name="logEntity">日志类</param>
	    /// <returns></returns>
	    public string DeleteSys_BusiUnit(int keyId,Log_Operate logEntity)
	    {
			string strResult = "";
			using (TransactionScope trans = new TransactionScope())
			{
				try
				{
					this._sys_BusiUnitDAO.DeleteSys_BusiUnit(keyId);
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
        /// 获取一个单位下的单位信息（包含自己及下属单位）
        /// </summary>
        /// <param name="busiid"></param>
        /// <param name="strbusitype"></param>
        /// <returns></returns>
        public DataTable GetBusiUnitOwnAndChildren(int busiid, string strbusitype)
        {
            return this._sys_BusiUnitDAO.GetBusiUnitOwnAndChildren(busiid, strbusitype);
        }
        /// <summary>
        /// 获取一个单位包含本单位及下级单位信息
        /// </summary>
        /// <param name="busiid"></param>
        /// <param name="strbusitype"></param>
        /// <returns></returns>
        public string GetBusiUnitOwnAndChildrenToStr(int busiid, string strbusitype)
        {
            DataTable dt = this._sys_BusiUnitDAO.GetBusiUnitOwnAndChildren(busiid, strbusitype);
            string strReturn = string.Empty;
            foreach (DataRow dr in dt.Rows)
            {
                if (strReturn.Length == 0)
                {
                    strReturn = dr["BusiUnitId"].ToString();
                }
                else
                {
                    strReturn += ","+dr["BusiUnitId"].ToString();
                }
            }
            return strReturn;
        }

        /// <summary>
        /// 取唯一编码最大值
        /// </summary>
        /// <returns>唯一编码最大值</returns>
        public string GetMaxBusiUnitCode(int iStartValue, int iStep)
        {
            string strBusiUnitCode = "";
            int intTemp = _sys_BusiUnitDAO.GetMaxBusiUnitCode(iStartValue);
            strBusiUnitCode = (intTemp + iStep).ToString();
            return strBusiUnitCode;
        }

        /// <summary>
        /// 返回状态为true的所有非叶子节点单位信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetParentBusiUnitForQuery()
        {
            return _sys_BusiUnitDAO.GetParentBusiUnitForQuery();
        }

        /// <summary>
        /// 返回状态为true的所有单位信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetBusiUnit()
        {
            return _sys_BusiUnitDAO.GetBusiUnit();
        }

        /// <summary>
        /// 返回满足查询条件的单位信息
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable GetBusiUnitByWhere(string strWhere)
        {
            return _sys_BusiUnitDAO.GetBusiUnitByWhere(strWhere);
        }

        /// <summary>
        /// 根据父节点id获得所有子节点
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public DataTable GetBusiUnitByParent(string parentId)
        {
            return _sys_BusiUnitDAO.GetBusiUnitByParent(parentId);
        }

        /// <summary>
        /// 删除单位
        /// </summary>
        /// <param name="unitid"></param>
        /// <returns></returns>
        public string DeleteUnit(int unitid)
        {
            return _sys_BusiUnitDAO.DeleteUnit(unitid);
        }
        #endregion
    }
}  
