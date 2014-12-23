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
    /// 业务单位类型,该表不做显示维护数据存取类
    /// 生成日期: 2014年10月 27日 21:07
    /// </summary>
    /// <remarks>
    /// 该文件是由代码生成器自动生成的, 请不要随意修改, 你的修改将在代码重新生成时会被覆盖，
    /// 如果要对该类进行修改, 请直接修改该分部类的Sys_BusiUnitType文件(文件名不含.designer)
    /// </remarks>
    public partial class Sys_BusiUnitTypeDAO : SysBaseDao
    {
       /// <summary>
       ///根据主键值查找业务单位类型,该表不做显示维护记录
       /// </summary>
       /// <param name="keyId">业务单位类型内码</param> 
       /// <returns>Sys_BusiUnitType</returns>
       public Sys_BusiUnitType FindSys_BusiUnitType(int keyId)
       {
            string sql = @"SELECT KeyId, BusiUnitTypeCode, BusiUnitTypeName, BusiUnitTypeShortName, ParentID, Status, DisplayOrder, Remark, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn FROM dbo.Sys_BusiUnitType WHERE KeyId = @KeyId";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);                      
            
            db.AddInParameter(command, "KeyId", DbType.Int32, keyId);
                        
            Sys_BusiUnitType sys_BusiUnitType = null;
            
            using (IDataReader dr = db.ExecuteReader(command))
            {
                if (dr.Read())
                {
                    sys_BusiUnitType = new Sys_BusiUnitType();               
                    
                    sys_BusiUnitType.KeyId = (int)dr["KeyId"]; 
                    sys_BusiUnitType.BusiUnitTypeCode = dr["BusiUnitTypeCode"] == DBNull.Value ? null : (string)dr["BusiUnitTypeCode"]; 
                    sys_BusiUnitType.BusiUnitTypeName = (string)dr["BusiUnitTypeName"]; 
                    sys_BusiUnitType.BusiUnitTypeShortName = dr["BusiUnitTypeShortName"] == DBNull.Value ? null : (string)dr["BusiUnitTypeShortName"]; 
                    sys_BusiUnitType.ParentID = dr["ParentID"] == DBNull.Value ? null : (int?)dr["ParentID"]; 
                    sys_BusiUnitType.Status = (bool)dr["Status"]; 
                    sys_BusiUnitType.DisplayOrder = (int)dr["DisplayOrder"]; 
                    sys_BusiUnitType.Remark = dr["Remark"] == DBNull.Value ? null : (string)dr["Remark"]; 
                    sys_BusiUnitType.CreatedBy = (int)dr["CreatedBy"]; 
                    sys_BusiUnitType.CreatedOn = (DateTime)dr["CreatedOn"]; 
                    sys_BusiUnitType.ModifiedBy = dr["ModifiedBy"] == DBNull.Value ? null : (int?)dr["ModifiedBy"]; 
                    sys_BusiUnitType.ModifiedOn = dr["ModifiedOn"] == DBNull.Value ? null : (DateTime?)dr["ModifiedOn"];                                    
                }
            }   
                   
            return sys_BusiUnitType; 
       }
       
       /// <summary>
       /// 获取全部业务单位类型,该表不做显示维护列表
       /// </summary>
       /// <returns>Sys_BusiUnitType对象列表</returns>
       public IList<Sys_BusiUnitType> GetSys_BusiUnitTypes()
       {
           return GetSys_BusiUnitTypes(null);
       } 
       
        /// <summary>
	 	/// 返回满足查询条件的业务单位类型,该表不做显示维护实体列表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>业务单位类型,该表不做显示维护实体列表</returns>
        public IList<Sys_BusiUnitType> GetSys_BusiUnitTypes(QueryParameter param)
        {
            string sql = @"SELECT KeyId, BusiUnitTypeCode, BusiUnitTypeName, BusiUnitTypeShortName, ParentID, Status, DisplayOrder, Remark, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn FROM dbo.Sys_BusiUnitType";

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
            
            IList<Sys_BusiUnitType> list = new List<Sys_BusiUnitType>();
                        
            using (IDataReader dr = db.ExecuteReader(command))
            {
                while (dr.Read())
                {
                    Sys_BusiUnitType sys_BusiUnitType = new Sys_BusiUnitType();                                
                    
                    sys_BusiUnitType.KeyId = (int)dr["KeyId"]; 
                    sys_BusiUnitType.BusiUnitTypeCode = dr["BusiUnitTypeCode"] == DBNull.Value ? null : (string)dr["BusiUnitTypeCode"]; 
                    sys_BusiUnitType.BusiUnitTypeName = (string)dr["BusiUnitTypeName"]; 
                    sys_BusiUnitType.BusiUnitTypeShortName = dr["BusiUnitTypeShortName"] == DBNull.Value ? null : (string)dr["BusiUnitTypeShortName"]; 
                    sys_BusiUnitType.ParentID = dr["ParentID"] == DBNull.Value ? null : (int?)dr["ParentID"]; 
                    sys_BusiUnitType.Status = (bool)dr["Status"]; 
                    sys_BusiUnitType.DisplayOrder = (int)dr["DisplayOrder"]; 
                    sys_BusiUnitType.Remark = dr["Remark"] == DBNull.Value ? null : (string)dr["Remark"]; 
                    sys_BusiUnitType.CreatedBy = (int)dr["CreatedBy"]; 
                    sys_BusiUnitType.CreatedOn = (DateTime)dr["CreatedOn"]; 
                    sys_BusiUnitType.ModifiedBy = dr["ModifiedBy"] == DBNull.Value ? null : (int?)dr["ModifiedBy"]; 
                    sys_BusiUnitType.ModifiedOn = dr["ModifiedOn"] == DBNull.Value ? null : (DateTime?)dr["ModifiedOn"];                   
                    
                    list.Add(sys_BusiUnitType);
                }
            }   
                   
            return list; 
        }
        
        /// <summary>
        /// 返回满足查询条件的业务单位类型,该表不做显示维护数据表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>业务单位类型,该表不做显示维护数据表</returns>
        public DataTable GetSys_BusiUnitTypeTable(QueryParameter param)
        {
            string sql = @"SELECT KeyId, BusiUnitTypeCode, BusiUnitTypeName, BusiUnitTypeShortName, ParentID, Status, DisplayOrder, Remark, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn FROM dbo.Sys_BusiUnitType";

            return GetDataTable(sql, param);
        }
        
        /// <summary>
        /// 插入业务单位类型,该表不做显示维护记录
        /// </summary>
        /// <param name="sys_BusiUnitType">业务单位类型,该表不做显示维护对象</param>
        /// <returns></returns>
        public int InsertSys_BusiUnitType(Sys_BusiUnitType sys_BusiUnitType)
        {
            string sql = @"INSERT INTO dbo.Sys_BusiUnitType(BusiUnitTypeCode, BusiUnitTypeName, BusiUnitTypeShortName, ParentID, Status, DisplayOrder, Remark, CreatedBy) VALUES(@BusiUnitTypeCode, @BusiUnitTypeName, @BusiUnitTypeShortName, @ParentID, @Status, @DisplayOrder, @Remark, @CreatedBy); SELECT @KeyId = SCOPE_IDENTITY()";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);                        
            
            db.AddOutParameter(command, "KeyId", DbType.Int32, sizeof(int)); 
            db.AddInParameter(command, "BusiUnitTypeCode", DbType.String, string.IsNullOrEmpty(sys_BusiUnitType.BusiUnitTypeCode) ? DBNull.Value : (object)sys_BusiUnitType.BusiUnitTypeCode); 
            db.AddInParameter(command, "BusiUnitTypeName", DbType.String, sys_BusiUnitType.BusiUnitTypeName); 
            db.AddInParameter(command, "BusiUnitTypeShortName", DbType.String, string.IsNullOrEmpty(sys_BusiUnitType.BusiUnitTypeShortName) ? DBNull.Value : (object)sys_BusiUnitType.BusiUnitTypeShortName); 
            db.AddInParameter(command, "ParentID", DbType.Int32, sys_BusiUnitType.ParentID.HasValue ? (object)sys_BusiUnitType.ParentID : DBNull.Value); 
            db.AddInParameter(command, "Status", DbType.Boolean, sys_BusiUnitType.Status); 
            db.AddInParameter(command, "DisplayOrder", DbType.Int32, sys_BusiUnitType.DisplayOrder); 
            db.AddInParameter(command, "Remark", DbType.String, string.IsNullOrEmpty(sys_BusiUnitType.Remark) ? DBNull.Value : (object)sys_BusiUnitType.Remark); 
            db.AddInParameter(command, "CreatedBy", DbType.Int32, sys_BusiUnitType.CreatedBy); 
            
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
        /// 更新业务单位类型,该表不做显示维护记录
        /// </summary>
        /// <param name="sys_BusiUnitType">业务单位类型,该表不做显示维护对象</param>
        /// <returns>受影响的记录数</returns>
        public int UpdateSys_BusiUnitType(Sys_BusiUnitType sys_BusiUnitType)
        {
            string sql = @"UPDATE dbo.Sys_BusiUnitType SET BusiUnitTypeCode = @BusiUnitTypeCode, BusiUnitTypeName = @BusiUnitTypeName, BusiUnitTypeShortName = @BusiUnitTypeShortName, ParentID = @ParentID, Status = @Status, DisplayOrder = @DisplayOrder, Remark = @Remark, ModifiedBy = @ModifiedBy, ModifiedOn = @ModifiedOn WHERE KeyId = @KeyId";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);            
            
            db.AddInParameter(command, "KeyId", DbType.Int32, sys_BusiUnitType.KeyId); 
            db.AddInParameter(command, "BusiUnitTypeCode", DbType.String, string.IsNullOrEmpty(sys_BusiUnitType.BusiUnitTypeCode) ? DBNull.Value : (object)sys_BusiUnitType.BusiUnitTypeCode); 
            db.AddInParameter(command, "BusiUnitTypeName", DbType.String, sys_BusiUnitType.BusiUnitTypeName); 
            db.AddInParameter(command, "BusiUnitTypeShortName", DbType.String, string.IsNullOrEmpty(sys_BusiUnitType.BusiUnitTypeShortName) ? DBNull.Value : (object)sys_BusiUnitType.BusiUnitTypeShortName); 
            db.AddInParameter(command, "ParentID", DbType.Int32, sys_BusiUnitType.ParentID.HasValue ? (object)sys_BusiUnitType.ParentID : DBNull.Value); 
            db.AddInParameter(command, "Status", DbType.Boolean, sys_BusiUnitType.Status); 
            db.AddInParameter(command, "DisplayOrder", DbType.Int32, sys_BusiUnitType.DisplayOrder); 
            db.AddInParameter(command, "Remark", DbType.String, string.IsNullOrEmpty(sys_BusiUnitType.Remark) ? DBNull.Value : (object)sys_BusiUnitType.Remark); 
            db.AddInParameter(command, "ModifiedBy", DbType.Int32, sys_BusiUnitType.ModifiedBy.HasValue ? (object)sys_BusiUnitType.ModifiedBy : DBNull.Value); 
            db.AddInParameter(command, "ModifiedOn", DbType.DateTime, sys_BusiUnitType.ModifiedOn.HasValue ? (object)sys_BusiUnitType.ModifiedOn : DBNull.Value);       
            
            return db.ExecuteNonQuery(command);            
        }
        
        /// <summary>
        /// 删除业务单位类型,该表不做显示维护记录
        /// </summary>
        /// <param name="keyId">业务单位类型内码</param> 
        /// <returns>受影响的记录数</returns>
        public int DeleteSys_BusiUnitType(int keyId)
        {
            string sql = @"DELETE FROM dbo.Sys_BusiUnitType WHERE KeyId = @KeyId";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);            
            
            db.AddInParameter(command, "KeyId", DbType.Int32, keyId);
                        
            return db.ExecuteNonQuery(command);
        }
    }
}