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
    /// Log_SqlError数据存取类
    /// 生成日期: 2014年10月 27日 20:39
    /// </summary>
    /// <remarks>
    /// 该文件是由代码生成器自动生成的, 请不要随意修改, 你的修改将在代码重新生成时会被覆盖，
    /// 如果要对该类进行修改, 请直接修改该分部类的Log_SqlError文件(文件名不含.designer)
    /// </remarks>
    public partial class Log_SqlErrorDAO : SysBaseDao
    {
       /// <summary>
       ///根据主键值查找Log_SqlError记录
       /// </summary>
       /// <param name="keyId">KeyId</param> 
       /// <returns>Log_SqlError</returns>
       public Log_SqlError FindLog_SqlError(int keyId)
       {
            string sql = @"SELECT KeyId, SPName, Description, LogTime FROM dbo.Log_SqlError WHERE KeyId = @KeyId";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);                      
            
            db.AddInParameter(command, "KeyId", DbType.Int32, keyId);
                        
            Log_SqlError log_SqlError = null;
            
            using (IDataReader dr = db.ExecuteReader(command))
            {
                if (dr.Read())
                {
                    log_SqlError = new Log_SqlError();               
                    
                    log_SqlError.KeyId = (int)dr["KeyId"]; 
                    log_SqlError.SPName = (string)dr["SPName"]; 
                    log_SqlError.Description = (string)dr["Description"]; 
                    log_SqlError.LogTime = (DateTime)dr["LogTime"];                                    
                }
            }   
                   
            return log_SqlError; 
       }
       
       /// <summary>
       /// 获取全部Log_SqlError列表
       /// </summary>
       /// <returns>Log_SqlError对象列表</returns>
       public IList<Log_SqlError> GetLog_SqlErrors()
       {
           return GetLog_SqlErrors(null);
       } 
       
        /// <summary>
	 	/// 返回满足查询条件的Log_SqlError实体列表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>Log_SqlError实体列表</returns>
        public IList<Log_SqlError> GetLog_SqlErrors(QueryParameter param)
        {
            string sql = @"SELECT KeyId, SPName, Description, LogTime FROM dbo.Log_SqlError";

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
            
            IList<Log_SqlError> list = new List<Log_SqlError>();
                        
            using (IDataReader dr = db.ExecuteReader(command))
            {
                while (dr.Read())
                {
                    Log_SqlError log_SqlError = new Log_SqlError();                                
                    
                    log_SqlError.KeyId = (int)dr["KeyId"]; 
                    log_SqlError.SPName = (string)dr["SPName"]; 
                    log_SqlError.Description = (string)dr["Description"]; 
                    log_SqlError.LogTime = (DateTime)dr["LogTime"];                   
                    
                    list.Add(log_SqlError);
                }
            }   
                   
            return list; 
        }
        
        /// <summary>
        /// 返回满足查询条件的Log_SqlError数据表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>Log_SqlError数据表</returns>
        public DataTable GetLog_SqlErrorTable(QueryParameter param)
        {
            string sql = @"SELECT KeyId, SPName, Description, LogTime FROM dbo.Log_SqlError";

            return GetDataTable(sql, param);
        }
        
        /// <summary>
        /// 插入Log_SqlError记录
        /// </summary>
        /// <param name="log_SqlError">Log_SqlError对象</param>
        /// <returns></returns>
        public int InsertLog_SqlError(Log_SqlError log_SqlError)
        {
            string sql = @"INSERT INTO dbo.Log_SqlError(SPName, Description, LogTime) VALUES(@SPName, @Description, @LogTime); SELECT @KeyId = SCOPE_IDENTITY()";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);                        
            
            db.AddOutParameter(command, "KeyId", DbType.Int32, sizeof(int)); 
            db.AddInParameter(command, "SPName", DbType.String, log_SqlError.SPName); 
            db.AddInParameter(command, "Description", DbType.String, log_SqlError.Description); 
            db.AddInParameter(command, "LogTime", DbType.DateTime, log_SqlError.LogTime); 
            
            int affectedRecords = db.ExecuteNonQuery(command);
            if (affectedRecords < 1)
            {
                throw new ApplicationException("插入数据失败, 没有记录被插入");
            }
            else
            {
                string strTemp = "select @@identity";
                DataTable dt = GetDataTable(strTemp);
                if (dt != null && dt.Rows.Count > 0)
                {
                    affectedRecords = int.Parse(dt.Rows[0][0].ToString());
                }
            }
            return affectedRecords;
        }
        
        /// <summary>
        /// 更新Log_SqlError记录
        /// </summary>
        /// <param name="log_SqlError">Log_SqlError对象</param>
        /// <returns>受影响的记录数</returns>
        public int UpdateLog_SqlError(Log_SqlError log_SqlError)
        {
            string sql = @"UPDATE dbo.Log_SqlError SET SPName = @SPName, Description = @Description, LogTime = @LogTime WHERE KeyId = @KeyId";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);            
            
            db.AddInParameter(command, "KeyId", DbType.Int32, log_SqlError.KeyId); 
            db.AddInParameter(command, "SPName", DbType.String, log_SqlError.SPName); 
            db.AddInParameter(command, "Description", DbType.String, log_SqlError.Description); 
            db.AddInParameter(command, "LogTime", DbType.DateTime, log_SqlError.LogTime);       
            
            return db.ExecuteNonQuery(command);            
        }
        
        /// <summary>
        /// 删除Log_SqlError记录
        /// </summary>
        /// <param name="keyId">KeyId</param> 
        /// <returns>受影响的记录数</returns>
        public int DeleteLog_SqlError(int keyId)
        {
            string sql = @"DELETE FROM dbo.Log_SqlError WHERE KeyId = @KeyId";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);            
            
            db.AddInParameter(command, "KeyId", DbType.Int32, keyId);
                        
            return db.ExecuteNonQuery(command);
        }
    }
}