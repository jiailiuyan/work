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
    /// 项目基本信息业务外观类
    /// 生成日期: 2014年10月 27日 20:34
    ///</summary>
    /// <remarks>
    /// 该文件是由代码生成器自动生成的, 请不要随意修改, 你的修改将在代码重新生成时会被覆盖，
    /// 如果要对该类进行修改, 请直接修改该分部类的Sys_ProjInfo文件(文件名不含.designer)
    /// </remarks>
    public partial class Sys_ProjInfoFacade
    {
       private Sys_ProjInfoDAO _sys_ProjInfoDAO = new Sys_ProjInfoDAO();
             
       /// <summary>
       /// 根据主键值查找项目基本信息对象
       /// </summary>
       /// <param name="keyId">项目内码</param> 
       /// <returns>Sys_ProjInfo</returns>
       public Sys_ProjInfo FindSys_ProjInfo(int keyId)
       {
           return this._sys_ProjInfoDAO.FindSys_ProjInfo(keyId); 
       } 
       
       /// <summary>
       /// 获取全部项目基本信息列表
       /// </summary>
       /// <returns>Sys_ProjInfo对象列表</returns>
       public IList<Sys_ProjInfo> GetSys_ProjInfos()
       {
            return this._sys_ProjInfoDAO.GetSys_ProjInfos();
       } 
       
        /// <summary>
        /// 返回满足查询条件的项目基本信息实体列表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>
        /// 项目基本信息实体列表</returns>
        public IList<Sys_ProjInfo> GetSys_ProjInfos(QueryParameter param)
        {
            return this._sys_ProjInfoDAO.GetSys_ProjInfos(param);
        }
        
        /// <summary>
        /// 返回满足查询条件的项目基本信息数据表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>
        /// 项目基本信息数据表</returns>
        public DataTable GetSys_ProjInfoTable(QueryParameter param)
        {
            return this._sys_ProjInfoDAO.GetSys_ProjInfoTable(param);
        }
       
       /// <summary>
       /// 创建项目基本信息记录
       /// </summary>
       /// <param name="sys_ProjInfo">
       /// 项目基本信息对象</param>
       /// <returns></returns>
       public int CreateSys_ProjInfo(Sys_ProjInfo sys_ProjInfo)
       {
           return this._sys_ProjInfoDAO.InsertSys_ProjInfo(sys_ProjInfo);
       }
       
        /// <summary>
        /// 更新项目基本信息记录
        /// </summary>
        /// <param name="sys_ProjInfo">
        /// 项目基本信息对象</param>
        /// <returns>受影响的记录数</returns>
        public int UpdateSys_ProjInfo(Sys_ProjInfo sys_ProjInfo)
        {
            return this._sys_ProjInfoDAO.UpdateSys_ProjInfo(sys_ProjInfo);
        } 

        /// <summary>
		/// 删除项目基本信息记录
        /// </summary>
        /// <param name="keyId">项目内码</param> 
        /// <returns>受影响的记录数</returns>
        public int DeleteSys_ProjInfo(int keyId)
        {
            return this._sys_ProjInfoDAO.DeleteSys_ProjInfo(keyId);
        } 
    }      
}  
