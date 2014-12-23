using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Common
{
    /// <summary>
    /// �����߼����ʽ SQL ���Ƭ����
    /// ����: ������
    /// ����ʱ��: 2012��06��
    /// </summary>
    public class LogicExpression : IExpression
    {
        private string _logic;

        private LogicExpression(string logic)
        {
            _logic = logic;
        }

        /// <summary>
        /// ����һ���߼����ʽ����
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static LogicExpression CreateInstance(string expression)
        {
            LogicExpression logic = null;
            
            switch (expression)
            {
                case "��":
                case "AND":
                case "And":
                case "and":
                    logic = LogicExpression.And();
                    break;

                case "��":
                case "OR":
                case "Or":
                case "or":
                    logic = LogicExpression.Or();
                    break;

                case "��":
                case "NOT":
                case "Not":
                case "not":
                    logic = LogicExpression.Not();
                    break;

                case "������":
                case "(":
                    logic = LogicExpression.OpenParentheses();
                    break;
                    
                case "������":
                case ")":
                    logic = LogicExpression.OpenParentheses();
                    break;

                default:
                    logic = LogicExpression.And();
                    break;
            }

            return logic;
        }
                
        /// <summary>
        /// ���� AND ������߼�ʽ
        /// </summary>
        /// <returns></returns>
        public static LogicExpression And()
        {
            return new LogicExpression(" AND ");
        }

        /// <summary>
        /// ���� OR ������߼�ʽ
        /// </summary>
        /// <returns></returns>
        public static LogicExpression Or()
        {
            return new LogicExpression(" OR ");
        }

        /// <summary>
        /// ���� NOT ������߼�ʽ
        /// </summary>
        /// <returns></returns>
        public static LogicExpression Not()
        {
            return new LogicExpression(" NOT ");
        }

        /// <summary>
        /// ���� '(' ������߼�ʽ
        /// </summary>
        /// <returns></returns>
        public static LogicExpression OpenParentheses()
        {
            return new LogicExpression("(");
        }

        /// <summary>
        /// ���� ')' ������߼�ʽ
        /// </summary>
        /// <returns></returns>
        public static LogicExpression ClosedParentheses()
        {
            return new LogicExpression(")");
        }

        #region IExpression ��Ա

        /// <summary>
        /// �����߼����ʽSQL���Ƭ��
        /// </summary>
        /// <returns></returns>
        public string ToSqlString()
        {
            return _logic == null ? string.Empty : _logic;
        }

        #endregion
    }
}
