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
    /// 业务单位类型,该表不做显示维护业务外观类
    /// 生成日期: 2014年10月 27日 21:07
    ///</summary>
    /// <remarks>
    /// 该文件是由代码生成器自动生成的, 请不要随意修改, 你的修改将在代码重新生成时会被覆盖，
    /// 如果要对该类进行修改, 请直接修改该分部类的Sys_BusiUnitType文件(文件名不含.designer)
    /// </remarks>
    public partial class Sys_BusiUnitTypeFacade
    {
       private Sys_BusiUnitTypeDAO _sys_BusiUnitTypeDAO = new Sys_BusiUnitTypeDAO();
             
       /// <summary>
       /// 根据主键值查找业务单位类型,该表不做显示维护对象
       /// </summary>
       /// <param name="keyId">业务单位类型内码</param> 
       /// <returns>Sys_BusiUnitType</returns>
       public Sys_BusiUnitType FindSys_BusiUnitType(int keyId)
       {
           return this._sys_BusiUnitTypeDAO.FindSys_BusiUnitType(keyId); 
       } 
       
       /// <summary>
       /// 获取全部业务单位类型,该表不做显示维护列表
       /// </summary>
       /// <returns>Sys_BusiUnitType对象列表</returns>
       public IList<Sys_BusiUnitType> GetSys_BusiUnitTypes()
       {
            return this._sys_BusiUnitTypeDAO.GetSys_BusiUnitTypes();
       } 
       
        /// <summary>
        /// 返回满足查询条件的业务单位类型,该表不做显示维护实体列表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>
        /// 业务单位类型,该表不做显示维护实体列表</returns>
        public IList<Sys_BusiUnitType> GetSys_BusiUnitTypes(QueryParameter param)
        {
            return this._sys_BusiUnitTypeDAO.GetSys_BusiUnitTypes(param);
        }
        
        /// <summary>
        /// 返回满足查询条件的业务单位类型,该表不做显示维护数据表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>
        /// 业务单位类型,该表不做显示维护数据表</returns>
        public DataTable GetSys_BusiUnitTypeTable(QueryParameter param)
        {
            return this._sys_BusiUnitTypeDAO.GetSys_BusiUnitTypeTable(param);
        }
       
       /// <summary>
       /// 创建业务单位类型,该表不做显示维护记录
       /// </summary>
       /// <param name="sys_BusiUnitType">
       /// 业务单位类型,该表不做显示维护对象</param>
       /// <returns></returns>
       public int CreateSys_BusiUnitType(Sys_BusiUnitType sys_BusiUnitType)
       {
           return this._sys_BusiUnitTypeDAO.InsertSys_BusiUnitType(sys_BusiUnitType);
       }
       
        /// <summary>
        /// 更新业务单位类型,该表不做显示维护记录
        /// </summary>
        /// <param name="sys_BusiUnitType">
        /// 业务单位类型,该表不做显示维护对象</param>
        /// <returns>受影响的记录数</returns>
        public int UpdateSys_BusiUnitType(Sys_BusiUnitType sys_BusiUnitType)
        {
            return this._sys_BusiUnitTypeDAO.UpdateSys_BusiUnitType(sys_BusiUnitType);
        } 

        /// <summary>
		/// 删除业务单位类型,该表不做显示维护记录
        /// </summary>
        /// <param name="keyId">业务单位类型内码</param> 
        /// <returns>受影响的记录数</returns>
        public int DeleteSys_BusiUnitType(int keyId)
        {
            return this._sys_BusiUnitTypeDAO.DeleteSys_BusiUnitType(keyId);
        } 
    }      
}  
