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
    /// 业务单位信息数据存取类
    /// 生成日期: 2014年10月 27日 21:07
    /// </summary>
    /// <remarks>
    /// 该文件是由代码生成器自动生成的, 请不要随意修改, 你的修改将在代码重新生成时会被覆盖，
    /// 如果要对该类进行修改, 请直接修改该分部类的Sys_BusiUnit文件(文件名不含.designer)
    /// </remarks>
    public partial class Sys_BusiUnitDAO : SysBaseDao
    {
       /// <summary>
       ///根据主键值查找业务单位信息记录
       /// </summary>
       /// <param name="keyId">业务单位内码</param> 
       /// <returns>Sys_BusiUnit</returns>
       public Sys_BusiUnit FindSys_BusiUnit(int keyId)
       {
            string sql = @"SELECT KeyId, BusiUnitTypeID, RegionCode, BusiUnitCode, BusiUnitName, ShortName, HelperCode, OrderId, Address, ParentId, WebSiteUrl, FtpSiteUrl, Telephone1, Telephone2, Fax, E_Mail, X, Y, IsSaleCount, Status, Remark, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn FROM dbo.Sys_BusiUnit WHERE KeyId = @KeyId";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);                      
            
            db.AddInParameter(command, "KeyId", DbType.Int32, keyId);
                        
            Sys_BusiUnit sys_BusiUnit = null;
            
            using (IDataReader dr = db.ExecuteReader(command))
            {
                if (dr.Read())
                {
                    sys_BusiUnit = new Sys_BusiUnit();               
                    
                    sys_BusiUnit.KeyId = (int)dr["KeyId"]; 
                    sys_BusiUnit.BusiUnitTypeID = (int)dr["BusiUnitTypeID"]; 
                    sys_BusiUnit.RegionCode = dr["RegionCode"] == DBNull.Value ? null : (string)dr["RegionCode"]; 
                    sys_BusiUnit.BusiUnitCode = dr["BusiUnitCode"] == DBNull.Value ? null : (string)dr["BusiUnitCode"]; 
                    sys_BusiUnit.BusiUnitName = (string)dr["BusiUnitName"]; 
                    sys_BusiUnit.ShortName = dr["ShortName"] == DBNull.Value ? null : (string)dr["ShortName"]; 
                    sys_BusiUnit.HelperCode = dr["HelperCode"] == DBNull.Value ? null : (string)dr["HelperCode"]; 
                    sys_BusiUnit.OrderId = dr["OrderId"] == DBNull.Value ? null : (int?)dr["OrderId"]; 
                    sys_BusiUnit.Address = dr["Address"] == DBNull.Value ? null : (string)dr["Address"]; 
                    sys_BusiUnit.ParentId = dr["ParentId"] == DBNull.Value ? null : (int?)dr["ParentId"]; 
                    sys_BusiUnit.WebSiteUrl = dr["WebSiteUrl"] == DBNull.Value ? null : (string)dr["WebSiteUrl"]; 
                    sys_BusiUnit.FtpSiteUrl = dr["FtpSiteUrl"] == DBNull.Value ? null : (string)dr["FtpSiteUrl"]; 
                    sys_BusiUnit.Telephone1 = dr["Telephone1"] == DBNull.Value ? null : (string)dr["Telephone1"]; 
                    sys_BusiUnit.Telephone2 = dr["Telephone2"] == DBNull.Value ? null : (string)dr["Telephone2"]; 
                    sys_BusiUnit.Fax = dr["Fax"] == DBNull.Value ? null : (string)dr["Fax"]; 
                    sys_BusiUnit.E_Mail = dr["E_Mail"] == DBNull.Value ? null : (string)dr["E_Mail"]; 
                    sys_BusiUnit.X = dr["X"] == DBNull.Value ? null : (decimal?)dr["X"]; 
                    sys_BusiUnit.Y = dr["Y"] == DBNull.Value ? null : (decimal?)dr["Y"]; 
                    sys_BusiUnit.IsSaleCount = (int)dr["IsSaleCount"]; 
                    sys_BusiUnit.Status = (bool)dr["Status"]; 
                    sys_BusiUnit.Remark = dr["Remark"] == DBNull.Value ? null : (string)dr["Remark"]; 
                    sys_BusiUnit.CreatedBy = (int)dr["CreatedBy"]; 
                    sys_BusiUnit.CreatedOn = (DateTime)dr["CreatedOn"]; 
                    sys_BusiUnit.ModifiedBy = dr["ModifiedBy"] == DBNull.Value ? null : (int?)dr["ModifiedBy"]; 
                    sys_BusiUnit.ModifiedOn = dr["ModifiedOn"] == DBNull.Value ? null : (DateTime?)dr["ModifiedOn"];                                    
                }
            }   
                   
            return sys_BusiUnit; 
       }
       
       /// <summary>
       /// 获取全部业务单位信息列表
       /// </summary>
       /// <returns>Sys_BusiUnit对象列表</returns>
       public IList<Sys_BusiUnit> GetSys_BusiUnits()
       {
           return GetSys_BusiUnits(null);
       } 
       
        /// <summary>
	 	/// 返回满足查询条件的业务单位信息实体列表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>业务单位信息实体列表</returns>
        public IList<Sys_BusiUnit> GetSys_BusiUnits(QueryParameter param)
        {
            string sql = @"SELECT KeyId, BusiUnitTypeID, RegionCode, BusiUnitCode, BusiUnitName, ShortName, HelperCode, OrderId, Address, ParentId, WebSiteUrl, FtpSiteUrl, Telephone1, Telephone2, Fax, E_Mail, X, Y, IsSaleCount, Status, Remark, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn FROM dbo.Sys_BusiUnit";

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
            
            IList<Sys_BusiUnit> list = new List<Sys_BusiUnit>();
                        
            using (IDataReader dr = db.ExecuteReader(command))
            {
                while (dr.Read())
                {
                    Sys_BusiUnit sys_BusiUnit = new Sys_BusiUnit();                                
                    
                    sys_BusiUnit.KeyId = (int)dr["KeyId"]; 
                    sys_BusiUnit.BusiUnitTypeID = (int)dr["BusiUnitTypeID"]; 
                    sys_BusiUnit.RegionCode = dr["RegionCode"] == DBNull.Value ? null : (string)dr["RegionCode"]; 
                    sys_BusiUnit.BusiUnitCode = dr["BusiUnitCode"] == DBNull.Value ? null : (string)dr["BusiUnitCode"]; 
                    sys_BusiUnit.BusiUnitName = (string)dr["BusiUnitName"]; 
                    sys_BusiUnit.ShortName = dr["ShortName"] == DBNull.Value ? null : (string)dr["ShortName"]; 
                    sys_BusiUnit.HelperCode = dr["HelperCode"] == DBNull.Value ? null : (string)dr["HelperCode"]; 
                    sys_BusiUnit.OrderId = dr["OrderId"] == DBNull.Value ? null : (int?)dr["OrderId"]; 
                    sys_BusiUnit.Address = dr["Address"] == DBNull.Value ? null : (string)dr["Address"]; 
                    sys_BusiUnit.ParentId = dr["ParentId"] == DBNull.Value ? null : (int?)dr["ParentId"]; 
                    sys_BusiUnit.WebSiteUrl = dr["WebSiteUrl"] == DBNull.Value ? null : (string)dr["WebSiteUrl"]; 
                    sys_BusiUnit.FtpSiteUrl = dr["FtpSiteUrl"] == DBNull.Value ? null : (string)dr["FtpSiteUrl"]; 
                    sys_BusiUnit.Telephone1 = dr["Telephone1"] == DBNull.Value ? null : (string)dr["Telephone1"]; 
                    sys_BusiUnit.Telephone2 = dr["Telephone2"] == DBNull.Value ? null : (string)dr["Telephone2"]; 
                    sys_BusiUnit.Fax = dr["Fax"] == DBNull.Value ? null : (string)dr["Fax"]; 
                    sys_BusiUnit.E_Mail = dr["E_Mail"] == DBNull.Value ? null : (string)dr["E_Mail"]; 
                    sys_BusiUnit.X = dr["X"] == DBNull.Value ? null : (decimal?)dr["X"]; 
                    sys_BusiUnit.Y = dr["Y"] == DBNull.Value ? null : (decimal?)dr["Y"]; 
                    sys_BusiUnit.IsSaleCount = (int)dr["IsSaleCount"]; 
                    sys_BusiUnit.Status = (bool)dr["Status"]; 
                    sys_BusiUnit.Remark = dr["Remark"] == DBNull.Value ? null : (string)dr["Remark"]; 
                    sys_BusiUnit.CreatedBy = (int)dr["CreatedBy"]; 
                    sys_BusiUnit.CreatedOn = (DateTime)dr["CreatedOn"]; 
                    sys_BusiUnit.ModifiedBy = dr["ModifiedBy"] == DBNull.Value ? null : (int?)dr["ModifiedBy"]; 
                    sys_BusiUnit.ModifiedOn = dr["ModifiedOn"] == DBNull.Value ? null : (DateTime?)dr["ModifiedOn"];                   
                    
                    list.Add(sys_BusiUnit);
                }
            }   
                   
            return list; 
        }
        
        /// <summary>
        /// 返回满足查询条件的业务单位信息数据表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>业务单位信息数据表</returns>
        public DataTable GetSys_BusiUnitTable(QueryParameter param)
        {
            string sql = @"SELECT KeyId, BusiUnitTypeID, RegionCode, BusiUnitCode, BusiUnitName, ShortName, HelperCode, OrderId, Address, ParentId, WebSiteUrl, FtpSiteUrl, Telephone1, Telephone2, Fax, E_Mail, X, Y, IsSaleCount, Status, Remark, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn FROM dbo.Sys_BusiUnit";

            return GetDataTable(sql, param);
        }
        
        /// <summary>
        /// 插入业务单位信息记录
        /// </summary>
        /// <param name="sys_BusiUnit">业务单位信息对象</param>
        /// <returns></returns>
        public int InsertSys_BusiUnit(Sys_BusiUnit sys_BusiUnit)
        {
            string sql = @"INSERT INTO dbo.Sys_BusiUnit(BusiUnitTypeID, RegionCode, BusiUnitCode, BusiUnitName, ShortName, HelperCode, OrderId, Address, ParentId, WebSiteUrl, FtpSiteUrl, Telephone1, Telephone2, Fax, E_Mail, X, Y, IsSaleCount, Status, Remark, CreatedBy) VALUES(@BusiUnitTypeID, @RegionCode, @BusiUnitCode, @BusiUnitName, @ShortName, @HelperCode, @OrderId, @Address, @ParentId, @WebSiteUrl, @FtpSiteUrl, @Telephone1, @Telephone2, @Fax, @E_Mail, @X, @Y, @IsSaleCount, @Status, @Remark, @CreatedBy); SELECT @KeyId = SCOPE_IDENTITY()";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);                        
            
            db.AddOutParameter(command, "KeyId", DbType.Int32, sizeof(int)); 
            db.AddInParameter(command, "BusiUnitTypeID", DbType.Int32, sys_BusiUnit.BusiUnitTypeID); 
            db.AddInParameter(command, "RegionCode", DbType.String, string.IsNullOrEmpty(sys_BusiUnit.RegionCode) ? DBNull.Value : (object)sys_BusiUnit.RegionCode); 
            db.AddInParameter(command, "BusiUnitCode", DbType.String, string.IsNullOrEmpty(sys_BusiUnit.BusiUnitCode) ? DBNull.Value : (object)sys_BusiUnit.BusiUnitCode); 
            db.AddInParameter(command, "BusiUnitName", DbType.String, sys_BusiUnit.BusiUnitName); 
            db.AddInParameter(command, "ShortName", DbType.String, string.IsNullOrEmpty(sys_BusiUnit.ShortName) ? DBNull.Value : (object)sys_BusiUnit.ShortName); 
            db.AddInParameter(command, "HelperCode", DbType.String, string.IsNullOrEmpty(sys_BusiUnit.HelperCode) ? DBNull.Value : (object)sys_BusiUnit.HelperCode); 
            db.AddInParameter(command, "OrderId", DbType.Int32, sys_BusiUnit.OrderId.HasValue ? (object)sys_BusiUnit.OrderId : DBNull.Value); 
            db.AddInParameter(command, "Address", DbType.String, string.IsNullOrEmpty(sys_BusiUnit.Address) ? DBNull.Value : (object)sys_BusiUnit.Address); 
            db.AddInParameter(command, "ParentId", DbType.Int32, sys_BusiUnit.ParentId.HasValue ? (object)sys_BusiUnit.ParentId : DBNull.Value); 
            db.AddInParameter(command, "WebSiteUrl", DbType.String, string.IsNullOrEmpty(sys_BusiUnit.WebSiteUrl) ? DBNull.Value : (object)sys_BusiUnit.WebSiteUrl); 
            db.AddInParameter(command, "FtpSiteUrl", DbType.String, string.IsNullOrEmpty(sys_BusiUnit.FtpSiteUrl) ? DBNull.Value : (object)sys_BusiUnit.FtpSiteUrl); 
            db.AddInParameter(command, "Telephone1", DbType.String, string.IsNullOrEmpty(sys_BusiUnit.Telephone1) ? DBNull.Value : (object)sys_BusiUnit.Telephone1); 
            db.AddInParameter(command, "Telephone2", DbType.String, string.IsNullOrEmpty(sys_BusiUnit.Telephone2) ? DBNull.Value : (object)sys_BusiUnit.Telephone2); 
            db.AddInParameter(command, "Fax", DbType.String, string.IsNullOrEmpty(sys_BusiUnit.Fax) ? DBNull.Value : (object)sys_BusiUnit.Fax); 
            db.AddInParameter(command, "E_Mail", DbType.String, string.IsNullOrEmpty(sys_BusiUnit.E_Mail) ? DBNull.Value : (object)sys_BusiUnit.E_Mail); 
            db.AddInParameter(command, "X", DbType.Decimal, sys_BusiUnit.X.HasValue ? (object)sys_BusiUnit.X : DBNull.Value); 
            db.AddInParameter(command, "Y", DbType.Decimal, sys_BusiUnit.Y.HasValue ? (object)sys_BusiUnit.Y : DBNull.Value); 
            db.AddInParameter(command, "IsSaleCount", DbType.Int32, sys_BusiUnit.IsSaleCount); 
            db.AddInParameter(command, "Status", DbType.Boolean, sys_BusiUnit.Status); 
            db.AddInParameter(command, "Remark", DbType.String, string.IsNullOrEmpty(sys_BusiUnit.Remark) ? DBNull.Value : (object)sys_BusiUnit.Remark); 
            db.AddInParameter(command, "CreatedBy", DbType.Int32, sys_BusiUnit.CreatedBy); 
            
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
        /// 更新业务单位信息记录
        /// </summary>
        /// <param name="sys_BusiUnit">业务单位信息对象</param>
        /// <returns>受影响的记录数</returns>
        public int UpdateSys_BusiUnit(Sys_BusiUnit sys_BusiUnit)
        {
            string sql = @"UPDATE dbo.Sys_BusiUnit SET BusiUnitTypeID = @BusiUnitTypeID, RegionCode = @RegionCode, BusiUnitCode = @BusiUnitCode, BusiUnitName = @BusiUnitName, ShortName = @ShortName, HelperCode = @HelperCode, OrderId = @OrderId, Address = @Address, ParentId = @ParentId, WebSiteUrl = @WebSiteUrl, FtpSiteUrl = @FtpSiteUrl, Telephone1 = @Telephone1, Telephone2 = @Telephone2, Fax = @Fax, E_Mail = @E_Mail, X = @X, Y = @Y, IsSaleCount = @IsSaleCount, Status = @Status, Remark = @Remark, ModifiedBy = @ModifiedBy, ModifiedOn = @ModifiedOn WHERE KeyId = @KeyId";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);            
            
            db.AddInParameter(command, "KeyId", DbType.Int32, sys_BusiUnit.KeyId); 
            db.AddInParameter(command, "BusiUnitTypeID", DbType.Int32, sys_BusiUnit.BusiUnitTypeID); 
            db.AddInParameter(command, "RegionCode", DbType.String, string.IsNullOrEmpty(sys_BusiUnit.RegionCode) ? DBNull.Value : (object)sys_BusiUnit.RegionCode); 
            db.AddInParameter(command, "BusiUnitCode", DbType.String, string.IsNullOrEmpty(sys_BusiUnit.BusiUnitCode) ? DBNull.Value : (object)sys_BusiUnit.BusiUnitCode); 
            db.AddInParameter(command, "BusiUnitName", DbType.String, sys_BusiUnit.BusiUnitName); 
            db.AddInParameter(command, "ShortName", DbType.String, string.IsNullOrEmpty(sys_BusiUnit.ShortName) ? DBNull.Value : (object)sys_BusiUnit.ShortName); 
            db.AddInParameter(command, "HelperCode", DbType.String, string.IsNullOrEmpty(sys_BusiUnit.HelperCode) ? DBNull.Value : (object)sys_BusiUnit.HelperCode); 
            db.AddInParameter(command, "OrderId", DbType.Int32, sys_BusiUnit.OrderId.HasValue ? (object)sys_BusiUnit.OrderId : DBNull.Value); 
            db.AddInParameter(command, "Address", DbType.String, string.IsNullOrEmpty(sys_BusiUnit.Address) ? DBNull.Value : (object)sys_BusiUnit.Address); 
            db.AddInParameter(command, "ParentId", DbType.Int32, sys_BusiUnit.ParentId.HasValue ? (object)sys_BusiUnit.ParentId : DBNull.Value); 
            db.AddInParameter(command, "WebSiteUrl", DbType.String, string.IsNullOrEmpty(sys_BusiUnit.WebSiteUrl) ? DBNull.Value : (object)sys_BusiUnit.WebSiteUrl); 
            db.AddInParameter(command, "FtpSiteUrl", DbType.String, string.IsNullOrEmpty(sys_BusiUnit.FtpSiteUrl) ? DBNull.Value : (object)sys_BusiUnit.FtpSiteUrl); 
            db.AddInParameter(command, "Telephone1", DbType.String, string.IsNullOrEmpty(sys_BusiUnit.Telephone1) ? DBNull.Value : (object)sys_BusiUnit.Telephone1); 
            db.AddInParameter(command, "Telephone2", DbType.String, string.IsNullOrEmpty(sys_BusiUnit.Telephone2) ? DBNull.Value : (object)sys_BusiUnit.Telephone2); 
            db.AddInParameter(command, "Fax", DbType.String, string.IsNullOrEmpty(sys_BusiUnit.Fax) ? DBNull.Value : (object)sys_BusiUnit.Fax); 
            db.AddInParameter(command, "E_Mail", DbType.String, string.IsNullOrEmpty(sys_BusiUnit.E_Mail) ? DBNull.Value : (object)sys_BusiUnit.E_Mail); 
            db.AddInParameter(command, "X", DbType.Decimal, sys_BusiUnit.X.HasValue ? (object)sys_BusiUnit.X : DBNull.Value); 
            db.AddInParameter(command, "Y", DbType.Decimal, sys_BusiUnit.Y.HasValue ? (object)sys_BusiUnit.Y : DBNull.Value); 
            db.AddInParameter(command, "IsSaleCount", DbType.Int32, sys_BusiUnit.IsSaleCount); 
            db.AddInParameter(command, "Status", DbType.Boolean, sys_BusiUnit.Status); 
            db.AddInParameter(command, "Remark", DbType.String, string.IsNullOrEmpty(sys_BusiUnit.Remark) ? DBNull.Value : (object)sys_BusiUnit.Remark); 
            db.AddInParameter(command, "ModifiedBy", DbType.Int32, sys_BusiUnit.ModifiedBy.HasValue ? (object)sys_BusiUnit.ModifiedBy : DBNull.Value); 
            db.AddInParameter(command, "ModifiedOn", DbType.DateTime, sys_BusiUnit.ModifiedOn.HasValue ? (object)sys_BusiUnit.ModifiedOn : DBNull.Value);       
            
            return db.ExecuteNonQuery(command);            
        }
        
        /// <summary>
        /// 删除业务单位信息记录
        /// </summary>
        /// <param name="keyId">业务单位内码</param> 
        /// <returns>受影响的记录数</returns>
        public int DeleteSys_BusiUnit(int keyId)
        {
            string sql = @"DELETE FROM dbo.Sys_BusiUnit WHERE KeyId = @KeyId";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);            
            
            db.AddInParameter(command, "KeyId", DbType.Int32, keyId);
                        
            return db.ExecuteNonQuery(command);
        }
    }
}