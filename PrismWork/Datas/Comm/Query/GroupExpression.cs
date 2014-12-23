using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Common
{
    /// <summary>
    /// ���������ʽ SQL ���Ƭ����
    /// ����: ������
    /// ����ʱ��: 2012��06��
    /// </summary>
    public class GroupExpression : IExpression
    {
        private string _groupExp;

        private GroupExpression(string groupExp)
        {
            this._groupExp = groupExp;
        }

        /// <summary>
        /// ����һ��������ʽ����
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static GroupExpression CreateInstance(string expression)
        {
            GroupExpression group = GroupBy(expression);

            return group;
        }

        /// <summary>
        /// ���ط�����ʽ����
        /// </summary>
        /// <param name="groupExp">������ʽ, �����Ƿ����ֶ�����, Ҳ�����Ǳ��ʽ(��: AVG(Age), ���� * ����)</param>
        /// <returns>������ʽ����</returns>
        public static GroupExpression GroupBy(string groupExp)
        {
            if (string.IsNullOrEmpty(groupExp))
                throw new ArgumentNullException("groupExp");

            return new GroupExpression(groupExp);
        }

        #region IExpression ��Ա

        /// <summary>
        /// ���ط�����ʽ����
        /// </summary>
        public string ToSqlString()
        {
            return string.IsNullOrEmpty(_groupExp) ? string.Empty : _groupExp.Trim();
        }

        #endregion
    }
}
