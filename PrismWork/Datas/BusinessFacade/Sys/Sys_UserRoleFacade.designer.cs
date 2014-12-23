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
    /// 用户角色对应关系业务外观类
    /// 生成日期: 2014年10月 27日 21:08
    ///</summary>
    /// <remarks>
    /// 该文件是由代码生成器自动生成的, 请不要随意修改, 你的修改将在代码重新生成时会被覆盖，
    /// 如果要对该类进行修改, 请直接修改该分部类的Sys_UserRole文件(文件名不含.designer)
    /// </remarks>
    public partial class Sys_UserRoleFacade
    {
       private Sys_UserRoleDAO _sys_UserRoleDAO = new Sys_UserRoleDAO();
             
       /// <summary>
       /// 根据主键值查找用户角色对应关系对象
       /// </summary>
       /// <param name="keyId">主键</param> 
       /// <returns>Sys_UserRole</returns>
       public Sys_UserRole FindSys_UserRole(int keyId)
       {
           return this._sys_UserRoleDAO.FindSys_UserRole(keyId); 
       } 
       
       /// <summary>
       /// 获取全部用户角色对应关系列表
       /// </summary>
       /// <returns>Sys_UserRole对象列表</returns>
       public IList<Sys_UserRole> GetSys_UserRoles()
       {
            return this._sys_UserRoleDAO.GetSys_UserRoles();
       } 
       
        /// <summary>
        /// 返回满足查询条件的用户角色对应关系实体列表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>
        /// 用户角色对应关系实体列表</returns>
        public IList<Sys_UserRole> GetSys_UserRoles(QueryParameter param)
        {
            return this._sys_UserRoleDAO.GetSys_UserRoles(param);
        }
        
        /// <summary>
        /// 返回满足查询条件的用户角色对应关系数据表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>
        /// 用户角色对应关系数据表</returns>
        public DataTable GetSys_UserRoleTable(QueryParameter param)
        {
            return this._sys_UserRoleDAO.GetSys_UserRoleTable(param);
        }
       
       /// <summary>
       /// 创建用户角色对应关系记录
       /// </summary>
       /// <param name="sys_UserRole">
       /// 用户角色对应关系对象</param>
       /// <returns></returns>
       public int CreateSys_UserRole(Sys_UserRole sys_UserRole)
       {
           return this._sys_UserRoleDAO.InsertSys_UserRole(sys_UserRole);
       }
       
        /// <summary>
        /// 更新用户角色对应关系记录
        /// </summary>
        /// <param name="sys_UserRole">
        /// 用户角色对应关系对象</param>
        /// <returns>受影响的记录数</returns>
        public int UpdateSys_UserRole(Sys_UserRole sys_UserRole)
        {
            return this._sys_UserRoleDAO.UpdateSys_UserRole(sys_UserRole);
        } 

        /// <summary>
		/// 删除用户角色对应关系记录
        /// </summary>
        /// <param name="keyId">主键</param> 
        /// <returns>受影响的记录数</returns>
        public int DeleteSys_UserRole(int keyId)
        {
            return this._sys_UserRoleDAO.DeleteSys_UserRole(keyId);
        } 
    }      
}  
