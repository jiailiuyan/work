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
    /// 项目模块数据存取类
    /// 生成日期: 2014年10月 27日 21:50
    /// </summary>
    /// <remarks>
    /// 该文件是由代码生成器自动生成的, 请不要随意修改, 你的修改将在代码重新生成时会被覆盖，
    /// 如果要对该类进行修改, 请直接修改该分部类的Sys_Modules文件(文件名不含.designer)
    /// </remarks>
    public partial class Sys_ModulesDAO : SysBaseDao
    {
       /// <summary>
       ///根据主键值查找项目模块记录
       /// </summary>
       /// <param name="keyId">模块内码</param> 
       /// <returns>Sys_Modules</returns>
       public Sys_Modules FindSys_Modules(int keyId)
       {
            string sql = @"SELECT KeyId, ProjID, ModuleCode, ModuleName, ShortName, ParentId, UrlString, ModuleEntry, ModuleIconS, ModuleIconB, IsShowInDeskTop, OpenType, Status, Hint, DisplayOrder, DisplayPrivilegeMask, HelpUrlString, Remark, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn FROM dbo.Sys_Modules WHERE KeyId = @KeyId";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);                      
            
            db.AddInParameter(command, "KeyId", DbType.Int32, keyId);
                        
            Sys_Modules sys_Modules = null;
            
            using (IDataReader dr = db.ExecuteReader(command))
            {
                if (dr.Read())
                {
                    sys_Modules = new Sys_Modules();               
                    
                    sys_Modules.KeyId = (int)dr["KeyId"]; 
                    sys_Modules.ProjID = (int)dr["ProjID"]; 
                    sys_Modules.ModuleCode = (string)dr["ModuleCode"]; 
                    sys_Modules.ModuleName = (string)dr["ModuleName"]; 
                    sys_Modules.ShortName = dr["ShortName"] == DBNull.Value ? null : (string)dr["ShortName"]; 
                    sys_Modules.ParentId = dr["ParentId"] == DBNull.Value ? null : (int?)dr["ParentId"]; 
                    sys_Modules.UrlString = dr["UrlString"] == DBNull.Value ? null : (string)dr["UrlString"]; 
                    sys_Modules.ModuleEntry = dr["ModuleEntry"] == DBNull.Value ? null : (string)dr["ModuleEntry"]; 
                    sys_Modules.ModuleIconS = dr["ModuleIconS"] == DBNull.Value ? null : (string)dr["ModuleIconS"]; 
                    sys_Modules.ModuleIconB = dr["ModuleIconB"] == DBNull.Value ? null : (string)dr["ModuleIconB"]; 
                    sys_Modules.IsShowInDeskTop = (bool)dr["IsShowInDeskTop"]; 
                    sys_Modules.OpenType = dr["OpenType"] == DBNull.Value ? null : (int?)dr["OpenType"]; 
                    sys_Modules.Status = (bool)dr["Status"]; 
                    sys_Modules.Hint = dr["Hint"] == DBNull.Value ? null : (string)dr["Hint"]; 
                    sys_Modules.DisplayOrder = dr["DisplayOrder"] == DBNull.Value ? null : (int?)dr["DisplayOrder"]; 
                    sys_Modules.DisplayPrivilegeMask = dr["DisplayPrivilegeMask"] == DBNull.Value ? null : (int?)dr["DisplayPrivilegeMask"]; 
                    sys_Modules.HelpUrlString = dr["HelpUrlString"] == DBNull.Value ? null : (string)dr["HelpUrlString"]; 
                    sys_Modules.Remark = dr["Remark"] == DBNull.Value ? null : (string)dr["Remark"]; 
                    sys_Modules.CreatedBy = (int)dr["CreatedBy"]; 
                    sys_Modules.CreatedOn = (DateTime)dr["CreatedOn"]; 
                    sys_Modules.ModifiedBy = dr["ModifiedBy"] == DBNull.Value ? null : (int?)dr["ModifiedBy"]; 
                    sys_Modules.ModifiedOn = dr["ModifiedOn"] == DBNull.Value ? null : (DateTime?)dr["ModifiedOn"];                                    
                }
            }   
                   
            return sys_Modules; 
       }
       
       /// <summary>
       /// 获取全部项目模块列表
       /// </summary>
       /// <returns>Sys_Modules对象列表</returns>
       public IList<Sys_Modules> GetSys_Moduless()
       {
           return GetSys_Moduless(null);
       } 
       
        /// <summary>
	 	/// 返回满足查询条件的项目模块实体列表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>项目模块实体列表</returns>
        public IList<Sys_Modules> GetSys_Moduless(QueryParameter param)
        {
            string sql = @"SELECT KeyId, ProjID, ModuleCode, ModuleName, ShortName, ParentId, UrlString, ModuleEntry, ModuleIconS, ModuleIconB, IsShowInDeskTop, OpenType, Status, Hint, DisplayOrder, DisplayPrivilegeMask, HelpUrlString, Remark, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn FROM dbo.Sys_Modules";

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
            
            IList<Sys_Modules> list = new List<Sys_Modules>();
                        
            using (IDataReader dr = db.ExecuteReader(command))
            {
                while (dr.Read())
                {
                    Sys_Modules sys_Modules = new Sys_Modules();                                
                    
                    sys_Modules.KeyId = (int)dr["KeyId"]; 
                    sys_Modules.ProjID = (int)dr["ProjID"]; 
                    sys_Modules.ModuleCode = (string)dr["ModuleCode"]; 
                    sys_Modules.ModuleName = (string)dr["ModuleName"]; 
                    sys_Modules.ShortName = dr["ShortName"] == DBNull.Value ? null : (string)dr["ShortName"]; 
                    sys_Modules.ParentId = dr["ParentId"] == DBNull.Value ? null : (int?)dr["ParentId"]; 
                    sys_Modules.UrlString = dr["UrlString"] == DBNull.Value ? null : (string)dr["UrlString"]; 
                    sys_Modules.ModuleEntry = dr["ModuleEntry"] == DBNull.Value ? null : (string)dr["ModuleEntry"]; 
                    sys_Modules.ModuleIconS = dr["ModuleIconS"] == DBNull.Value ? null : (string)dr["ModuleIconS"]; 
                    sys_Modules.ModuleIconB = dr["ModuleIconB"] == DBNull.Value ? null : (string)dr["ModuleIconB"]; 
                    sys_Modules.IsShowInDeskTop = (bool)dr["IsShowInDeskTop"]; 
                    sys_Modules.OpenType = dr["OpenType"] == DBNull.Value ? null : (int?)dr["OpenType"]; 
                    sys_Modules.Status = (bool)dr["Status"]; 
                    sys_Modules.Hint = dr["Hint"] == DBNull.Value ? null : (string)dr["Hint"]; 
                    sys_Modules.DisplayOrder = dr["DisplayOrder"] == DBNull.Value ? null : (int?)dr["DisplayOrder"]; 
                    sys_Modules.DisplayPrivilegeMask = dr["DisplayPrivilegeMask"] == DBNull.Value ? null : (int?)dr["DisplayPrivilegeMask"]; 
                    sys_Modules.HelpUrlString = dr["HelpUrlString"] == DBNull.Value ? null : (string)dr["HelpUrlString"]; 
                    sys_Modules.Remark = dr["Remark"] == DBNull.Value ? null : (string)dr["Remark"]; 
                    sys_Modules.CreatedBy = (int)dr["CreatedBy"]; 
                    sys_Modules.CreatedOn = (DateTime)dr["CreatedOn"]; 
                    sys_Modules.ModifiedBy = dr["ModifiedBy"] == DBNull.Value ? null : (int?)dr["ModifiedBy"]; 
                    sys_Modules.ModifiedOn = dr["ModifiedOn"] == DBNull.Value ? null : (DateTime?)dr["ModifiedOn"];                   
                    
                    list.Add(sys_Modules);
                }
            }   
                   
            return list; 
        }
        
        /// <summary>
        /// 返回满足查询条件的项目模块数据表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>项目模块数据表</returns>
        public DataTable GetSys_ModulesTable(QueryParameter param)
        {
            string sql = @"SELECT KeyId, ProjID, ModuleCode, ModuleName, ShortName, ParentId, UrlString, ModuleEntry, ModuleIconS, ModuleIconB, IsShowInDeskTop, OpenType, Status, Hint, DisplayOrder, DisplayPrivilegeMask, HelpUrlString, Remark, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn FROM dbo.Sys_Modules";

            return GetDataTable(sql, param);
        }
        
        /// <summary>
        /// 插入项目模块记录
        /// </summary>
        /// <param name="sys_Modules">项目模块对象</param>
        /// <returns></returns>
        public int InsertSys_Modules(Sys_Modules sys_Modules)
        {
            string sql = @"INSERT INTO dbo.Sys_Modules(ProjID, ModuleCode, ModuleName, ShortName, ParentId, UrlString, ModuleEntry, ModuleIconS, ModuleIconB, IsShowInDeskTop, OpenType, Status, Hint, DisplayOrder, DisplayPrivilegeMask, HelpUrlString, Remark, CreatedBy) VALUES(@ProjID, @ModuleCode, @ModuleName, @ShortName, @ParentId, @UrlString, @ModuleEntry, @ModuleIconS, @ModuleIconB, @IsShowInDeskTop, @OpenType, @Status, @Hint, @DisplayOrder, @DisplayPrivilegeMask, @HelpUrlString, @Remark, @CreatedBy); SELECT @KeyId = SCOPE_IDENTITY()";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);                        
            
            db.AddOutParameter(command, "KeyId", DbType.Int32, sizeof(int)); 
            db.AddInParameter(command, "ProjID", DbType.Int32, sys_Modules.ProjID); 
            db.AddInParameter(command, "ModuleCode", DbType.String, sys_Modules.ModuleCode); 
            db.AddInParameter(command, "ModuleName", DbType.String, sys_Modules.ModuleName); 
            db.AddInParameter(command, "ShortName", DbType.String, string.IsNullOrEmpty(sys_Modules.ShortName) ? DBNull.Value : (object)sys_Modules.ShortName); 
            db.AddInParameter(command, "ParentId", DbType.Int32, sys_Modules.ParentId.HasValue ? (object)sys_Modules.ParentId : DBNull.Value); 
            db.AddInParameter(command, "UrlString", DbType.String, string.IsNullOrEmpty(sys_Modules.UrlString) ? DBNull.Value : (object)sys_Modules.UrlString); 
            db.AddInParameter(command, "ModuleEntry", DbType.String, string.IsNullOrEmpty(sys_Modules.ModuleEntry) ? DBNull.Value : (object)sys_Modules.ModuleEntry); 
            db.AddInParameter(command, "ModuleIconS", DbType.String, string.IsNullOrEmpty(sys_Modules.ModuleIconS) ? DBNull.Value : (object)sys_Modules.ModuleIconS); 
            db.AddInParameter(command, "ModuleIconB", DbType.String, string.IsNullOrEmpty(sys_Modules.ModuleIconB) ? DBNull.Value : (object)sys_Modules.ModuleIconB); 
            db.AddInParameter(command, "IsShowInDeskTop", DbType.Boolean, sys_Modules.IsShowInDeskTop); 
            db.AddInParameter(command, "OpenType", DbType.Int32, sys_Modules.OpenType.HasValue ? (object)sys_Modules.OpenType : DBNull.Value); 
            db.AddInParameter(command, "Status", DbType.Boolean, sys_Modules.Status); 
            db.AddInParameter(command, "Hint", DbType.String, string.IsNullOrEmpty(sys_Modules.Hint) ? DBNull.Value : (object)sys_Modules.Hint); 
            db.AddInParameter(command, "DisplayOrder", DbType.Int32, sys_Modules.DisplayOrder.HasValue ? (object)sys_Modules.DisplayOrder : DBNull.Value); 
            db.AddInParameter(command, "DisplayPrivilegeMask", DbType.Int32, sys_Modules.DisplayPrivilegeMask.HasValue ? (object)sys_Modules.DisplayPrivilegeMask : DBNull.Value); 
            db.AddInParameter(command, "HelpUrlString", DbType.String, string.IsNullOrEmpty(sys_Modules.HelpUrlString) ? DBNull.Value : (object)sys_Modules.HelpUrlString); 
            db.AddInParameter(command, "Remark", DbType.String, string.IsNullOrEmpty(sys_Modules.Remark) ? DBNull.Value : (object)sys_Modules.Remark); 
            db.AddInParameter(command, "CreatedBy", DbType.Int32, sys_Modules.CreatedBy); 
            
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
        /// 更新项目模块记录
        /// </summary>
        /// <param name="sys_Modules">项目模块对象</param>
        /// <returns>受影响的记录数</returns>
        public int UpdateSys_Modules(Sys_Modules sys_Modules)
        {
            string sql = @"UPDATE dbo.Sys_Modules SET ProjID = @ProjID, ModuleCode = @ModuleCode, ModuleName = @ModuleName, ShortName = @ShortName, ParentId = @ParentId, UrlString = @UrlString, ModuleEntry = @ModuleEntry, ModuleIconS = @ModuleIconS, ModuleIconB = @ModuleIconB, IsShowInDeskTop = @IsShowInDeskTop, OpenType = @OpenType, Status = @Status, Hint = @Hint, DisplayOrder = @DisplayOrder, DisplayPrivilegeMask = @DisplayPrivilegeMask, HelpUrlString = @HelpUrlString, Remark = @Remark, ModifiedBy = @ModifiedBy, ModifiedOn = @ModifiedOn WHERE KeyId = @KeyId";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);            
            
            db.AddInParameter(command, "KeyId", DbType.Int32, sys_Modules.KeyId); 
            db.AddInParameter(command, "ProjID", DbType.Int32, sys_Modules.ProjID); 
            db.AddInParameter(command, "ModuleCode", DbType.String, sys_Modules.ModuleCode); 
            db.AddInParameter(command, "ModuleName", DbType.String, sys_Modules.ModuleName); 
            db.AddInParameter(command, "ShortName", DbType.String, string.IsNullOrEmpty(sys_Modules.ShortName) ? DBNull.Value : (object)sys_Modules.ShortName); 
            db.AddInParameter(command, "ParentId", DbType.Int32, sys_Modules.ParentId.HasValue ? (object)sys_Modules.ParentId : DBNull.Value); 
            db.AddInParameter(command, "UrlString", DbType.String, string.IsNullOrEmpty(sys_Modules.UrlString) ? DBNull.Value : (object)sys_Modules.UrlString); 
            db.AddInParameter(command, "ModuleEntry", DbType.String, string.IsNullOrEmpty(sys_Modules.ModuleEntry) ? DBNull.Value : (object)sys_Modules.ModuleEntry); 
            db.AddInParameter(command, "ModuleIconS", DbType.String, string.IsNullOrEmpty(sys_Modules.ModuleIconS) ? DBNull.Value : (object)sys_Modules.ModuleIconS); 
            db.AddInParameter(command, "ModuleIconB", DbType.String, string.IsNullOrEmpty(sys_Modules.ModuleIconB) ? DBNull.Value : (object)sys_Modules.ModuleIconB); 
            db.AddInParameter(command, "IsShowInDeskTop", DbType.Boolean, sys_Modules.IsShowInDeskTop); 
            db.AddInParameter(command, "OpenType", DbType.Int32, sys_Modules.OpenType.HasValue ? (object)sys_Modules.OpenType : DBNull.Value); 
            db.AddInParameter(command, "Status", DbType.Boolean, sys_Modules.Status); 
            db.AddInParameter(command, "Hint", DbType.String, string.IsNullOrEmpty(sys_Modules.Hint) ? DBNull.Value : (object)sys_Modules.Hint); 
            db.AddInParameter(command, "DisplayOrder", DbType.Int32, sys_Modules.DisplayOrder.HasValue ? (object)sys_Modules.DisplayOrder : DBNull.Value); 
            db.AddInParameter(command, "DisplayPrivilegeMask", DbType.Int32, sys_Modules.DisplayPrivilegeMask.HasValue ? (object)sys_Modules.DisplayPrivilegeMask : DBNull.Value); 
            db.AddInParameter(command, "HelpUrlString", DbType.String, string.IsNullOrEmpty(sys_Modules.HelpUrlString) ? DBNull.Value : (object)sys_Modules.HelpUrlString); 
            db.AddInParameter(command, "Remark", DbType.String, string.IsNullOrEmpty(sys_Modules.Remark) ? DBNull.Value : (object)sys_Modules.Remark); 
            db.AddInParameter(command, "ModifiedBy", DbType.Int32, sys_Modules.ModifiedBy.HasValue ? (object)sys_Modules.ModifiedBy : DBNull.Value); 
            db.AddInParameter(command, "ModifiedOn", DbType.DateTime, sys_Modules.ModifiedOn.HasValue ? (object)sys_Modules.ModifiedOn : DBNull.Value);       
            
            return db.ExecuteNonQuery(command);            
        }
        
        /// <summary>
        /// 删除项目模块记录
        /// </summary>
        /// <param name="keyId">模块内码</param> 
        /// <returns>受影响的记录数</returns>
        public int DeleteSys_Modules(int keyId)
        {
            string sql = @"DELETE FROM dbo.Sys_Modules WHERE KeyId = @KeyId";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);            
            
            db.AddInParameter(command, "KeyId", DbType.Int32, keyId);
                        
            return db.ExecuteNonQuery(command);
        }
    }
}