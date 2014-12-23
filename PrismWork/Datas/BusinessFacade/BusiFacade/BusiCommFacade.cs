using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Project.DataAccess;

namespace Project.BusinessFacade
{
    public class BusiCommFacade
    {
        BusiCommDAO dao = new BusiCommDAO();
        /// <summary>
        /// 获取加油站简称（为加载下拉框）
        /// </summary>
        /// <returns></returns>
        public DataTable GetUsingOilStation()
        {
            return dao.GetUsingOilStation();
        }
        /// <summary>
        /// 获取加油站简称R2（为加载下拉框）
        /// </summary>
        /// <returns></returns>
        public DataTable GetUsingOilStation(string _stationcode, string _regioncode)
        {
            return dao.GetUsingOilStation(_stationcode, _regioncode);
        }

        /// <summary>
        /// 根据站组id获取加油站简称（为加载下拉框）
        /// </summary>
        /// <param name="_groupid"></param>
        /// <returns></returns>
        public DataTable GetUsingOilStation(string _groupid)
        {
            return dao.GetUsingOilStation(_groupid);
        }

        /// <summary>
        /// 根据二级公司获取加油站
        /// </summary>
        /// <param name="CITY_COMP"></param>
        /// <returns></returns>
        public DataTable GetStationDataByComp(string CITY_COMP)
        {
            return dao.GetStationDataByComp(CITY_COMP);
        }

        /// <summary>
        /// 根据加油站id获取站组id
        /// </summary>
        /// <param name="_stationid"></param>
        /// <returns></returns>
        public string GetGroupIdByStationId(string _stationid)
        {
            return dao.GetGroupIdByStationId(_stationid);
        }

        /// <summary>
        /// 根据片区code获取站组信息（为加载下拉框）
        /// </summary>
        /// <param name="_regioncode"></param>
        /// <returns></returns>
        public DataTable GetStationGroup(string _regioncode)
        {
            return dao.GetStationGroup(_regioncode);
        }

        /// <summary>
        /// 显示加油站相关GIS信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllStationForGis(string _regioncode, string _filter)
        {
            return dao.GetAllStationForGis(_regioncode, _filter);
        }
        /// <summary>
        /// 根据加油站代码获取对应罐号
        /// </summary>
        /// <param name="_stationcode"></param>
        /// <returns></returns>
        public DataTable GetUsingTankByStation(string _stationcode)
        {
            return dao.GetUsingTankByStation(_stationcode);
        }
        /// <summary>
        /// 根据加油站id和油罐id获取油罐名称
        /// </summary>
        /// <param name="_stationid"></param>
        /// <param name="tankid"></param>
        /// <returns></returns>
        public string GetTankNameByStationIdAndTankId(string _stationid, string _tankid)
        {
            return dao.GetTankNameByStationIdAndTankId(_stationid, _tankid);
        }
        /// <summary>
        /// 根据加油站代码获取该站的加油枪号
        /// </summary>
        /// <param name="_stationcode"></param>
        /// <returns></returns>
        public DataTable GetPumpByStation(string _stationcode)
        {
            return dao.GetPumpByStation(_stationcode);
        }
        /// <summary>
        /// 根据加油站和罐号获取枪号
        /// </summary>
        /// <param name="_stationcode"></param>
        /// <param name="_tankid"></param>
        /// <returns></returns>
        public DataTable GetPumpByStationAndTank(string _stationcode, string _tankid)
        {
            return dao.GetPumpByStationAndTank(_stationcode, _tankid);
        }
        /// <summary>
        /// 根据公司代码获取该公司的片区信息
        /// </summary>
        /// <param name="_compcode"></param>
        /// <returns></returns>
        public DataTable GetCompRegion(string _compcode)
        {
            return dao.GetCompRegion(_compcode);
        }
        /// <summary>
        /// 根据片区代码获取该片区加油站信息
        /// </summary>
        /// <param name="_regioncode"></param>
        /// <returns></returns>
        public DataTable GetRegionStation(string _regioncode)
        {
            return dao.GetRegionStation(_regioncode);
        }
        /// <summary>
        /// 获取交易方式
        /// </summary>
        /// <returns></returns>
        public DataTable GetExchangeType()
        {
            return dao.GetExchangeType();
        }

