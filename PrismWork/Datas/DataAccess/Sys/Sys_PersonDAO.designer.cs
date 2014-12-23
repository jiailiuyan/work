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
    /// 人员基本信息数据存取类
    /// 生成日期: 2014年10月 27日 23:20
    /// </summary>
    /// <remarks>
    /// 该文件是由代码生成器自动生成的, 请不要随意修改, 你的修改将在代码重新生成时会被覆盖，
    /// 如果要对该类进行修改, 请直接修改该分部类的Sys_Person文件(文件名不含.designer)
    /// </remarks>
    public partial class Sys_PersonDAO : SysBaseDao
    {
       /// <summary>
       ///根据主键值查找人员基本信息记录
       /// </summary>
       /// <param name="keyId">员工内码</param> 
       /// <returns>Sys_Person</returns>
       public Sys_Person FindSys_Person(int keyId)
       {
            string sql = @"SELECT KeyId, PersonCode, WorkNo, PersonName, DomainAcc, PassWord, SexId, Birthday, IDCard, Photo, Nation, NativePlace, PoliticalId, JoinDate, EducationId, GraduateFrom, Perfessional, Ability, IsFulltime, WorkType, WorkStartDate, JobLevel, JobSure, DutyId, DutySure, Contact, Officephone, Homephone, Mobilephone, HomeAddress, PostCode, EMail, Certificate, Status, DisplayOrder, Remark, HelperCode, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn FROM dbo.Sys_Person WHERE KeyId = @KeyId";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);                      
            
            db.AddInParameter(command, "KeyId", DbType.Int32, keyId);
                        
            Sys_Person sys_Person = null;
            
            using (IDataReader dr = db.ExecuteReader(command))
            {
                if (dr.Read())
                {
                    sys_Person = new Sys_Person();               
                    
                    sys_Person.KeyId = (int)dr["KeyId"]; 
                    sys_Person.PersonCode = (string)dr["PersonCode"]; 
                    sys_Person.WorkNo = dr["WorkNo"] == DBNull.Value ? null : (string)dr["WorkNo"]; 
                    sys_Person.PersonName = (string)dr["PersonName"]; 
                    sys_Person.DomainAcc = (string)dr["DomainAcc"]; 
                    sys_Person.PassWord = (string)dr["PassWord"]; 
                    sys_Person.SexId = (int)dr["SexId"]; 
                    sys_Person.Birthday = dr["Birthday"] == DBNull.Value ? null : (DateTime?)dr["Birthday"]; 
                    sys_Person.IDCard = dr["IDCard"] == DBNull.Value ? null : (string)dr["IDCard"]; 
                    sys_Person.Photo = dr["Photo"] == DBNull.Value ? null : (byte[])dr["Photo"]; 
                    sys_Person.Nation = dr["Nation"] == DBNull.Value ? null : (string)dr["Nation"]; 
                    sys_Person.NativePlace = dr["NativePlace"] == DBNull.Value ? null : (string)dr["NativePlace"]; 
                    sys_Person.PoliticalId = dr["PoliticalId"] == DBNull.Value ? null : (int?)dr["PoliticalId"]; 
                    sys_Person.JoinDate = dr["JoinDate"] == DBNull.Value ? null : (DateTime?)dr["JoinDate"]; 
                    sys_Person.EducationId = dr["EducationId"] == DBNull.Value ? null : (int?)dr["EducationId"]; 
                    sys_Person.GraduateFrom = dr["GraduateFrom"] == DBNull.Value ? null : (string)dr["GraduateFrom"]; 
                    sys_Person.Perfessional = dr["Perfessional"] == DBNull.Value ? null : (string)dr["Perfessional"]; 
                    sys_Person.Ability = dr["Ability"] == DBNull.Value ? null : (string)dr["Ability"]; 
                    sys_Person.IsFulltime = dr["IsFulltime"] == DBNull.Value ? null : (bool?)dr["IsFulltime"]; 
                    sys_Person.WorkType = dr["WorkType"] == DBNull.Value ? null : (int?)dr["WorkType"]; 
                    sys_Person.WorkStartDate = dr["WorkStartDate"] == DBNull.Value ? null : (DateTime?)dr["WorkStartDate"]; 
                    sys_Person.JobLevel = dr["JobLevel"] == DBNull.Value ? null : (int?)dr["JobLevel"]; 
                    sys_Person.JobSure = dr["JobSure"] == DBNull.Value ? null : (DateTime?)dr["JobSure"]; 
                    sys_Person.DutyId = dr["DutyId"] == DBNull.Value ? null : (int?)dr["DutyId"]; 
                    sys_Person.DutySure = dr["DutySure"] == DBNull.Value ? null : (DateTime?)dr["DutySure"]; 
                    sys_Person.Contact = dr["Contact"] == DBNull.Value ? null : (string)dr["Contact"]; 
                    sys_Person.Officephone = dr["Officephone"] == DBNull.Value ? null : (string)dr["Officephone"]; 
                    sys_Person.Homephone = dr["Homephone"] == DBNull.Value ? null : (string)dr["Homephone"]; 
                    sys_Person.Mobilephone = dr["Mobilephone"] == DBNull.Value ? null : (string)dr["Mobilephone"]; 
                    sys_Person.HomeAddress = dr["HomeAddress"] == DBNull.Value ? null : (string)dr["HomeAddress"]; 
                    sys_Person.PostCode = dr["PostCode"] == DBNull.Value ? null : (string)dr["PostCode"]; 
                    sys_Person.EMail = dr["EMail"] == DBNull.Value ? null : (string)dr["EMail"]; 
                    sys_Person.Certificate = dr["Certificate"] == DBNull.Value ? null : (string)dr["Certificate"]; 
                    sys_Person.Status = (bool)dr["Status"]; 
                    sys_Person.DisplayOrder = dr["DisplayOrder"] == DBNull.Value ? null : (int?)dr["DisplayOrder"]; 
                    sys_Person.Remark = dr["Remark"] == DBNull.Value ? null : (string)dr["Remark"]; 
                    sys_Person.HelperCode = dr["HelperCode"] == DBNull.Value ? null : (string)dr["HelperCode"]; 
                    sys_Person.CreatedBy = (int)dr["CreatedBy"]; 
                    sys_Person.CreatedOn = (DateTime)dr["CreatedOn"]; 
                    sys_Person.ModifiedBy = dr["ModifiedBy"] == DBNull.Value ? null : (int?)dr["ModifiedBy"]; 
                    sys_Person.ModifiedOn = dr["ModifiedOn"] == DBNull.Value ? null : (DateTime?)dr["ModifiedOn"];                                    
                }
            }   
                   
            return sys_Person; 
       }
       
       /// <summary>
       /// 获取全部人员基本信息列表
       /// </summary>
       /// <returns>Sys_Person对象列表</returns>
       public IList<Sys_Person> GetSys_Persons()
       {
           return GetSys_Persons(null);
       } 
       
        /// <summary>
	 	/// 返回满足查询条件的人员基本信息实体列表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>人员基本信息实体列表</returns>
        public IList<Sys_Person> GetSys_Persons(QueryParameter param)
        {
            string sql = @"SELECT KeyId, PersonCode, WorkNo, PersonName, DomainAcc, PassWord, SexId, Birthday, IDCard, Photo, Nation, NativePlace, PoliticalId, JoinDate, EducationId, GraduateFrom, Perfessional, Ability, IsFulltime, WorkType, WorkStartDate, JobLevel, JobSure, DutyId, DutySure, Contact, Officephone, Homephone, Mobilephone, HomeAddress, PostCode, EMail, Certificate, Status, DisplayOrder, Remark, HelperCode, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn FROM dbo.Sys_Person";

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
            
            IList<Sys_Person> list = new List<Sys_Person>();
                        
            using (IDataReader dr = db.ExecuteReader(command))
            {
                while (dr.Read())
                {
                    Sys_Person sys_Person = new Sys_Person();                                
                    
                    sys_Person.KeyId = (int)dr["KeyId"]; 
                    sys_Person.PersonCode = (string)dr["PersonCode"]; 
                    sys_Person.WorkNo = dr["WorkNo"] == DBNull.Value ? null : (string)dr["WorkNo"]; 
                    sys_Person.PersonName = (string)dr["PersonName"]; 
                    sys_Person.DomainAcc = (string)dr["DomainAcc"]; 
                    sys_Person.PassWord = (string)dr["PassWord"]; 
                    sys_Person.SexId = (int)dr["SexId"]; 
                    sys_Person.Birthday = dr["Birthday"] == DBNull.Value ? null : (DateTime?)dr["Birthday"]; 
                    sys_Person.IDCard = dr["IDCard"] == DBNull.Value ? null : (string)dr["IDCard"]; 
                    sys_Person.Photo = dr["Photo"] == DBNull.Value ? null : (byte[])dr["Photo"]; 
                    sys_Person.Nation = dr["Nation"] == DBNull.Value ? null : (string)dr["Nation"]; 
                    sys_Person.NativePlace = dr["NativePlace"] == DBNull.Value ? null : (string)dr["NativePlace"]; 
                    sys_Person.PoliticalId = dr["PoliticalId"] == DBNull.Value ? null : (int?)dr["PoliticalId"]; 
                    sys_Person.JoinDate = dr["JoinDate"] == DBNull.Value ? null : (DateTime?)dr["JoinDate"]; 
                    sys_Person.EducationId = dr["EducationId"] == DBNull.Value ? null : (int?)dr["EducationId"]; 
                    sys_Person.GraduateFrom = dr["GraduateFrom"] == DBNull.Value ? null : (string)dr["GraduateFrom"]; 
                    sys_Person.Perfessional = dr["Perfessional"] == DBNull.Value ? null : (string)dr["Perfessional"]; 
                    sys_Person.Ability = dr["Ability"] == DBNull.Value ? null : (string)dr["Ability"]; 
                    sys_Person.IsFulltime = dr["IsFulltime"] == DBNull.Value ? null : (bool?)dr["IsFulltime"]; 
                    sys_Person.WorkType = dr["WorkType"] == DBNull.Value ? null : (int?)dr["WorkType"]; 
                    sys_Person.WorkStartDate = dr["WorkStartDate"] == DBNull.Value ? null : (DateTime?)dr["WorkStartDate"]; 
                    sys_Person.JobLevel = dr["JobLevel"] == DBNull.Value ? null : (int?)dr["JobLevel"]; 
                    sys_Person.JobSure = dr["JobSure"] == DBNull.Value ? null : (DateTime?)dr["JobSure"]; 
                    sys_Person.DutyId = dr["DutyId"] == DBNull.Value ? null : (int?)dr["DutyId"]; 
                    sys_Person.DutySure = dr["DutySure"] == DBNull.Value ? null : (DateTime?)dr["DutySure"]; 
                    sys_Person.Contact = dr["Contact"] == DBNull.Value ? null : (string)dr["Contact"]; 
                    sys_Person.Officephone = dr["Officephone"] == DBNull.Value ? null : (string)dr["Officephone"]; 
                    sys_Person.Homephone = dr["Homephone"] == DBNull.Value ? null : (string)dr["Homephone"]; 
                    sys_Person.Mobilephone = dr["Mobilephone"] == DBNull.Value ? null : (string)dr["Mobilephone"]; 
                    sys_Person.HomeAddress = dr["HomeAddress"] == DBNull.Value ? null : (string)dr["HomeAddress"]; 
                    sys_Person.PostCode = dr["PostCode"] == DBNull.Value ? null : (string)dr["PostCode"]; 
                    sys_Person.EMail = dr["EMail"] == DBNull.Value ? null : (string)dr["EMail"]; 
                    sys_Person.Certificate = dr["Certificate"] == DBNull.Value ? null : (string)dr["Certificate"]; 
                    sys_Person.Status = (bool)dr["Status"]; 
                    sys_Person.DisplayOrder = dr["DisplayOrder"] == DBNull.Value ? null : (int?)dr["DisplayOrder"]; 
                    sys_Person.Remark = dr["Remark"] == DBNull.Value ? null : (string)dr["Remark"]; 
                    sys_Person.HelperCode = dr["HelperCode"] == DBNull.Value ? null : (string)dr["HelperCode"]; 
                    sys_Person.CreatedBy = (int)dr["CreatedBy"]; 
                    sys_Person.CreatedOn = (DateTime)dr["CreatedOn"]; 
                    sys_Person.ModifiedBy = dr["ModifiedBy"] == DBNull.Value ? null : (int?)dr["ModifiedBy"]; 
                    sys_Person.ModifiedOn = dr["ModifiedOn"] == DBNull.Value ? null : (DateTime?)dr["ModifiedOn"];                   
                    
                    list.Add(sys_Person);
                }
            }   
                   
            return list; 
        }
        
        /// <summary>
        /// 返回满足查询条件的人员基本信息数据表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>人员基本信息数据表</returns>
        public DataTable GetSys_PersonTable(QueryParameter param)
        {
            string sql = @"SELECT KeyId, PersonCode, WorkNo, PersonName, DomainAcc, PassWord, SexId, Birthday, IDCard, Photo, Nation, NativePlace, PoliticalId, JoinDate, EducationId, GraduateFrom, Perfessional, Ability, IsFulltime, WorkType, WorkStartDate, JobLevel, JobSure, DutyId, DutySure, Contact, Officephone, Homephone, Mobilephone, HomeAddress, PostCode, EMail, Certificate, Status, DisplayOrder, Remark, HelperCode, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn FROM dbo.Sys_Person";

            return GetDataTable(sql, param);
        }
        
        /// <summary>
        /// 插入人员基本信息记录
        /// </summary>
        /// <param name="sys_Person">人员基本信息对象</param>
        /// <returns></returns>
        public int InsertSys_Person(Sys_Person sys_Person)
        {
            string sql = @"INSERT INTO dbo.Sys_Person(PersonCode, WorkNo, PersonName, DomainAcc, PassWord, SexId, Birthday, IDCard, Photo, Nation, NativePlace, PoliticalId, JoinDate, EducationId, GraduateFrom, Perfessional, Ability, IsFulltime, WorkType, WorkStartDate, JobLevel, JobSure, DutyId, DutySure, Contact, Officephone, Homephone, Mobilephone, HomeAddress, PostCode, EMail, Certificate, Status, DisplayOrder, Remark, HelperCode, CreatedBy) VALUES(@PersonCode, @WorkNo, @PersonName, @DomainAcc, @PassWord, @SexId, @Birthday, @IDCard, @Photo, @Nation, @NativePlace, @PoliticalId, @JoinDate, @EducationId, @GraduateFrom, @Perfessional, @Ability, @IsFulltime, @WorkType, @WorkStartDate, @JobLevel, @JobSure, @DutyId, @DutySure, @Contact, @Officephone, @Homephone, @Mobilephone, @HomeAddress, @PostCode, @EMail, @Certificate, @Status, @DisplayOrder, @Remark, @HelperCode, @CreatedBy); SELECT @KeyId = SCOPE_IDENTITY()";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);                        
            
            db.AddOutParameter(command, "KeyId", DbType.Int32, sizeof(int)); 
            db.AddInParameter(command, "PersonCode", DbType.String, sys_Person.PersonCode); 
            db.AddInParameter(command, "WorkNo", DbType.String, string.IsNullOrEmpty(sys_Person.WorkNo) ? DBNull.Value : (object)sys_Person.WorkNo); 
            db.AddInParameter(command, "PersonName", DbType.String, sys_Person.PersonName); 
            db.AddInParameter(command, "DomainAcc", DbType.String, sys_Person.DomainAcc); 
            db.AddInParameter(command, "PassWord", DbType.String, sys_Person.PassWord); 
            db.AddInParameter(command, "SexId", DbType.Int32, sys_Person.SexId); 
            db.AddInParameter(command, "Birthday", DbType.DateTime, sys_Person.Birthday.HasValue ? (object)sys_Person.Birthday : DBNull.Value); 
            db.AddInParameter(command, "IDCard", DbType.String, string.IsNullOrEmpty(sys_Person.IDCard) ? DBNull.Value : (object)sys_Person.IDCard);
            db.AddInParameter(command, "Photo", DbType.Binary, sys_Person.Photo != null ? (object)sys_Person.Photo : DBNull.Value);
            db.AddInParameter(command, "Nation", DbType.String, string.IsNullOrEmpty(sys_Person.Nation) ? DBNull.Value : (object)sys_Person.Nation); 
            db.AddInParameter(command, "NativePlace", DbType.String, string.IsNullOrEmpty(sys_Person.NativePlace) ? DBNull.Value : (object)sys_Person.NativePlace); 
            db.AddInParameter(command, "PoliticalId", DbType.Int32, sys_Person.PoliticalId.HasValue ? (object)sys_Person.PoliticalId : DBNull.Value); 
            db.AddInParameter(command, "JoinDate", DbType.DateTime, sys_Person.JoinDate.HasValue ? (object)sys_Person.JoinDate : DBNull.Value); 
            db.AddInParameter(command, "EducationId", DbType.Int32, sys_Person.EducationId.HasValue ? (object)sys_Person.EducationId : DBNull.Value); 
            db.AddInParameter(command, "GraduateFrom", DbType.String, string.IsNullOrEmpty(sys_Person.GraduateFrom) ? DBNull.Value : (object)sys_Person.GraduateFrom); 
            db.AddInParameter(command, "Perfessional", DbType.String, string.IsNullOrEmpty(sys_Person.Perfessional) ? DBNull.Value : (object)sys_Person.Perfessional); 
            db.AddInParameter(command, "Ability", DbType.String, string.IsNullOrEmpty(sys_Person.Ability) ? DBNull.Value : (object)sys_Person.Ability); 
            db.AddInParameter(command, "IsFulltime", DbType.Boolean, sys_Person.IsFulltime.HasValue ? (object)sys_Person.IsFulltime : DBNull.Value); 
            db.AddInParameter(command, "WorkType", DbType.Int32, sys_Person.WorkType.HasValue ? (object)sys_Person.WorkType : DBNull.Value); 
            db.AddInParameter(command, "WorkStartDate", DbType.DateTime, sys_Person.WorkStartDate.HasValue ? (object)sys_Person.WorkStartDate : DBNull.Value); 
            db.AddInParameter(command, "JobLevel", DbType.Int32, sys_Person.JobLevel.HasValue ? (object)sys_Person.JobLevel : DBNull.Value); 
            db.AddInParameter(command, "JobSure", DbType.DateTime, sys_Person.JobSure.HasValue ? (object)sys_Person.JobSure : DBNull.Value); 
            db.AddInParameter(command, "DutyId", DbType.Int32, sys_Person.DutyId.HasValue ? (object)sys_Person.DutyId : DBNull.Value); 
            db.AddInParameter(command, "DutySure", DbType.DateTime, sys_Person.DutySure.HasValue ? (object)sys_Person.DutySure : DBNull.Value); 
            db.AddInParameter(command, "Contact", DbType.String, string.IsNullOrEmpty(sys_Person.Contact) ? DBNull.Value : (object)sys_Person.Contact); 
            db.AddInParameter(command, "Officephone", DbType.String, string.IsNullOrEmpty(sys_Person.Officephone) ? DBNull.Value : (object)sys_Person.Officephone); 
            db.AddInParameter(command, "Homephone", DbType.String, string.IsNullOrEmpty(sys_Person.Homephone) ? DBNull.Value : (object)sys_Person.Homephone); 
            db.AddInParameter(command, "Mobilephone", DbType.String, string.IsNullOrEmpty(sys_Person.Mobilephone) ? DBNull.Value : (object)sys_Person.Mobilephone); 
            db.AddInParameter(command, "HomeAddress", DbType.String, string.IsNullOrEmpty(sys_Person.HomeAddress) ? DBNull.Value : (object)sys_Person.HomeAddress); 
            db.AddInParameter(command, "PostCode", DbType.String, string.IsNullOrEmpty(sys_Person.PostCode) ? DBNull.Value : (object)sys_Person.PostCode); 
            db.AddInParameter(command, "EMail", DbType.String, string.IsNullOrEmpty(sys_Person.EMail) ? DBNull.Value : (object)sys_Person.EMail); 
            db.AddInParameter(command, "Certificate", DbType.String, string.IsNullOrEmpty(sys_Person.Certificate) ? DBNull.Value : (object)sys_Person.Certificate); 
            db.AddInParameter(command, "Status", DbType.Boolean, sys_Person.Status); 
            db.AddInParameter(command, "DisplayOrder", DbType.Int32, sys_Person.DisplayOrder.HasValue ? (object)sys_Person.DisplayOrder : DBNull.Value); 
            db.AddInParameter(command, "Remark", DbType.String, string.IsNullOrEmpty(sys_Person.Remark) ? DBNull.Value : (object)sys_Person.Remark); 
            db.AddInParameter(command, "HelperCode", DbType.String, string.IsNullOrEmpty(sys_Person.HelperCode) ? DBNull.Value : (object)sys_Person.HelperCode); 
            db.AddInParameter(command, "CreatedBy", DbType.Int32, sys_Person.CreatedBy); 
            
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
        /// 更新人员基本信息记录
        /// </summary>
        /// <param name="sys_Person">人员基本信息对象</param>
        /// <returns>受影响的记录数</returns>
        public int UpdateSys_Person(Sys_Person sys_Person)
        {
            string sql = @"UPDATE dbo.Sys_Person SET PersonCode = @PersonCode, WorkNo = @WorkNo, PersonName = @PersonName, DomainAcc = @DomainAcc, PassWord = @PassWord, SexId = @SexId, Birthday = @Birthday, IDCard = @IDCard, Photo = @Photo, Nation = @Nation, NativePlace = @NativePlace, PoliticalId = @PoliticalId, JoinDate = @JoinDate, EducationId = @EducationId, GraduateFrom = @GraduateFrom, Perfessional = @Perfessional, Ability = @Ability, IsFulltime = @IsFulltime, WorkType = @WorkType, WorkStartDate = @WorkStartDate, JobLevel = @JobLevel, JobSure = @JobSure, DutyId = @DutyId, DutySure = @DutySure, Contact = @Contact, Officephone = @Officephone, Homephone = @Homephone, Mobilephone = @Mobilephone, HomeAddress = @HomeAddress, PostCode = @PostCode, EMail = @EMail, Certificate = @Certificate, Status = @Status, DisplayOrder = @DisplayOrder, Remark = @Remark, HelperCode = @HelperCode, ModifiedBy = @ModifiedBy, ModifiedOn = @ModifiedOn WHERE KeyId = @KeyId";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);            
            
            db.AddInParameter(command, "KeyId", DbType.Int32, sys_Person.KeyId); 
            db.AddInParameter(command, "PersonCode", DbType.String, sys_Person.PersonCode); 
            db.AddInParameter(command, "WorkNo", DbType.String, string.IsNullOrEmpty(sys_Person.WorkNo) ? DBNull.Value : (object)sys_Person.WorkNo); 
            db.AddInParameter(command, "PersonName", DbType.String, sys_Person.PersonName); 
            db.AddInParameter(command, "DomainAcc", DbType.String, sys_Person.DomainAcc); 
            db.AddInParameter(command, "PassWord", DbType.String, sys_Person.PassWord); 
            db.AddInParameter(command, "SexId", DbType.Int32, sys_Person.SexId); 
            db.AddInParameter(command, "Birthday", DbType.DateTime, sys_Person.Birthday.HasValue ? (object)sys_Person.Birthday : DBNull.Value); 
            db.AddInParameter(command, "IDCard", DbType.String, string.IsNullOrEmpty(sys_Person.IDCard) ? DBNull.Value : (object)sys_Person.IDCard);
            db.AddInParameter(command, "Photo", DbType.Binary, sys_Person.Photo != null ? (object)sys_Person.Photo : DBNull.Value);
            db.AddInParameter(command, "Nation", DbType.String, string.IsNullOrEmpty(sys_Person.Nation) ? DBNull.Value : (object)sys_Person.Nation); 
            db.AddInParameter(command, "NativePlace", DbType.String, string.IsNullOrEmpty(sys_Person.NativePlace) ? DBNull.Value : (object)sys_Person.NativePlace); 
            db.AddInParameter(command, "PoliticalId", DbType.Int32, sys_Person.PoliticalId.HasValue ? (object)sys_Person.PoliticalId : DBNull.Value); 
            db.AddInParameter(command, "JoinDate", DbType.DateTime, sys_Person.JoinDate.HasValue ? (object)sys_Person.JoinDate : DBNull.Value); 
            db.AddInParameter(command, "EducationId", DbType.Int32, sys_Person.EducationId.HasValue ? (object)sys_Person.EducationId : DBNull.Value); 
            db.AddInParameter(command, "GraduateFrom", DbType.String, string.IsNullOrEmpty(sys_Person.GraduateFrom) ? DBNull.Value : (object)sys_Person.GraduateFrom); 
            db.AddInParameter(command, "Perfessional", DbType.String, string.IsNullOrEmpty(sys_Person.Perfessional) ? DBNull.Value : (object)sys_Person.Perfessional); 
            db.AddInParameter(command, "Ability", DbType.String, string.IsNullOrEmpty(sys_Person.Ability) ? DBNull.Value : (object)sys_Person.Ability); 
            db.AddInParameter(command, "IsFulltime", DbType.Boolean, sys_Person.IsFulltime.HasValue ? (object)sys_Person.IsFulltime : DBNull.Value); 
            db.AddInParameter(command, "WorkType", DbType.Int32, sys_Person.WorkType.HasValue ? (object)sys_Person.WorkType : DBNull.Value); 
            db.AddInParameter(command, "WorkStartDate", DbType.DateTime, sys_Person.WorkStartDate.HasValue ? (object)sys_Person.WorkStartDate : DBNull.Value); 
            db.AddInParameter(command, "JobLevel", DbType.Int32, sys_Person.JobLevel.HasValue ? (object)sys_Person.JobLevel : DBNull.Value); 
            db.AddInParameter(command, "JobSure", DbType.DateTime, sys_Person.JobSure.HasValue ? (object)sys_Person.JobSure : DBNull.Value); 
            db.AddInParameter(command, "DutyId", DbType.Int32, sys_Person.DutyId.HasValue ? (object)sys_Person.DutyId : DBNull.Value); 
            db.AddInParameter(command, "DutySure", DbType.DateTime, sys_Person.DutySure.HasValue ? (object)sys_Person.DutySure : DBNull.Value); 
            db.AddInParameter(command, "Contact", DbType.String, string.IsNullOrEmpty(sys_Person.Contact) ? DBNull.Value : (object)sys_Person.Contact); 
            db.AddInParameter(command, "Officephone", DbType.String, string.IsNullOrEmpty(sys_Person.Officephone) ? DBNull.Value : (object)sys_Person.Officephone); 
            db.AddInParameter(command, "Homephone", DbType.String, string.IsNullOrEmpty(sys_Person.Homephone) ? DBNull.Value : (object)sys_Person.Homephone); 
            db.AddInParameter(command, "Mobilephone", DbType.String, string.IsNullOrEmpty(sys_Person.Mobilephone) ? DBNull.Value : (object)sys_Person.Mobilephone); 
            db.AddInParameter(command, "HomeAddress", DbType.String, string.IsNullOrEmpty(sys_Person.HomeAddress) ? DBNull.Value : (object)sys_Person.HomeAddress); 
            db.AddInParameter(command, "PostCode", DbType.String, string.IsNullOrEmpty(sys_Person.PostCode) ? DBNull.Value : (object)sys_Person.PostCode); 
            db.AddInParameter(command, "EMail", DbType.String, string.IsNullOrEmpty(sys_Person.EMail) ? DBNull.Value : (object)sys_Person.EMail); 
            db.AddInParameter(command, "Certificate", DbType.String, string.IsNullOrEmpty(sys_Person.Certificate) ? DBNull.Value : (object)sys_Person.Certificate); 
            db.AddInParameter(command, "Status", DbType.Boolean, sys_Person.Status); 
            db.AddInParameter(command, "DisplayOrder", DbType.Int32, sys_Person.DisplayOrder.HasValue ? (object)sys_Person.DisplayOrder : DBNull.Value); 
            db.AddInParameter(command, "Remark", DbType.String, string.IsNullOrEmpty(sys_Person.Remark) ? DBNull.Value : (object)sys_Person.Remark); 
            db.AddInParameter(command, "HelperCode", DbType.String, string.IsNullOrEmpty(sys_Person.HelperCode) ? DBNull.Value : (object)sys_Person.HelperCode); 
            db.AddInParameter(command, "ModifiedBy", DbType.Int32, sys_Person.ModifiedBy.HasValue ? (object)sys_Person.ModifiedBy : DBNull.Value); 
            db.AddInParameter(command, "ModifiedOn", DbType.DateTime, sys_Person.ModifiedOn.HasValue ? (object)sys_Person.ModifiedOn : DBNull.Value);       
            
            return db.ExecuteNonQuery(command);            
        }
        
        /// <summary>
        /// 删除人员基本信息记录
        /// </summary>
        /// <param name="keyId">员工内码</param> 
        /// <returns>受影响的记录数</returns>
        public int DeleteSys_Person(int keyId)
        {
            string sql = @"DELETE FROM dbo.Sys_Person WHERE KeyId = @KeyId";

            Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);            
            
            db.AddInParameter(command, "KeyId", DbType.Int32, keyId);
                        
            return db.ExecuteNonQuery(command);
        }
    }
}