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
    /// 项目基本信息数据存取类
    /// 生成日期: 2014年10月 27日 20:12
    /// </summary>
    /// <remarks>
    /// 该文件是由代码生成器自动生成的, 请不要随意修改, 你的修改将在代码重新生成时会被覆盖，
    /// 如果要对该类进行修改, 请直接修改该分部类的Sys_ProjInfo文件(文件名不含.designer)
    /// </remarks>
    public partial class Sys_ProjInfoDAO : SysBaseDao
    {
       /// <summary>
       ///根据主键值查找项目基本信息记录
       /// </summary>
       /// <param name="keyId">项目内码</param> 
       /// <returns>Sys_ProjInfo</returns>
       public Sys_ProjInfo FindSys_ProjInfo(int keyId)
       {
            string sql = @"SELECT KeyId, ProjName, ProjFullName, ProjStage, ProjCode, ProjDetails, OnlineDate, Director, DBIPAdd, DBName, DBUser, DBPwd, Status, Remark, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn FROM dbo.Sys_ProjInfo WHERE KeyId = @KeyId";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);                      
            
            db.AddInParameter(command, "KeyId", DbType.Int32, keyId);
                        
            Sys_ProjInfo sys_ProjInfo = null;
            
            using (IDataReader dr = db.ExecuteReader(command))
            {
                if (dr.Read())
                {
                    sys_ProjInfo = new Sys_ProjInfo();               
                    
                    sys_ProjInfo.KeyId = (int)dr["KeyId"]; 
                    sys_ProjInfo.ProjName = (string)dr["ProjName"]; 
                    sys_ProjInfo.ProjFullName = (string)dr["ProjFullName"]; 
                    sys_ProjInfo.ProjStage = dr["ProjStage"] == DBNull.Value ? null : (string)dr["ProjStage"]; 
                    sys_ProjInfo.ProjCode = (string)dr["ProjCode"]; 
                    sys_ProjInfo.ProjDetails = dr["ProjDetails"] == DBNull.Value ? null : (string)dr["ProjDetails"]; 
                    sys_ProjInfo.OnlineDate = dr["OnlineDate"] == DBNull.Value ? null : (DateTime?)dr["OnlineDate"]; 
                    sys_ProjInfo.Director = dr["Director"] == DBNull.Value ? null : (string)dr["Director"]; 
                    sys_ProjInfo.DBIPAdd = dr["DBIPAdd"] == DBNull.Value ? null : (string)dr["DBIPAdd"]; 
                    sys_ProjInfo.DBName = dr["DBName"] == DBNull.Value ? null : (string)dr["DBName"]; 
                    sys_ProjInfo.DBUser = dr["DBUser"] == DBNull.Value ? null : (string)dr["DBUser"]; 
                    sys_ProjInfo.DBPwd = dr["DBPwd"] == DBNull.Value ? null : (string)dr["DBPwd"]; 
                    sys_ProjInfo.Status = (bool)dr["Status"]; 
                    sys_ProjInfo.Remark = dr["Remark"] == DBNull.Value ? null : (string)dr["Remark"]; 
                    sys_ProjInfo.CreatedBy = (int)dr["CreatedBy"]; 
                    sys_ProjInfo.CreatedOn = (DateTime)dr["CreatedOn"]; 
                    sys_ProjInfo.ModifiedBy = dr["ModifiedBy"] == DBNull.Value ? null : (int?)dr["ModifiedBy"]; 
                    sys_ProjInfo.ModifiedOn = dr["ModifiedOn"] == DBNull.Value ? null : (DateTime?)dr["ModifiedOn"];                                    
                }
            }   
                   
            return sys_ProjInfo; 
       }
       
       /// <summary>
       /// 获取全部项目基本信息列表
       /// </summary>
       /// <returns>Sys_ProjInfo对象列表</returns>
       public IList<Sys_ProjInfo> GetSys_ProjInfos()
       {
           return GetSys_ProjInfos(null);
       } 
       
        /// <summary>
	 	/// 返回满足查询条件的项目基本信息实体列表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>项目基本信息实体列表</returns>
        public IList<Sys_ProjInfo> GetSys_ProjInfos(QueryParameter param)
        {
            string sql = @"SELECT KeyId, ProjName, ProjFullName, ProjStage, ProjCode, ProjDetails, OnlineDate, Director, DBIPAdd, DBName, DBUser, DBPwd, Status, Remark, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn FROM dbo.Sys_ProjInfo";

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
            
            IList<Sys_ProjInfo> list = new List<Sys_ProjInfo>();
                        
            using (IDataReader dr = db.ExecuteReader(command))
            {
                while (dr.Read())
                {
                    Sys_ProjInfo sys_ProjInfo = new Sys_ProjInfo();                                
                    
                    sys_ProjInfo.KeyId = (int)dr["KeyId"]; 
                    sys_ProjInfo.ProjName = (string)dr["ProjName"]; 
                    sys_ProjInfo.ProjFullName = (string)dr["ProjFullName"]; 
                    sys_ProjInfo.ProjStage = dr["ProjStage"] == DBNull.Value ? null : (string)dr["ProjStage"]; 
                    sys_ProjInfo.ProjCode = (string)dr["ProjCode"]; 
                    sys_ProjInfo.ProjDetails = dr["ProjDetails"] == DBNull.Value ? null : (string)dr["ProjDetails"]; 
                    sys_ProjInfo.OnlineDate = dr["OnlineDate"] == DBNull.Value ? null : (DateTime?)dr["OnlineDate"]; 
                    sys_ProjInfo.Director = dr["Director"] == DBNull.Value ? null : (string)dr["Director"]; 
                    sys_ProjInfo.DBIPAdd = dr["DBIPAdd"] == DBNull.Value ? null : (string)dr["DBIPAdd"]; 
                    sys_ProjInfo.DBName = dr["DBName"] == DBNull.Value ? null : (string)dr["DBName"]; 
                    sys_ProjInfo.DBUser = dr["DBUser"] == DBNull.Value ? null : (string)dr["DBUser"]; 
                    sys_ProjInfo.DBPwd = dr["DBPwd"] == DBNull.Value ? null : (string)dr["DBPwd"]; 
                    sys_ProjInfo.Status = (bool)dr["Status"]; 
                    sys_ProjInfo.Remark = dr["Remark"] == DBNull.Value ? null : (string)dr["Remark"]; 
                    sys_ProjInfo.CreatedBy = (int)dr["CreatedBy"]; 
                    sys_ProjInfo.CreatedOn = (DateTime)dr["CreatedOn"]; 
                    sys_ProjInfo.ModifiedBy = dr["ModifiedBy"] == DBNull.Value ? null : (int?)dr["ModifiedBy"]; 
                    sys_ProjInfo.ModifiedOn = dr["ModifiedOn"] == DBNull.Value ? null : (DateTime?)dr["ModifiedOn"];                   
                    
                    list.Add(sys_ProjInfo);
                }
            }   
                   
            return list; 
        }
        
        /// <summary>
        /// 返回满足查询条件的项目基本信息数据表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>项目基本信息数据表</returns>
        public DataTable GetSys_ProjInfoTable(QueryParameter param)
        {
            string sql = @"SELECT KeyId, ProjName, ProjFullName, ProjStage, ProjCode, ProjDetails, OnlineDate, Director, DBIPAdd, DBName, DBUser, DBPwd, Status, Remark, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn FROM dbo.Sys_ProjInfo";

            return GetDataTable(sql, param);
        }
        
        /// <summary>
        /// 插入项目基本信息记录
        /// </summary>
        /// <param name="sys_ProjInfo">项目基本信息对象</param>
        /// <returns></returns>
        public int InsertSys_ProjInfo(Sys_ProjInfo sys_ProjInfo)
        {
            string sql = @"INSERT INTO dbo.Sys_ProjInfo(ProjName, ProjFullName, ProjStage, ProjCode, ProjDetails, OnlineDate, Director, DBIPAdd, DBName, DBUser, DBPwd, Status, Remark, CreatedBy) VALUES(@ProjName, @ProjFullName, @ProjStage, @ProjCode, @ProjDetails, @OnlineDate, @Director, @DBIPAdd, @DBName, @DBUser, @DBPwd, @Status, @Remark, @CreatedBy); SELECT @KeyId = SCOPE_IDENTITY()";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);                        
            
            db.AddOutParameter(command, "KeyId", DbType.Int32, sizeof(int)); 
            db.AddInParameter(command, "ProjName", DbType.String, sys_ProjInfo.ProjName); 
            db.AddInParameter(command, "ProjFullName", DbType.String, sys_ProjInfo.ProjFullName); 
            db.AddInParameter(command, "ProjStage", DbType.String, string.IsNullOrEmpty(sys_ProjInfo.ProjStage) ? DBNull.Value : (object)sys_ProjInfo.ProjStage); 
            db.AddInParameter(command, "ProjCode", DbType.String, sys_ProjInfo.ProjCode); 
            db.AddInParameter(command, "ProjDetails", DbType.String, string.IsNullOrEmpty(sys_ProjInfo.ProjDetails) ? DBNull.Value : (object)sys_ProjInfo.ProjDetails); 
            db.AddInParameter(command, "OnlineDate", DbType.DateTime, sys_ProjInfo.OnlineDate.HasValue ? (object)sys_ProjInfo.OnlineDate : DBNull.Value); 
            db.AddInParameter(command, "Director", DbType.String, string.IsNullOrEmpty(sys_ProjInfo.Director) ? DBNull.Value : (object)sys_ProjInfo.Director); 
            db.AddInParameter(command, "DBIPAdd", DbType.String, string.IsNullOrEmpty(sys_ProjInfo.DBIPAdd) ? DBNull.Value : (object)sys_ProjInfo.DBIPAdd); 
            db.AddInParameter(command, "DBName", DbType.String, string.IsNullOrEmpty(sys_ProjInfo.DBName) ? DBNull.Value : (object)sys_ProjInfo.DBName); 
            db.AddInParameter(command, "DBUser", DbType.String, string.IsNullOrEmpty(sys_ProjInfo.DBUser) ? DBNull.Value : (object)sys_ProjInfo.DBUser); 
            db.AddInParameter(command, "DBPwd", DbType.String, string.IsNullOrEmpty(sys_ProjInfo.DBPwd) ? DBNull.Value : (object)sys_ProjInfo.DBPwd); 
            db.AddInParameter(command, "Status", DbType.Boolean, sys_ProjInfo.Status); 
            db.AddInParameter(command, "Remark", DbType.String, string.IsNullOrEmpty(sys_ProjInfo.Remark) ? DBNull.Value : (object)sys_ProjInfo.Remark); 
            db.AddInParameter(command, "CreatedBy", DbType.Int32, sys_ProjInfo.CreatedBy); 
            
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
        /// 更新项目基本信息记录
        /// </summary>
        /// <param name="sys_ProjInfo">项目基本信息对象</param>
        /// <returns>受影响的记录数</returns>
        public int UpdateSys_ProjInfo(Sys_ProjInfo sys_ProjInfo)
        {
            string sql = @"UPDATE dbo.Sys_ProjInfo SET ProjName = @ProjName, ProjFullName = @ProjFullName, ProjStage = @ProjStage, ProjCode = @ProjCode, ProjDetails = @ProjDetails, OnlineDate = @OnlineDate, Director = @Director, DBIPAdd = @DBIPAdd, DBName = @DBName, DBUser = @DBUser, DBPwd = @DBPwd, Status = @Status, Remark = @Remark, ModifiedBy = @ModifiedBy, ModifiedOn = @ModifiedOn WHERE KeyId = @KeyId";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);            
            
            db.AddInParameter(command, "KeyId", DbType.Int32, sys_ProjInfo.KeyId); 
            db.AddInParameter(command, "ProjName", DbType.String, sys_ProjInfo.ProjName); 
            db.AddInParameter(command, "ProjFullName", DbType.String, sys_ProjInfo.ProjFullName); 
            db.AddInParameter(command, "ProjStage", DbType.String, string.IsNullOrEmpty(sys_ProjInfo.ProjStage) ? DBNull.Value : (object)sys_ProjInfo.ProjStage); 
            db.AddInParameter(command, "ProjCode", DbType.String, sys_ProjInfo.ProjCode); 
            db.AddInParameter(command, "ProjDetails", DbType.String, string.IsNullOrEmpty(sys_ProjInfo.ProjDetails) ? DBNull.Value : (object)sys_ProjInfo.ProjDetails); 
            db.AddInParameter(command, "OnlineDate", DbType.DateTime, sys_ProjInfo.OnlineDate.HasValue ? (object)sys_ProjInfo.OnlineDate : DBNull.Value); 
            db.AddInParameter(command, "Director", DbType.String, string.IsNullOrEmpty(sys_ProjInfo.Director) ? DBNull.Value : (object)sys_ProjInfo.Director); 
            db.AddInParameter(command, "DBIPAdd", DbType.String, string.IsNullOrEmpty(sys_ProjInfo.DBIPAdd) ? DBNull.Value : (object)sys_ProjInfo.DBIPAdd); 
            db.AddInParameter(command, "DBName", DbType.String, string.IsNullOrEmpty(sys_ProjInfo.DBName) ? DBNull.Value : (object)sys_ProjInfo.DBName); 
            db.AddInParameter(command, "DBUser", DbType.String, string.IsNullOrEmpty(sys_ProjInfo.DBUser) ? DBNull.Value : (object)sys_ProjInfo.DBUser); 
            db.AddInParameter(command, "DBPwd", DbType.String, string.IsNullOrEmpty(sys_ProjInfo.DBPwd) ? DBNull.Value : (object)sys_ProjInfo.DBPwd); 
            db.AddInParameter(command, "Status", DbType.Boolean, sys_ProjInfo.Status); 
            db.AddInParameter(command, "Remark", DbType.String, string.IsNullOrEmpty(sys_ProjInfo.Remark) ? DBNull.Value : (object)sys_ProjInfo.Remark); 
            db.AddInParameter(command, "ModifiedBy", DbType.Int32, sys_ProjInfo.ModifiedBy.HasValue ? (object)sys_ProjInfo.ModifiedBy : DBNull.Value); 
            db.AddInParameter(command, "ModifiedOn", DbType.DateTime, sys_ProjInfo.ModifiedOn.HasValue ? (object)sys_ProjInfo.ModifiedOn : DBNull.Value);       
            
            return db.ExecuteNonQuery(command);            
        }
        
        /// <summary>
        /// 删除项目基本信息记录
        /// </summary>
        /// <param name="keyId">项目内码</param> 
        /// <returns>受影响的记录数</returns>
        public int DeleteSys_ProjInfo(int keyId)
        {
            string sql = @"DELETE FROM dbo.Sys_ProjInfo WHERE KeyId = @KeyId";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);            
            
            db.AddInParameter(command, "KeyId", DbType.Int32, keyId);
                        
            return db.ExecuteNonQuery(command);
        }
    }
}