        /// <summary>
        /// 获取报表参数
        /// </summary>
        /// <param name="_reportcode"></param>
        /// <returns></returns>
        public DataTable GetRptParasSet(string _reportcode)
        {
            return dao.GetRptParasSet(_reportcode);
        }
        /// <summary>
        /// 获取油品类别数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetOilInfo()
        {
            return dao.GetOilInfo();
        }
        /// <summary>
        /// 获取某站油品数据
        /// </summary>
        /// <param name="_RegionCode"></param>
        /// <param name="_StationId"></param>
        /// <returns></returns>
        public DataTable GetOilInfo_Ex(string _RegionCode, string _StationId)
        {
            return dao.GetOilInfo_Ex(_RegionCode, _StationId);
        }
        /// <summary>
        /// 获取油品类别数据，text字段无code
        /// </summary>
        /// <returns></returns>
        public DataTable GetOilInfoWithOutCode()
        {
            return dao.GetOilInfoWithOutCode();
        }
        /// <summary>
        /// 获取油品Erp数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetOilErpInfo()
        {
            return dao.GetOilErpInfo();
        }
        /// <summary>
        /// 查询液位仪某日的读数点
        /// </summary>
        /// <param name="_stationid"></param>
        /// <param name="_date"></param>
        /// <returns></returns>
        //public DataTable GetTankReadingTime(string _stationid, string _date)
        //{
        //    return dao.GetTankReadingTime(_stationid, _date);
        //}
        /// <summary>
        /// 根据加油站和交易日期获得当天工作的枪号
        /// </summary>
        /// <param name="_station"></param>
        /// <param name="_date"></param>
        /// <returns></returns>
        public DataTable GetStationPumpInfo(string _station, string _date)
        {
            return dao.GetStationPumpInfo(_station, _date);
        }
        /// <summary>
        /// 根据登陆者信息获取可查看范围加油站的全部编码集合的sql语句
        /// </summary>
        /// <param name="_regionCode">片区代码</param>
        /// <param name="_stationCode">加油站代码</param>
        /// <returns></returns>
        public static string GetStationsFilterSql(string _regionCode, string _stationCode)
        {
            string FilterSql = string.Empty;
            if (string.IsNullOrEmpty(_regionCode) && string.IsNullOrEmpty(_stationCode))
            {//公司级用户，不做过滤
                return FilterSql;
            }
            if (false == string.IsNullOrEmpty(_stationCode))
            {//加油站用户

                FilterSql += " AND StationId LIKE '" + _stationCode + "'";
            }
            else if (false == string.IsNullOrEmpty(_regionCode))
            {//片区用户
                FilterSql = " AND StationId IN (SELECT Sys_Code FROM vw_Station_Info WHERE region LIKE '" + _regionCode + "')";
            }
            //else
            //{//公司用户不做过滤，可以查看全部加油站

            //}
            return FilterSql;
        }

        /// <summary>
        /// 查询加油站简称
        /// </summary>
        /// <param name="_stationid"></param>
        /// <returns></returns>
        public string GetStationShortName(string _stationid)
        {
            return dao.GetStationShortName(_stationid);
        }
        /// <summary>
        /// 查询加油站全称
        /// </summary>
        /// <param name="_stationid"></param>
        /// <returns></returns>
        public string GetStationName(string _stationid)
        {
            return dao.GetStationName(_stationid);
        }

        /// <summary>
        /// 查询加油站GisShowName
        /// </summary>
        /// <param name="_stationid"></param>
        /// <returns></returns>
        public string GetStationGisShowName(string _stationid)
        {
            return dao.GetStationGisShowName(_stationid);
        }
        /// <summary>
        /// 根据加油站内码获取该加油站的片区代码
        /// </summary>
        /// <param name="_stationid"></param>
        /// <returns></returns>
        public string GetRegionByStationId(string _stationid)
        {
            return dao.GetRegionByStationId(_stationid);
        }

        /// <summary>
        /// 根据加油站内码获取该加油站所属片区名称
        /// </summary>
        /// <param name="_stationid"></param>
        /// <returns></returns>
        public string GetRegionNameByStationId(string _stationid)
        {
            return dao.GetRegionNameByStationId(_stationid);
        }

        /// <summary>
        /// 查询片区名称
        /// </summary>
        /// <param name="_reigoncode"></param>
        /// <returns></returns>
        public string GetRegionName(string _reigoncode)
        {
            return dao.GetRegionName(_reigoncode);
        }

