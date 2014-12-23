using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

namespace Project.Common
{
    /// <summary>
    /// 代表一个字符串类型的 SQL 语句片段, 该语句片段只原样嵌入最终构造的 sql 语句条件语句中
    /// 开发人员: 王博雯
    /// 开发日期: 2012年06月
    /// </summary>
    public class SegmentExpression : IExpression
    {
        private string _expression;

        private SegmentExpression(string expression)
        {
            this._expression = expression;
        }

        public string Expression
        {
            get
            {
                return string.IsNullOrEmpty(this._expression) ? string.Empty : this._expression.Trim();
            }
        }

        /// <summary>
        /// 构造sql语句片段
        /// </summary>
        /// <param name="sqlSegment"></param>
        /// <returns></returns>
        public static SegmentExpression SqlSegment(string sqlSegment)
        {
            SegmentExpression exp = new SegmentExpression(sqlSegment);
            return exp;
        }


        #region IExpression 成员
        
        public string ToSqlString()
        {
            return this.Expression;
        }

        #endregion
    }
}
