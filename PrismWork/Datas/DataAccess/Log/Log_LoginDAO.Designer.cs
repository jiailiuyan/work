using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Project.Common;
using Project.Entities;

namespace Project.DataAccess
{
    /// <summary>
    /// Log_Login数据存取类
    /// 生成日期: 2014年10月 28日 14:36
    /// </summary>
    /// <remarks>
    /// 该文件是由代码生成器自动生成的, 请不要随意修改, 你的修改将在代码重新生成时会被覆盖，
    /// 如果要对该类进行修改, 请直接修改该分部类的Log_Login文件(文件名不含.designer)
    /// </remarks>
    public partial class Log_LoginDAO : SysBaseDao
    {
       /// <summary>
       ///根据主键值查找Log_Login记录
       /// </summary>
       /// <param name="keyId">KeyId</param> 
       /// <returns>Log_Login</returns>
       public Log_Login FindLog_Login(int keyId)
       {
            string sql = @"SELECT KeyId, PersonId, LoginIP, LoginHostName, LoginMac, LoginTime, LogoutTime FROM dbo.Log_Login WHERE KeyId = @KeyId";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);                      
            
            db.AddInParameter(command, "KeyId", DbType.Int32, keyId);
                        
            Log_Login log_Login = null;
            
            using (IDataReader dr = db.ExecuteReader(command))
            {
                if (dr.Read())
                {
                    log_Login = new Log_Login();               
                    
                    log_Login.KeyId = (int)dr["KeyId"]; 
                    log_Login.PersonId = (int)dr["PersonId"]; 
                    log_Login.LoginIP = (string)dr["LoginIP"]; 
                    log_Login.LoginHostName = (string)dr["LoginHostName"]; 
                    log_Login.LoginMac = (string)dr["LoginMac"]; 
                    log_Login.LoginTime = (DateTime)dr["LoginTime"]; 
                    log_Login.LogoutTime = dr["LogoutTime"] == DBNull.Value ? null : (DateTime?)dr["LogoutTime"];                                    
                }
            }   
                   
            return log_Login; 
       }
       
       /// <summary>
       /// 获取全部Log_Login列表
       /// </summary>
       /// <returns>Log_Login对象列表</returns>
       public IList<Log_Login> GetLog_Logins()
       {
           return GetLog_Logins(null);
       } 
       
        /// <summary>
	 	/// 返回满足查询条件的Log_Login实体列表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>Log_Login实体列表</returns>
        public IList<Log_Login> GetLog_Logins(QueryParameter param)
        {
            string sql = @"SELECT KeyId, PersonId, LoginIP, LoginHostName, LoginMac, LoginTime, LogoutTime FROM dbo.Log_Login";

            if (param != null)
            {
                sql = QueryParameter.CompleteSqlString(sql, param);
            }

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);

            if (param != null)
            {
                //设置参数
                foreach (IExpression exp in param.WhereExpressions)
                {
                    if (exp is SimpleExpression)
                    {
                        SimpleExpression simple = exp as SimpleExpression;
                        db.AddInParameter(command, simple.ExpName, simple.DbType, simple.Value);
                    }
                }
            }
            
            IList<Log_Login> list = new List<Log_Login>();
                        
            using (IDataReader dr = db.ExecuteReader(command))
            {
                while (dr.Read())
                {
                    Log_Login log_Login = new Log_Login();                                
                    
                    log_Login.KeyId = (int)dr["KeyId"]; 
                    log_Login.PersonId = (int)dr["PersonId"]; 
                    log_Login.LoginIP = (string)dr["LoginIP"]; 
                    log_Login.LoginHostName = (string)dr["LoginHostName"]; 
                    log_Login.LoginMac = (string)dr["LoginMac"]; 
                    log_Login.LoginTime = (DateTime)dr["LoginTime"]; 
                    log_Login.LogoutTime = dr["LogoutTime"] == DBNull.Value ? null : (DateTime?)dr["LogoutTime"];                   
                    
                    list.Add(log_Login);
                }
            }   
                   
