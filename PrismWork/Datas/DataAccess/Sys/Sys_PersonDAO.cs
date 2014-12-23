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
    /// Sys_PersonDAO 人员基本信息数据存取类
    /// 开发人员:
    /// 开发日期: 2014年10月 27日
    /// </summary>
    public partial class Sys_PersonDAO
    {
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public DataTable GetDataList(string where)
        {
            string sql = @"SELECT a.KeyId,PersonName,DomainAcc,PassWord,pbu.BusiUnitID,bu.BusiUnitName
                        ,SexId,CASE SexId WHEN 0 THEN '男' WHEN 1 THEN '女' ELSE '未知' END AS SexName
                        ,Officephone,Mobilephone
                        ,a.Status, case when(a.Status=1) then '√' else '×' end as StatusName  
                        FROM dbo.Sys_Person a 
                        LEFT JOIN dbo.Sys_PersonBusiUnit pbu on a.KeyId=pbu.personid 
                        LEFT JOIN  dbo.Sys_BusiUnit bu ON pbu.BusiUnitID=bu.KeyId
                        WHERE 1=1 ";
            if (!string.IsNullOrWhiteSpace(where))
                sql += where;
            return GetDataTable(sql);
        }
        
        /// <summary>
        /// 判断一个用户是否是一个领导较色
        /// </summary>
        /// <param name="_personid"></param>
        /// <returns></returns>
        public DataTable CheckPersonHaveLeaderRole(string _personid)
        {
            string sql = "SELECT * FROM dbo.Sys_UserRole WHERE PersonID=" + _personid + " AND RoleId=(SELECT roleid FROM dbo.Sys_Roles WHERE RoleCode='1060')";
            return GetDataTable(sql);
        }

        /// <summary>
        /// 根据人员内码找到该人员的主要单位编码
        /// </summary>
        /// <param name="_personid"></param>
        /// <returns></returns>
        public string GetPersonBusiUnitCode(string _personid)
        {
            string sql = "select busiunitcode from Sys_BusiUnit where KeyId=(select BusiUnitId from Sys_PersonBusiUnit where KeyId=" + _personid + " and IsMaster=1)";
            return GetDataString(sql);
        }

        /// <summary>
        /// 登陆校验
        /// </summary>
        /// <param name="_loginName"></param>
        /// <param name="_passWord"></param>
        /// <param name="strErr"></param>
        /// <returns></returns>
        public Sys_Person FindSys_Person(string _loginName, ref string strErr)
        {
            Sys_Person systemUser = null;
            try
            {
                string sql = "select a.KeyId,a.PersonCode,a.PersonName,a.DomainAcc,a.PassWord from Sys_Person a where a.DomainAcc='" + _loginName + "'";
                Database db = DatabaseFactory.CreateDatabase(DBLink.SysDBLink.ToString());
                DbCommand command = db.GetSqlStringCommand(sql);
                command.CommandTimeout = 3;
                using (IDataReader dr = db.ExecuteReader(command))
                {
                    if (dr.Read())
                    {
                        systemUser = new Sys_Person();
                        systemUser.KeyId = (int)dr["KeyId"];
                        systemUser.PersonCode = (string)dr["PersonCode"];
                        systemUser.PersonName = (string)dr["PersonName"];
                        systemUser.DomainAcc = (string)dr["DomainAcc"];
                        systemUser.PassWord = (string)dr["PassWord"];
                    }
                }
            }
            catch (DbException ex)
            {
                if (ex.ErrorCode.ToString().Equals("-2146232060"))
                {
                    strErr = ex.Message;
                }
                else
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return systemUser;
        }

        /// <summary>
        /// 根据主键值查找员工基本信息记录
        /// </summary>
        /// <param name="loginName">用户账号</param> 
        /// <param name="passWord">用户密码</param> 
        /// <returns>SystemUser</returns>
        public Sys_Person FindSys_Person(string loginName, string passWord)
        {
            string sql = @"select a.KeyId,a.PersonCode,a.PersonName,a.DomainAcc from Sys_Person a where a.DomainAcc=@LoginName and a.PassWord=@PassWord";

            Database db = DatabaseFactory.CreateDatabase("SysDBLink");
            DbCommand command = db.GetSqlStringCommand(sql);

            db.AddInParameter(command, "LoginName", DbType.String, loginName);
            db.AddInParameter(command, "PassWord", DbType.String, passWord);

            Sys_Person systemUser = null;

            using (IDataReader dr = db.ExecuteReader(command))
            {
                if (dr.Read())
                {
                    systemUser = new Sys_Person();

                    systemUser.KeyId = (int)dr["KeyId"];
                    systemUser.PersonCode = (string)dr["PersonCode"];
                    systemUser.PersonName = (string)dr["PersonName"];
                    systemUser.DomainAcc = (string)dr["DomainAcc"];
                }
            }

            return systemUser;
        }

        /// <summary>
        /// 获取用户功能模块
        /// </summary>
        /// <param name="personID"></param>
        /// <param name="projID"></param>
        /// <returns></returns>
        public DataTable GetUserModules(int personID, int projID)
        {
            Database db = DatabaseFactory.CreateDatabase("SysDBLink");
            DbCommand command = db.GetStoredProcCommand("p_Sys_GetUserModulePrivilage");

            db.AddInParameter(command, "PersonID", DbType.Int32, personID);
            db.AddInParameter(command, "ProjID", DbType.Int32, projID);
            return db.ExecuteDataSet(command).Tables[0];
        }

        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="person"></param>
        public void UpdatePersonPass(Sys_Person person, string pass)
        {
            string sql = @"update Sys_Person set PassWord=@PassWord where KeyId=@PersonID";

            Database db = DatabaseFactory.CreateDatabase("SysDBLink");
            DbCommand command = db.GetSqlStringCommand(sql);

            db.AddInParameter(command, "PassWord", DbType.String, pass);
            db.AddInParameter(command, "PersonID", DbType.String, person.KeyId);
            db.ExecuteNonQuery(command);
        }

        public void UpdateInfo(Sys_Person person)
        {
            string sql = @"update Sys_Person set SexId=@SexId,Officephone=@Officephone,Mobilephone=@Mobilephone where KeyId=@PersonID";

            Database db = DatabaseFactory.CreateDatabase("SysDBLink");
            DbCommand command = db.GetSqlStringCommand(sql);

            db.AddInParameter(command, "SexId", DbType.String, person.SexId);
            db.AddInParameter(command, "Officephone", DbType.String, person.Officephone);
            db.AddInParameter(command, "Mobilephone", DbType.String, person.Mobilephone);
            db.AddInParameter(command, "PersonID", DbType.String, person.KeyId);
            db.ExecuteNonQuery(command);
        }
    }
}