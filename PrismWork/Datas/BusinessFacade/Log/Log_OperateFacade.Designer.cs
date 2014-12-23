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
    /// Log_Operate业务外观类
    /// 生成日期: 2014年10月 27日 20:24
    ///</summary>
    /// <remarks>
    /// 该文件是由代码生成器自动生成的, 请不要随意修改, 你的修改将在代码重新生成时会被覆盖，
    /// 如果要对该类进行修改, 请直接修改该分部类的Log_Operate文件(文件名不含.designer)
    /// </remarks>
    public partial class Log_OperateFacade
    {
       private Log_OperateDAO _log_OperateDAO = new Log_OperateDAO();
             
       /// <summary>
       /// 根据主键值查找Log_Operate对象
       /// </summary>
       /// <param name="keyId">KeyId</param> 
       /// <returns>Log_Operate</returns>
       public Log_Operate FindLog_Operate(int keyId)
       {
           return this._log_OperateDAO.FindLog_Operate(keyId); 
       } 
       
       /// <summary>
       /// 获取全部Log_Operate列表
       /// </summary>
       /// <returns>Log_Operate对象列表</returns>
       public IList<Log_Operate> GetLog_Operates()
       {
            return this._log_OperateDAO.GetLog_Operates();
       } 
       
        /// <summary>
        /// 返回满足查询条件的Log_Operate实体列表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>
        /// Log_Operate实体列表</returns>
        public IList<Log_Operate> GetLog_Operates(QueryParameter param)
        {
            return this._log_OperateDAO.GetLog_Operates(param);
        }
        
        /// <summary>
        /// 返回满足查询条件的Log_Operate数据表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>
        /// Log_Operate数据表</returns>
        public DataTable GetLog_OperateTable(QueryParameter param)
        {
            return this._log_OperateDAO.GetLog_OperateTable(param);
        }
       
       /// <summary>
       /// 创建Log_Operate记录
       /// </summary>
       /// <param name="log_Operate">
       /// Log_Operate对象</param>
       /// <returns></returns>
       public int CreateLog_Operate(Log_Operate log_Operate)
       {
           return this._log_OperateDAO.InsertLog_Operate(log_Operate);
       }
       
        /// <summary>
        /// 更新Log_Operate记录
        /// </summary>
        /// <param name="log_Operate">
        /// Log_Operate对象</param>
        /// <returns>受影响的记录数</returns>
        public int UpdateLog_Operate(Log_Operate log_Operate)
        {
            return this._log_OperateDAO.UpdateLog_Operate(log_Operate);
        } 

        /// <summary>
		/// 删除Log_Operate记录
        /// </summary>
        /// <param name="keyId">KeyId</param> 
        /// <returns>受影响的记录数</returns>
        public int DeleteLog_Operate(int keyId)
        {
            return this._log_OperateDAO.DeleteLog_Operate(keyId);
        } 
    }      
}  
