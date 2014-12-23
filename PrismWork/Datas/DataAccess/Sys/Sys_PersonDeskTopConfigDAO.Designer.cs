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
    /// Sys_PersonDeskTopConfig数据存取类
    /// 生成日期: 2014年11月 20日 18:49
    /// </summary>
    /// <remarks>
    /// 该文件是由代码生成器自动生成的, 请不要随意修改, 你的修改将在代码重新生成时会被覆盖，
    /// 如果要对该类进行修改, 请直接修改该分部类的Sys_PersonDeskTopConfig文件(文件名不含.designer)
    /// </remarks>
    public partial class Sys_PersonDeskTopConfigDAO : SysBaseDao
    {
       /// <summary>
       ///根据主键值查找Sys_PersonDeskTopConfig记录
       /// </summary>
       /// <param name="keyId">KeyId</param> 
       /// <returns>Sys_PersonDeskTopConfig</returns>
       public Sys_PersonDeskTopConfig FindSys_PersonDeskTopConfig(int keyId)
       {
            string sql = @"SELECT KeyId, PersonId, ConfigName, ConfigValue, CreateDate, ModifiedDate FROM dbo.Sys_PersonDeskTopConfig WHERE KeyId = @KeyId";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);                      
            
            db.AddInParameter(command, "KeyId", DbType.Int32, keyId);
                        
            Sys_PersonDeskTopConfig sys_PersonDeskTopConfig = null;
            
            using (IDataReader dr = db.ExecuteReader(command))
            {
                if (dr.Read())
                {
                    sys_PersonDeskTopConfig = new Sys_PersonDeskTopConfig();               
                    
                    sys_PersonDeskTopConfig.KeyId = (int)dr["KeyId"]; 
                    sys_PersonDeskTopConfig.PersonId = (int)dr["PersonId"]; 
                    sys_PersonDeskTopConfig.ConfigName = (string)dr["ConfigName"]; 
                    sys_PersonDeskTopConfig.ConfigValue = (string)dr["ConfigValue"]; 
                    sys_PersonDeskTopConfig.CreateDate = dr["CreateDate"] == DBNull.Value ? null : (DateTime?)dr["CreateDate"]; 
                    sys_PersonDeskTopConfig.ModifiedDate = dr["ModifiedDate"] == DBNull.Value ? null : (DateTime?)dr["ModifiedDate"];                                    
                }
            }   
                   
            return sys_PersonDeskTopConfig; 
       }
       
       /// <summary>
       /// 获取全部Sys_PersonDeskTopConfig列表
       /// </summary>
       /// <returns>Sys_PersonDeskTopConfig对象列表</returns>
       public IList<Sys_PersonDeskTopConfig> GetSys_PersonDeskTopConfigs()
       {
           return GetSys_PersonDeskTopConfigs(null);
       } 
       
        /// <summary>
	 	/// 返回满足查询条件的Sys_PersonDeskTopConfig实体列表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>Sys_PersonDeskTopConfig实体列表</returns>
        public IList<Sys_PersonDeskTopConfig> GetSys_PersonDeskTopConfigs(QueryParameter param)
        {
            string sql = @"SELECT KeyId, PersonId, ConfigName, ConfigValue, CreateDate, ModifiedDate FROM dbo.Sys_PersonDeskTopConfig";

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
            
            IList<Sys_PersonDeskTopConfig> list = new List<Sys_PersonDeskTopConfig>();
                        
            using (IDataReader dr = db.ExecuteReader(command))
            {
                while (dr.Read())
                {
                    Sys_PersonDeskTopConfig sys_PersonDeskTopConfig = new Sys_PersonDeskTopConfig();                                
                    
                    sys_PersonDeskTopConfig.KeyId = (int)dr["KeyId"]; 
                    sys_PersonDeskTopConfig.PersonId = (int)dr["PersonId"]; 
                    sys_PersonDeskTopConfig.ConfigName = (string)dr["ConfigName"]; 
                    sys_PersonDeskTopConfig.ConfigValue = (string)dr["ConfigValue"]; 
                    sys_PersonDeskTopConfig.CreateDate = dr["CreateDate"] == DBNull.Value ? null : (DateTime?)dr["CreateDate"]; 
                    sys_PersonDeskTopConfig.ModifiedDate = dr["ModifiedDate"] == DBNull.Value ? null : (DateTime?)dr["ModifiedDate"];                   
                    
                    list.Add(sys_PersonDeskTopConfig);
                }
            }   
                   
            return list; 
        }
        
        /// <summary>
        /// 返回满足查询条件的Sys_PersonDeskTopConfig数据表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>Sys_PersonDeskTopConfig数据表</returns>
        public DataTable GetSys_PersonDeskTopConfigTable(QueryParameter param)
        {
            string sql = @"SELECT KeyId, PersonId, ConfigName, ConfigValue, CreateDate, ModifiedDate FROM dbo.Sys_PersonDeskTopConfig";

            return GetDataTable(sql, param);
        }
        
        /// <summary>
        /// 插入Sys_PersonDeskTopConfig记录
        /// </summary>
        /// <param name="sys_PersonDeskTopConfig">Sys_PersonDeskTopConfig对象</param>
        /// <returns></returns>
        public int InsertSys_PersonDeskTopConfig(Sys_PersonDeskTopConfig sys_PersonDeskTopConfig)
        {
            string sql = @"INSERT INTO dbo.Sys_PersonDeskTopConfig(PersonId, ConfigName, ConfigValue, CreateDate, ModifiedDate) VALUES(@PersonId, @ConfigName, @ConfigValue, @CreateDate, @ModifiedDate); SELECT @KeyId = SCOPE_IDENTITY()";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);                        
            
            db.AddOutParameter(command, "KeyId", DbType.Int32, sizeof(int)); 
            db.AddInParameter(command, "PersonId", DbType.Int32, sys_PersonDeskTopConfig.PersonId); 
            db.AddInParameter(command, "ConfigName", DbType.String, sys_PersonDeskTopConfig.ConfigName); 
            db.AddInParameter(command, "ConfigValue", DbType.String, sys_PersonDeskTopConfig.ConfigValue); 
            db.AddInParameter(command, "CreateDate", DbType.DateTime, sys_PersonDeskTopConfig.CreateDate.HasValue ? (object)sys_PersonDeskTopConfig.CreateDate : DBNull.Value); 
            db.AddInParameter(command, "ModifiedDate", DbType.DateTime, sys_PersonDeskTopConfig.ModifiedDate.HasValue ? (object)sys_PersonDeskTopConfig.ModifiedDate : DBNull.Value); 
            
            int affectedRecords = db.ExecuteNonQuery(command);
            if (affectedRecords < 1)
            {
                throw new ApplicationException("插入数据失败, 没有记录被插入");
            }
            return affectedRecords;
        }
        
        /// <summary>
        /// 更新Sys_PersonDeskTopConfig记录
        /// </summary>
        /// <param name="sys_PersonDeskTopConfig">Sys_PersonDeskTopConfig对象</param>
        /// <returns>受影响的记录数</returns>
        public int UpdateSys_PersonDeskTopConfig(Sys_PersonDeskTopConfig sys_PersonDeskTopConfig)
        {
            string sql = @"UPDATE dbo.Sys_PersonDeskTopConfig SET PersonId = @PersonId, ConfigName = @ConfigName, ConfigValue = @ConfigValue, CreateDate = @CreateDate, ModifiedDate = @ModifiedDate WHERE KeyId = @KeyId";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);            
            
            db.AddInParameter(command, "KeyId", DbType.Int32, sys_PersonDeskTopConfig.KeyId); 
            db.AddInParameter(command, "PersonId", DbType.Int32, sys_PersonDeskTopConfig.PersonId); 
            db.AddInParameter(command, "ConfigName", DbType.String, sys_PersonDeskTopConfig.ConfigName); 
            db.AddInParameter(command, "ConfigValue", DbType.String, sys_PersonDeskTopConfig.ConfigValue); 
            db.AddInParameter(command, "CreateDate", DbType.DateTime, sys_PersonDeskTopConfig.CreateDate.HasValue ? (object)sys_PersonDeskTopConfig.CreateDate : DBNull.Value); 
            db.AddInParameter(command, "ModifiedDate", DbType.DateTime, sys_PersonDeskTopConfig.ModifiedDate.HasValue ? (object)sys_PersonDeskTopConfig.ModifiedDate : DBNull.Value);       
            
            return db.ExecuteNonQuery(command);            
        }
        
        /// <summary>
        /// 删除Sys_PersonDeskTopConfig记录
        /// </summary>
        /// <param name="keyId">KeyId</param> 
        /// <returns>受影响的记录数</returns>
        public int DeleteSys_PersonDeskTopConfig(int keyId)
        {
            string sql = @"DELETE FROM dbo.Sys_PersonDeskTopConfig WHERE KeyId = @KeyId";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);            
            
            db.AddInParameter(command, "KeyId", DbType.Int32, keyId);
                        
            return db.ExecuteNonQuery(command);
        }
    }
}