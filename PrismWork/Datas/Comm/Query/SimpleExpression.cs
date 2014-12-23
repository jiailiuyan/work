using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

namespace Project.Common
{
    /// <summary>
    /// ����򵥲�ѯ�������ʽ SQL ���Ƭ����
    /// ����: ������
    /// ����ʱ��: 2012��06��
    /// </summary>
    public class SimpleExpression : IExpression
    {
        private string _expName;
        private string _expression;
        private object _value;
        private string _op;
        private DbType _dbType = DbType.Object;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="expression">���ʽ(��: �ֶ���, AVG(�ֶ���), ���� * ����)</param>
        /// <param name="value"></param>
        /// <param name="op"></param>
        private SimpleExpression(string expression, object value, string op)
        {
            if (string.IsNullOrEmpty(expression))
                throw new ArgumentNullException("�����ѯ������ expression Ϊ��");

            if (value == null)
                throw new ArgumentNullException("�����ѯ�����ı�ʾʽ�� value Ϊ��");

            this._expression = expression;
            this._value = value;
            this._op = op;
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="expression">���ʽ(��: �ֶ���, AVG(�ֶ���), ���� * ����)</param>
        /// <param name="value"></param>
        /// <param name="op"></param>
        private SimpleExpression(string expression, DbType dbType, object value, string op)
            : this(expression, value, op)
        {
            this._dbType = dbType;
        }

        /// <summary>
        /// ���캯��
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
        /// ���캯��
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
        /// ������ѯ�������ʽ
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
                case "����":
                case "=":
                    result = SimpleExpression.Equal(expression, expName, value);
                    break;

                case "������":
                case "<>":
                    result = SimpleExpression.NotEqual(expression, expName, value);
                    break;

                case "����":
                case ">":
                    result = SimpleExpression.GreaterThan(expression, expName, value);
                    break;

                case "���ڵ���":
                case "���ڻ����":
                case ">=":
                    result = SimpleExpression.GreaterEqual(expression, expName, value);
                    break;

                case "С��":
                case "<":
                    result = SimpleExpression.LessThan(expression, expName, value);
                    break;

                case "С�ڵ���":
                case "С�ڻ����":
                case "<=":
                    result = SimpleExpression.LessEqual(expression, expName, value);
                    break;

                case "������":
                case "��":
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
        /// ������ѯ�������ʽ
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
        /// ��ȡ���ʽ
        /// </summary>
        public string Expression
        {
            get
            {
                return string.IsNullOrEmpty(_expression) ? string.Empty : _expression.Trim();
            }
        }

        /// <summary>
        /// ��ȡ���ʽ����
        /// </summary>
        public string ExpName
        {
            get
            {
                return string.IsNullOrEmpty(_expName) ? Expression : _expName.Trim();
            }
        }

        /// <summary>
        /// ��ȡ��������
        /// </summary>
        public DbType DbType
        {
            get
            {
                return this._dbType;
            }
        }

        /// <summary>
        /// ��ȡ�����ֶ�ֵ
        /// </summary>
        public object Value
        {
            get
            {
                return _value;
            }
        }

        /// <summary>
        /// ��ȡ��ѯ������
        /// </summary>
        public string Op
        {
            get
            {
                return _op;
            }
        }

        /// <summary>
        /// �����ֶ�ֵ���ڲ���ֵ���������ʽ
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static SimpleExpression Equal(string expression, object value)
        {
            return new SimpleExpression(expression, value, "=");
        }

        /// <summary>
        /// �����ֶ�ֵ���ڲ���ֵ���������ʽ
        /// </summary>
        /// <param name="expression">��ʾʽ</param>
        /// <param name="dbType">��������</param>
        /// <param name="value">����ֵ����</param>
        /// <returns></returns>
        public static SimpleExpression Equal(string expression, DbType dbType, object value)
        {
            return new SimpleExpression(expression, dbType, value, "=");
        }

        /// <summary>
        /// �����ֶ�ֵ���ڲ���ֵ���������ʽ
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
        /// �����ֶ�ֵ���ڲ���ֵ���������ʽ
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
        /// �����ֶ�ֵ�����ڲ���ֵ���������ʽ
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static SimpleExpression NotEqual(string expression, object value)
        {
            return new SimpleExpression(expression, value, "<>");
        }

        /// <summary>
        /// �����ֶ�ֵ�����ڲ���ֵ���������ʽ
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static SimpleExpression NotEqual(string expression, DbType dbType, object value)
        {
            return new SimpleExpression(expression, dbType, value, "<>");
        }

        /// <summary>
        /// �����ֶ�ֵ�����ڲ���ֵ���������ʽ
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
        /// �����ֶ�ֵ�����ڲ���ֵ���������ʽ
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
        /// �����ֶ�ֵ�����ڲ���ֵ���������ʽ
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static SimpleExpression Like(string expression, object value)
        {
            return new SimpleExpression(expression, value, "LIKE");
        }

        /// <summary>
        /// �����ֶ�ֵ�����ڲ���ֵ���������ʽ
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static SimpleExpression Like(string expression, DbType dbType, object value)
        {
            return new SimpleExpression(expression, dbType, value, "LIKE");
        }

        /// <summary>
        /// �����ֶ�ֵ�����ڲ���ֵ���������ʽ
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
        /// �����ֶ�ֵ�����ڲ���ֵ���������ʽ
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
        /// �����ֶ�ֵ���ڲ���ֵ���������ʽ
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static SimpleExpression GreaterThan(string expression, object value)
        {
            return new SimpleExpression(expression, value, ">");
        }

        /// <summary>
        /// �����ֶ�ֵ���ڲ���ֵ���������ʽ
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static SimpleExpression GreaterThan(string expression, DbType dbType, object value)
        {
            return new SimpleExpression(expression, dbType, value, ">");
        }

        /// <summary>
        /// �����ֶ�ֵ���ڲ���ֵ���������ʽ
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
        /// �����ֶ�ֵ���ڲ���ֵ���������ʽ
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
        /// �����ֶ�ֵС�ڲ���ֵ���������ʽ
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static SimpleExpression LessThan(string expression, object value)
        {
            return new SimpleExpression(expression, value, "<");
        }

        /// <summary>
        /// �����ֶ�ֵС�ڲ���ֵ���������ʽ
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static SimpleExpression LessThan(string expression, DbType dbType, object value)
        {
            return new SimpleExpression(expression, dbType, value, "<");
        }

        /// <summary>
        /// �����ֶ�ֵС�ڲ���ֵ���������ʽ
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
        /// �����ֶ�ֵС�ڲ���ֵ���������ʽ
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
        /// �����ֶ�ֵ���ڵ��ڲ���ֵ���������ʽ
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static SimpleExpression GreaterEqual(string expression, object value)
        {
            return new SimpleExpression(expression, value, ">=");
        }

        /// <summary>
        /// �����ֶ�ֵ���ڵ��ڲ���ֵ���������ʽ
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
        /// �����ֶ�ֵ���ڵ��ڲ���ֵ���������ʽ
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
        /// �����ֶ�ֵ���ڵ��ڲ���ֵ���������ʽ
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
        /// С�ڵ���
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static SimpleExpression LessEqual(string expression, object value)
        {
            return new SimpleExpression(expression, value, "<=");
        }

        /// <summary>
        /// С�ڵ���
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
        /// С�ڵ���
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
        /// С�ڵ���
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

        #region IExpression ��Ա

        /// <summary>
        /// ���� SQL �����ʽƬ��
        /// </summary>
        /// <returns></returns>
        public string ToSqlString()
        {
            return Expression + " " + Op + " @" + ExpName;
        }

        #endregion
    }
}
