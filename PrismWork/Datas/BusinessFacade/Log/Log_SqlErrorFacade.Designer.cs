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
    /// Log_SqlError业务外观类
    /// 生成日期: 2014年10月 27日 20:39
    ///</summary>
    /// <remarks>
    /// 该文件是由代码生成器自动生成的, 请不要随意修改, 你的修改将在代码重新生成时会被覆盖，
    /// 如果要对该类进行修改, 请直接修改该分部类的Log_SqlError文件(文件名不含.designer)
    /// </remarks>
    public partial class Log_SqlErrorFacade
    {
       private Log_SqlErrorDAO _log_SqlErrorDAO = new Log_SqlErrorDAO();
             
       /// <summary>
       /// 根据主键值查找Log_SqlError对象
       /// </summary>
       /// <param name="keyId">KeyId</param> 
       /// <returns>Log_SqlError</returns>
       public Log_SqlError FindLog_SqlError(int keyId)
       {
           return this._log_SqlErrorDAO.FindLog_SqlError(keyId); 
       } 
       
       /// <summary>
       /// 获取全部Log_SqlError列表
       /// </summary>
       /// <returns>Log_SqlError对象列表</returns>
       public IList<Log_SqlError> GetLog_SqlErrors()
       {
            return this._log_SqlErrorDAO.GetLog_SqlErrors();
       } 
       
        /// <summary>
        /// 返回满足查询条件的Log_SqlError实体列表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>
        /// Log_SqlError实体列表</returns>
        public IList<Log_SqlError> GetLog_SqlErrors(QueryParameter param)
        {
            return this._log_SqlErrorDAO.GetLog_SqlErrors(param);
        }
        
        /// <summary>
        /// 返回满足查询条件的Log_SqlError数据表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>
        /// Log_SqlError数据表</returns>
        public DataTable GetLog_SqlErrorTable(QueryParameter param)
        {
            return this._log_SqlErrorDAO.GetLog_SqlErrorTable(param);
        }
       
       /// <summary>
       /// 创建Log_SqlError记录
       /// </summary>
       /// <param name="log_SqlError">
       /// Log_SqlError对象</param>
       /// <returns></returns>
       public int CreateLog_SqlError(Log_SqlError log_SqlError)
       {
           return this._log_SqlErrorDAO.InsertLog_SqlError(log_SqlError);
       }
       
        /// <summary>
        /// 更新Log_SqlError记录
        /// </summary>
        /// <param name="log_SqlError">
        /// Log_SqlError对象</param>
        /// <returns>受影响的记录数</returns>
        public int UpdateLog_SqlError(Log_SqlError log_SqlError)
        {
            return this._log_SqlErrorDAO.UpdateLog_SqlError(log_SqlError);
        } 

        /// <summary>
		/// 删除Log_SqlError记录
        /// </summary>
        /// <param name="keyId">KeyId</param> 
        /// <returns>受影响的记录数</returns>
        public int DeleteLog_SqlError(int keyId)
        {
            return this._log_SqlErrorDAO.DeleteLog_SqlError(keyId);
        } 
    }      
}  
