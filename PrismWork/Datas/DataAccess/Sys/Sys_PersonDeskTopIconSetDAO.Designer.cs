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
    /// Sys_PersonDeskTopIconSet数据存取类
    /// 生成日期: 2014年11月 20日 18:47
    /// </summary>
    /// <remarks>
    /// 该文件是由代码生成器自动生成的, 请不要随意修改, 你的修改将在代码重新生成时会被覆盖，
    /// 如果要对该类进行修改, 请直接修改该分部类的Sys_PersonDeskTopIconSet文件(文件名不含.designer)
    /// </remarks>
    public partial class Sys_PersonDeskTopIconSetDAO : SysBaseDao
    {
       /// <summary>
       ///根据主键值查找Sys_PersonDeskTopIconSet记录
       /// </summary>
       /// <param name="keyId">KeyId</param> 
       /// <returns>Sys_PersonDeskTopIconSet</returns>
       public Sys_PersonDeskTopIconSet FindSys_PersonDeskTopIconSet(int keyId)
       {
            string sql = @"SELECT KeyId, ModuleId, PersonId, IconFileName FROM dbo.Sys_PersonDeskTopIconSet WHERE KeyId = @KeyId";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);                      
            
            db.AddInParameter(command, "KeyId", DbType.Int32, keyId);
                        
            Sys_PersonDeskTopIconSet sys_PersonDeskTopIconSet = null;
            
            using (IDataReader dr = db.ExecuteReader(command))
            {
                if (dr.Read())
                {
                    sys_PersonDeskTopIconSet = new Sys_PersonDeskTopIconSet();               
                    
                    sys_PersonDeskTopIconSet.KeyId = (int)dr["KeyId"]; 
                    sys_PersonDeskTopIconSet.ModuleId = (int)dr["ModuleId"]; 
                    sys_PersonDeskTopIconSet.PersonId = (int)dr["PersonId"]; 
                    sys_PersonDeskTopIconSet.IconFileName = (string)dr["IconFileName"];                                    
                }
            }   
                   
            return sys_PersonDeskTopIconSet; 
       }
       
       /// <summary>
       /// 获取全部Sys_PersonDeskTopIconSet列表
       /// </summary>
       /// <returns>Sys_PersonDeskTopIconSet对象列表</returns>
       public IList<Sys_PersonDeskTopIconSet> GetSys_PersonDeskTopIconSets()
       {
           return GetSys_PersonDeskTopIconSets(null);
       } 
       
        /// <summary>
	 	/// 返回满足查询条件的Sys_PersonDeskTopIconSet实体列表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>Sys_PersonDeskTopIconSet实体列表</returns>
        public IList<Sys_PersonDeskTopIconSet> GetSys_PersonDeskTopIconSets(QueryParameter param)
        {
            string sql = @"SELECT KeyId, ModuleId, PersonId, IconFileName FROM dbo.Sys_PersonDeskTopIconSet";

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
            
            IList<Sys_PersonDeskTopIconSet> list = new List<Sys_PersonDeskTopIconSet>();
                        
            using (IDataReader dr = db.ExecuteReader(command))
            {
                while (dr.Read())
                {
                    Sys_PersonDeskTopIconSet sys_PersonDeskTopIconSet = new Sys_PersonDeskTopIconSet();                                
                    
                    sys_PersonDeskTopIconSet.KeyId = (int)dr["KeyId"]; 
                    sys_PersonDeskTopIconSet.ModuleId = (int)dr["ModuleId"]; 
                    sys_PersonDeskTopIconSet.PersonId = (int)dr["PersonId"]; 
                    sys_PersonDeskTopIconSet.IconFileName = (string)dr["IconFileName"];                   
                    
                    list.Add(sys_PersonDeskTopIconSet);
                }
            }   
                   
            return list; 
        }
        
        /// <summary>
        /// 返回满足查询条件的Sys_PersonDeskTopIconSet数据表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>Sys_PersonDeskTopIconSet数据表</returns>
        public DataTable GetSys_PersonDeskTopIconSetTable(QueryParameter param)
        {
            string sql = @"SELECT KeyId, ModuleId, PersonId, IconFileName FROM dbo.Sys_PersonDeskTopIconSet";

            return GetDataTable(sql, param);
        }
        
        /// <summary>
        /// 插入Sys_PersonDeskTopIconSet记录
        /// </summary>
        /// <param name="sys_PersonDeskTopIconSet">Sys_PersonDeskTopIconSet对象</param>
        /// <returns></returns>
        public int InsertSys_PersonDeskTopIconSet(Sys_PersonDeskTopIconSet sys_PersonDeskTopIconSet)
        {
            string sql = @"INSERT INTO dbo.Sys_PersonDeskTopIconSet(ModuleId, PersonId, IconFileName) VALUES(@ModuleId, @PersonId, @IconFileName); SELECT @KeyId = SCOPE_IDENTITY()";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);                        
            
            db.AddOutParameter(command, "KeyId", DbType.Int32, sizeof(int)); 
            db.AddInParameter(command, "ModuleId", DbType.Int32, sys_PersonDeskTopIconSet.ModuleId); 
            db.AddInParameter(command, "PersonId", DbType.Int32, sys_PersonDeskTopIconSet.PersonId); 
            db.AddInParameter(command, "IconFileName", DbType.String, sys_PersonDeskTopIconSet.IconFileName); 
            
            int affectedRecords = db.ExecuteNonQuery(command);
            if (affectedRecords < 1)
            {
                throw new ApplicationException("插入数据失败, 没有记录被插入");
            }
            return affectedRecords;
        }
        
        /// <summary>
        /// 更新Sys_PersonDeskTopIconSet记录
        /// </summary>
        /// <param name="sys_PersonDeskTopIconSet">Sys_PersonDeskTopIconSet对象</param>
        /// <returns>受影响的记录数</returns>
        public int UpdateSys_PersonDeskTopIconSet(Sys_PersonDeskTopIconSet sys_PersonDeskTopIconSet)
        {
            string sql = @"UPDATE dbo.Sys_PersonDeskTopIconSet SET ModuleId = @ModuleId, PersonId = @PersonId, IconFileName = @IconFileName WHERE KeyId = @KeyId";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);            
            
            db.AddInParameter(command, "KeyId", DbType.Int32, sys_PersonDeskTopIconSet.KeyId); 
            db.AddInParameter(command, "ModuleId", DbType.Int32, sys_PersonDeskTopIconSet.ModuleId); 
            db.AddInParameter(command, "PersonId", DbType.Int32, sys_PersonDeskTopIconSet.PersonId); 
            db.AddInParameter(command, "IconFileName", DbType.String, sys_PersonDeskTopIconSet.IconFileName);       
            
            return db.ExecuteNonQuery(command);            
        }
        
        /// <summary>
        /// 删除Sys_PersonDeskTopIconSet记录
        /// </summary>
        /// <param name="keyId">KeyId</param> 
        /// <returns>受影响的记录数</returns>
        public int DeleteSys_PersonDeskTopIconSet(int keyId)
        {
            string sql = @"DELETE FROM dbo.Sys_PersonDeskTopIconSet WHERE KeyId = @KeyId";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);            
            
            db.AddInParameter(command, "KeyId", DbType.Int32, keyId);
                        
            return db.ExecuteNonQuery(command);
        }
    }
}