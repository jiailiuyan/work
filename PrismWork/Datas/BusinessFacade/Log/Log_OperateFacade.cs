using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Transactions;
using Project.Common;
using Project.DataAccess;
using Project.Entities;

namespace Project.BusinessFacade 
{  
	/// <summary>
    /// Log_Operate业务外观类
    /// 开发人员: 
    /// 生成日期: 2014年10月 27日 20:24
    ///</summary>
    public partial class Log_OperateFacade
    {
        /// <summary>
        /// 根据传入的条件获取日志信息
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable GetLogData(string strWhere)
        {
            return null;
        }
    }
}  
