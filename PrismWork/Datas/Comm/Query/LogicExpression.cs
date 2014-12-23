using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Common
{
    /// <summary>
    /// 构造逻辑表达式 SQL 语句片段类
    /// 作者: 王博雯
    /// 开发时间: 2012年06月
    /// </summary>
    public class LogicExpression : IExpression
    {
        private string _logic;

        private LogicExpression(string logic)
        {
            _logic = logic;
        }

        /// <summary>
        /// 创建一个逻辑表达式对象
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static LogicExpression CreateInstance(string expression)
        {
            LogicExpression logic = null;
            
            switch (expression)
            {
                case "和":
                case "AND":
                case "And":
                case "and":
                    logic = LogicExpression.And();
                    break;

                case "或":
                case "OR":
                case "Or":
                case "or":
                    logic = LogicExpression.Or();
                    break;

                case "非":
                case "NOT":
                case "Not":
                case "not":
                    logic = LogicExpression.Not();
                    break;

                case "左括号":
                case "(":
                    logic = LogicExpression.OpenParentheses();
                    break;
                    
                case "右括号":
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
        /// 返回 AND 运算符逻辑式
        /// </summary>
        /// <returns></returns>
        public static LogicExpression And()
        {
            return new LogicExpression(" AND ");
        }

        /// <summary>
        /// 返回 OR 运算符逻辑式
        /// </summary>
        /// <returns></returns>
        public static LogicExpression Or()
        {
            return new LogicExpression(" OR ");
        }

        /// <summary>
        /// 返回 NOT 运算符逻辑式
        /// </summary>
        /// <returns></returns>
        public static LogicExpression Not()
        {
            return new LogicExpression(" NOT ");
        }

        /// <summary>
        /// 返回 '(' 运算符逻辑式
        /// </summary>
        /// <returns></returns>
        public static LogicExpression OpenParentheses()
        {
            return new LogicExpression("(");
        }

        /// <summary>
        /// 返回 ')' 运算符逻辑式
        /// </summary>
        /// <returns></returns>
        public static LogicExpression ClosedParentheses()
        {
            return new LogicExpression(")");
        }

        #region IExpression 成员

        /// <summary>
        /// 返回逻辑表达式SQL语句片段
        /// </summary>
        /// <returns></returns>
        public string ToSqlString()
        {
            return _logic == null ? string.Empty : _logic;
        }

        #endregion
    }
}
