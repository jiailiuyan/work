using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Project.Common;
using System.Text.RegularExpressions;


namespace Project.DataAccess
{
    /// <summary>
    /// BaseDAO 数据存取基类
    /// 开发人员: 王博雯
    /// 开发日期: 2012年06月
    /// </summary>
    public class BusiBaseDAO:DAOCommConfig
    {
        
        /// <summary>
        /// 获取当前数据库服务器的日期时间
        /// </summary>
        /// <returns></returns>
        public static DateTime GetDbDate()
        {
            Database db = DatabaseFactory.CreateDatabase(DBLink.BusiDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand("SELECT GETDATE()");

            return (DateTime)db.ExecuteScalar(command);
        }

        /// <summary>
        /// 获取当前数据库服务器名称
        /// </summary>
        /// <returns></returns>
        public static string GetDbServerName()
        {
            Database db = DatabaseFactory.CreateDatabase(DBLink.BusiDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand("SELECT @@SERVERNAME");

            return (string)db.ExecuteScalar(command);
        }

        /// <summary>
        /// 通过数据库服务器生成一个新的Guid
        /// </summary>
        /// <returns></returns>
        public static Guid NewGuid()
        {
            Database db = DatabaseFactory.CreateDatabase(DBLink.BusiDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand("SELECT NEWID()");

            return (Guid)db.ExecuteScalar(command);
        }

        /// <summary>
        /// 根据传入ID查询名称
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <param name="keyField"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public static string GetNamebyId(string tableName, string fieldName, string keyField, string keyValue)
        {
            string sql = "SELECT " + fieldName + " FROM " + tableName + " WHERE " + keyField + " ='" + keyValue + "' ";


            Database db = DatabaseFactory.CreateDatabase(DBLink.BusiDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);

            if (db.ExecuteScalar(command) == null)
            {
                return "";
            }
            else
            {
                return (string)db.ExecuteScalar(command);
            }
        }

        /// <summary>
        /// 根据传入名称查询
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="keyField"></param>
        /// <param name="fieldName"></param>
        /// <param name="fileValue"></param>
        /// <returns></returns>
        public static decimal GetIdbyName(string tableName, string keyField, string fieldName, string fileValue)
        {
            string sql = "SELECT " + keyField + " FROM " + tableName + " WHERE " + fieldName + " like '" + fileValue + "' ";


            Database db = DatabaseFactory.CreateDatabase(DBLink.BusiDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);

            return (decimal)db.ExecuteScalar(command);
        }

        /// <summary>
        /// 判断指定数据表中指定字段的值是否已经存在
        /// </summary>
        /// <param name="tableName">数据表名</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="fieldValue">字段值</param>
        /// <param name="keyField">主键列</param>
        /// <param name="keyValue">主键值</param>
        /// <returns>是否已经存在</returns>
        public static bool Exists(string tableName, string fieldName, string fieldValue, string keyField, string keyValue)
        {
            string sql = "SELECT " + fieldName + " FROM " + tableName + " WHERE " + fieldName + " ='" + fieldValue + "' ";

            if (string.IsNullOrEmpty(keyField) == false && string.IsNullOrEmpty(keyValue) == false)
            {
                sql += " AND " + keyField + " != " + keyValue;
            }

            Database db = DatabaseFactory.CreateDatabase(DBLink.BusiDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);

            return db.ExecuteScalar(command) != null;
        }
        /// <summary>
        /// 和exists唯一不同是判断主键由不等改为相等
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <param name="fieldValue"></param>
        /// <param name="keyField"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public static bool ExistsEqual(string tableName, string fieldName, string fieldValue, string keyField, string keyValue)
        {
            string sql = "SELECT " + fieldName + " FROM " + tableName + " WHERE " + fieldName + " ='" + fieldValue + "' ";

            if (string.IsNullOrEmpty(keyField) == false && string.IsNullOrEmpty(keyValue) == false)
            {
                sql += " AND " + keyField + " = " + keyValue;
            }

            Database db = DatabaseFactory.CreateDatabase(DBLink.BusiDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);

            return db.ExecuteScalar(command) != null;
        }

        /// <summary>
        /// 和exists唯一不同是判断主键由不等改为相等(扩展3个主键)
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <param name="fieldValue"></param>
        /// <param name="keyField"></param>
        /// <param name="keyValue"></param>
        /// <param name="fKeyField"></param>
        /// <param name="fKeyValue"></param>
        /// <returns></returns>
        public static bool ExistsEqual(string tableName, string fieldName, string fieldValue, string keyField, string keyValue, string fKeyField, string fKeyValue)
        {
            string sql = "SELECT " + fieldName + " FROM " + tableName + " WHERE " + fieldName + " ='" + fieldValue + "' ";

            if (string.IsNullOrEmpty(keyField) == false && string.IsNullOrEmpty(keyValue) == false)
            {
                sql += " AND " + keyField + " = " + keyValue;
            }

            if (string.IsNullOrEmpty(fKeyField) == false && string.IsNullOrEmpty(fKeyValue) == false)
            {
                sql += " AND " + fKeyField + " = " + fKeyValue;
            }

            Database db = DatabaseFactory.CreateDatabase(DBLink.BusiDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);

            return db.ExecuteScalar(command) != null;
        }

        /// <summary>
        /// 判断未通过审核数据是否存在，IsAgree is null or IsAgree = 0,fieldName = 'fieldValue',keyFieldName = keyFieldValue,fieldName1 != fieldValue1,fieldName2 = fieldValue2
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <param name="fieldValue"></param>
        /// <param name="keyFieldName"></param>
        /// <param name="keyFieldValue"></param>
        /// <param name="fieldName1"></param>
        /// <param name="fieldValue1"></param>
        /// <param name="fieldName2"></param>
        /// <param name="fieldValue2"></param>
        /// <returns></returns>
        public static bool IsExist(string tableName, string fieldName, string fieldValue, string keyFieldName, string keyFieldValue, string fieldName1, string fieldValue1, string fieldName2, string fieldValue2)
        {
            string sql = "SELECT " + fieldName + " FROM " + tableName + " WHERE " + fieldName + " ='" + fieldValue + "' ";
            if (keyFieldName != "")
            {
                sql += " AND " + keyFieldName + " = " + keyFieldValue;
            }
            if (fieldName1 != "")
            {
                sql += " AND " + fieldName1 + " != " + fieldValue1;
            }
            if (fieldName2 != "")
            {
                sql += " AND " + fieldName2 + " = " + fieldValue2;
            }
            sql += " AND ( IsAgree is null or IsAgree = 0 )";

            Database db = DatabaseFactory.CreateDatabase(DBLink.BusiDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);

            return db.ExecuteScalar(command) != null;
        }


        /// <summary>
        /// 返回满足查询条件的数据表对象
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public DataTable GetDataTable(string commandText)
        {
            return this.GetDataTable(commandText, null, null);
        }

        /// <summary>
        /// 返回满足查询条件的数据表对象
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        protected DataTable GetDataTable(string commandText, DbTransaction transaction)
        {
            return this.GetDataTable(commandText, null, transaction);
        }

        /// <summary>
        /// 返回满足查询条件的数据表对象
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected DataTable GetDataTable(string commandText, QueryParameter param)
        {
            return GetDataTable(commandText, param, null);
        }


        /// <summary>
        /// 返回满足查询条件的数据表对象(DataTable)
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        protected DataTable GetDataTable(string commandText, QueryParameter param, DbTransaction transaction)
        {
            try
            {
                if (string.IsNullOrEmpty(commandText))
                {
                    throw new ArgumentNullException("commandText 不允许为空!");
                }

                if (param != null)
                {
                    commandText = QueryParameter.CompleteSqlString(commandText, param);
                }

                Database db = DatabaseFactory.CreateDatabase(DBLink.BusiDBLink.ToString());
                DbCommand command = db.GetSqlStringCommand(commandText);
                command.CommandTimeout = 300000;

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

                DataSet dataSet = transaction == null ? db.ExecuteDataSet(command) : db.ExecuteDataSet(commandText, transaction);

                if (dataSet == null || dataSet.Tables.Count == 0)
                {
                    throw new ApplicationException("执行查询出现异常, 无法返回指定的数据表!");
                }

                return dataSet.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// 返回满足查询条件的数据表对象(DataSet)
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        protected DataSet GetDataSet(string commandText, QueryParameter param, DbTransaction transaction)
        {
            if (string.IsNullOrEmpty(commandText))
            {
                throw new ArgumentNullException("commandText 不允许为空!");
            }

            if (param != null)
            {
                commandText = QueryParameter.CompleteSqlString(commandText, param);
            }

            Database db = DatabaseFactory.CreateDatabase(DBLink.BusiDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(commandText);
            command.CommandTimeout = 300000;

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

            DataSet dataSet = transaction == null ? db.ExecuteDataSet(command) : db.ExecuteDataSet(commandText, transaction);

            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                throw new ApplicationException("执行查询出现异常, 无法返回指定的数据表!");
            }

            return dataSet;
        }
        /// <summary>
        /// 返回满足查询条件的数据表对象
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public DataSet GetDataSet(string commandText)
        {
            return this.GetDataSet(commandText, null, null);
        }
        /// <summary>
        /// 执行一条没有返回结果的 Sql 语句
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public int ExecuteSql(string commandText)
        {
            return this.ExecuteSql(null, commandText);
        }

        /// <summary>
        /// 执行一条没有返回结果的 Sql 语句
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public int ExecuteSql(DbTransaction transaction, string commandText)
        {
            if (string.IsNullOrEmpty(commandText))
            {
                throw new ArgumentNullException(" commandText 不允许为空!");
            }

            Database db = DatabaseFactory.CreateDatabase(DBLink.BusiDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(commandText);

            return transaction == null ? db.ExecuteNonQuery(command) : db.ExecuteNonQuery(commandText, transaction);
        }

        /// <summary>
        /// 执行一条返回结果为单个值的 Sql 语句
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public object ExecuteScalar(string commandText)
        {
            return this.ExecuteScalar(null, commandText);
        }

        /// <summary>
        /// 执行一条返回结果为单个值的 Sql 语句
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public object ExecuteScalar(DbTransaction transaction, string commandText)
        {
            if (string.IsNullOrEmpty(commandText))
            {
                throw new ArgumentNullException("commandText 不允许为空!");
            }

            Database db = DatabaseFactory.CreateDatabase(DBLink.BusiDBLink.ToString());

            DbCommand command = db.GetSqlStringCommand(commandText);

            return transaction == null ? db.ExecuteScalar(command) : db.ExecuteScalar(command, transaction);
        }

        /// <summary>
        /// 获取指定表的显示文本和显示文本对应的值的列表
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="valueField">值字段</param>
        /// <param name="textField">文本字段</param>
        /// <param name="whereStr">查询条件</param>
        /// <param name="orderStr">排序字符串</param>
        /// <returns></returns>
        public IList<Project.Entities.ValueText> GetValueText(string tableName, string valueField, string textField, string whereStr, string orderStr)
        {
            string sql = string.Format("SELECT {0}, {1} FROM {2} ", valueField, textField, tableName);

            if (string.IsNullOrEmpty(whereStr) == false && whereStr.Trim().Length > 0)
            {
                sql += " where " + whereStr;
            }

            if (string.IsNullOrEmpty(orderStr) == false && orderStr.Trim().Length > 0)
            {
                sql += " order by " + orderStr;
            }

            Database db = DatabaseFactory.CreateDatabase(DBLink.BusiDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);

            IList<Project.Entities.ValueText> list = new List<Project.Entities.ValueText>();

            using (IDataReader dr = db.ExecuteReader(command))
            {
                while (dr.Read())
                {
                    Project.Entities.ValueText valueText = new Project.Entities.ValueText();

                    valueText.Value = dr[0].ToString();
                    valueText.Text = dr[1].ToString();

                    list.Add(valueText);
                }
            }

            return list;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="fieldvalue"></param>
        /// <param name="fieldtext"></param>
        /// <param name="strWhere"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public static DataTable GetDataTable(string tablename, string fieldvalue, string fieldtext, string strWhere, string orderby)
        {
            string sql = "SELECT " + fieldvalue + "," + fieldtext + " FROM " + tablename + " WHERE 1=1 ";

            if (strWhere != "")
            {
                sql += " AND " + strWhere;
            }
            if (orderby != "")
            {
                sql += " ORDER BY " + orderby;
            }

            Database db = DatabaseFactory.CreateDatabase(DBLink.BusiDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);
            DataSet ds = db.ExecuteDataSet(command);
            if (ds != null)
            {
                return db.ExecuteDataSet(command).Tables[0];
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 判断是否有重复的验证码
        /// </summary>
        /// <param name="vNum"></param>
        /// <returns></returns>
        public static bool hasValidate(string vNum)
        {
            return Exists("OrderInfo", "orderValidate", vNum, "", "");
        }

        /// <summary>
        /// 返回自动编码-带类型区分
        /// </summary>
        /// <param name="strTableName">表名</param>
        /// <param name="strCodeName">自动编码列名</param>
        /// <param name="iCodeType">类型</param>
        /// <param name="iStartCode">初始编码</param>
        /// <param name="iStep">步长</param>
        /// <returns></returns>
        public static string GetNextCode(string strTableName, string strCodeName, int iCodeType, int iStartCode, int iStep)
        {
            string sql = string.Format("select isnull(max(cast({0} as int)),{1}-{2})+{2} from {3} where TypeID={4}", strCodeName, iStartCode, iStep, strTableName, iCodeType);
            Database db = DatabaseFactory.CreateDatabase(DBLink.BusiDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);
            return db.ExecuteScalar(command).ToString();
        }

        /// <summary>
        /// 返回自动编码-通用无类型区分
        /// </summary>
        /// <param name="strTableName">表名</param>
        /// <param name="strCodeName">自动编码列名</param>
        /// <param name="iStartCode">初始编码</param>
        /// <param name="iStep">步长</param>
        /// <returns></returns>
        public static string GetNextCode(string strTableName, string strCodeName, int iStartCode, int iStep)
        {
            string sql = string.Format("select isnull(max(cast({0} as int)),{1}-{2})+{2} from {3}", strCodeName, iStartCode, iStep, strTableName);
            Database db = DatabaseFactory.CreateDatabase(DBLink.BusiDBLink.ToString());
            DbCommand command = db.GetSqlStringCommand(sql);
            return db.ExecuteScalar(command).ToString();
        }
       /// <summary>
        /// 根据sql返回的datatable，将每行每列用分隔符隔开
       /// </summary>
       /// <param name="sql"></param>
       /// <param name="FieldCompartChar"></param>
       /// <param name="RowCompartChar"></param>
       /// <returns></returns>
        public string getDataString(string sql, string FieldCompartChar, string RowCompartChar)
        {
            DataTable dtData = this.GetDataTable(sql);
            string strResult;
            string strRowResult = "";
            string strColResult = "";
            foreach (DataRow drData in dtData.Rows)
            {
                strColResult = "";
                foreach (DataColumn dcData in dtData.Columns)
                {
                    strColResult += FieldCompartChar + drData[dcData.ColumnName].ToString();
                }
                if (strColResult.Length > 0 && FieldCompartChar != "")
                {
                    strColResult = strColResult.Substring(FieldCompartChar.Length);
                }
                strRowResult += RowCompartChar + strColResult;
            }
            if (strRowResult.Length > 0 && RowCompartChar != "")
            {
                strResult = strRowResult.Substring(RowCompartChar.Length);
            }
            else
            {
                strResult = strRowResult;
            }
            return strResult;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="FieldCompartChar"></param>
        /// <returns></returns>
        public string GetDataString(string sql, string FieldCompartChar)
        {
            return this.getDataString(sql, FieldCompartChar, FieldCompartChar);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public string GetDataString(string sql)
        {
            return this.GetDataString(sql, "");
        }
        /// <summary>
        /// 判断是否为一个数字
        /// </summary>
        /// <param name="strNumber"></param>
        /// <returns></returns>
        public bool IsNumber(String strNumber)
        {
            Regex objNotNumberPattern = new Regex("[^0-9.-]");
            Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
            Regex objTwoMinusPattern = new Regex("[0-9]*[-][0-9]*[-][0-9]*");
            String strValidRealPattern = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
            String strValidIntegerPattern = "^([-]|[0-9])[0-9]*$";
            Regex objNumberPattern = new Regex("(" + strValidRealPattern + ")|(" + strValidIntegerPattern + ")");
            return !objNotNumberPattern.IsMatch(strNumber) &&
                   !objTwoDotPattern.IsMatch(strNumber) &&
                   !objTwoMinusPattern.IsMatch(strNumber) &&
                   objNumberPattern.IsMatch(strNumber);
        }
    }
}