        /// <summary>
        /// 获取GIS容器的加油站数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllStationForGisContainer()
        {
            return dao.GetAllStationForGisContainer();
        }
        /// <summary>
        /// 获取首页的预警消息数据
        /// </summary>
        /// <param name="_region">片区代码</param>
        /// <param name="_stationid">加油站代码</param>
        /// <returns></returns>
        public DataTable GetHomePageAlertMessages(string _region, string _stationid, string _personid)
        {
            return dao.GetHomePageAlertMessages(_region, _stationid, _personid);
        }
        /// <summary>
        /// 获取首页的通知数据
        /// </summary>
        /// <param name="_region"></param>
        /// <param name="_stationid"></param>
        /// <returns></returns>
        public DataTable GetHomePageMyMessages(string _region, string _stationid, string _personid)
        {
            return dao.GetHomePageMyMessages(_region, _stationid, _personid);
        }

        /// <summary>
        /// 根据油品编码获取油品名称数据表
        /// </summary>
        /// <param name="_oilcode"></param>
        /// <returns></returns>
        public DataTable GetOilNameByCodeTable(string _oilcode)
        {
            return dao.GetOilNameByCodeTable(_oilcode);
        }

        /// <summary>
        /// 根据油品编码获取油品名称
        /// </summary>
        /// <param name="_oilcode"></param>
        /// <returns></returns>
        public string GetOilNameByCode(string _oilcode)
        {
            return dao.GetOilNameByCode(_oilcode);
        }

        /// <summary>
        /// 根据油品ERPCODE获取油品Code
        /// </summary>
        /// <param name="erpcode"></param>
        /// <returns></returns>
        public string GetOilCodeByOilErpCode(int erpcode)
        {
            return dao.GetOilCodeByOilErpCode(erpcode);
        }

        
        /// <summary>
        /// 根据单一油品code返回该油品时QY还是CY
        /// </summary>
        /// <param name="_oilcode"></param>
        /// <returns></returns>
        public string GetOilTypeByCode(string _oilcode)
        {
            return dao.GetOilTypeByCode(_oilcode);
        }

        /// <summary>
        /// 根据油罐id获取油品code
        /// </summary>
        /// <param name="_tankid"></param>
        /// <returns></returns>
        public string GetOilCodeByTankId(string _tankid, string _stationid)
        {
            return dao.GetOilCodeByTankId(_tankid, _stationid);
        }

        /// <summary>
        /// 获取在售油品信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetSalingOil()
        {
            return dao.GetSalingOil();
        }
        /// <summary>
        /// 获取油品号对应的油品类别
        /// </summary>
        /// <param name="_qCode"></param>
        /// <returns></returns>
        public string GetOilClassificationCode(string _qCode)
        {
            return dao.GetOilClassificationCode(_qCode);
        }

        /// <summary>
        /// 获取非油大类数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetNoOilSuperDept()
        {
            return dao.GetNoOilSuperDept();
        }

        /// <summary>
        /// 根据id获取非油大类名称
        /// </summary>
        /// <param name="superdeptid"></param>
        /// <returns></returns>
        public string GetNoOilSuperDeptName(string superdeptid)
        {
            return dao.GetNoOilSuperDeptName(superdeptid);
        }

        /// <summary>
        /// 根据商品条码获取商品名称
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        public string GetNoOilItemName(string barcode)
        {
            return dao.GetNoOilItemName(barcode);
        }

        /// <summary>
        /// 为树型结构提供运维故障类别数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetErr_ReasonTypeTreeData(string where)
        {
            return dao.GetErr_ReasonTypeTreeData(where);
        }

        /// <summary>
        /// 油品片区和加油站树
        /// </summary>
        /// <param name="_regioncode"></param>
        /// <param name="_stationid"></param>
        /// <returns></returns>
        public DataTable GetRegionAndStationTreeData(string _regioncode, string _stationid)
        {
            return dao.GetRegionAndStationTreeData(_regioncode, _stationid);
        }

        /// <summary>
        /// 非油片区和加油站树
        /// </summary>
        /// <param name="_regioncode"></param>
        /// <param name="_stationid"></param>
        /// <returns></returns>
        public DataTable GetNoOilRegionAndStationTreeData(string _regioncode, string _stationid)
        {
            return dao.GetNoOilRegionAndStationTreeData(_regioncode, _stationid);
        }

