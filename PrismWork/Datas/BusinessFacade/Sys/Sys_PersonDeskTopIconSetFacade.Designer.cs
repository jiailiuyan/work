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
    /// Sys_PersonDeskTopIconSet业务外观类
    /// 生成日期: 2014年11月 20日 18:47
    ///</summary>
    /// <remarks>
    /// 该文件是由代码生成器自动生成的, 请不要随意修改, 你的修改将在代码重新生成时会被覆盖，
    /// 如果要对该类进行修改, 请直接修改该分部类的Sys_PersonDeskTopIconSet文件(文件名不含.designer)
    /// </remarks>
    public partial class Sys_PersonDeskTopIconSetFacade
    {
       private Sys_PersonDeskTopIconSetDAO _sys_PersonDeskTopIconSetDAO = new Sys_PersonDeskTopIconSetDAO();
             
       /// <summary>
       /// 根据主键值查找Sys_PersonDeskTopIconSet对象
       /// </summary>
       /// <param name="keyId">KeyId</param> 
       /// <returns>Sys_PersonDeskTopIconSet</returns>
       public Sys_PersonDeskTopIconSet FindSys_PersonDeskTopIconSet(int keyId)
       {
           return this._sys_PersonDeskTopIconSetDAO.FindSys_PersonDeskTopIconSet(keyId); 
       } 
       
       /// <summary>
       /// 获取全部Sys_PersonDeskTopIconSet列表
       /// </summary>
       /// <returns>Sys_PersonDeskTopIconSet对象列表</returns>
       public IList<Sys_PersonDeskTopIconSet> GetSys_PersonDeskTopIconSets()
       {
            return this._sys_PersonDeskTopIconSetDAO.GetSys_PersonDeskTopIconSets();
       } 
       
        /// <summary>
        /// 返回满足查询条件的Sys_PersonDeskTopIconSet实体列表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>
        /// Sys_PersonDeskTopIconSet实体列表</returns>
        public IList<Sys_PersonDeskTopIconSet> GetSys_PersonDeskTopIconSets(QueryParameter param)
        {
            return this._sys_PersonDeskTopIconSetDAO.GetSys_PersonDeskTopIconSets(param);
        }
        
        /// <summary>
        /// 返回满足查询条件的Sys_PersonDeskTopIconSet数据表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>
        /// Sys_PersonDeskTopIconSet数据表</returns>
        public DataTable GetSys_PersonDeskTopIconSetTable(QueryParameter param)
        {
            return this._sys_PersonDeskTopIconSetDAO.GetSys_PersonDeskTopIconSetTable(param);
        }
       
       /// <summary>
       /// 创建Sys_PersonDeskTopIconSet记录
       /// </summary>
       /// <param name="sys_PersonDeskTopIconSet">
       /// Sys_PersonDeskTopIconSet对象</param>
       /// <returns></returns>
       public int CreateSys_PersonDeskTopIconSet(Sys_PersonDeskTopIconSet sys_PersonDeskTopIconSet)
       {
           return this._sys_PersonDeskTopIconSetDAO.InsertSys_PersonDeskTopIconSet(sys_PersonDeskTopIconSet);
       }
       
        /// <summary>
        /// 更新Sys_PersonDeskTopIconSet记录
        /// </summary>
        /// <param name="sys_PersonDeskTopIconSet">
        /// Sys_PersonDeskTopIconSet对象</param>
        /// <returns>受影响的记录数</returns>
        public int UpdateSys_PersonDeskTopIconSet(Sys_PersonDeskTopIconSet sys_PersonDeskTopIconSet)
        {
            return this._sys_PersonDeskTopIconSetDAO.UpdateSys_PersonDeskTopIconSet(sys_PersonDeskTopIconSet);
        } 

        /// <summary>
		/// 删除Sys_PersonDeskTopIconSet记录
        /// </summary>
        /// <param name="keyId">KeyId</param> 
        /// <returns>受影响的记录数</returns>
        public int DeleteSys_PersonDeskTopIconSet(int keyId)
        {
            return this._sys_PersonDeskTopIconSetDAO.DeleteSys_PersonDeskTopIconSet(keyId);
        } 
    }      
}  
