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
    /// 业务单位信息业务外观类
    /// 生成日期: 2014年10月 27日 21:07
    ///</summary>
    /// <remarks>
    /// 该文件是由代码生成器自动生成的, 请不要随意修改, 你的修改将在代码重新生成时会被覆盖，
    /// 如果要对该类进行修改, 请直接修改该分部类的Sys_BusiUnit文件(文件名不含.designer)
    /// </remarks>
    public partial class Sys_BusiUnitFacade
    {
       private Sys_BusiUnitDAO _sys_BusiUnitDAO = new Sys_BusiUnitDAO();
             
       /// <summary>
       /// 根据主键值查找业务单位信息对象
       /// </summary>
       /// <param name="keyId">业务单位内码</param> 
       /// <returns>Sys_BusiUnit</returns>
       public Sys_BusiUnit FindSys_BusiUnit(int keyId)
       {
           return this._sys_BusiUnitDAO.FindSys_BusiUnit(keyId); 
       } 
       
       /// <summary>
       /// 获取全部业务单位信息列表
       /// </summary>
       /// <returns>Sys_BusiUnit对象列表</returns>
       public IList<Sys_BusiUnit> GetSys_BusiUnits()
       {
            return this._sys_BusiUnitDAO.GetSys_BusiUnits();
       } 
       
        /// <summary>
        /// 返回满足查询条件的业务单位信息实体列表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>
        /// 业务单位信息实体列表</returns>
        public IList<Sys_BusiUnit> GetSys_BusiUnits(QueryParameter param)
        {
            return this._sys_BusiUnitDAO.GetSys_BusiUnits(param);
        }
        
        /// <summary>
        /// 返回满足查询条件的业务单位信息数据表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>
        /// 业务单位信息数据表</returns>
        public DataTable GetSys_BusiUnitTable(QueryParameter param)
        {
            return this._sys_BusiUnitDAO.GetSys_BusiUnitTable(param);
        }
       
       /// <summary>
       /// 创建业务单位信息记录
       /// </summary>
       /// <param name="sys_BusiUnit">
       /// 业务单位信息对象</param>
       /// <returns></returns>
       public int CreateSys_BusiUnit(Sys_BusiUnit sys_BusiUnit)
       {
           return this._sys_BusiUnitDAO.InsertSys_BusiUnit(sys_BusiUnit);
       }
       
        /// <summary>
        /// 更新业务单位信息记录
        /// </summary>
        /// <param name="sys_BusiUnit">
        /// 业务单位信息对象</param>
        /// <returns>受影响的记录数</returns>
        public int UpdateSys_BusiUnit(Sys_BusiUnit sys_BusiUnit)
        {
            return this._sys_BusiUnitDAO.UpdateSys_BusiUnit(sys_BusiUnit);
        } 

        /// <summary>
		/// 删除业务单位信息记录
        /// </summary>
        /// <param name="keyId">业务单位内码</param> 
        /// <returns>受影响的记录数</returns>
        public int DeleteSys_BusiUnit(int keyId)
        {
            return this._sys_BusiUnitDAO.DeleteSys_BusiUnit(keyId);
        } 
    }      
}  
