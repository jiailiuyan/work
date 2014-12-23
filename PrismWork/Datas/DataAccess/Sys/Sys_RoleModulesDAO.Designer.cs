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
    /// 角色功能对应表数据存取类
    /// 生成日期: 2014年10月 27日 20:59
    /// </summary>
    /// <remarks>
    /// 该文件是由代码生成器自动生成的, 请不要随意修改, 你的修改将在代码重新生成时会被覆盖，
    /// 如果要对该类进行修改, 请直接修改该分部类的Sys_RoleModules文件(文件名不含.designer)
    /// </remarks>
    public partial class Sys_RoleModulesDAO : SysBaseDao
    {
       /// <summary>
       ///根据主键值查找角色功能对应表记录
       /// </summary>
       /// <param name="keyId">角色权限主键</param> 
       /// <returns>Sys_RoleModules</returns>
       public Sys_RoleModules FindSys_RoleModules(int keyId)
       {
            string sql = @"SELECT KeyId, ProjID, RoleId, ModulesID, PrivilegeMask, Status, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn FROM dbo.Sys_RoleModules WHERE KeyId = @KeyId";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);                      
            
            db.AddInParameter(command, "KeyId", DbType.Int32, keyId);
                        
            Sys_RoleModules sys_RoleModules = null;
            
            using (IDataReader dr = db.ExecuteReader(command))
            {
                if (dr.Read())
                {
                    sys_RoleModules = new Sys_RoleModules();               
                    
                    sys_RoleModules.KeyId = (int)dr["KeyId"]; 
                    sys_RoleModules.ProjID = (int)dr["ProjID"]; 
                    sys_RoleModules.RoleId = (int)dr["RoleId"]; 
                    sys_RoleModules.ModulesID = (int)dr["ModulesID"]; 
                    sys_RoleModules.PrivilegeMask = (int)dr["PrivilegeMask"]; 
                    sys_RoleModules.Status = (bool)dr["Status"]; 
                    sys_RoleModules.CreatedBy = (int)dr["CreatedBy"]; 
                    sys_RoleModules.CreatedOn = (DateTime)dr["CreatedOn"]; 
                    sys_RoleModules.ModifiedBy = dr["ModifiedBy"] == DBNull.Value ? null : (int?)dr["ModifiedBy"]; 
                    sys_RoleModules.ModifiedOn = dr["ModifiedOn"] == DBNull.Value ? null : (DateTime?)dr["ModifiedOn"];                                    
                }
            }   
                   
            return sys_RoleModules; 
       }
       
       /// <summary>
       /// 获取全部角色功能对应表列表
       /// </summary>
       /// <returns>Sys_RoleModules对象列表</returns>
       public IList<Sys_RoleModules> GetSys_RoleModuless()
       {
           return GetSys_RoleModuless(null);
       } 
       
        /// <summary>
	 	/// 返回满足查询条件的角色功能对应表实体列表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>角色功能对应表实体列表</returns>
        public IList<Sys_RoleModules> GetSys_RoleModuless(QueryParameter param)
        {
            string sql = @"SELECT KeyId, ProjID, RoleId, ModulesID, PrivilegeMask, Status, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn FROM dbo.Sys_RoleModules";

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
            
            IList<Sys_RoleModules> list = new List<Sys_RoleModules>();
                        
            using (IDataReader dr = db.ExecuteReader(command))
            {
                while (dr.Read())
                {
                    Sys_RoleModules sys_RoleModules = new Sys_RoleModules();                                
                    
                    sys_RoleModules.KeyId = (int)dr["KeyId"]; 
                    sys_RoleModules.ProjID = (int)dr["ProjID"]; 
                    sys_RoleModules.RoleId = (int)dr["RoleId"]; 
                    sys_RoleModules.ModulesID = (int)dr["ModulesID"]; 
                    sys_RoleModules.PrivilegeMask = (int)dr["PrivilegeMask"]; 
                    sys_RoleModules.Status = (bool)dr["Status"]; 
                    sys_RoleModules.CreatedBy = (int)dr["CreatedBy"]; 
                    sys_RoleModules.CreatedOn = (DateTime)dr["CreatedOn"]; 
                    sys_RoleModules.ModifiedBy = dr["ModifiedBy"] == DBNull.Value ? null : (int?)dr["ModifiedBy"]; 
                    sys_RoleModules.ModifiedOn = dr["ModifiedOn"] == DBNull.Value ? null : (DateTime?)dr["ModifiedOn"];                   
                    
                    list.Add(sys_RoleModules);
                }
            }   
                   
            return list; 
        }
        
        /// <summary>
        /// 返回满足查询条件的角色功能对应表数据表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>角色功能对应表数据表</returns>
        public DataTable GetSys_RoleModulesTable(QueryParameter param)
        {
            string sql = @"SELECT KeyId, ProjID, RoleId, ModulesID, PrivilegeMask, Status, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn FROM dbo.Sys_RoleModules";

            return GetDataTable(sql, param);
        }
        
        /// <summary>
        /// 插入角色功能对应表记录
        /// </summary>
        /// <param name="sys_RoleModules">角色功能对应表对象</param>
        /// <returns></returns>
        public int InsertSys_RoleModules(Sys_RoleModules sys_RoleModules)
        {
            string sql = @"INSERT INTO dbo.Sys_RoleModules(ProjID, RoleId, ModulesID, PrivilegeMask, Status, CreatedBy) VALUES(@ProjID, @RoleId, @ModulesID, @PrivilegeMask, @Status, @CreatedBy); SELECT @KeyId = SCOPE_IDENTITY()";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);                        
            
            db.AddOutParameter(command, "KeyId", DbType.Int32, sizeof(int)); 
            db.AddInParameter(command, "ProjID", DbType.Int32, sys_RoleModules.ProjID); 
            db.AddInParameter(command, "RoleId", DbType.Int32, sys_RoleModules.RoleId); 
            db.AddInParameter(command, "ModulesID", DbType.Int32, sys_RoleModules.ModulesID); 
            db.AddInParameter(command, "PrivilegeMask", DbType.Int32, sys_RoleModules.PrivilegeMask); 
            db.AddInParameter(command, "Status", DbType.Boolean, sys_RoleModules.Status); 
            db.AddInParameter(command, "CreatedBy", DbType.Int32, sys_RoleModules.CreatedBy); 
            
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
        /// 更新角色功能对应表记录
        /// </summary>
        /// <param name="sys_RoleModules">角色功能对应表对象</param>
        /// <returns>受影响的记录数</returns>
        public int UpdateSys_RoleModules(Sys_RoleModules sys_RoleModules)
        {
            string sql = @"UPDATE dbo.Sys_RoleModules SET ProjID = @ProjID, RoleId = @RoleId, ModulesID = @ModulesID, PrivilegeMask = @PrivilegeMask, Status = @Status, ModifiedBy = @ModifiedBy, ModifiedOn = @ModifiedOn WHERE KeyId = @KeyId";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);            
            
            db.AddInParameter(command, "KeyId", DbType.Int32, sys_RoleModules.KeyId); 
            db.AddInParameter(command, "ProjID", DbType.Int32, sys_RoleModules.ProjID); 
            db.AddInParameter(command, "RoleId", DbType.Int32, sys_RoleModules.RoleId); 
            db.AddInParameter(command, "ModulesID", DbType.Int32, sys_RoleModules.ModulesID); 
            db.AddInParameter(command, "PrivilegeMask", DbType.Int32, sys_RoleModules.PrivilegeMask); 
            db.AddInParameter(command, "Status", DbType.Boolean, sys_RoleModules.Status); 
            db.AddInParameter(command, "ModifiedBy", DbType.Int32, sys_RoleModules.ModifiedBy.HasValue ? (object)sys_RoleModules.ModifiedBy : DBNull.Value); 
            db.AddInParameter(command, "ModifiedOn", DbType.DateTime, sys_RoleModules.ModifiedOn.HasValue ? (object)sys_RoleModules.ModifiedOn : DBNull.Value);       
            
            return db.ExecuteNonQuery(command);            
        }
        
        /// <summary>
        /// 删除角色功能对应表记录
        /// </summary>
        /// <param name="keyId">角色权限主键</param> 
        /// <returns>受影响的记录数</returns>
        public int DeleteSys_RoleModules(int keyId)
        {
            string sql = @"DELETE FROM dbo.Sys_RoleModules WHERE KeyId = @KeyId";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);            
            
            db.AddInParameter(command, "KeyId", DbType.Int32, keyId);
                        
            return db.ExecuteNonQuery(command);
        }
    }
}