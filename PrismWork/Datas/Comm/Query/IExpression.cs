using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Common
{
    /// <summary>
    /// ���� SQL ���Ƭ�εĽӿ�
    /// ����: ������
    /// ����ʱ��: 2012��06��
    /// </summary>
    public interface IExpression
    {
        /// <summary>
        /// ���� SQL ���Ƭ�εĽӿ�
        /// ����: chunjianjun
        /// ����ʱ��: 2007-9
        /// </summary>
        string ToSqlString();
    }
}
