using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Common
{
    /// <summary>
    /// 构造分组表达式 SQL 语句片段类
    /// 作者: 王博雯
    /// 开发时间: 2012年06月
    /// </summary>
    public class GroupExpression : IExpression
    {
        private string _groupExp;

        private GroupExpression(string groupExp)
        {
            this._groupExp = groupExp;
        }

        /// <summary>
        /// 创建一个分组表达式对象
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static GroupExpression CreateInstance(string expression)
        {
            GroupExpression group = GroupBy(expression);

            return group;
        }

        /// <summary>
        /// 返回分组表达式对象
        /// </summary>
        /// <param name="groupExp">分组表达式, 可以是分组字段名称, 也可以是表达式(如: AVG(Age), 数量 * 单价)</param>
        /// <returns>分组表达式对象</returns>
        public static GroupExpression GroupBy(string groupExp)
        {
            if (string.IsNullOrEmpty(groupExp))
                throw new ArgumentNullException("groupExp");

            return new GroupExpression(groupExp);
        }

        #region IExpression 成员

        /// <summary>
        /// 返回分组表达式对象
        /// </summary>
        public string ToSqlString()
        {
            return string.IsNullOrEmpty(_groupExp) ? string.Empty : _groupExp.Trim();
        }

        #endregion
    }
}
