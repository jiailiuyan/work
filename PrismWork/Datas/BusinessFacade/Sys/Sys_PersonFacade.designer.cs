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
    /// 人员基本信息业务外观类
    /// 生成日期: 2014年10月 27日 23:20
    ///</summary>
    /// <remarks>
    /// 该文件是由代码生成器自动生成的, 请不要随意修改, 你的修改将在代码重新生成时会被覆盖，
    /// 如果要对该类进行修改, 请直接修改该分部类的Sys_Person文件(文件名不含.designer)
    /// </remarks>
    public partial class Sys_PersonFacade
    {
       private Sys_PersonDAO _sys_PersonDAO = new Sys_PersonDAO();
             
       /// <summary>
       /// 根据主键值查找人员基本信息对象
       /// </summary>
       /// <param name="keyId">员工内码</param> 
       /// <returns>Sys_Person</returns>
       public Sys_Person FindSys_Person(int keyId)
       {
           return this._sys_PersonDAO.FindSys_Person(keyId); 
       } 
       
       /// <summary>
       /// 获取全部人员基本信息列表
       /// </summary>
       /// <returns>Sys_Person对象列表</returns>
       public IList<Sys_Person> GetSys_Persons()
       {
            return this._sys_PersonDAO.GetSys_Persons();
       } 
       
        /// <summary>
        /// 返回满足查询条件的人员基本信息实体列表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>
        /// 人员基本信息实体列表</returns>
        public IList<Sys_Person> GetSys_Persons(QueryParameter param)
        {
            return this._sys_PersonDAO.GetSys_Persons(param);
        }
        
        /// <summary>
        /// 返回满足查询条件的人员基本信息数据表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <returns>
        /// 人员基本信息数据表</returns>
        public DataTable GetSys_PersonTable(QueryParameter param)
        {
            return this._sys_PersonDAO.GetSys_PersonTable(param);
        }
       
       /// <summary>
       /// 创建人员基本信息记录
       /// </summary>
       /// <param name="sys_Person">
       /// 人员基本信息对象</param>
       /// <returns></returns>
       public int CreateSys_Person(Sys_Person sys_Person)
       {
           return this._sys_PersonDAO.InsertSys_Person(sys_Person);
       }
       
        /// <summary>
        /// 更新人员基本信息记录
        /// </summary>
        /// <param name="sys_Person">
        /// 人员基本信息对象</param>
        /// <returns>受影响的记录数</returns>
        public int UpdateSys_Person(Sys_Person sys_Person)
        {
            return this._sys_PersonDAO.UpdateSys_Person(sys_Person);
        } 

        /// <summary>
		/// 删除人员基本信息记录
        /// </summary>
        /// <param name="keyId">员工内码</param> 
        /// <returns>受影响的记录数</returns>
        public int DeleteSys_Person(int keyId)
        {
            return this._sys_PersonDAO.DeleteSys_Person(keyId);
        } 
    }      
}  
