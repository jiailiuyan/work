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
    /// 用户角色对应关系数据存取类
    /// 生成日期: 2014年10月 27日 21:08
    /// </summary>
    /// <remarks>
    /// 该文件是由代码生成器自动生成的, 请不要随意修改, 你的修改将在代码重新生成时会被覆盖，
    /// 如果要对该类进行修改, 请直接修改该分部类的Sys_UserRole文件(文件名不含.designer)
    /// </remarks>
    public partial class Sys_UserRoleDAO : SysBaseDao
    {
       /// <summary>
       ///根据主键值查找用户角色对应关系记录
       /// </summary>
       /// <param name="keyId">主键</param> 
       /// <returns>Sys_UserRole</returns>
       public Sys_UserRole FindSys_UserRole(int keyId)
       {
            string sql = @"SELECT KeyId, ProjID, PersonID, RoleId, Status, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn FROM dbo.Sys_UserRole WHERE KeyId = @KeyId";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);                      
            
            db.AddInParameter(command, "KeyId", DbType.Int32, keyId);
                        
            Sys_UserRole sys_UserRole = null;
            
            using (IDataReader dr = db.ExecuteReader(command))
            {
                if (dr.Read())
                {
                    sys_UserRole = new Sys_UserRole();               
                    
                    sys_UserRole.KeyId = (int)dr["KeyId"]; 
                    sys_UserRole.ProjID = dr["ProjID"] == DBNull.Value ? null : (int?)dr["ProjID"]; 
                    sys_UserRole.PersonID = (int)dr["PersonID"]; 
                    sys_UserRole.RoleId = (int)dr["RoleId"]; 
                    sys_UserRole.Status = (bool)dr["Status"]; 
                    sys_UserRole.CreatedBy = (int)dr["CreatedBy"]; 
                    sys_UserRole.CreatedOn = (DateTime)dr["CreatedOn"]; 
                    sys_UserRole.ModifiedBy = dr["ModifiedBy"] == DBNull.Value ? null : (int?)dr["ModifiedBy"]; 
                    sys_UserRole.ModifiedOn = dr["ModifiedOn"] == DBNull.Value ? null : (DateTime?)dr["ModifiedOn"];                                    
                }
            }   
                   
            return sys_UserRole; 
       }
       
       /// <summary>
       /// 获取全部用户角色对应关系列表
       /// </summary>
       /// <returns>Sys_UserRole对象列表</returns>
       public IList<Sys_UserRole> GetSys_UserRoles()
       {
           return GetSys_UserRoles(null);
       } 
       
        /// <summary>
	 	/// 返回满足查询条件的用户角色对应关系实体列表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>用户角色对应关系实体列表</returns>
        public IList<Sys_UserRole> GetSys_UserRoles(QueryParameter param)
        {
            string sql = @"SELECT KeyId, ProjID, PersonID, RoleId, Status, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn FROM dbo.Sys_UserRole";

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
            
            IList<Sys_UserRole> list = new List<Sys_UserRole>();
                        
            using (IDataReader dr = db.ExecuteReader(command))
            {
                while (dr.Read())
                {
                    Sys_UserRole sys_UserRole = new Sys_UserRole();                                
                    
                    sys_UserRole.KeyId = (int)dr["KeyId"]; 
                    sys_UserRole.ProjID = dr["ProjID"] == DBNull.Value ? null : (int?)dr["ProjID"]; 
                    sys_UserRole.PersonID = (int)dr["PersonID"]; 
                    sys_UserRole.RoleId = (int)dr["RoleId"]; 
                    sys_UserRole.Status = (bool)dr["Status"]; 
                    sys_UserRole.CreatedBy = (int)dr["CreatedBy"]; 
                    sys_UserRole.CreatedOn = (DateTime)dr["CreatedOn"]; 
                    sys_UserRole.ModifiedBy = dr["ModifiedBy"] == DBNull.Value ? null : (int?)dr["ModifiedBy"]; 
                    sys_UserRole.ModifiedOn = dr["ModifiedOn"] == DBNull.Value ? null : (DateTime?)dr["ModifiedOn"];                   
                    
                    list.Add(sys_UserRole);
                }
            }   
                   
            return list; 
        }
        
        /// <summary>
        /// 返回满足查询条件的用户角色对应关系数据表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>用户角色对应关系数据表</returns>
        public DataTable GetSys_UserRoleTable(QueryParameter param)
        {
            string sql = @"SELECT KeyId, ProjID, PersonID, RoleId, Status, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn FROM dbo.Sys_UserRole";

            return GetDataTable(sql, param);
        }
        
        /// <summary>
        /// 插入用户角色对应关系记录
        /// </summary>
        /// <param name="sys_UserRole">用户角色对应关系对象</param>
        /// <returns></returns>
        public int InsertSys_UserRole(Sys_UserRole sys_UserRole)
        {
            string sql = @"INSERT INTO dbo.Sys_UserRole(ProjID, PersonID, RoleId, Status, CreatedBy) VALUES(@ProjID, @PersonID, @RoleId, @Status, @CreatedBy); SELECT @KeyId = SCOPE_IDENTITY()";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);                        
            
            db.AddOutParameter(command, "KeyId", DbType.Int32, sizeof(int)); 
            db.AddInParameter(command, "ProjID", DbType.Int32, sys_UserRole.ProjID.HasValue ? (object)sys_UserRole.ProjID : DBNull.Value); 
            db.AddInParameter(command, "PersonID", DbType.Int32, sys_UserRole.PersonID); 
            db.AddInParameter(command, "RoleId", DbType.Int32, sys_UserRole.RoleId); 
            db.AddInParameter(command, "Status", DbType.Boolean, sys_UserRole.Status); 
            db.AddInParameter(command, "CreatedBy", DbType.Int32, sys_UserRole.CreatedBy); 
            
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
        /// 更新用户角色对应关系记录
        /// </summary>
        /// <param name="sys_UserRole">用户角色对应关系对象</param>
        /// <returns>受影响的记录数</returns>
        public int UpdateSys_UserRole(Sys_UserRole sys_UserRole)
        {
            string sql = @"UPDATE dbo.Sys_UserRole SET ProjID = @ProjID, PersonID = @PersonID, RoleId = @RoleId, Status = @Status, ModifiedBy = @ModifiedBy, ModifiedOn = @ModifiedOn WHERE KeyId = @KeyId";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);            
            
            db.AddInParameter(command, "KeyId", DbType.Int32, sys_UserRole.KeyId); 
            db.AddInParameter(command, "ProjID", DbType.Int32, sys_UserRole.ProjID.HasValue ? (object)sys_UserRole.ProjID : DBNull.Value); 
            db.AddInParameter(command, "PersonID", DbType.Int32, sys_UserRole.PersonID); 
            db.AddInParameter(command, "RoleId", DbType.Int32, sys_UserRole.RoleId); 
            db.AddInParameter(command, "Status", DbType.Boolean, sys_UserRole.Status); 
            db.AddInParameter(command, "ModifiedBy", DbType.Int32, sys_UserRole.ModifiedBy.HasValue ? (object)sys_UserRole.ModifiedBy : DBNull.Value); 
            db.AddInParameter(command, "ModifiedOn", DbType.DateTime, sys_UserRole.ModifiedOn.HasValue ? (object)sys_UserRole.ModifiedOn : DBNull.Value);       
            
            return db.ExecuteNonQuery(command);            
        }
        
        /// <summary>
        /// 删除用户角色对应关系记录
        /// </summary>
        /// <param name="keyId">主键</param> 
        /// <returns>受影响的记录数</returns>
        public int DeleteSys_UserRole(int keyId)
        {
            string sql = @"DELETE FROM dbo.Sys_UserRole WHERE KeyId = @KeyId";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);            
            
            db.AddInParameter(command, "KeyId", DbType.Int32, keyId);
                        
            return db.ExecuteNonQuery(command);
        }
    }
}