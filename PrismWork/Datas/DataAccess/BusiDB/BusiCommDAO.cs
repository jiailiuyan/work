using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Project.DataAccess
{
    public class BusiCommDAO : BusiBaseDAO
    {
        /// <summary>
        /// 获取加油站简称（为加载下拉框）
        /// </summary>
        /// <returns></returns>
        public DataTable GetUsingOilStation()
        {
            string sql = "select SYS_CODE,ERP_CODE,'【'+SYS_CODE+'】 '+dbo.f_GetRegionNameByCode(REGION,DEFAULT)+GisShowName as STATION_SHORT_NAME,gisshowname from vw_station_info vsi JOIN dbo.v_CompRegion vcr ON vsi.REGION=vcr.RegionCode where use_flag in(4,1) ORDER BY vsi.Region,SYS_CODE";
            return this.GetDataTable(sql);
        }
        /// <summary>
        /// 获取加油站简称（为加载下拉框）
        /// </summary>
        /// <returns></returns>
        public DataTable GetUsingOilStation(string _stationcode, string _regioncode)
        {
            string sql = "select SYS_CODE,ERP_CODE,'【'+SYS_CODE+'】 '+GisShowName as STATION_SHORT_NAME,gisshowname from vw_station_info where 1=1 and use_flag in(4,1) ";
            if (_stationcode.Length > 0)
                sql += " and sys_code in ('" + _stationcode + "')";
            if (_regioncode.Length > 0)
                sql += " and region='" + _regioncode + "'";
            sql += " ORDER BY SYS_CODE";
            return this.GetDataTable(sql);
        }

        /// <summary>
        /// 根据站组id获取加油站简称（为加载下拉框）
        /// </summary>
        /// <param name="_groupid"></param>
        /// <returns></returns>
        public DataTable GetUsingOilStation(string _groupid)
        {
            string sql = @"select SYS_CODE,ERP_CODE,'【'+SYS_CODE+'】 '+dbo.f_GetRegionNameByCode(REGION,DEFAULT)+GisShowName as STATION_SHORT_NAME,gisshowname 
                            from vw_station_info a
                            JOIN dbo.v_CompRegion vcr ON a.REGION=vcr.RegionCode
                            JOIN dbo.OWN_StationGroupSet b ON b.StationId=a.StationId
                            where 1=1 and use_flag in(4,1) ";
            if (!string.IsNullOrWhiteSpace(_groupid))
            {
                sql += " AND b.GroupId=" + _groupid;
            }
            sql += " ORDER BY SYS_CODE";
            return this.GetDataTable(sql);
        }

        /// <summary>
        /// 根据二级公司获取加油站
        /// </summary>
        /// <param name="CITY_COMP"></param>
        /// <returns></returns>
        public DataTable GetStationDataByComp(string CITY_COMP)
        {
            string sql = @"SELECT vsi.StationId ,'【'+vsi.StationId+'】'+vsi.GisShowName AS GisShowName
                             FROM dbo.vw_Station_Info vsi
                             WHERE 1=1 ";
            if (!string.IsNullOrWhiteSpace(CITY_COMP))
            {
                sql += " AND CITY_COMP='" + CITY_COMP + "'";
            }
            return this.GetDataTable(sql);
        }

        /// <summary>
        /// 根据加油站id获取站组id
        /// </summary>
        /// <param name="_stationid"></param>
        /// <returns></returns>
        public string GetGroupIdByStationId(string _stationid)
        {
            string sql = "SELECT * FROM dbo.OWN_StationGroupSet WHERE 1=1 ";
            if (!string.IsNullOrWhiteSpace(_stationid))
            {
                sql += " AND StationId='" + _stationid + "'";
                DataTable dt = GetDataTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["GroupId"].ToString();
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 根据片区code获取站组信息（为加载下拉框）
        /// </summary>
        /// <param name="_regioncode"></param>
        /// <returns></returns>
        public DataTable GetStationGroup(string _regioncode)
        {
            string sql = @"SELECT DISTINCT GroupName,GroupId FROM dbo.OWN_StationGroupSet a
                            JOIN dbo.vw_Station_Info b ON a.StationId=b.StationId
                            JOIN dbo.v_CompRegion c ON b.REGION=c.RegionCode
                                WHERE 1=1";
            if (!string.IsNullOrWhiteSpace(_regioncode))
            {
                sql += " AND c.RegionCode='" + _regioncode + "' ";
            }
            sql += " ORDER BY GroupId";
            return GetDataTable(sql);
        }

        /// <summary>
        /// 显示中石油公司内部加油站相关GIS信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllStationForGis(string _regioncode, string _filter)
        {
            string sql = "exec p_GetStationDataForGis '','1'";
            return this.GetDataTable(sql);
        }
        /// <summary>
        /// 根据加油站代码获取对应罐号
        /// </summary>
        /// <param name="_stationcode"></param>
        /// <returns></returns>
        public DataTable GetUsingTankByStation(string _stationcode)
        {
            string sql = "SELECT Tank_ID,TanK_Name+'('+ dbo.F_GetOilName(Grade_PLU)+')' AS Tank_Name FROM dbo.vw_TankInfo ft WHERE 1=1 AND ft.STATIONID='" + _stationcode + "'";
            //if (!string.IsNullOrWhiteSpace(_stationcode))
            //    sql += " AND ft.STATIONID='" + _stationcode + "'";
            return this.GetDataTable(sql);
        }
        /// <summary>
        /// 根据加油站id和油罐id获取油罐名称
        /// </summary>
        /// <param name="_stationid"></param>
        /// <param name="tankid"></param>
        /// <returns></returns>
        public string GetTankNameByStationIdAndTankId(string _stationid, string tankid)
        {
            string sql = "SELECT TanK_Name FROM dbo.vw_TankInfo ft WHERE ft.STATIONID='" + _stationid + "' AND ft.Tank_ID=" + tankid;
            return GetDataString(sql);
        }
        /// <summary>
        /// 根据加油站代码获取该站的加油枪号
        /// </summary>
        /// <param name="_stationcode"></param>
        /// <returns></returns>
        public DataTable GetPumpByStation(string _stationcode)
        {
            string sql = "SELECT DISTINCT Pump_Id,CONVERT(VARCHAR(2),Pump_Id)+'号枪' AS PumpName FROM dbo.OWN_StationTankAndPump WHERE StationId='" + _stationcode + "' ORDER BY PUMP_ID";
            return this.GetDataTable(sql);
        }
        /// <summary>
        /// 根据加油站和罐号获取枪号
        /// </summary>
        /// <param name="_stationcode"></param>
        /// <param name="_tankid"></param>
        /// <returns></returns>
        public DataTable GetPumpByStationAndTank(string _stationcode, string _tankid)
        {
            string where = string.Empty;
            if (!string.IsNullOrWhiteSpace(_stationcode))
            {
                where += "AND StationId='" + _stationcode + "' ";
            }
            if (!string.IsNullOrWhiteSpace(_tankid))
            {
                where += "AND Tank_Id='" + _tankid + "' ";
            }
            string sql = "SELECT DISTINCT Pump_Id,CONVERT(VARCHAR(2),Pump_Id)+'号枪' AS PumpName FROM dbo.OWN_StationTankAndPump WHERE 1=1 ";
            if (!string.IsNullOrWhiteSpace(where))
            {
                sql += where;
            }
            sql += " ORDER BY PUMP_ID";
            return this.GetDataTable(sql);
        }
        /// <summary>
        /// 根据公司代码获取该公司的片区信息
        /// </summary>
        /// <param name="_compcode"></param>
        /// <returns></returns>
        public DataTable GetCompRegion(string _compcode)
        {
            string sql = "select RegionCode,RegionName from v_CompRegion where CompCode='" + _compcode + "'";
            return this.GetDataTable(sql);
        }
        /// <summary>
        /// 根据片区代码获取该片区加油站信息
        /// </summary>
        /// <param name="_regioncode"></param>
        /// <returns></returns>
        public DataTable GetRegionStation(string _regioncode)
        {
            string sql = "SELECT * FROM dbo.OWN_Station_Info WHERE REGION='" + _regioncode + "'";
            return this.GetDataTable(sql);
        }
        /// <summary>
        /// 根据加油站内码获取该加油站的片区代码
        /// </summary>
        /// <param name="_stationid"></param>
        /// <returns></returns>
        public string GetRegionByStationId(string _stationid)
        {
            string sql = "select region from vw_station_info where sys_code='" + _stationid + "'";
            return GetDataString(sql) ;
        }
        /// <summary>
        /// 根据加油站内码获取该加油站所属片区名称
        /// </summary>
        /// <param name="_stationid"></param>
        /// <returns></returns>
        public string GetRegionNameByStationId(string _stationid)
        {
            string sql = @"select RegionName from v_CompRegion where RegionCode = (
                           select region from vw_station_info where sys_code='" + _stationid + "')";
            return this.GetDataTable(sql).Rows[0][0].ToString();
        }

        /// <summary>
        /// 获取交易方式
        /// </summary>
        /// <returns></returns>
        public DataTable GetExchangeType()
        {
            string sql = "select distinct PMNT_id,PMNT_NAME from " + LinkServerADD + "dbo.vw_pmnt ";
            return this.GetDataTable(sql);
        }

        /// <summary>
        /// 获取报表参数
        /// </summary>
        /// <param name="_reportcode"></param>
        /// <returns></returns>
        public DataTable GetRptParasSet(string _reportcode)
        {
            string sql = "select * from OWN_ReportParasSet where ReportCode='" + _reportcode + "'";
            return this.GetDataTable(sql);
        }
        /// <summary>
        /// 获取油品类别数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetOilInfo()
        {
            string sql = "select cast(OilCode as varchar(10)) as OilCode,'【'+CONVERT(VARCHAR(10),OilCode)+'】'+OilName as OilName from vw_OilInfo WHERE IsSaling=1 ";
            return this.GetDataTable(sql);
        }
        /// <summary>
        /// 获取某站油品数据
        /// </summary>
        /// <param name="_RegionCode"></param>
        /// <param name="_StationId"></param>
        /// <returns></returns>
        public DataTable GetOilInfo_Ex(string _RegionCode, string _StationId)
        {
            string sql = @"SELECT DISTINCT cast(oi.OilCode as varchar(10)) as OilCode,'【'+CONVERT(VARCHAR(10),oi.OilCode)+'】'+oi.OilName as OilName,oi.OilType
                    FROM dbo.vw_OilInfo oi
                    INNER HASH JOIN dbo.vw_DailySum v ON oi.OilCode=v.GRADE
                    JOIN dbo.vw_Station_Info vsi ON v.STATIONID=vsi.StationId
                    WHERE YEAR(v.DAY_BATCH_DATE)=YEAR(GETDATE())";
            if(!string.IsNullOrWhiteSpace(_RegionCode))
            {
                sql+=" And vsi.Region='"+_RegionCode+"'";
            }
            if (!string.IsNullOrWhiteSpace(_StationId))
            {
                 sql+=" And v.STATIONID='"+_StationId+"'";
            }
            sql+= @" AND ISNULL(v.Weight,0)>0
                    AND oi.IsSaling=1
            UNION
            SELECT CAST(a.Grade AS VARCHAR(10)),'【'+CONVERT(VARCHAR(10),oi.OilCode)+'】'+oi.OilName as OilName,OI.OilType
            FROM dbo.OWN_TankInfo a
            JOIN dbo.vw_Station_Info vsi on a.StationId=vsi.StationId
            JOIN dbo.vw_OilInfo oi ON a.Grade=oi.OilCode";
            if(!string.IsNullOrWhiteSpace(_RegionCode))
            {
                sql+=" And vsi.Region='"+_RegionCode+"'";
            }
            if (!string.IsNullOrWhiteSpace(_StationId))
            {
                 sql+=" And vsi.STATIONID='"+_StationId+"'";
            }
            sql+=" ORDER BY oi.OilType";
            return this.GetDataTable(sql);
        }
            
        /// <summary>
        /// 获取油品类别数据，text字段无code
        /// </summary>
        /// <returns></returns>
        public DataTable GetOilInfoWithOutCode()
        {
            string sql = "select cast(OilCode as varchar(10)) as OilCode,OilName from vw_OilInfo WHERE IsSaling=1 ";
            return this.GetDataTable(sql);
        }

        /// <summary>
        /// 获取油品Erp数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetOilErpInfo()
        {
            string sql = "select ERP_OilCode AS ErpCode,'【'+ERP_OilCode+'】'+OilName as OilName from vw_OilInfo WHERE IsSaling=1 ";
            return GetDataTable(sql);
        }
        /// <summary>
        /// 查询液位仪某日的读数点
        /// </summary>
        /// <param name="_stationid"></param>
        /// <param name="_date"></param>
        /// <returns></returns>
        //public DataTable GetTankReadingTime(string _stationid, string _date)
        //{
        //    string sql = "select distinct substring(convert(varchar(13),start_reading,120),12,2) as reading_yy";
        //    sql += " from OilStationDataTrans.dbo.FUEL_TANK_READING where StationID='" + _stationid + "' and START_READING between '" + _date + " 00:00' and '" + _date + " 23:59'";
        //    sql+=" order by reading_yy";
        //    return this.GetDataTable(sql);
        //}
        /// <summary>
        /// 根据加油站和交易日期获得当天工作的枪号
        /// </summary>
        /// <param name="_station"></param>
        /// <param name="_date"></param>
        /// <returns></returns>
        public DataTable GetStationPumpInfo(string _station, string _date)
        {
            string sql = "select distinct PUMP_ID from OWN_EveryDaySalesDetail where STATIONID='" + _station + "' and DAY_BATCH_DATE='" + _date + "'";
            return this.GetDataTable(sql);
        }

        /// <summary>
        /// 查询加油站简称
        /// </summary>
        /// <param name="_stationid"></param>
        /// <returns></returns>
        public string GetStationShortName(string _stationid)
        {
            string sql = "select STATION_SHORT_NAME from vw_station_info where SYS_CODE ='" + _stationid + "'";
            return this.GetDataString(sql);
        }

        /// <summary>
        /// 查询加油站全称
        /// </summary>
        /// <param name="_stationid"></param>
        /// <returns></returns>
        public string GetStationName(string _stationid)
        {
            string sql = "select STATION_NAME from vw_station_info where SYS_CODE ='" + _stationid + "'";
            return this.GetDataString(sql);
        }

        /// <summary>
        /// 查询加油站GisShowName
        /// </summary>
        /// <param name="_stationid"></param>
        /// <returns></returns>
        public string GetStationGisShowName(string _stationid)
        {
            if (string.IsNullOrWhiteSpace(_stationid))
            {
                return "";
            }
            else
            {
                string sql = "select GisShowName from vw_station_info where SYS_CODE ='" + _stationid + "'";
                return this.GetDataString(sql);
            }
        }

        /// <summary>
        /// 查询片区名称
        /// </summary>
        /// <param name="_reigoncode"></param>
        /// <returns></returns>
        public string GetRegionName(string _reigoncode)
        {
            string sql = "select RegionName from v_CompRegion where 1=1 ";
            if (!string.IsNullOrWhiteSpace(_reigoncode))
            {
                sql += " AND RegionCode ='" + _reigoncode + "'";
                return this.GetDataString(sql);
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 获取GIS容器的加油站数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllStationForGisContainer()
        {
            return this.GetDataTable("SELECT SYS_CODE,RegionName, GisShowName, X, Y FROM v_StationInfoForGis WHERE USE_FLAG = 1");
        }
        /// <summary>
        /// 获取首页的预警消息数据
        /// </summary>
        /// <param name="_region"></param>
        /// <param name="_stationid"></param>
        /// <returns></returns>
        public DataTable GetHomePageAlertMessages(string _region, string _stationid, string _personid)
        {
            string sql = "EXEC p_GetHomePageAlertMessages '" + _region + "','" + _stationid + "','" + _personid + "'";
            return this.GetDataTable(sql);
        }
        /// <summary>
        /// 获取首页的通知数据；前两个参数暂时未使用
        /// </summary>
        /// <param name="_region"></param>
        /// <param name="_stationid"></param>
        /// <returns></returns>
        public DataTable GetHomePageMyMessages(string _region, string _stationid, string _personid)
        {
            string sql = "EXEC P_GetHomePageOwnToDo '" + _personid + "'";
            return this.GetDataTable(sql);
        }

        /// <summary>
        /// 根据油品编码获取油品名称数据表
        /// </summary>
        /// <param name="_oilcode"></param>
        /// <returns></returns>
        public DataTable GetOilNameByCodeTable(string _oilcode)
        {
            string sql = @"SELECT * FROM dbo.OWN_OilInfo ";
            if (!string.IsNullOrWhiteSpace(_oilcode))
            {
                sql += " WHERE OilCode IN (" + _oilcode + ")";
            }
            else
            {
                sql += " WHERE OilCode='' ";
            }
            return this.GetDataTable(sql);
        }

        /// <summary>
        /// 根据油品编码获取油品名称
        /// </summary>
        /// <param name="_oilcode"></param>
        /// <returns></returns>
        public string GetOilNameByCode(string _oilcode)
        {
            string sql = @"SELECT OilName FROM dbo.vw_OilInfo WHERE 1=1 ";
            if (!string.IsNullOrWhiteSpace(_oilcode))
            {
                sql += " AND OilCode=" + _oilcode;
                return GetDataString(sql);
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 根据油品ERPCODE获取油品Code
        /// </summary>
        /// <param name="erpcode"></param>
        /// <returns></returns>
        public string GetOilCodeByOilErpCode(int erpcode)
        {
            string sql = "SELECT dbo.f_GetOilIdByErpCode(" + erpcode.ToString() + ")";
            return GetDataString(sql);
        }

        /// <summary>
        /// 根据单一油品code返回该油品时QY还是CY
        /// </summary>
        /// <param name="_oilcode"></param>
        /// <returns></returns>
        public string GetOilTypeByCode(string _oilcode)
        {
            if (!string.IsNullOrWhiteSpace(_oilcode))
            {
                string sql = @"SELECT dbo.f_OilClassification(" + _oilcode + ")";
                DataTable dt = GetDataTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt.Rows[0][0].ToString();
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }
        
        /// <summary>
        /// 根据油罐id获取油品code
        /// </summary>
        /// <param name="_tankid"></param>
        /// <returns></returns>
        public string GetOilCodeByTankId(string _tankid,string _stationid)
        {
            if (!string.IsNullOrWhiteSpace(_tankid)&&!string.IsNullOrWhiteSpace(_stationid))
            {
                string sql = @"SELECT Grade_PLU FROM dbo.vw_TankInfo WHERE STATIONID='" + _stationid + "' AND Tank_ID=" + _tankid;
                DataTable dt = GetDataTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["Grade_PLU"].ToString();
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 获取在售油品信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetSalingOil()
        {
            string sql = "SELECT * FROM dbo.OWN_OilInfo WHERE IsSaling=1 ";
            return GetDataTable(sql);
        }
        /// <summary>
        /// 获取油品号对应的油品类别
        /// </summary>
        /// <param name="_qCode"></param>
        /// <returns></returns>
        public string GetOilClassificationCode(string _qCode)
        {
            string strReturn = string.Empty;
            strReturn = _qCode;
            if (this.IsNumber(_qCode))
            {
                string sql = "SELECT dbo.f_OilClassification(" + _qCode + ")";
                strReturn=GetDataTable(sql).Rows[0][0].ToString();
            }
            return strReturn;
        }

        /// <summary>
        /// 获取非油大类数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetNoOilSuperDept()
        {
            string sql = "SELECT SUPERDEPTID,REPLACE(SUPERDEPTNAME,' ','') AS SUPERDEPTNAME FROM DGLINK.DG.dbo.vw_SUPERDEPT WHERE SUPERDEPTID BETWEEN 2000 AND 3000";
            return GetDataTable(sql);
        }

        /// <summary>
        /// 根据id获取非油大类名称
        /// </summary>
        /// <param name="superdeptid"></param>
        /// <returns></returns>
        public string GetNoOilSuperDeptName(string superdeptid)
        {
            if (!string.IsNullOrWhiteSpace(superdeptid))
            {
                string sql = "SELECT SUPERDEPTNAME FROM DGLINK.DG.dbo.vw_SUPERDEPT WHERE SUPERDEPTID BETWEEN 2000 AND 3000 AND SUPERDEPTID='" + superdeptid + "'";
                return GetDataString(sql);
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 根据商品条码获取商品名称
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        public string GetNoOilItemName(string barcode)
        {
            if (!string.IsNullOrWhiteSpace(barcode))
            {
                string sql = "SELECT DISTINCT ItemName FROM dbo.vw_NoOilGoodsBaseInfo WHERE BARCODE='" + barcode + "'";
                return GetDataString(sql);
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 为树型结构提供运维故障类别数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetErr_ReasonTypeTreeData(string where)
        {
            string sql = "SELECT KeyId,ERRName,ParentId FROM dbo.ERR_ReasonType WHERE Status=1 ";
            if (!string.IsNullOrWhiteSpace(where))
            {
                sql += where;
            }
            return GetDataTable(sql);
        }

        /// <summary>
        /// 油品片区和加油站树
        /// </summary>
        /// <param name="_regioncode"></param>
        /// <param name="_stationid"></param>
        /// <returns></returns>
        public DataTable GetRegionAndStationTreeData(string _regioncode, string _stationid)
        {
            string where = " WHERE 1=1 ";
            if (!string.IsNullOrWhiteSpace(_regioncode))
            {
                where += " AND vsi.REGION='" + _regioncode + "'";
            }
            if (!string.IsNullOrWhiteSpace(_stationid))
            {
                where += " AND vsi.StationId='" + _stationid + "'";
            }
            string sql = @"SELECT vsi.StationId AS KeyId,vsi.GisShowName AS Name,vcr.RegionCode AS ParentId
                            FROM dbo.vw_Station_Info vsi
                            JOIN dbo.v_CompRegion vcr ON vsi.REGION=vcr.RegionCode "
                            + where + " UNION " +
                            @"SELECT DISTINCT vcr.RegionCode AS KeyId,vcr.RegionName AS Name,NULL AS ParentId
                            FROM dbo.vw_Station_Info vsi
                            JOIN dbo.v_CompRegion vcr ON vsi.REGION=vcr.RegionCode " + where;
            return GetDataTable(sql);
        }

        /// <summary>
        /// 非油片区和加油站树
        /// </summary>
        /// <param name="_regioncode"></param>
        /// <param name="_stationid"></param>
        /// <returns></returns>
        public DataTable GetNoOilRegionAndStationTreeData(string _regioncode, string _stationid)
        {
            string where = " WHERE 1=1 ";
            if (!string.IsNullOrWhiteSpace(_regioncode))
            {
                where += " AND vsi.REGION='" + _regioncode + "'";
            }
            if (!string.IsNullOrWhiteSpace(_stationid))
            {
                where += " AND vsi.StationId='" + _stationid + "'";
            }
            string sql = @"SELECT vsi.StationId AS KeyId,vsi.GisShowName AS Name,vcr.RegionCode AS ParentId
                            FROM dbo.vw_Station_Info vsi
                            JOIN dbo.v_NoOilCompRegion vcr ON vsi.REGION=vcr.RegionCode "
                            + where + " UNION " +
                            @"SELECT DISTINCT vcr.RegionCode AS KeyId,vcr.RegionName AS Name,NULL AS ParentId
                            FROM dbo.vw_Station_Info vsi
                            JOIN dbo.v_NoOilCompRegion vcr ON vsi.REGION=vcr.RegionCode " + where;
            return GetDataTable(sql);
        }

        /// <summary>
        /// 根据人员id获取人员名称
        /// </summary>
        /// <param name="_personid"></param>
        /// <returns></returns>
        public string GetPersonNameByPersonId(string _personid)
        {
            if (!string.IsNullOrWhiteSpace(_personid))
            {
                string sql = "SELECT PersonName FROM DC.dbo.DepotPerson WHERE PersonID=" + _personid;
                return GetDataString(sql);
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 获取预警对象数据表（可用于tree和combobox）
        /// </summary>
        /// <returns></returns>
        public DataTable GetTmp_BusiAlarmObjData()
        {
            string sql = "SELECT BusiAlarmObjCode AS Id,BusiAlarmObjName AS Name,NULL AS PId FROM dbo.Tmp_BusiAlarmObj WHERE Use_Flag=1";
            return GetDataTable(sql);
        }

        /// <summary>
        /// 获取所属二级公司信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetSubCompInfo()
        {
            string sql = "SELECT * FROM dbo.vg_SubComp ORDER BY OrderId";
            return GetDataTable(sql);
        }

        /// <summary>
        /// 二级公司信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetCompInfo()
        {
            string sql = @"SELECT DISTINCT a.CITY_COMP,b.CompName,b.OrderId
             FROM dbo.vw_Station_Info a
             JOIN dbo.vg_SubComp b ON a.CITY_COMP=b.CompCode
             ORDER BY b.OrderId";
            return GetDataTable(sql);
        }

        /// <summary>
        /// 根据人员id获取
        /// </summary>
        /// <param name="personid"></param>
        /// <returns></returns>
        public DataTable GetBusiUnitInfoByPersonId(int personid)
        {
            string sql = @"SELECT a.BusiUnitId,a.BusiUnitName FROM DC.dbo.BusiUnit a 
                    LEFT JOIN DC.dbo.PersonBusiUnit b ON b.BusiUnitID=a.BusiUnitId
                    WHERE b.IsMaster=1 AND b.PersonID=" + personid.ToString().Trim();
            return GetDataTable(sql);
        }

        /// <summary>
        /// 根据单位id获取单位名称
        /// </summary>
        /// <param name="busiunitid"></param>
        /// <returns></returns>
        public string GetBusiUnitNameByBusiUnitId(int busiunitid)
        {
            string sql = "SELECT BusiUnitName FROM DC.dbo.BusiUnit WHERE BusiUnitId=" + busiunitid.ToString().Trim();
            return GetDataString(sql);
        }
        /// <summary>
        /// 获取油库信息
        /// </summary>
        /// <param name="_companycode"></param>
        /// <returns></returns>
        public DataTable GetStoreInfo(string _companycode)
        {
            string sql = "SELECT ErpName,storename,owncomp FROM OWN_OilStoreInfo";
            if (!string.IsNullOrWhiteSpace(_companycode))
            {
                sql += " where OwnComp='"+_companycode+"'";
            }
            return GetDataTable(sql);
        }
        /// <summary>
        /// 获取客户信息
        /// </summary>
        /// <param name="_regioncode"></param>
        /// <param name="_stationid"></param>
        /// <param name="_customercode"></param>
        /// <returns></returns>
        public DataTable GetWholeSaleCustomer(string _regioncode, string _stationid, string _customercode,string _customername,string where)
        {
            string sql = @"SELECT CustomerCode,CustomerName 
                        FROM dbo.OWN_WholeSaleCustomer a
                        LEFT JOIN dbo.vw_Station_Info vsi ON a.StationId=vsi.StationId AND VSI.USE_FLAG=1
                        LEFT JOIN dbo.v_CompRegion vcr ON vsi.REGION=vcr.RegionCode
                        WHERE 1=1
            ";
            if (!string.IsNullOrWhiteSpace(_regioncode))
                sql += " AND vsi.Region='"+_regioncode+"' ";
            if (!string.IsNullOrWhiteSpace(_stationid))
                sql += " AND vsi.StationId='" + _stationid + "' ";
            if (!string.IsNullOrWhiteSpace(_customercode))
                sql += " AND a.CustomerCode='"+_customercode+"' ";
            if (!string.IsNullOrWhiteSpace(_customername))
                sql += " AND a.CustomerName like '%"+_customername+"%' ";
            if (!string.IsNullOrWhiteSpace(where))
            {
                sql += where;
            }
            return GetDataTable(sql);
        }

        /// <summary>
        /// 根据CustomerCode获取CustomerName
        /// </summary>
        /// <param name="customercode"></param>
        /// <returns></returns>
        public string GetCustomerNameByCustomerCode(string customercode)
        {
            string customername = string.Empty;
            if (!string.IsNullOrWhiteSpace(customercode))
            {
                string sql = "SELECT CustomerCode,CustomerName FROM dbo.OWN_WholeSaleCustomer WHERE CustomerCode='" + customercode + "'";
                customername = GetDataString(sql);
            }
            return customername;
        }
        /// <summary>
        /// 获取站员工列表
        /// </summary>
        /// <param name="_regioncode"></param>
        /// <param name="_stationid"></param>
        /// <returns></returns>
        public DataTable GetEmployeeList(string _regioncode,string _stationid,string _employeename)
        {
            string sql = @"SELECT EmployeeId,EmpName,vsi.StationId
                FROM dbo.OWN_StationEmployees a
                JOIN dbo.vw_Station_Info vsi ON a.StationId=vsi.StationId
                JOIN dbo.v_CompRegion vcr ON vsi.REGION=vcr.RegionCode
                WHERE 1=1
                ";
            if(!string.IsNullOrWhiteSpace(_regioncode))
                sql+=" AND vsi.Region='"+_regioncode+"'";
            if(!string.IsNullOrWhiteSpace(_stationid))
                sql+=" AND vsi.StationId='"+_stationid+"'";
            if (!string.IsNullOrWhiteSpace(_employeename))
                sql += " AND a.EmpName like '%"+_employeename+"%'";
            return GetDataTable(sql);
        }

        /// <summary>
        /// 根据查询条件获取油库信息
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public DataTable GetOilStoreInfo(string where)
        {
            string sql = "SELECT * FROM dbo.OWN_OilStoreInfo WHERE 1=1 ";
            if (!string.IsNullOrWhiteSpace(where))
            {
                sql += where;
            }
            return GetDataTable(sql);
        }

        /// <summary>
        /// 获取加油站员工信息
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public DataTable GetStationEmployees(string where)
        {
            string sql = "SELECT EmployeeId,StationId,EmpNo,LTRIM(RTRIM(EmpName)) AS EmpName FROM OWN_StationEmployees WHERE 1=1 ";
            if (!string.IsNullOrWhiteSpace(where))
            {
                sql += where;
            }
            return GetDataTable(sql);
        }

        /// <summary>
        /// 根据加油站id获取UsingBos
        /// </summary>
        /// <param name="stationid"></param>
        /// <returns></returns>
        public string GetUsingBosByStationId(string stationid)
        {
            string result = string.Empty;
            if (!string.IsNullOrWhiteSpace(stationid))
            {
                string sql = "SELECT UsingBos FROM dbo.vw_Station_Info WHERE StationId='" + stationid + "'";
                result = GetDataString(sql);
            }
            return result;
        }

        /// <summary>
        /// 供应商hoscode和shortname
        /// </summary>
        /// <returns></returns>
        public DataTable GetSupplierHosAndShort()
        {
            string sql = "SELECT SupplierHosCode,SupplierShortName FROM dbo.OWN_SupplierInfo";
            return GetDataTable(sql);
        }

        /// <summary>
        /// 根据供应商hoscode获取shortname
        /// </summary>
        /// <param name="hoscode"></param>
        /// <returns></returns>
        public string GetSupplierShortNameByHosCode(string hoscode)
        {
            string sql = "SELECT SupplierShortName FROM dbo.OWN_SupplierInfo WHERE SupplierHosCode='" + hoscode + "'";
            return GetDataString(sql);
        }

        /// <summary>
        /// 获取商品信息（带条码信息）
        /// </summary>
        /// <param name="stationid"></param>
        /// <param name="superdeptid"></param>
        /// <returns></returns>
        public DataTable GetGoodsInfo(string stationid, string superdeptid, string itemname)
        {
            string sql = "SELECT DISTINCT BARCODE,ItemName,upc FROM dbo.vw_NoOilGoodsBaseInfo WHERE 1=1 ";
            if (!string.IsNullOrWhiteSpace(stationid))
            {
                sql += " AND stationid='" + stationid + "'";
            }
            if (!string.IsNullOrWhiteSpace(superdeptid))
            {
                sql += " AND SUPERDEPTID='" + superdeptid + "'";
            }
            if (!string.IsNullOrWhiteSpace(itemname))
            {
                sql += " AND ItemName LIKE '%" + itemname + "%'";
            }
            return GetDataTable(sql);
        }

        /// <summary>
        /// 获取商品信息（不带带条码信息）
        /// </summary>
        /// <param name="stationid"></param>
        /// <param name="superdeptid"></param>
        /// <returns></returns>
        public DataTable GetGoodsInfo_ex(string stationid, string superdeptid, string itemname)
        {
            string sql = "SELECT DISTINCT BARCODE,ItemName FROM dbo.vw_NoOilGoodsBaseInfo WHERE 1=1 ";
            if (!string.IsNullOrWhiteSpace(stationid))
            {
                sql += " AND stationid='" + stationid + "'";
            }
            if (!string.IsNullOrWhiteSpace(superdeptid))
            {
                sql += " AND SUPERDEPTID='" + superdeptid + "'";
            }
            if (!string.IsNullOrWhiteSpace(itemname))
            {
                sql += " AND ItemName LIKE '%" + itemname + "%'";
            }
            return GetDataTable(sql);
        }

        /// <summary>
        /// 根据barcode获取商品名称
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        public string GetItemNameByBarCode(string barcode)
        {
            string sql = "SELECT DISTINCT ItemName FROM dbo.vw_NoOilGoodsBaseInfo WHERE BARCODE='" + barcode + "'";
            return GetDataString(sql);
        }

        /// <summary>
        /// 根据barcode和stationid获取商品条码
        /// </summary>
        /// <param name="barcode"></param>
        /// <param name="stationid"></param>
        /// <returns></returns>
        public string GetUpcByBorCodeAndStationId(string barcode, string stationid)
        {
            string sql = "SELECT upc FROM dbo.vw_NoOilGoodsBaseInfo WHERE BARCODE='" + barcode + "' AND stationid='" + stationid + "'";
            return GetDataString(sql);
        }

        /// <summary>
        /// 供应商商品信息
        /// </summary>
        /// <param name="supplierhoscode">供应商id</param>
        /// <param name="goodsname">商品名称（模糊）</param>
        /// <returns></returns>
        public DataTable GetSupplierGoodsInfo(string supplierhoscode, string goodsname)
        {
            string sql = @"SELECT DISTINCT a.KeyId,a.SupplierId,si.SupplierName,si.SupplierShortName,a.BarCode,a.UPC,a.GoodsName FROM dbo.OWN_SupplierAndGoodsRef a
                        LEFT JOIN dbo.OWN_SupplierInfo si ON a.SupplierId=si.KeyId
                        WHERE 1=1 ";
            if (!string.IsNullOrWhiteSpace(supplierhoscode))
            {
                sql += " AND SupplierHosCode='" + supplierhoscode + "'";
            }
            if (!string.IsNullOrWhiteSpace(goodsname))
            {
                sql += " AND GoodsName LIKE '%" + goodsname + "%'";
            }
            return GetDataTable(sql);
        }

        /// <summary>
        /// 根据加油站获取主卡信息
        /// </summary>
        /// <param name="stationid"></param>
        /// <returns></returns>
        public DataTable GetCrd_MainInfo(string stationid)
        {
            string sql = "SELECT * FROM dbo.Crd_MainInfo WHERE IssueStationId='" + stationid + "'";
            return GetDataTable(sql);
        }

        /// <summary>
        /// 根据主卡id获取子卡信息
        /// </summary>
        /// <param name="MainId"></param>
        /// <returns></returns>
        public DataTable GetCrd_DetailInfo(string where)
        {
            string sql = @"SELECT DISTINCT a.*,main.CardNum,main.IssueStationId FROM dbo.Crd_SubInfo a
                            JOIN dbo.Crd_MainInfo main ON main.KeyId=a.MainId 
                            WHERE 1=1 ";
            if (!string.IsNullOrWhiteSpace(where))
            {
                sql += where;
            }
            return GetDataTable(sql);
        }

        /// <summary>
        /// 获取全年数据表
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public DataTable GetYearDateList(string year)
        {
            string sql = @"SELECT '" + year + "' AS Name,'" + year + "' AS ID,'0' AS PID UNION SELECT LEFT(myday,7),LEFT(myday,7),LEFT(myday,4) FROM dbo.f_GetDaysByYearMonth('" + year + "-01-01','" + year + "-12-31') UNION SELECT myday,myday,LEFT(myday,7) AS MyMonth FROM dbo.f_GetDaysByYearMonth('" + year + "-01-01','" + year + "-12-31')  ORDER BY ID";
            return GetDataTable(sql);
        }

        /// <summary>
        /// 获取用于Combobox的促销品一级分类数据表
        /// </summary>
        /// <returns></returns>
        public DataTable GetHelpSaleFirstTypeForCombobox()
        {
            string sql = @"SELECT KeyId,FirstName FROM dbo.HelpSale_GoodsFirstType WHERE UseFlag=1";
            return GetDataTable(sql);
        }

        /// <summary>
        /// 获取用于Tree的促销品一级分类数据表
        /// </summary>
        /// <returns></returns>
        public DataTable GetHelpSaleFirstTypeForTree()
        {
            string sql = @"SELECT KeyId,FirstName,'0' AS PID FROM dbo.HelpSale_GoodsFirstType WHERE UseFlag=1";
            return GetDataTable(sql);
        }

        /// <summary>
        /// 获取企业性质信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetCompanyPropertyData()
        {
            string sql = "SELECT * FROM Foe_BaseCode WHERE Type='AI'";
            return GetDataTable(sql);
        }

        /// <summary>
        /// 获取促销品供应商信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetHelpSaleSupplierInfo()
        {
            string sql = "SELECT KeyId,SupplierName,SupplierShortName FROM dbo.HelpSale_SupplierInfo";
            return GetDataTable(sql);
        }

        /// <summary>
        /// 获取已通过审核的促销方案
        /// </summary>
        /// <returns></returns>
        public DataTable GetHelpSaleScheme()
        {
            string sql = "SELECT KeyId,SchemeName FROM dbo.HelpSale_SchemeMain WHERE DataFlag=1";
            return GetDataTable(sql);
        }

        /// <summary>
        /// 获取JT非油商品信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetJTNoOilGoodsList()
        {
            string sql = "SELECT KeyId,ItemName FROM dbo.OWN_NoOilGoodsBaseInfo";
            return GetDataTable(sql);
        }

        /// 判断某站某天是否已经日结
        /// </summary>
        /// <param name="station"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public bool IsDayBatched(string station, DateTime date)
        {
            bool blReturn = false;
            string sql = @"SELECT COUNT(1) AS IsDayBatched FROM DGLINK.DG.dbo.vw_DAYBATCH
                WHERE STATUS='C'
                AND stationid='" + station + "' AND DAY_BATCH_DATE='" + date.ToString("yyyy-MM-dd") + "'";
            DataTable dt = this.GetDataTable(sql);
            if (dt.Rows.Count == 0 || dt.Rows[0]["IsDayBatched"].ToString().Equals("0"))
            {
                blReturn = false;
            }
            else
            {
                blReturn = true;
            }
            return blReturn;
        }

    }
}
