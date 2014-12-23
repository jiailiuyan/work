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
    /// Sys_PersonDeskTopConfig业务外观类
    /// 生成日期: 2014年11月 20日 18:49
    ///</summary>
    /// <remarks>
    /// 该文件是由代码生成器自动生成的, 请不要随意修改, 你的修改将在代码重新生成时会被覆盖，
    /// 如果要对该类进行修改, 请直接修改该分部类的Sys_PersonDeskTopConfig文件(文件名不含.designer)
    /// </remarks>
    public partial class Sys_PersonDeskTopConfigFacade
    {
       private Sys_PersonDeskTopConfigDAO _sys_PersonDeskTopConfigDAO = new Sys_PersonDeskTopConfigDAO();
             
       /// <summary>
       /// 根据主键值查找Sys_PersonDeskTopConfig对象
       /// </summary>
       /// <param name="keyId">KeyId</param> 
       /// <returns>Sys_PersonDeskTopConfig</returns>
       public Sys_PersonDeskTopConfig FindSys_PersonDeskTopConfig(int keyId)
       {
           return this._sys_PersonDeskTopConfigDAO.FindSys_PersonDeskTopConfig(keyId); 
       } 
       
       /// <summary>
       /// 获取全部Sys_PersonDeskTopConfig列表
       /// </summary>
       /// <returns>Sys_PersonDeskTopConfig对象列表</returns>
       public IList<Sys_PersonDeskTopConfig> GetSys_PersonDeskTopConfigs()
       {
            return this._sys_PersonDeskTopConfigDAO.GetSys_PersonDeskTopConfigs();
       } 
       
        /// <summary>
        /// 返回满足查询条件的Sys_PersonDeskTopConfig实体列表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>
        /// Sys_PersonDeskTopConfig实体列表</returns>
        public IList<Sys_PersonDeskTopConfig> GetSys_PersonDeskTopConfigs(QueryParameter param)
        {
            return this._sys_PersonDeskTopConfigDAO.GetSys_PersonDeskTopConfigs(param);
        }
        
        /// <summary>
        /// 返回满足查询条件的Sys_PersonDeskTopConfig数据表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>
        /// Sys_PersonDeskTopConfig数据表</returns>
        public DataTable GetSys_PersonDeskTopConfigTable(QueryParameter param)
        {
            return this._sys_PersonDeskTopConfigDAO.GetSys_PersonDeskTopConfigTable(param);
        }
       
       /// <summary>
       /// 创建Sys_PersonDeskTopConfig记录
       /// </summary>
       /// <param name="sys_PersonDeskTopConfig">
       /// Sys_PersonDeskTopConfig对象</param>
       /// <returns></returns>
       public int CreateSys_PersonDeskTopConfig(Sys_PersonDeskTopConfig sys_PersonDeskTopConfig)
       {
           return this._sys_PersonDeskTopConfigDAO.InsertSys_PersonDeskTopConfig(sys_PersonDeskTopConfig);
       }
       
        /// <summary>
        /// 更新Sys_PersonDeskTopConfig记录
        /// </summary>
        /// <param name="sys_PersonDeskTopConfig">
        /// Sys_PersonDeskTopConfig对象</param>
        /// <returns>受影响的记录数</returns>
        public int UpdateSys_PersonDeskTopConfig(Sys_PersonDeskTopConfig sys_PersonDeskTopConfig)
        {
            return this._sys_PersonDeskTopConfigDAO.UpdateSys_PersonDeskTopConfig(sys_PersonDeskTopConfig);
        } 

        /// <summary>
		/// 删除Sys_PersonDeskTopConfig记录
        /// </summary>
        /// <param name="keyId">KeyId</param> 
        /// <returns>受影响的记录数</returns>
        public int DeleteSys_PersonDeskTopConfig(int keyId)
        {
            return this._sys_PersonDeskTopConfigDAO.DeleteSys_PersonDeskTopConfig(keyId);
        } 
    }      
}  
