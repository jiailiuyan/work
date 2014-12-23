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
    /// 人员基本信息业务外观类
    /// 开发人员: 
    /// 生成日期: 2014年10月 27日 23:20
    ///</summary>
    public partial class Sys_PersonFacade
    {
	    /// <summary>
	    /// 更新Sys_Person
	    /// </summary>
	    /// <param name="sys_Person">实体类</param>
	    /// <param name="logEntity">日志类</param>
	    /// <returns></returns>
	    public string UpdateSys_Person(Sys_Person sys_Person,Log_Operate logEntity)
	    {
			string strResult = "";
			using (TransactionScope trans = new TransactionScope())
	        {
	             try
	             {
	                 this._sys_PersonDAO.UpdateSys_Person(sys_Person);
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
	    /// 创建Sys_Person
	   	/// </summary>
	   	/// <param name="sys_Person">实体类</param>
	    /// <param name="logEntity">日志类</param>
	    /// <param name="strResult">错误信息</param>
	    /// <returns></returns>
	    public int InsertSys_Person(Sys_Person sys_Person,Log_Operate logEntity,ref string strResult)
	    {
			int intResult = 0;
			using (TransactionScope trans = new TransactionScope())
	        {
	             try
	             {
                     intResult = this._sys_PersonDAO.InsertSys_Person(sys_Person);
	                 Log_OperateFacade logFacade = new Log_OperateFacade();
	                 logEntity.OperateFunction = "新增_sys_Person表ID为" + intResult.ToString() + "的数据";
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
	    /// 删除Sys_Person
	    /// </summary>
	    /// <param name="Sys_PersonId">主码</param>
	    /// <param name="logEntity">日志类</param>
	    /// <returns></returns>
	    public string DeleteSys_Person(int keyId,Log_Operate logEntity)
	    {
			string strResult = "";
			using (TransactionScope trans = new TransactionScope())
			{
				try
				{
					this._sys_PersonDAO.DeleteSys_Person(keyId);
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
            return _sys_PersonDAO.GetDataList(where);
        }

        /// <summary>
        /// 判断一个用户是否是一个领导较色
        /// </summary>
        /// <param name="_personid"></param>
        /// <returns></returns>
        public DataTable CheckPersonHaveLeaderRole(string _personid)
        {
            return this._sys_PersonDAO.CheckPersonHaveLeaderRole(_personid);
        }

        /// <summary>
        /// 根据人员内码找到该人员的主要单位编码
        /// </summary>
        /// <param name="_personid"></param>
        /// <returns></returns>
        public string GetPersonBusiUnitCode(string _personid)
        {
            string strReturn = "";
            string busiunitcode = this._sys_PersonDAO.GetPersonBusiUnitCode(_personid);
            if (busiunitcode.Length >= 6)
                strReturn = busiunitcode.Substring(4);
            return strReturn;
        }

        /// <summary>
        /// 根据主键值查找员工基本信息对象
        /// </summary>
        /// <param name="loginName">用户账号</param> 
        /// <returns>SystemUser</returns>
        public Sys_Person FindSys_Person(string loginName, ref string strErr)
        {
            return this._sys_PersonDAO.FindSys_Person(loginName, ref strErr);
        }

        /// <summary>
        /// 根据主键值查找员工基本信息记录
        /// </summary>
        /// <param name="loginName">用户账号</param> 
        /// <param name="passWord">用户密码</param> 
        /// <returns>SystemUser</returns>
        public Sys_Person FindSys_Person(string loginName, string passWord)
        {
            return _sys_PersonDAO.FindSys_Person(loginName, passWord);
        }

        /// <summary>
        /// 根据用户的内码及项目编码内码，获得该用户在该项目中的功能权限（2013-08-10号修改）
        /// </summary>
        /// <param name="personID"></param>
        /// <param name="projID"></param>
        /// <returns></returns>
        public DataTable GetUserModules(int personID, int projID)
        {
            return _sys_PersonDAO.GetUserModules(personID, projID);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="person"></param>
        public void UpdatePersonPass(Sys_Person person, string pass)
        {
            this._sys_PersonDAO.UpdatePersonPass(person, pass);
        }

        /// <summary>
        /// 修改个人信息
        /// </summary>
        /// <param name="person"></param>
        public void UpdateInfo(Sys_Person person)
        {
            this._sys_PersonDAO.UpdateInfo(person);
        }

        /// <summary>
        /// 删除Sys_Person
        /// </summary>
        /// <param name="Sys_PersonId">主码</param>
        /// <param name="logEntity">日志类</param>
        /// <returns></returns>
        public string DeleteSys_Person_Ex(int keyId, Log_Operate logEntity)
        {
            string strResult = "";
            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    new Sys_PersonBusiUnitDAO().DeleteByPerson(keyId.ToString());
                    this._sys_PersonDAO.DeleteSys_Person(keyId);
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
