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
    /// Log_Operate数据存取类
    /// 生成日期: 2014年10月 27日 20:24
    /// </summary>
    /// <remarks>
    /// 该文件是由代码生成器自动生成的, 请不要随意修改, 你的修改将在代码重新生成时会被覆盖，
    /// 如果要对该类进行修改, 请直接修改该分部类的Log_Operate文件(文件名不含.designer)
    /// </remarks>
    public partial class Log_OperateDAO : SysBaseDao
    {
       /// <summary>
       ///根据主键值查找Log_Operate记录
       /// </summary>
       /// <param name="keyId">KeyId</param> 
       /// <returns>Log_Operate</returns>
       public Log_Operate FindLog_Operate(int keyId)
       {
            string sql = @"SELECT KeyId, PersonId, OperateTime, OperateModule, OperateType, OperateFunction FROM dbo.Log_Operate WHERE KeyId = @KeyId";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);                      
            
            db.AddInParameter(command, "KeyId", DbType.Int32, keyId);
                        
            Log_Operate log_Operate = null;
            
            using (IDataReader dr = db.ExecuteReader(command))
            {
                if (dr.Read())
                {
                    log_Operate = new Log_Operate();               
                    
                    log_Operate.KeyId = (int)dr["KeyId"]; 
                    log_Operate.PersonId = (int)dr["PersonId"]; 
                    log_Operate.OperateTime = (DateTime)dr["OperateTime"]; 
                    log_Operate.OperateModule = (int)dr["OperateModule"]; 
                    log_Operate.OperateType = (string)dr["OperateType"]; 
                    log_Operate.OperateFunction = dr["OperateFunction"] == DBNull.Value ? null : (string)dr["OperateFunction"];                                    
                }
            }   
                   
            return log_Operate; 
       }
       
       /// <summary>
       /// 获取全部Log_Operate列表
       /// </summary>
       /// <returns>Log_Operate对象列表</returns>
       public IList<Log_Operate> GetLog_Operates()
       {
           return GetLog_Operates(null);
       } 
       
        /// <summary>
	 	/// 返回满足查询条件的Log_Operate实体列表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>Log_Operate实体列表</returns>
        public IList<Log_Operate> GetLog_Operates(QueryParameter param)
        {
            string sql = @"SELECT KeyId, PersonId, OperateTime, OperateModule, OperateType, OperateFunction FROM dbo.Log_Operate";

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
            
            IList<Log_Operate> list = new List<Log_Operate>();
                        
            using (IDataReader dr = db.ExecuteReader(command))
            {
                while (dr.Read())
                {
                    Log_Operate log_Operate = new Log_Operate();                                
                    
                    log_Operate.KeyId = (int)dr["KeyId"]; 
                    log_Operate.PersonId = (int)dr["PersonId"]; 
                    log_Operate.OperateTime = (DateTime)dr["OperateTime"]; 
                    log_Operate.OperateModule = (int)dr["OperateModule"]; 
                    log_Operate.OperateType = (string)dr["OperateType"]; 
                    log_Operate.OperateFunction = dr["OperateFunction"] == DBNull.Value ? null : (string)dr["OperateFunction"];                   
                    
                    list.Add(log_Operate);
                }
            }   
                   
            return list; 
        }
        
        /// <summary>
        /// 返回满足查询条件的Log_Operate数据表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>Log_Operate数据表</returns>
        public DataTable GetLog_OperateTable(QueryParameter param)
        {
            string sql = @"SELECT KeyId, PersonId, OperateTime, OperateModule, OperateType, OperateFunction FROM dbo.Log_Operate";

            return GetDataTable(sql, param);
        }
        
        /// <summary>
        /// 插入Log_Operate记录
        /// </summary>
        /// <param name="log_Operate">Log_Operate对象</param>
        /// <returns></returns>
        public int InsertLog_Operate(Log_Operate log_Operate)
        {
            string sql = @"INSERT INTO dbo.Log_Operate(PersonId, OperateTime, OperateModule, OperateType, OperateFunction) VALUES(@PersonId, @OperateTime, @OperateModule, @OperateType, @OperateFunction); SELECT @KeyId = SCOPE_IDENTITY()";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);                        
            
            db.AddOutParameter(command, "KeyId", DbType.Int32, sizeof(int)); 
            db.AddInParameter(command, "PersonId", DbType.Int32, log_Operate.PersonId); 
            db.AddInParameter(command, "OperateTime", DbType.DateTime, log_Operate.OperateTime); 
            db.AddInParameter(command, "OperateModule", DbType.Int32, log_Operate.OperateModule); 
            db.AddInParameter(command, "OperateType", DbType.String, log_Operate.OperateType); 
            db.AddInParameter(command, "OperateFunction", DbType.String, string.IsNullOrEmpty(log_Operate.OperateFunction) ? DBNull.Value : (object)log_Operate.OperateFunction); 
            
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
        /// 更新Log_Operate记录
        /// </summary>
        /// <param name="log_Operate">Log_Operate对象</param>
        /// <returns>受影响的记录数</returns>
        public int UpdateLog_Operate(Log_Operate log_Operate)
        {
            string sql = @"UPDATE dbo.Log_Operate SET PersonId = @PersonId, OperateTime = @OperateTime, OperateModule = @OperateModule, OperateType = @OperateType, OperateFunction = @OperateFunction WHERE KeyId = @KeyId";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);            
            
            db.AddInParameter(command, "KeyId", DbType.Int32, log_Operate.KeyId); 
            db.AddInParameter(command, "PersonId", DbType.Int32, log_Operate.PersonId); 
            db.AddInParameter(command, "OperateTime", DbType.DateTime, log_Operate.OperateTime); 
            db.AddInParameter(command, "OperateModule", DbType.Int32, log_Operate.OperateModule); 
            db.AddInParameter(command, "OperateType", DbType.String, log_Operate.OperateType); 
            db.AddInParameter(command, "OperateFunction", DbType.String, string.IsNullOrEmpty(log_Operate.OperateFunction) ? DBNull.Value : (object)log_Operate.OperateFunction);       
            
            return db.ExecuteNonQuery(command);            
        }
        
        /// <summary>
        /// 删除Log_Operate记录
        /// </summary>
        /// <param name="keyId">KeyId</param> 
        /// <returns>受影响的记录数</returns>
        public int DeleteLog_Operate(int keyId)
        {
            string sql = @"DELETE FROM dbo.Log_Operate WHERE KeyId = @KeyId";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);            
            
            db.AddInParameter(command, "KeyId", DbType.Int32, keyId);
                        
            return db.ExecuteNonQuery(command);
        }
    }
}