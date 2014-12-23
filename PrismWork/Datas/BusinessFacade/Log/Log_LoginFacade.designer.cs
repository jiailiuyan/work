using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Project.Common;
using Project.DataAccess;
using Project.Entities;

namespace Project.BusinessFacade 
{
    /// <summary>
    /// Log_Login业务外观类
    /// 生成日期: 2014年10月 28日 14:36
    ///</summary>
    /// <remarks>
    /// 该文件是由代码生成器自动生成的, 请不要随意修改, 你的修改将在代码重新生成时会被覆盖，
    /// 如果要对该类进行修改, 请直接修改该分部类的Log_Login文件(文件名不含.designer)
    /// </remarks>
    public partial class Log_LoginFacade
    {
       private Log_LoginDAO _log_LoginDAO = new Log_LoginDAO();
             
       /// <summary>
       /// 根据主键值查找Log_Login对象
       /// </summary>
       /// <param name="keyId">KeyId</param> 
       /// <returns>Log_Login</returns>
       public Log_Login FindLog_Login(int keyId)
       {
           return this._log_LoginDAO.FindLog_Login(keyId); 
       } 
       
       /// <summary>
       /// 获取全部Log_Login列表
       /// </summary>
       /// <returns>Log_Login对象列表</returns>
       public IList<Log_Login> GetLog_Logins()
       {
            return this._log_LoginDAO.GetLog_Logins();
       } 
       
        /// <summary>
        /// 返回满足查询条件的Log_Login实体列表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>
        /// Log_Login实体列表</returns>
        public IList<Log_Login> GetLog_Logins(QueryParameter param)
        {
            return this._log_LoginDAO.GetLog_Logins(param);
        }
        
        /// <summary>
        /// 返回满足查询条件的Log_Login数据表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>
        /// Log_Login数据表</returns>
        public DataTable GetLog_LoginTable(QueryParameter param)
        {
            return this._log_LoginDAO.GetLog_LoginTable(param);
        }
       
       /// <summary>
       /// 创建Log_Login记录
       /// </summary>
       /// <param name="log_Login">
       /// Log_Login对象</param>
       /// <returns></returns>
       public int CreateLog_Login(Log_Login log_Login)
       {
           return this._log_LoginDAO.InsertLog_Login(log_Login);
       }
       
        /// <summary>
        /// 更新Log_Login记录
        /// </summary>
        /// <param name="log_Login">
        /// Log_Login对象</param>
        /// <returns>受影响的记录数</returns>
        public int UpdateLog_Login(Log_Login log_Login)
        {
            return this._log_LoginDAO.UpdateLog_Login(log_Login);
        } 

        /// <summary>
		/// 删除Log_Login记录
        /// </summary>
        /// <param name="keyId">KeyId</param> 
        /// <returns>受影响的记录数</returns>
        public int DeleteLog_Login(int keyId)
        {
            return this._log_LoginDAO.DeleteLog_Login(keyId);
        } 
    }      
}  
