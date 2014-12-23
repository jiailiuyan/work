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
    /// 角色功能对应表业务外观类
    /// 生成日期: 2014年10月 27日 20:59
    ///</summary>
    /// <remarks>
    /// 该文件是由代码生成器自动生成的, 请不要随意修改, 你的修改将在代码重新生成时会被覆盖，
    /// 如果要对该类进行修改, 请直接修改该分部类的Sys_RoleModules文件(文件名不含.designer)
    /// </remarks>
    public partial class Sys_RoleModulesFacade
    {
       private Sys_RoleModulesDAO _sys_RoleModulesDAO = new Sys_RoleModulesDAO();
             
       /// <summary>
       /// 根据主键值查找角色功能对应表对象
       /// </summary>
       /// <param name="keyId">角色权限主键</param> 
       /// <returns>Sys_RoleModules</returns>
       public Sys_RoleModules FindSys_RoleModules(int keyId)
       {
           return this._sys_RoleModulesDAO.FindSys_RoleModules(keyId); 
       } 
       
       /// <summary>
       /// 获取全部角色功能对应表列表
       /// </summary>
       /// <returns>Sys_RoleModules对象列表</returns>
       public IList<Sys_RoleModules> GetSys_RoleModuless()
       {
            return this._sys_RoleModulesDAO.GetSys_RoleModuless();
       } 
       
        /// <summary>
        /// 返回满足查询条件的角色功能对应表实体列表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>
        /// 角色功能对应表实体列表</returns>
        public IList<Sys_RoleModules> GetSys_RoleModuless(QueryParameter param)
        {
            return this._sys_RoleModulesDAO.GetSys_RoleModuless(param);
        }
        
        /// <summary>
        /// 返回满足查询条件的角色功能对应表数据表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>
        /// 角色功能对应表数据表</returns>
        public DataTable GetSys_RoleModulesTable(QueryParameter param)
        {
            return this._sys_RoleModulesDAO.GetSys_RoleModulesTable(param);
        }
       
       /// <summary>
       /// 创建角色功能对应表记录
       /// </summary>
       /// <param name="sys_RoleModules">
       /// 角色功能对应表对象</param>
       /// <returns></returns>
       public int CreateSys_RoleModules(Sys_RoleModules sys_RoleModules)
       {
           return this._sys_RoleModulesDAO.InsertSys_RoleModules(sys_RoleModules);
       }
       
        /// <summary>
        /// 更新角色功能对应表记录
        /// </summary>
        /// <param name="sys_RoleModules">
        /// 角色功能对应表对象</param>
        /// <returns>受影响的记录数</returns>
        public int UpdateSys_RoleModules(Sys_RoleModules sys_RoleModules)
        {
            return this._sys_RoleModulesDAO.UpdateSys_RoleModules(sys_RoleModules);
        } 

        /// <summary>
		/// 删除角色功能对应表记录
        /// </summary>
        /// <param name="keyId">角色权限主键</param> 
        /// <returns>受影响的记录数</returns>
        public int DeleteSys_RoleModules(int keyId)
        {
            return this._sys_RoleModulesDAO.DeleteSys_RoleModules(keyId);
        } 
    }      
}  
