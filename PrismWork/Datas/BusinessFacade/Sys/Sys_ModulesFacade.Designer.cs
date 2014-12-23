using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Project.Common;
using Project.DataAccess;
using Project.Entities;

namespace Project.BusinessFacade 
{
    /// <summary>
    /// 项目模块业务外观类
    /// 生成日期: 2014年10月 27日 21:50
    ///</summary>
    /// <remarks>
    /// 该文件是由代码生成器自动生成的, 请不要随意修改, 你的修改将在代码重新生成时会被覆盖，
    /// 如果要对该类进行修改, 请直接修改该分部类的Sys_Modules文件(文件名不含.designer)
    /// </remarks>
    public partial class Sys_ModulesFacade
    {
       private Sys_ModulesDAO _sys_ModulesDAO = new Sys_ModulesDAO();
             
       /// <summary>
       /// 根据主键值查找项目模块对象
       /// </summary>
       /// <param name="keyId">模块内码</param> 
       /// <returns>Sys_Modules</returns>
       public Sys_Modules FindSys_Modules(int keyId)
       {
           return this._sys_ModulesDAO.FindSys_Modules(keyId); 
       } 
       
       /// <summary>
       /// 获取全部项目模块列表
       /// </summary>
       /// <returns>Sys_Modules对象列表</returns>
       public IList<Sys_Modules> GetSys_Moduless()
       {
            return this._sys_ModulesDAO.GetSys_Moduless();
       } 
       
        /// <summary>
        /// 返回满足查询条件的项目模块实体列表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>
        /// 项目模块实体列表</returns>
        public IList<Sys_Modules> GetSys_Moduless(QueryParameter param)
        {
            return this._sys_ModulesDAO.GetSys_Moduless(param);
        }
        
        /// <summary>
        /// 返回满足查询条件的项目模块数据表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>
        /// 项目模块数据表</returns>
        public DataTable GetSys_ModulesTable(QueryParameter param)
        {
            return this._sys_ModulesDAO.GetSys_ModulesTable(param);
        }
       
       /// <summary>
       /// 创建项目模块记录
       /// </summary>
       /// <param name="sys_Modules">
       /// 项目模块对象</param>
       /// <returns></returns>
       public int CreateSys_Modules(Sys_Modules sys_Modules)
       {
           return this._sys_ModulesDAO.InsertSys_Modules(sys_Modules);
       }
       
        /// <summary>
        /// 更新项目模块记录
        /// </summary>
        /// <param name="sys_Modules">
        /// 项目模块对象</param>
        /// <returns>受影响的记录数</returns>
        public int UpdateSys_Modules(Sys_Modules sys_Modules)
        {
            return this._sys_ModulesDAO.UpdateSys_Modules(sys_Modules);
        } 

        /// <summary>
		/// 删除项目模块记录
        /// </summary>
        /// <param name="keyId">模块内码</param> 
        /// <returns>受影响的记录数</returns>
        public int DeleteSys_Modules(int keyId)
        {
            return this._sys_ModulesDAO.DeleteSys_Modules(keyId);
        } 
    }      
}  
