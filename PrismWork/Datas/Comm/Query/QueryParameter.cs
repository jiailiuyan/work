using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Common
{
    /// <summary>
    /// QueryParameter 查询参数类
    /// 作者: 王博雯
    /// 开发时间: 2012年06月
    /// </summary>
    [Serializable]
    public class QueryParameter
    {
        private string _tableName;
        private string _tableAliasName;

        private IList<IExpression> _whereExpressions = new List<IExpression>();
        private IList<IExpression> _groupExpressions = new List<IExpression>();
        private IList<IExpression> _havingExpressions = new List<IExpression>();
        private IList<IExpression> _orderExpressions = new List<IExpression>();

        /// <summary>
        /// 获取或设置一个值, 该值表示查询条件存取的数据表名称
        /// </summary>
        public string TableName
        {
            get
            {
                return string.IsNullOrEmpty(this._tableName) ? string.Empty : this._tableName.Trim();
            }
            set
            {
                _tableName = value;
            }
        }

        /// <summary>
        /// 获取或设置一个值, 该值表示查询条件存取的数据表别名
        /// </summary>
        public string TableAliasName
        {
            get
            {
                return string.IsNullOrEmpty(this._tableAliasName) ? TableName : this._tableAliasName.Trim();
            }
            set
            {
                this._tableAliasName = value;
            }
        }

        /// <summary>
        /// 添加查询条件表达式
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public QueryParameter AddWhereExpr(LogicExpression exp)
        {
            this._whereExpressions.Add(exp);
            return this;
        }

        /// <summary>
        /// 添加查询条件表达式
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public QueryParameter AddWhereExpr(SimpleExpression exp)
        {
            this._whereExpressions.Add(exp);
            return this;
        }

        /// <summary>
        /// 添加查询条件表达式
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public QueryParameter AddWhereExpr(SegmentExpression exp)
        {
            this._whereExpressions.Add(exp);
            return this;
        }


        /// <summary>
        /// 获取查询条件表达式对象集合
        /// </summary>
        public IList<IExpression> WhereExpressions
        {
            get
            {
                return _whereExpressions;
            }
        }

        /// <summary>
        /// 添加分组表达式对象
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public QueryParameter AddGroupExpr(GroupExpression exp)
        {
            this._groupExpressions.Add(exp);
            return this;
        }

        /// <summary>
        /// 获取一个值, 该值表示分组表达式对象集合
        /// </summary>
        public IList<IExpression> GroupExpressions
        {
            get
            {
                return _groupExpressions;
            }
        }

        /// <summary>
        /// 添加 HAVING 表达式
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public QueryParameter AddHavingExpr(LogicExpression exp)
        {
            this._havingExpressions.Add(exp);
            return this;
        }

        /// <summary>
        /// 添加 HAVING 表达式
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public QueryParameter AddHavingExpr(SimpleExpression exp)
        {
            this._havingExpressions.Add(exp);
            return this;
        }

        /// <summary>
        /// 获取 HAVING 表达式集合
        /// </summary>
        public IList<IExpression> HavingExpressions
        {
            get
            {
                return _havingExpressions;
            }
        }

        /// <summary>
        /// 添加排序表达式
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public QueryParameter AddOrderExpr(OrderExpression exp)
        {
            this._orderExpressions.Add(exp);
            return this;
        }

        /// <summary>
        /// 获取排序字段集合
        /// </summary>
        public IList<IExpression> OrderExpressions
        {
            get
            {
                return _orderExpressions;
            }
        }
        
        /// <summary>
        /// 完善数据检索 SELECT 语句的查询部分
        /// </summary>
        /// <returns></returns>
        public string CompleteSelectSql()
        {
            StringBuilder sb = new StringBuilder();

            foreach (IExpression exp in this.WhereExpressions)
            {
                sb.Append(exp.ToSqlString());
            }

            if (this.GroupExpressions.Count > 0)
            {
                sb.Append(" GROUP BY ");

                bool requiredCommaSpace = false;
                foreach (IExpression exp in this.GroupExpressions)
                {
                    if (requiredCommaSpace == true) sb.Append(", ");
                    sb.Append(exp.ToSqlString());
                    requiredCommaSpace = true;
                }

                if (this.HavingExpressions.Count > 0)
                {
                    sb.Append(" HAVING ");
                    foreach (IExpression exp in this.HavingExpressions)
                        sb.Append(exp.ToSqlString());
                }
            }
            
            if (this.OrderExpressions.Count > 0)
            {
                sb.Append(" ORDER BY ");

                bool requiredCommaSpace = false;
                foreach (IExpression exp in this.OrderExpressions)
                {
                    if (requiredCommaSpace) sb.Append(", ");
                    sb.Append(exp.ToSqlString());
                    requiredCommaSpace = true;
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// 完善数据检索 SELECT 语句的查询部分
        /// </summary>
        /// <param name="select"></param>
        /// <returns></returns>
        public string CompleteSelectSql(string select)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(select);

            if (select.ToUpper().Contains(" WHERE ") == false)
            {
                if (this.WhereExpressions.Count > 0)
                {
                    sb.Append(" WHERE ");
                }
            }
            else
            {
                if (this.WhereExpressions.Count > 0)
                {
                    sb.Append(" AND ");
                }
            }

            sb.Append(this.CompleteSelectSql());

            return sb.ToString();
        }

        /// <summary>
        /// 完善SQL语句查询条件
        /// </summary>
        /// <param name="select"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string CompleteSqlString(string select, QueryParameter param)
        {
            if (param == null)
            {
                return select;
            }

            return param.CompleteSelectSql(select);
        }

    }
}
