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
    /// Sys_PersonDeskTopConfig业务外观类
    /// 开发人员: 
    /// 生成日期: 2014年11月 20日 18:49
    ///</summary>
    public partial class Sys_PersonDeskTopConfigFacade
    {
	    /// <summary>
	    /// 更新Sys_PersonDeskTopConfig
	    /// </summary>
	    /// <param name="sys_PersonDeskTopConfig">实体类</param>
	    /// <param name="logEntity">日志类</param>
	    /// <returns></returns>
	    public string UpdateSys_PersonDeskTopConfig(Sys_PersonDeskTopConfig sys_PersonDeskTopConfig,Log_Operate logEntity)
	    {
			string strResult = "";
			using (TransactionScope trans = new TransactionScope())
	        {
	             try
	             {
	                 this._sys_PersonDeskTopConfigDAO.UpdateSys_PersonDeskTopConfig(sys_PersonDeskTopConfig);
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
	    /// 创建Sys_PersonDeskTopConfig
	   	/// </summary>
	   	/// <param name="sys_PersonDeskTopConfig">实体类</param>
	    /// <param name="logEntity">日志类</param>
	    /// <param name="strResult">错误信息</param>
	    /// <returns></returns>
	    public int InsertSys_PersonDeskTopConfig(Sys_PersonDeskTopConfig sys_PersonDeskTopConfig,Log_Operate logEntity,ref string strResult)
	    {
			int intResult = 0;
			using (TransactionScope trans = new TransactionScope())
	        {
	             try
	             {
	                 this._sys_PersonDeskTopConfigDAO.InsertSys_PersonDeskTopConfig(sys_PersonDeskTopConfig);
	                 Log_OperateFacade logFacade = new Log_OperateFacade();
	                 logEntity.OperateFunction = "新增_sys_PersonDeskTopConfig表ID为" + intResult.ToString() + "的数据";
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
	    /// 删除Sys_PersonDeskTopConfig
	    /// </summary>
	    /// <param name="Sys_PersonDeskTopConfigId">主码</param>
	    /// <param name="logEntity">日志类</param>
	    /// <returns></returns>
	    public string DeleteSys_PersonDeskTopConfig(int keyId,Log_Operate logEntity)
	    {
			string strResult = "";
			using (TransactionScope trans = new TransactionScope())
			{
				try
				{
					this._sys_PersonDeskTopConfigDAO.DeleteSys_PersonDeskTopConfig(keyId);
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
        /// 更新桌面设置
        /// </summary>
        /// <param name="personId">用户ID</param>
        /// <param name="name">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public string UpdateSys_PersonDeskTopConfig(int personId, string name, string value)
        {
            string strResult = "";
            try
            {
                _sys_PersonDeskTopConfigDAO.UpdateSys_PersonDeskTopConfig(personId, name, value);
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }
            return strResult;
        }

        /// <summary>
        /// 保存桌面设置，如果存在就更新不存在就创建
        /// </summary>
        /// <param name="personId">用户ID</param>
        /// <param name="name">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public String SaveSys_PersonDeskTopConfig(int personId, string name, string value)
        {
            string strResult = "";
            try
            {
                if (this._sys_PersonDeskTopConfigDAO.CheckPersonConfigName(personId, name) == 0)
                {
                    Sys_PersonDeskTopConfig config = new Sys_PersonDeskTopConfig()
                    {
                        PersonId = personId,
                        ConfigName = name,
                        ConfigValue = value,
                        CreateDate = DateTime.Now,
                        ModifiedDate = DateTime.Now
                    };
                    this.CreateSys_PersonDeskTopConfig(config);
                }
                else
                {
                    this._sys_PersonDeskTopConfigDAO.UpdateSys_PersonDeskTopConfig(personId, name, value);
                }
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }
            return strResult;
        }

        /// <summary>
        /// 获取用户的所有桌面设置
        /// </summary>
        /// <param name="person">用户ID</param>
        /// <returns></returns>
        public Dictionary<string,string> FindSys_PersonDeskTopConfigByPerson(int person)
        {
            Dictionary<string, string> dicts = new Dictionary<string, string>();
            QueryParameter parameter = new QueryParameter();
            parameter.AddWhereExpr(SimpleExpression.Equal("PersonId",person));
            IList<Sys_PersonDeskTopConfig> result = this._sys_PersonDeskTopConfigDAO.GetSys_PersonDeskTopConfigs(parameter);
            if (result != null && result.Count > 0)
            {

                foreach (Sys_PersonDeskTopConfig item in result)
                {
                    dicts.Add(item.ConfigName, item.ConfigValue);
                }
            }
            return dicts;
        }
    }
}  