        /// <summary>
        /// 根据人员id获取人员名称
        /// </summary>
        /// <param name="_personid"></param>
        /// <returns></returns>
        public string GetPersonNameByPersonId(string _personid)
        {
            return dao.GetPersonNameByPersonId(_personid);
        }

        /// <summary>
        /// 获取预警对象数据表（可用于tree和combobox）
        /// </summary>
        /// <returns></returns>
        public DataTable GetTmp_BusiAlarmObjData()
        {
            return dao.GetTmp_BusiAlarmObjData();
        }

        /// <summary>
        /// 获取所属二级公司信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetSubCompInfo()
        {
            return dao.GetSubCompInfo();
        }

        /// <summary>
        /// 二级公司信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetCompInfo()
        {
            return dao.GetCompInfo();
        }


        /// <summary>
        /// 根据人员id获取人员单位信息
        /// </summary>
        /// <param name="personid"></param>
        /// <returns></returns>
        public DataTable GetBusiUnitInfoByPersonId(int personid)
        {
            return dao.GetBusiUnitInfoByPersonId(personid);
        }
        
        /// <summary>
        /// 根据单位id获取单位名称
        /// </summary>
        /// <param name="busiunitid"></param>
        /// <returns></returns>
        public string GetBusiUnitNameByBusiUnitId(int busiunitid)
        {
            return dao.GetBusiUnitNameByBusiUnitId(busiunitid);
        }

        /// <summary>
        /// 获取油库信息
        /// </summary>
        /// <param name="_companycode"></param>
        /// <returns></returns>
        public DataTable GetStoreInfo(string _companycode)
        {
            return dao.GetStoreInfo(_companycode);
        }
        /// <summary>
        /// 获取客户信息
        /// </summary>
        /// <param name="_regioncode"></param>
        /// <param name="_stationid"></param>
        /// <param name="_customercode"></param>
        /// <returns></returns>
        public DataTable GetWholeSaleCustomer(string _regioncode, string _stationid, string _customercode, string _customername, string where)
        {
            return dao.GetWholeSaleCustomer(_regioncode, _stationid, _customercode, _customername, where);
        }
        /// <summary>
        /// 根据CustomerCode获取CustomerName
        /// </summary>
        /// <param name="customercode"></param>
        /// <returns></returns>
        public string GetCustomerNameByCustomerCode(string customercode)
        {
            return dao.GetCustomerNameByCustomerCode(customercode);
        }
        /// <summary>
        /// 获取站员工列表
        /// </summary>
        /// <param name="_regioncode"></param>
        /// <param name="_stationid"></param>
        /// <returns></returns>
        public DataTable GetEmployeeList(string _regioncode, string _stationid, string _employeename)
        {
            return dao.GetEmployeeList(_regioncode, _stationid, _employeename);
        }

        /// <summary>
        /// 根据查询条件获取油库信息
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public DataTable GetOilStoreInfo(string where)
        {
            return dao.GetOilStoreInfo(where);
        }

        /// <summary>
        /// 获取加油站员工信息
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public DataTable GetStationEmployees(string where)
        {
            return dao.GetStationEmployees(where);
        }

        /// <summary>
        /// 根据加油站id获取UsingBos
        /// </summary>
        /// <param name="stationid"></param>
        /// <returns></returns>
        public string GetUsingBosByStationId(string stationid)
        {
            return dao.GetUsingBosByStationId(stationid);
        }

        /// <summary>
        /// 供应上hoscode和shortname
        /// </summary>
        /// <returns></returns>
        public DataTable GetSupplierHosAndShort()
        {
            return dao.GetSupplierHosAndShort();
        }

        /// <summary>
        /// 根据供应商hoscode获取shortname
        /// </summary>
        /// <param name="hoscode"></param>
        /// <returns></returns>
        public string GetSupplierShortNameByHosCode(string hoscode)
        {
            return dao.GetSupplierShortNameByHosCode(hoscode);
        }

        /// <summary>
        /// 获取商品信息（带条码信息）
        /// </summary>
        /// <param name="stationid"></param>
        /// <param name="superdeptid"></param>
        /// <returns></returns>
        public DataTable GetGoodsInfo(string stationid, string superdeptid, string itemname)
        {
            return dao.GetGoodsInfo(stationid, superdeptid, itemname);
        }

