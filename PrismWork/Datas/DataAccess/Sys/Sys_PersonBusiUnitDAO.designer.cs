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
    /// 一个人可能属于多个单位数据存取类
    /// 生成日期: 2014年10月 27日 21:08
    /// </summary>
    /// <remarks>
    /// 该文件是由代码生成器自动生成的, 请不要随意修改, 你的修改将在代码重新生成时会被覆盖，
    /// 如果要对该类进行修改, 请直接修改该分部类的Sys_PersonBusiUnit文件(文件名不含.designer)
    /// </remarks>
    public partial class Sys_PersonBusiUnitDAO : SysBaseDao
    {
       /// <summary>
       ///根据主键值查找一个人可能属于多个单位记录
       /// </summary>
       /// <param name="keyId">KeyId</param> 
       /// <returns>Sys_PersonBusiUnit</returns>
       public Sys_PersonBusiUnit FindSys_PersonBusiUnit(int keyId)
       {
            string sql = @"SELECT KeyId, PersonID, BusiUnitID, IsMaster, BeginDate, EndDate, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn FROM dbo.Sys_PersonBusiUnit WHERE KeyId = @KeyId";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);                      
            
            db.AddInParameter(command, "KeyId", DbType.Int32, keyId);
                        
            Sys_PersonBusiUnit sys_PersonBusiUnit = null;
            
            using (IDataReader dr = db.ExecuteReader(command))
            {
                if (dr.Read())
                {
                    sys_PersonBusiUnit = new Sys_PersonBusiUnit();               
                    
                    sys_PersonBusiUnit.KeyId = (int)dr["KeyId"]; 
                    sys_PersonBusiUnit.PersonID = (int)dr["PersonID"]; 
                    sys_PersonBusiUnit.BusiUnitID = (int)dr["BusiUnitID"]; 
                    sys_PersonBusiUnit.IsMaster = (Int16)dr["IsMaster"]; 
                    sys_PersonBusiUnit.BeginDate = (DateTime)dr["BeginDate"]; 
                    sys_PersonBusiUnit.EndDate = dr["EndDate"] == DBNull.Value ? null : (DateTime?)dr["EndDate"]; 
                    sys_PersonBusiUnit.CreatedBy = (int)dr["CreatedBy"]; 
                    sys_PersonBusiUnit.CreatedOn = (DateTime)dr["CreatedOn"]; 
                    sys_PersonBusiUnit.ModifiedBy = dr["ModifiedBy"] == DBNull.Value ? null : (int?)dr["ModifiedBy"]; 
                    sys_PersonBusiUnit.ModifiedOn = dr["ModifiedOn"] == DBNull.Value ? null : (DateTime?)dr["ModifiedOn"];                                    
                }
            }   
                   
            return sys_PersonBusiUnit; 
       }
       
       /// <summary>
       /// 获取全部一个人可能属于多个单位列表
       /// </summary>
       /// <returns>Sys_PersonBusiUnit对象列表</returns>
       public IList<Sys_PersonBusiUnit> GetSys_PersonBusiUnits()
       {
           return GetSys_PersonBusiUnits(null);
       } 
       
        /// <summary>
	 	/// 返回满足查询条件的一个人可能属于多个单位实体列表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>一个人可能属于多个单位实体列表</returns>
        public IList<Sys_PersonBusiUnit> GetSys_PersonBusiUnits(QueryParameter param)
        {
            string sql = @"SELECT KeyId, PersonID, BusiUnitID, IsMaster, BeginDate, EndDate, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn FROM dbo.Sys_PersonBusiUnit";

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
            
            IList<Sys_PersonBusiUnit> list = new List<Sys_PersonBusiUnit>();
                        
            using (IDataReader dr = db.ExecuteReader(command))
            {
                while (dr.Read())
                {
                    Sys_PersonBusiUnit sys_PersonBusiUnit = new Sys_PersonBusiUnit();                                
                    
                    sys_PersonBusiUnit.KeyId = (int)dr["KeyId"]; 
                    sys_PersonBusiUnit.PersonID = (int)dr["PersonID"]; 
                    sys_PersonBusiUnit.BusiUnitID = (int)dr["BusiUnitID"]; 
                    sys_PersonBusiUnit.IsMaster = (Int16)dr["IsMaster"]; 
                    sys_PersonBusiUnit.BeginDate = (DateTime)dr["BeginDate"]; 
                    sys_PersonBusiUnit.EndDate = dr["EndDate"] == DBNull.Value ? null : (DateTime?)dr["EndDate"]; 
                    sys_PersonBusiUnit.CreatedBy = (int)dr["CreatedBy"]; 
                    sys_PersonBusiUnit.CreatedOn = (DateTime)dr["CreatedOn"]; 
                    sys_PersonBusiUnit.ModifiedBy = dr["ModifiedBy"] == DBNull.Value ? null : (int?)dr["ModifiedBy"]; 
                    sys_PersonBusiUnit.ModifiedOn = dr["ModifiedOn"] == DBNull.Value ? null : (DateTime?)dr["ModifiedOn"];                   
                    
                    list.Add(sys_PersonBusiUnit);
                }
            }   
                   
            return list; 
        }
        
        /// <summary>
        /// 返回满足查询条件的一个人可能属于多个单位数据表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>一个人可能属于多个单位数据表</returns>
        public DataTable GetSys_PersonBusiUnitTable(QueryParameter param)
        {
            string sql = @"SELECT KeyId, PersonID, BusiUnitID, IsMaster, BeginDate, EndDate, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn FROM dbo.Sys_PersonBusiUnit";

            return GetDataTable(sql, param);
        }
        
        /// <summary>
        /// 插入一个人可能属于多个单位记录
        /// </summary>
        /// <param name="sys_PersonBusiUnit">一个人可能属于多个单位对象</param>
        /// <returns></returns>
        public int InsertSys_PersonBusiUnit(Sys_PersonBusiUnit sys_PersonBusiUnit)
        {
            string sql = @"INSERT INTO dbo.Sys_PersonBusiUnit(PersonID, BusiUnitID, IsMaster, BeginDate, EndDate, CreatedBy) VALUES(@PersonID, @BusiUnitID, @IsMaster, @BeginDate, @EndDate, @CreatedBy); SELECT @KeyId = SCOPE_IDENTITY()";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);                        
            
            db.AddOutParameter(command, "KeyId", DbType.Int32, sizeof(int)); 
            db.AddInParameter(command, "PersonID", DbType.Int32, sys_PersonBusiUnit.PersonID); 
            db.AddInParameter(command, "BusiUnitID", DbType.Int32, sys_PersonBusiUnit.BusiUnitID); 
            db.AddInParameter(command, "IsMaster", DbType.Int16, sys_PersonBusiUnit.IsMaster); 
            db.AddInParameter(command, "BeginDate", DbType.DateTime, sys_PersonBusiUnit.BeginDate); 
            db.AddInParameter(command, "EndDate", DbType.DateTime, sys_PersonBusiUnit.EndDate.HasValue ? (object)sys_PersonBusiUnit.EndDate : DBNull.Value); 
            db.AddInParameter(command, "CreatedBy", DbType.Int32, sys_PersonBusiUnit.CreatedBy); 
            
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
        /// 更新一个人可能属于多个单位记录
        /// </summary>
        /// <param name="sys_PersonBusiUnit">一个人可能属于多个单位对象</param>
        /// <returns>受影响的记录数</returns>
        public int UpdateSys_PersonBusiUnit(Sys_PersonBusiUnit sys_PersonBusiUnit)
        {
            string sql = @"UPDATE dbo.Sys_PersonBusiUnit SET PersonID = @PersonID, BusiUnitID = @BusiUnitID, IsMaster = @IsMaster, BeginDate = @BeginDate, EndDate = @EndDate, ModifiedBy = @ModifiedBy, ModifiedOn = @ModifiedOn WHERE KeyId = @KeyId";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);            
            
            db.AddInParameter(command, "KeyId", DbType.Int32, sys_PersonBusiUnit.KeyId); 
            db.AddInParameter(command, "PersonID", DbType.Int32, sys_PersonBusiUnit.PersonID); 
            db.AddInParameter(command, "BusiUnitID", DbType.Int32, sys_PersonBusiUnit.BusiUnitID); 
            db.AddInParameter(command, "IsMaster", DbType.Int16, sys_PersonBusiUnit.IsMaster); 
            db.AddInParameter(command, "BeginDate", DbType.DateTime, sys_PersonBusiUnit.BeginDate); 
            db.AddInParameter(command, "EndDate", DbType.DateTime, sys_PersonBusiUnit.EndDate.HasValue ? (object)sys_PersonBusiUnit.EndDate : DBNull.Value); 
            db.AddInParameter(command, "ModifiedBy", DbType.Int32, sys_PersonBusiUnit.ModifiedBy.HasValue ? (object)sys_PersonBusiUnit.ModifiedBy : DBNull.Value); 
            db.AddInParameter(command, "ModifiedOn", DbType.DateTime, sys_PersonBusiUnit.ModifiedOn.HasValue ? (object)sys_PersonBusiUnit.ModifiedOn : DBNull.Value);       
            
            return db.ExecuteNonQuery(command);            
        }
        
        /// <summary>
        /// 删除一个人可能属于多个单位记录
        /// </summary>
        /// <param name="keyId">KeyId</param> 
        /// <returns>受影响的记录数</returns>
        public int DeleteSys_PersonBusiUnit(int keyId)
        {
            string sql = @"DELETE FROM dbo.Sys_PersonBusiUnit WHERE KeyId = @KeyId";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);            
            
            db.AddInParameter(command, "KeyId", DbType.Int32, keyId);
                        
            return db.ExecuteNonQuery(command);
        }
    }
}