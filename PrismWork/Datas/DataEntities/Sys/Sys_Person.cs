using System;
using System.Collections.Generic;
using System.Text;
using Project.Common;

namespace Project.Entities 
{    
    /// <summary>
    /// Sys_Person 人员基本信息数据实体类
    /// 开发人员: 
    /// 开发日期: 2014年10月
    /// </summary>
    public partial class Sys_Person : BusinessEntity
    {
        private IDictionary<int, int> _privilegeDictionary = new Dictionary<int, int>();
        private string _projFullName = string.Empty;
        private string _projCode = string.Empty;
        private string _homePath = string.Empty;
        private string _compName = string.Empty;
        private string _compCode = string.Empty;

        private List<Sys_PersonBusiUnit> _lstPersonBusiUnit = new List<Sys_PersonBusiUnit>();
        private bool _isDataCenterSys = false;

        private string _stationCode = string.Empty;
        private string _regionCode = string.Empty;

        /// <summary>
        /// 片区代码
        /// </summary>
        public string RegionCode
        {
            get { return _regionCode; }
            set { _regionCode = value; }
        }
        /// <summary>
        /// 人员所在主要单位代码（可能为空、加油站）
        /// </summary>
        public string StationCode
        {
            get { return _stationCode; }
            set { _stationCode = value; }
        }

        ///// <summary>
        ///// 人员所在的单位列表
        ///// </summary>
        public List<Sys_PersonBusiUnit> LstPersonBusiUnit
        {
            get { return _lstPersonBusiUnit; }
            set { _lstPersonBusiUnit = value; }
        }

        /// <summary>
        /// 是否是数据中心系统
        /// </summary>
        public bool IsDataCenterSystem
        {
            get { return _isDataCenterSys; }
            set { _isDataCenterSys = value; }
        }

        /// <summary>
        /// 主页路径
        /// </summary>
        public string HomePath
        {
            get { return _homePath; }
            set { _homePath = value; }
        }

        /// <summary>
        /// 项目代码
        /// </summary>
        public string ProjCode
        {
            get { return _projCode; }
            set { _projCode = value; }
        }
        /// <summary>
        /// 项目全称
        /// </summary>
        public string ProjFullName
        {
            get { return _projFullName; }
            set { _projFullName = value; }
        }
        /// <summary>
        /// 公司全称
        /// </summary>
        public string CompName
        {
            get { return _compName; }
            set { _compName = value; }
        }
        /// <summary>
        /// 公司代码
        /// </summary>
        public string CompCode
        {
            get { return _compCode; }
            set { _compCode = value; }
        }
        /// <summary>
        /// 获取一个值, 该值代表用户的模块权限值的数据字典
        /// </summary>
        /// <remarks>字典键代表模块内码, 字典键值代表该模块的权限值</remarks>
        public IDictionary<int, int> PrivilegeDictionary
        {
            get
            {
                return this._privilegeDictionary;
            }
        }


        /// <summary>
        /// 获取指定模块内码的模块的操作权限对象
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public Privilege GetPrivilege(int moduleId)
        {
            int privilege = 0;
            if (this._privilegeDictionary.ContainsKey(moduleId))
            {
                privilege = this._privilegeDictionary[moduleId];
            }

            return new Privilege(privilege);
        }
        /// <summary>
        /// 重写ToString()方法
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.PersonName;
        }

        public string BusiUnitName
        {
            get;
            set;
        }
    }
}  