        /// <summary>
        /// 获取商品信息（不带条码信息）
        /// </summary>
        /// <param name="stationid"></param>
        /// <param name="superdeptid"></param>
        /// <returns></returns>
        public DataTable GetGoodsInfo_ex(string stationid, string superdeptid, string itemname)
        {
            return dao.GetGoodsInfo_ex(stationid, superdeptid, itemname);
        }

        /// <summary>
        /// 根据barcode获取商品名称
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        public string GetItemNameByBarCode(string barcode)
        {
            return dao.GetItemNameByBarCode(barcode);
        }

        /// <summary>
        /// 根据barcode和stationid获取商品条码
        /// </summary>
        /// <param name="barcode"></param>
        /// <param name="stationid"></param>
        /// <returns></returns>
        public string GetUpcByBorCodeAndStationId(string barcode, string stationid)
        {
            return dao.GetUpcByBorCodeAndStationId(barcode, stationid);
        }

        /// <summary>
        /// 供应商商品信息
        /// </summary>
        /// <param name="supplierid">供应商id</param>
        /// <param name="goodsname">商品名称（模糊）</param>
        /// <returns></returns>
        public DataTable GetSupplierGoodsInfo(string supplierhoscode, string goodsname)
        {
            return dao.GetSupplierGoodsInfo(supplierhoscode, goodsname);
        }

        /// <summary>
        /// 根据加油站获取主卡信息
        /// </summary>
        /// <param name="stationid"></param>
        /// <returns></returns>
        public DataTable GetCrd_MainInfo(string stationid)
        {
            return dao.GetCrd_MainInfo(stationid);
        }

        /// <summary>
        /// 根据主卡id获取子卡信息
        /// </summary>
        /// <param name="MainId"></param>
        /// <returns></returns>
        public DataTable GetCrd_DetailInfo(string where)
        {
            return dao.GetCrd_DetailInfo(where);
        }

        /// <summary>
        /// 获取全年数据表
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public DataTable GetYearDateList(string year)
        {
            return dao.GetYearDateList(year);
        }

        /// <summary>
        /// 获取用于Combobox的促销品一级分类数据表
        /// </summary>
        /// <returns></returns>
        public DataTable GetHelpSaleFirstTypeForCombobox()
        {
            return dao.GetHelpSaleFirstTypeForCombobox();
        }

        /// <summary>
        /// 获取用于Tree的促销品一级分类数据表
        /// </summary>
        /// <returns></returns>
        public DataTable GetHelpSaleFirstTypeForTree()
        {
            return dao.GetHelpSaleFirstTypeForTree();
        }

        /// <summary>
        /// 获取企业性质信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetCompanyPropertyData()
        {
            return dao.GetCompanyPropertyData();
        }

        /// <summary>
        /// 获取促销品供应商信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetHelpSaleSupplierInfo()
        {
            return dao.GetHelpSaleSupplierInfo();
        }

        /// <summary>
        /// 获取已通过审核的促销方案
        /// </summary>
        /// <returns></returns>
        public DataTable GetHelpSaleScheme()
        {
            return dao.GetHelpSaleScheme();
        }

        /// <summary>
        /// 构建修改痕迹用人员和时间字符串
        /// </summary>
        /// <param name="region"></param>
        /// <param name="station"></param>
        /// <param name="userid"></param>
        /// <returns>XXX单位XXX【人员编号】在XXX时候</returns>
        public string ConstructModifierInfo(string region, string station, string userid)
        {
            string result = string.Empty;
            if (!string.IsNullOrWhiteSpace(region))
            {
                result += GetRegionName(region);
            }
            if (!string.IsNullOrWhiteSpace(station))
            {
                result += GetStationGisShowName(station) + "加油站";
            }
            if (!string.IsNullOrWhiteSpace(userid))
            {
                result += GetPersonNameByPersonId(userid) + "【" + userid + "】";
            }
            result += "在" + DateTime.Now.ToString("yyyy年M月d日H时m分s秒");
            return result;
        }

        /// <summary>
        /// 获取JT非油商品信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetJTNoOilGoodsList()
        {
            return dao.GetJTNoOilGoodsList();
        }
        /// <summary>
        /// 判断某站某天是否已经日结
        /// </summary>
        /// <param name="station"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public bool CheckStationIsDayBatched(string station, DateTime date)
        {
            return this.dao.IsDayBatched(station, date);
        }
    }
}
