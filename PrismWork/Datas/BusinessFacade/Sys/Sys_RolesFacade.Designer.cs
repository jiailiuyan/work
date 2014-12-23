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
    /// 项目角色业务外观类
    /// 生成日期: 2014年10月 27日 20:45
    ///</summary>
    /// <remarks>
    /// 该文件是由代码生成器自动生成的, 请不要随意修改, 你的修改将在代码重新生成时会被覆盖，
    /// 如果要对该类进行修改, 请直接修改该分部类的Sys_Roles文件(文件名不含.designer)
    /// </remarks>
    public partial class Sys_RolesFacade
    {
       private Sys_RolesDAO _sys_RolesDAO = new Sys_RolesDAO();
             
       /// <summary>
       /// 根据主键值查找项目角色对象
       /// </summary>
       /// <param name="keyId">主键</param> 
       /// <returns>Sys_Roles</returns>
       public Sys_Roles FindSys_Roles(int keyId)
       {
           return this._sys_RolesDAO.FindSys_Roles(keyId); 
       } 
       
       /// <summary>
       /// 获取全部项目角色列表
       /// </summary>
       /// <returns>Sys_Roles对象列表</returns>
       public IList<Sys_Roles> GetSys_Roless()
       {
            return this._sys_RolesDAO.GetSys_Roless();
       } 
       
        /// <summary>
        /// 返回满足查询条件的项目角色实体列表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>
        /// 项目角色实体列表</returns>
        public IList<Sys_Roles> GetSys_Roless(QueryParameter param)
        {
            return this._sys_RolesDAO.GetSys_Roless(param);
        }
        
        /// <summary>
        /// 返回满足查询条件的项目角色数据表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>
        /// 项目角色数据表</returns>
        public DataTable GetSys_RolesTable(QueryParameter param)
        {
            return this._sys_RolesDAO.GetSys_RolesTable(param);
        }
       
       /// <summary>
       /// 创建项目角色记录
       /// </summary>
       /// <param name="sys_Roles">
       /// 项目角色对象</param>
       /// <returns></returns>
       public int CreateSys_Roles(Sys_Roles sys_Roles)
       {
           return this._sys_RolesDAO.InsertSys_Roles(sys_Roles);
       }
       
        /// <summary>
        /// 更新项目角色记录
        /// </summary>
        /// <param name="sys_Roles">
        /// 项目角色对象</param>
        /// <returns>受影响的记录数</returns>
        public int UpdateSys_Roles(Sys_Roles sys_Roles)
        {
            return this._sys_RolesDAO.UpdateSys_Roles(sys_Roles);
        } 

        /// <summary>
		/// 删除项目角色记录
        /// </summary>
        /// <param name="keyId">主键</param> 
        /// <returns>受影响的记录数</returns>
        public int DeleteSys_Roles(int keyId)
        {
            return this._sys_RolesDAO.DeleteSys_Roles(keyId);
        } 
    }      
}  
