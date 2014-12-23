using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Common
{
    /// <summary>
    /// ����������ʽ SQL ���Ƭ����
    /// ����: ������
    /// ����ʱ��: 2012��06��
    /// </summary>
    public class OrderExpression : IExpression
    {
        private bool _ascending;
        private string _fieldName;

        private OrderExpression(string fieldName, bool ascending)
        {
            this._fieldName = fieldName;
            this._ascending = ascending;
        }

        /// <summary>
        /// ����һ��������ʽʵ��
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static OrderExpression CreateInstance(string fieldName, string direction)
        {
            OrderExpression order = null;
            switch (direction)
            {
                case "����":
                case "��":
                case "DESC":
                case "Desc":
                case "desc":
                case "D":
                case "d":
                    order = OrderExpression.Desc(fieldName);
                    break;

                default:
                    order = OrderExpression.Asc(fieldName);
                    break;

            }

            return order;
        }

        /// <summary>
        /// ��ȡ������
        /// </summary>
        public bool Ascending
        {
            get
            {
                return _ascending;
            }
        }

        /// <summary>
        /// ��ȡ�����ֶ�����
        /// </summary>
        public string FieldName
        {
            get
            {
                return _fieldName.Trim();
            }
        }

        /// <summary>
        /// ��ȡָ���ֶ�����������������
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static OrderExpression Asc(string fieldName)
        {
            if (string.IsNullOrEmpty(fieldName))
                throw new ArgumentNullException("fieldName");
            OrderExpression exp = new OrderExpression(fieldName, true);
            return exp;
        }

        /// <summary>
        /// ��ȡָ���ֶν���������������
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static OrderExpression Desc(string fieldName)
        {
            if (string.IsNullOrEmpty(fieldName))
                throw new ArgumentNullException("fieldName");

            OrderExpression exp = new OrderExpression(fieldName, false);

            return exp;
        }

        /// <summary>
        /// ��ȡ������
        /// </summary>
        public string SortDirection
        {
            get
            {
                return _ascending ? "ASC" : "DESC";
            }
        }

        /// <summary>
        /// ���������ֶε� SQL ���Ƭ��
        /// </summary>
        /// <returns></returns>
        public string ToSqlString()
        {
            return _fieldName + (_ascending ? " ASC" : " DESC");
        }
    }
}
