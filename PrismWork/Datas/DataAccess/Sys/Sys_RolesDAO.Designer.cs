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
    /// 项目角色数据存取类
    /// 生成日期: 2014年10月 27日 20:45
    /// </summary>
    /// <remarks>
    /// 该文件是由代码生成器自动生成的, 请不要随意修改, 你的修改将在代码重新生成时会被覆盖，
    /// 如果要对该类进行修改, 请直接修改该分部类的Sys_Roles文件(文件名不含.designer)
    /// </remarks>
    public partial class Sys_RolesDAO : SysBaseDao
    {
       /// <summary>
       ///根据主键值查找项目角色记录
       /// </summary>
       /// <param name="keyId">主键</param> 
       /// <returns>Sys_Roles</returns>
       public Sys_Roles FindSys_Roles(int keyId)
       {
            string sql = @"SELECT KeyId, ProjID, RoleName, RoleCode, Status, Remark, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn FROM dbo.Sys_Roles WHERE KeyId = @KeyId";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);                      
            
            db.AddInParameter(command, "KeyId", DbType.Int32, keyId);
                        
            Sys_Roles sys_Roles = null;
            
            using (IDataReader dr = db.ExecuteReader(command))
            {
                if (dr.Read())
                {
                    sys_Roles = new Sys_Roles();               
                    
                    sys_Roles.KeyId = (int)dr["KeyId"]; 
                    sys_Roles.ProjID = (int)dr["ProjID"]; 
                    sys_Roles.RoleName = (string)dr["RoleName"]; 
                    sys_Roles.RoleCode = (string)dr["RoleCode"]; 
                    sys_Roles.Status = (bool)dr["Status"]; 
                    sys_Roles.Remark = dr["Remark"] == DBNull.Value ? null : (string)dr["Remark"]; 
                    sys_Roles.CreatedBy = (int)dr["CreatedBy"]; 
                    sys_Roles.CreatedOn = (DateTime)dr["CreatedOn"]; 
                    sys_Roles.ModifiedBy = dr["ModifiedBy"] == DBNull.Value ? null : (int?)dr["ModifiedBy"]; 
                    sys_Roles.ModifiedOn = dr["ModifiedOn"] == DBNull.Value ? null : (DateTime?)dr["ModifiedOn"];                                    
                }
            }   
                   
            return sys_Roles; 
       }
       
       /// <summary>
       /// 获取全部项目角色列表
       /// </summary>
       /// <returns>Sys_Roles对象列表</returns>
       public IList<Sys_Roles> GetSys_Roless()
       {
           return GetSys_Roless(null);
       } 
       
        /// <summary>
	 	/// 返回满足查询条件的项目角色实体列表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>项目角色实体列表</returns>
        public IList<Sys_Roles> GetSys_Roless(QueryParameter param)
        {
            string sql = @"SELECT KeyId, ProjID, RoleName, RoleCode, Status, Remark, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn FROM dbo.Sys_Roles";

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
            
            IList<Sys_Roles> list = new List<Sys_Roles>();
                        
            using (IDataReader dr = db.ExecuteReader(command))
            {
                while (dr.Read())
                {
                    Sys_Roles sys_Roles = new Sys_Roles();                                
                    
                    sys_Roles.KeyId = (int)dr["KeyId"]; 
                    sys_Roles.ProjID = (int)dr["ProjID"]; 
                    sys_Roles.RoleName = (string)dr["RoleName"]; 
                    sys_Roles.RoleCode = (string)dr["RoleCode"]; 
                    sys_Roles.Status = (bool)dr["Status"]; 
                    sys_Roles.Remark = dr["Remark"] == DBNull.Value ? null : (string)dr["Remark"]; 
                    sys_Roles.CreatedBy = (int)dr["CreatedBy"]; 
                    sys_Roles.CreatedOn = (DateTime)dr["CreatedOn"]; 
                    sys_Roles.ModifiedBy = dr["ModifiedBy"] == DBNull.Value ? null : (int?)dr["ModifiedBy"]; 
                    sys_Roles.ModifiedOn = dr["ModifiedOn"] == DBNull.Value ? null : (DateTime?)dr["ModifiedOn"];                   
                    
                    list.Add(sys_Roles);
                }
            }   
                   
            return list; 
        }
        
        /// <summary>
        /// 返回满足查询条件的项目角色数据表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>项目角色数据表</returns>
        public DataTable GetSys_RolesTable(QueryParameter param)
        {
            string sql = @"SELECT KeyId, ProjID, RoleName, RoleCode, Status, Remark, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn FROM dbo.Sys_Roles";

            return GetDataTable(sql, param);
        }
        
        /// <summary>
        /// 插入项目角色记录
        /// </summary>
        /// <param name="sys_Roles">项目角色对象</param>
        /// <returns></returns>
        public int InsertSys_Roles(Sys_Roles sys_Roles)
        {
            string sql = @"INSERT INTO dbo.Sys_Roles(ProjID, RoleName, RoleCode, Status, Remark, CreatedBy) VALUES(@ProjID, @RoleName, @RoleCode, @Status, @Remark, @CreatedBy); SELECT @KeyId = SCOPE_IDENTITY()";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);                        
            
            db.AddOutParameter(command, "KeyId", DbType.Int32, sizeof(int)); 
            db.AddInParameter(command, "ProjID", DbType.Int32, sys_Roles.ProjID); 
            db.AddInParameter(command, "RoleName", DbType.String, sys_Roles.RoleName); 
            db.AddInParameter(command, "RoleCode", DbType.String, sys_Roles.RoleCode); 
            db.AddInParameter(command, "Status", DbType.Boolean, sys_Roles.Status); 
            db.AddInParameter(command, "Remark", DbType.String, string.IsNullOrEmpty(sys_Roles.Remark) ? DBNull.Value : (object)sys_Roles.Remark); 
            db.AddInParameter(command, "CreatedBy", DbType.Int32, sys_Roles.CreatedBy); 
            
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
        /// 更新项目角色记录
        /// </summary>
        /// <param name="sys_Roles">项目角色对象</param>
        /// <returns>受影响的记录数</returns>
        public int UpdateSys_Roles(Sys_Roles sys_Roles)
        {
            string sql = @"UPDATE dbo.Sys_Roles SET ProjID = @ProjID, RoleName = @RoleName, RoleCode = @RoleCode, Status = @Status, Remark = @Remark, ModifiedBy = @ModifiedBy, ModifiedOn = @ModifiedOn WHERE KeyId = @KeyId";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);            
            
            db.AddInParameter(command, "KeyId", DbType.Int32, sys_Roles.KeyId); 
            db.AddInParameter(command, "ProjID", DbType.Int32, sys_Roles.ProjID); 
            db.AddInParameter(command, "RoleName", DbType.String, sys_Roles.RoleName); 
            db.AddInParameter(command, "RoleCode", DbType.String, sys_Roles.RoleCode); 
            db.AddInParameter(command, "Status", DbType.Boolean, sys_Roles.Status); 
            db.AddInParameter(command, "Remark", DbType.String, string.IsNullOrEmpty(sys_Roles.Remark) ? DBNull.Value : (object)sys_Roles.Remark); 
            db.AddInParameter(command, "ModifiedBy", DbType.Int32, sys_Roles.ModifiedBy.HasValue ? (object)sys_Roles.ModifiedBy : DBNull.Value); 
            db.AddInParameter(command, "ModifiedOn", DbType.DateTime, sys_Roles.ModifiedOn.HasValue ? (object)sys_Roles.ModifiedOn : DBNull.Value);       
            
            return db.ExecuteNonQuery(command);            
        }
        
        /// <summary>
        /// 删除项目角色记录
        /// </summary>
        /// <param name="keyId">主键</param> 
        /// <returns>受影响的记录数</returns>
        public int DeleteSys_Roles(int keyId)
        {
            string sql = @"DELETE FROM dbo.Sys_Roles WHERE KeyId = @KeyId";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);            
            
            db.AddInParameter(command, "KeyId", DbType.Int32, keyId);
                        
            return db.ExecuteNonQuery(command);
        }
    }
}