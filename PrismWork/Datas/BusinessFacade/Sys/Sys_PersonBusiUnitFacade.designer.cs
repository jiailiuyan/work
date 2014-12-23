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
    /// 一个人可能属于多个单位业务外观类
    /// 生成日期: 2014年10月 27日 21:08
    ///</summary>
    /// <remarks>
    /// 该文件是由代码生成器自动生成的, 请不要随意修改, 你的修改将在代码重新生成时会被覆盖，
    /// 如果要对该类进行修改, 请直接修改该分部类的Sys_PersonBusiUnit文件(文件名不含.designer)
    /// </remarks>
    public partial class Sys_PersonBusiUnitFacade
    {
       private Sys_PersonBusiUnitDAO _sys_PersonBusiUnitDAO = new Sys_PersonBusiUnitDAO();
             
       /// <summary>
       /// 根据主键值查找一个人可能属于多个单位对象
       /// </summary>
       /// <param name="keyId">KeyId</param> 
       /// <returns>Sys_PersonBusiUnit</returns>
       public Sys_PersonBusiUnit FindSys_PersonBusiUnit(int keyId)
       {
           return this._sys_PersonBusiUnitDAO.FindSys_PersonBusiUnit(keyId); 
       } 
       
       /// <summary>
       /// 获取全部一个人可能属于多个单位列表
       /// </summary>
       /// <returns>Sys_PersonBusiUnit对象列表</returns>
       public IList<Sys_PersonBusiUnit> GetSys_PersonBusiUnits()
       {
            return this._sys_PersonBusiUnitDAO.GetSys_PersonBusiUnits();
       } 
       
        /// <summary>
        /// 返回满足查询条件的一个人可能属于多个单位实体列表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>
        /// 一个人可能属于多个单位实体列表</returns>
        public IList<Sys_PersonBusiUnit> GetSys_PersonBusiUnits(QueryParameter param)
        {
            return this._sys_PersonBusiUnitDAO.GetSys_PersonBusiUnits(param);
        }
        
        /// <summary>
        /// 返回满足查询条件的一个人可能属于多个单位数据表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>
        /// 一个人可能属于多个单位数据表</returns>
        public DataTable GetSys_PersonBusiUnitTable(QueryParameter param)
        {
            return this._sys_PersonBusiUnitDAO.GetSys_PersonBusiUnitTable(param);
        }
       
       /// <summary>
       /// 创建一个人可能属于多个单位记录
       /// </summary>
       /// <param name="sys_PersonBusiUnit">
       /// 一个人可能属于多个单位对象</param>
       /// <returns></returns>
       public int CreateSys_PersonBusiUnit(Sys_PersonBusiUnit sys_PersonBusiUnit)
       {
           return this._sys_PersonBusiUnitDAO.InsertSys_PersonBusiUnit(sys_PersonBusiUnit);
       }
       
        /// <summary>
        /// 更新一个人可能属于多个单位记录
        /// </summary>
        /// <param name="sys_PersonBusiUnit">
        /// 一个人可能属于多个单位对象</param>
        /// <returns>受影响的记录数</returns>
        public int UpdateSys_PersonBusiUnit(Sys_PersonBusiUnit sys_PersonBusiUnit)
        {
            return this._sys_PersonBusiUnitDAO.UpdateSys_PersonBusiUnit(sys_PersonBusiUnit);
        } 

        /// <summary>
		/// 删除一个人可能属于多个单位记录
        /// </summary>
        /// <param name="keyId">KeyId</param> 
        /// <returns>受影响的记录数</returns>
        public int DeleteSys_PersonBusiUnit(int keyId)
        {
            return this._sys_PersonBusiUnitDAO.DeleteSys_PersonBusiUnit(keyId);
        } 
    }      
}  
