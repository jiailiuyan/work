using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

namespace Project.Common
{
    /// <summary>
    /// 构造简单查询条件表达式 SQL 语句片段类
    /// 作者: 王博雯
    /// 开发时间: 2012年06月
    /// </summary>
    public class SimpleExpression : IExpression
    {
        private string _expName;
        private string _expression;
        private object _value;
        private string _op;
        private DbType _dbType = DbType.Object;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="expression">表达式(如: 字段名, AVG(字段名), 数量 * 单价)</param>
        /// <param name="value"></param>
        /// <param name="op"></param>
        private SimpleExpression(string expression, object value, string op)
        {
            if (string.IsNullOrEmpty(expression))
                throw new ArgumentNullException("构造查询条件的 expression 为空");

            if (value == null)
                throw new ArgumentNullException("构造查询条件的表示式的 value 为空");

            this._expression = expression;
            this._value = value;
            this._op = op;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="expression">表达式(如: 字段名, AVG(字段名), 数量 * 单价)</param>
        /// <param name="value"></param>
        /// <param name="op"></param>
        private SimpleExpression(string expression, DbType dbType, object value, string op)
            : this(expression, value, op)
        {
            this._dbType = dbType;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="expName"></param>
        /// <param name="value"></param>
        /// <param name="op"></param>
        private SimpleExpression(string expression, string expName, object value, string op)
            : this(expression, value, op)
        {
            this._expName = expName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="expName"></param>
        /// <param name="value"></param>
        /// <param name="op"></param>
        private SimpleExpression(string expression, string expName, DbType dbType, object value, string op)
            : this(expression, expName, value, op)
        {
            this._dbType = dbType;
        }

        /// <summary>
        /// 创建查询条件表达式
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="expName"></param>
        /// <param name="op"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static SimpleExpression CreateInstance(string expression, string expName, string op, object value)
        {
            SimpleExpression result = null;

            if (string.IsNullOrEmpty(expression) || value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return result;
            }

            switch (op)
            {
                case "等于":
                case "=":
                    result = SimpleExpression.Equal(expression, expName, value);
                    break;

                case "不等于":
                case "<>":
                    result = SimpleExpression.NotEqual(expression, expName, value);
                    break;

                case "大于":
                case ">":
                    result = SimpleExpression.GreaterThan(expression, expName, value);
                    break;

                case "大于等于":
                case "大于或等于":
                case ">=":
                    result = SimpleExpression.GreaterEqual(expression, expName, value);
                    break;

                case "小于":
                case "<":
                    result = SimpleExpression.LessThan(expression, expName, value);
                    break;

                case "小于等于":
                case "小于或等于":
                case "<=":
                    result = SimpleExpression.LessEqual(expression, expName, value);
                    break;

                case "相似于":
                case "象":
                case "like":
                    result = SimpleExpression.Like(expression, expName, value);
                    break;

                default:
                    result = SimpleExpression.Equal(expression, expName, value);
                    break;
            }

            return result;
        }

        /// <summary>
        /// 创建查询条件表达式
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="expName"></param>
        /// <param name="dbType"></param>
        /// <param name="op"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static SimpleExpression CreateInstance(string expression, string expName, DbType dbType, string op, object value)
        {
            SimpleExpression exp = CreateInstance(expression, expName, op, value);
            if (exp != null)
            {
                exp._dbType = dbType;
            }

            return exp;
        }

        /// <summary>
        /// 获取表达式
        /// </summary>
        public string Expression
        {
            get
            {
                return string.IsNullOrEmpty(_expression) ? string.Empty : _expression.Trim();
            }
        }

        /// <summary>
        /// 获取表达式名称
        /// </summary>
        public string ExpName
        {
            get
            {
                return string.IsNullOrEmpty(_expName) ? Expression : _expName.Trim();
            }
        }

        /// <summary>
        /// 获取数据类型
        /// </summary>
        public DbType DbType
        {
            get
            {
                return this._dbType;
            }
        }

        /// <summary>
        /// 获取条件字段值
        /// </summary>
        public object Value
        {
            get
            {
                return _value;
            }
        }

        /// <summary>
        /// 获取查询操作符
        /// </summary>
        public string Op
        {
            get
            {
                return _op;
            }
        }

        /// <summary>
        /// 构建字段值等于参数值的条件表达式
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static SimpleExpression Equal(string expression, object value)
        {
            return new SimpleExpression(expression, value, "=");
        }

        /// <summary>
        /// 构建字段值等于参数值的条件表达式
        /// </summary>
        /// <param name="expression">表示式</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="value">参数值对象</param>
        /// <returns></returns>
        public static SimpleExpression Equal(string expression, DbType dbType, object value)
        {
            return new SimpleExpression(expression, dbType, value, "=");
        }

        /// <summary>
        /// 构建字段值等于参数值的条件表达式
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="expName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static SimpleExpression Equal(string expression, string expName, object value)
        {
            return new SimpleExpression(expression, expName, value, "=");
        }

        /// <summary>
        /// 构建字段值等于参数值的条件表达式
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="expName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static SimpleExpression Equal(string expression, string expName, DbType dbType, object value)
        {
            return new SimpleExpression(expression, expName, dbType, value, "=");
        }

        /// <summary>
        /// 构建字段值不等于参数值的条件表达式
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static SimpleExpression NotEqual(string expression, object value)
        {
            return new SimpleExpression(expression, value, "<>");
        }

        /// <summary>
        /// 构建字段值不等于参数值的条件表达式
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static SimpleExpression NotEqual(string expression, DbType dbType, object value)
        {
            return new SimpleExpression(expression, dbType, value, "<>");
        }

        /// <summary>
        /// 构建字段值不等于参数值的条件表达式
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="expName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static SimpleExpression NotEqual(string expression, string expName, object value)
        {
            return new SimpleExpression(expression, expName, value, "<>");
        }

        /// <summary>
        /// 构建字段值不等于参数值的条件表达式
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="expName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static SimpleExpression NotEqual(string expression, string expName, DbType dbType, object value)
        {
            return new SimpleExpression(expression, expName, dbType, value, "<>");
        }

        /// <summary>
        /// 构建字段值相似于参数值的条件表达式
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static SimpleExpression Like(string expression, object value)
        {
            return new SimpleExpression(expression, value, "LIKE");
        }

        /// <summary>
        /// 构建字段值相似于参数值的条件表达式
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static SimpleExpression Like(string expression, DbType dbType, object value)
        {
            return new SimpleExpression(expression, dbType, value, "LIKE");
        }

        /// <summary>
        /// 构建字段值相似于参数值的条件表达式
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="expName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static SimpleExpression Like(string expression, string expName, object value)
        {
            return new SimpleExpression(expression, expName, value, "LIKE");
        }

        /// <summary>
        /// 构建字段值相似于参数值的条件表达式
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="expName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static SimpleExpression Like(string expression, string expName, DbType dbType, object value)
        {
            return new SimpleExpression(expression, expName, dbType, value, "LIKE");
        }


        /// <summary>
        /// 构建字段值大于参数值的条件表达式
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static SimpleExpression GreaterThan(string expression, object value)
        {
            return new SimpleExpression(expression, value, ">");
        }

        /// <summary>
        /// 构建字段值大于参数值的条件表达式
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static SimpleExpression GreaterThan(string expression, DbType dbType, object value)
        {
            return new SimpleExpression(expression, dbType, value, ">");
        }

        /// <summary>
        /// 构建字段值大于参数值的条件表达式
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="expName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static SimpleExpression GreaterThan(string expression, string expName, object value)
        {
            return new SimpleExpression(expression, expName, value, ">");
        }

        /// <summary>
        /// 构建字段值大于参数值的条件表达式
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="expName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static SimpleExpression GreaterThan(string expression, string expName, DbType dbType, object value)
        {
            return new SimpleExpression(expression, expName, dbType, value, ">");
        }

        /// <summary>
        /// 构建字段值小于参数值的条件表达式
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static SimpleExpression LessThan(string expression, object value)
        {
            return new SimpleExpression(expression, value, "<");
        }

        /// <summary>
        /// 构建字段值小于参数值的条件表达式
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static SimpleExpression LessThan(string expression, DbType dbType, object value)
        {
            return new SimpleExpression(expression, dbType, value, "<");
        }

        /// <summary>
        /// 构建字段值小于参数值的条件表达式
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="expName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static SimpleExpression LessThan(string expression, string expName, object value)
        {
            return new SimpleExpression(expression, expName, value, "<");
        }

        /// <summary>
        /// 构建字段值小于参数值的条件表达式
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="expName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static SimpleExpression LessThan(string expression, string expName, DbType dbType, object value)
        {
            return new SimpleExpression(expression, expName, dbType, value, "<");
        }

        /// <summary>
        /// 构建字段值大于等于参数值的条件表达式
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static SimpleExpression GreaterEqual(string expression, object value)
        {
            return new SimpleExpression(expression, value, ">=");
        }

        /// <summary>
        /// 构建字段值大于等于参数值的条件表达式
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="expName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static SimpleExpression GreaterEqual(string expression, string expName, object value)
        {
            return new SimpleExpression(expression, expName, value, ">=");
        }

        /// <summary>
        /// 构建字段值大于等于参数值的条件表达式
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="dbType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static SimpleExpression GreaterEqual(string expression, DbType dbType, object value)
        {
            return new SimpleExpression(expression, dbType, value, ">=");
        }

        /// <summary>
        /// 构建字段值大于等于参数值的条件表达式
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="expName"></param>
        /// <param name="dbType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static SimpleExpression GreaterEqual(string expression, string expName, DbType dbType, object value)
        {
            return new SimpleExpression(expression, expName, dbType, value, ">=");
        }

        /// <summary>
        /// 小于等于
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static SimpleExpression LessEqual(string expression, object value)
        {
            return new SimpleExpression(expression, value, "<=");
        }

        /// <summary>
        /// 小于等于
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="expName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static SimpleExpression LessEqual(string expression, string expName, object value)
        {
            return new SimpleExpression(expression, expName, value, "<=");
        }

        /// <summary>
        /// 小于等于
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="dbType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static SimpleExpression LessEqual(string expression, DbType dbType, object value)
        {
            return new SimpleExpression(expression, dbType, value, "<=");
        }

        /// <summary>
        /// 小于等于
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="expName"></param>
        /// <param name="dbType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static SimpleExpression LessEqual(string expression, string expName, DbType dbType, object value)
        {
            return new SimpleExpression(expression, expName, dbType, value, "<=");
        }

        #region IExpression 成员

        /// <summary>
        /// 返回 SQL 语句表达式片段
        /// </summary>
        /// <returns></returns>
        public string ToSqlString()
        {
            return Expression + " " + Op + " @" + ExpName;
        }

        #endregion
    }
}