            return list; 
        }
        
        /// <summary>
        /// 返回满足查询条件的Log_Login数据表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>Log_Login数据表</returns>
        public DataTable GetLog_LoginTable(QueryParameter param)
        {
            string sql = @"SELECT KeyId, PersonId, LoginIP, LoginHostName, LoginMac, LoginTime, LogoutTime FROM dbo.Log_Login";

            return GetDataTable(sql, param);
        }
        
        /// <summary>
        /// 插入Log_Login记录
        /// </summary>
        /// <param name="log_Login">Log_Login对象</param>
        /// <returns></returns>
        public int InsertLog_Login(Log_Login log_Login)
        {
            string sql = @"INSERT INTO dbo.Log_Login(PersonId, LoginIP, LoginHostName, LoginMac, LoginTime, LogoutTime) VALUES(@PersonId, @LoginIP, @LoginHostName, @LoginMac, @LoginTime, @LogoutTime); SELECT @KeyId = SCOPE_IDENTITY()";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);                        
            
            db.AddOutParameter(command, "KeyId", DbType.Int32, sizeof(int)); 
            db.AddInParameter(command, "PersonId", DbType.Int32, log_Login.PersonId); 
            db.AddInParameter(command, "LoginIP", DbType.String, log_Login.LoginIP); 
            db.AddInParameter(command, "LoginHostName", DbType.String, log_Login.LoginHostName); 
            db.AddInParameter(command, "LoginMac", DbType.String, log_Login.LoginMac); 
            db.AddInParameter(command, "LoginTime", DbType.DateTime, log_Login.LoginTime); 
            db.AddInParameter(command, "LogoutTime", DbType.DateTime, log_Login.LogoutTime.HasValue ? (object)log_Login.LogoutTime : DBNull.Value); 
            
            int affectedRecords = db.ExecuteNonQuery(command);
            if (affectedRecords < 1)
            {
                throw new ApplicationException("插入数据失败, 没有记录被插入");
            }
            return affectedRecords;
        }
        
        /// <summary>
        /// 更新Log_Login记录
        /// </summary>
        /// <param name="log_Login">Log_Login对象</param>
        /// <returns>受影响的记录数</returns>
        public int UpdateLog_Login(Log_Login log_Login)
        {
            string sql = @"UPDATE dbo.Log_Login SET PersonId = @PersonId, LoginIP = @LoginIP, LoginHostName = @LoginHostName, LoginMac = @LoginMac, LoginTime = @LoginTime, LogoutTime = @LogoutTime WHERE KeyId = @KeyId";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);            
            
            db.AddInParameter(command, "KeyId", DbType.Int32, log_Login.KeyId); 
            db.AddInParameter(command, "PersonId", DbType.Int32, log_Login.PersonId); 
            db.AddInParameter(command, "LoginIP", DbType.String, log_Login.LoginIP); 
            db.AddInParameter(command, "LoginHostName", DbType.String, log_Login.LoginHostName); 
            db.AddInParameter(command, "LoginMac", DbType.String, log_Login.LoginMac); 
            db.AddInParameter(command, "LoginTime", DbType.DateTime, log_Login.LoginTime); 
            db.AddInParameter(command, "LogoutTime", DbType.DateTime, log_Login.LogoutTime.HasValue ? (object)log_Login.LogoutTime : DBNull.Value);       
            
            return db.ExecuteNonQuery(command);            
        }
        
        /// <summary>
        /// 删除Log_Login记录
        /// </summary>
        /// <param name="keyId">KeyId</param> 
        /// <returns>受影响的记录数</returns>
        public int DeleteLog_Login(int keyId)
        {
            string sql = @"DELETE FROM dbo.Log_Login WHERE KeyId = @KeyId";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);            
            
            db.AddInParameter(command, "KeyId", DbType.Int32, keyId);
                        
            return db.ExecuteNonQuery(command);
        }
    }
}