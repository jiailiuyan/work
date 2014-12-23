using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Common
{
    /// <summary>
    /// 构造 SQL 语句片段的接口
    /// 作者: 王博雯
    /// 开发时间: 2012年06月
    /// </summary>
    public interface IExpression
    {
        /// <summary>
        /// 构造 SQL 语句片段的接口
        /// 作者: chunjianjun
        /// 开发时间: 2007-9
        /// </summary>
        string ToSqlString();
    }
}
