using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Common
{
    /// <summary>
    /// 构造排序表达式 SQL 语句片段类
    /// 作者: 王博雯
    /// 开发时间: 2012年06月
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
        /// 创建一个排序表达式实例
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static OrderExpression CreateInstance(string fieldName, string direction)
        {
            OrderExpression order = null;
            switch (direction)
            {
                case "降序":
                case "降":
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
        /// 获取排序方向
        /// </summary>
        public bool Ascending
        {
            get
            {
                return _ascending;
            }
        }

        /// <summary>
        /// 获取排序字段名称
        /// </summary>
        public string FieldName
        {
            get
            {
                return _fieldName.Trim();
            }
        }

        /// <summary>
        /// 获取指定字段升序排序的排序对象
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
        /// 获取指定字段降序排序的排序对象
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
        /// 获取排序方向
        /// </summary>
        public string SortDirection
        {
            get
            {
                return _ascending ? "ASC" : "DESC";
            }
        }

        /// <summary>
        /// 返回排序字段的 SQL 语句片段
        /// </summary>
        /// <returns></returns>
        public string ToSqlString()
        {
            return _fieldName + (_ascending ? " ASC" : " DESC");
        }
    }
}
