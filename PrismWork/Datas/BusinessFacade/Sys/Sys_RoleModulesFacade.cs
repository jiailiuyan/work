using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Transactions;
using Project.Common;
using Project.DataAccess;
using Project.Entities;

namespace Project.BusinessFacade 
{  
	/// <summary>
    /// 角色功能对应表业务外观类
    /// 开发人员: 
    /// 生成日期: 2014年10月 27日 20:59
    ///</summary>
    public partial class Sys_RoleModulesFacade
    {
	    /// <summary>
	    /// 更新Sys_RoleModules
	    /// </summary>
	    /// <param name="sys_RoleModules">实体类</param>
	    /// <param name="logEntity">日志类</param>
	    /// <returns></returns>
	    public string UpdateSys_RoleModules(Sys_RoleModules sys_RoleModules,Log_Operate logEntity)
	    {
			string strResult = "";
			using (TransactionScope trans = new TransactionScope())
	        {
	             try
	             {
	                 this._sys_RoleModulesDAO.UpdateSys_RoleModules(sys_RoleModules);
	                 Log_OperateFacade logFacade = new Log_OperateFacade();
	                 int intLog = logFacade.CreateLog_Operate(logEntity);
	                 trans.Complete();
	             }
	             catch (Exception ex)
	             {
	                 strResult = ex.Message;
	             }
	         }
	         return strResult;
	    }
    
	    /// <summary>
	    /// 创建Sys_RoleModules
	   	/// </summary>
	   	/// <param name="sys_RoleModules">实体类</param>
	    /// <param name="logEntity">日志类</param>
	    /// <param name="strResult">错误信息</param>
	    /// <returns></returns>
	    public int InsertSys_RoleModules(Sys_RoleModules sys_RoleModules,Log_Operate logEntity,ref string strResult)
	    {
			int intResult = 0;
			using (TransactionScope trans = new TransactionScope())
	        {
	             try
	             {
	                 this._sys_RoleModulesDAO.InsertSys_RoleModules(sys_RoleModules);
	                 Log_OperateFacade logFacade = new Log_OperateFacade();
	                 logEntity.OperateFunction = "新增_sys_RoleModules表ID为" + intResult.ToString() + "的数据";
	                 int intLog = logFacade.CreateLog_Operate(logEntity);
	                 trans.Complete();
	             }
	             catch (Exception ex)
	             {
	                 strResult = ex.Message;
	             }
	         }
	         return intResult;
	    }
    
	    /// <summary>
	    /// 删除Sys_RoleModules
	    /// </summary>
	    /// <param name="Sys_RoleModulesId">主码</param>
	    /// <param name="logEntity">日志类</param>
	    /// <returns></returns>
	    public string DeleteSys_RoleModules(int keyId,Log_Operate logEntity)
	    {
			string strResult = "";
			using (TransactionScope trans = new TransactionScope())
			{
				try
				{
					this._sys_RoleModulesDAO.DeleteSys_RoleModules(keyId);
					Log_OperateFacade logFacade = new Log_OperateFacade();
					int intLog = logFacade.CreateLog_Operate(logEntity);
					trans.Complete();
				}
				catch (Exception ex)
				{
					strResult = ex.Message;
				}
			}
			 return strResult;
	    }

        #region V1
        /// <summary>
        /// 保存角色拥有的模块权限
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="role_id"></param>
        /// <param name="user_id"></param>
        /// <param name="Proj_id"></param>
        public void SaveRoleModules(DataTable dt, string role_id, string user_id, int Proj_id)
        {
            
            DataTable new_dt = new DataTable();
            DataColumn col;

            col = new DataColumn("ModuleId");
            new_dt.Columns.Add(col);

            col = new DataColumn("PrivilegeMask");
            new_dt.Columns.Add(col);

            int pri_mask = 0;

            foreach (DataRow mod_row in dt.Rows)
            {
                int c_01 = bool.Parse(mod_row["c_01"] + "") ? 1 : 0;
                int c_02 = bool.Parse(mod_row["c_02"] + "") ? 1 : 0;
                int c_03 = bool.Parse(mod_row["c_03"] + "") ? 1 : 0;
                int c_04 = bool.Parse(mod_row["c_04"] + "") ? 1 : 0;
                int c_05 = bool.Parse(mod_row["c_05"] + "") ? 1 : 0;
                int c_06 = bool.Parse(mod_row["c_06"] + "") ? 1 : 0;
                int c_07 = bool.Parse(mod_row["c_07"] + "") ? 1 : 0;

                int pri_01 = c_01 * Privilege.SELECT_PRIVILEGE_MASK;

                int pri_02 = c_02 * Privilege.CREATE_PRIVILEGE_MASK;

                int pri_03 = c_03 * Privilege.UPDATE_PRIVILEGE_MASK;

                int pri_04 = c_04 * Privilege.DELETE_PRIVILEGE_MASK;

                int pri_05 = c_05 * Privilege.PRINT_PRIVILEGE_MASK;

                int pri_06 = c_06 * Privilege.EXPORT_PRIVILEGE_MASK;

                int pri_07 = c_07 * Privilege.APPROVE_PRIVILEGE_MASK;

                //计算出权限值
                pri_mask = pri_01 | pri_02 | pri_03 | pri_04 | pri_04 | pri_05 | pri_06 | pri_07;

                //生成所需保存的数据
                DataRow new_row = new_dt.NewRow();
                //new_row.ItemArray = new object[3] { mod_row["ModuleId"] + "", pri_mask, mod_row["Description"] + "" };
                new_row.ItemArray = new object[2] { mod_row["ModuleId"] + "", pri_mask };
                new_dt.Rows.Add(new_row);
            }

            //执行保存数据
            new Sys_RoleModulesDAO().SaveRoleModules(new_dt, role_id, user_id, Proj_id);
        }

        /// <summary>
        /// 返回模块权限
        /// </summary>
        /// <param name="role_id"></param>
        /// <param name="parent_role_id"></param>
        /// <param name="module_id"></param>
        /// <param name="ProjID"></param>
        /// <returns></returns>
        public DataTable GetRoleModulesTable(int role_id, int parent_role_id, int module_id, int ProjID)
        {
            DataTable source_dt = new Sys_RoleModulesDAO().GetRoleModulesTable(role_id, parent_role_id, module_id, ProjID);

            DataTable dt = new DataTable();
            this.SetRolePrivilegeTableColums(ref dt);

            foreach (DataRow source_row in source_dt.Rows)
            {
                DataRow new_row = dt.NewRow();
                this.SetRolePrivilegeTable_DataRow(ref new_row, source_row);
                dt.Rows.Add(new_row);
            }
            return dt;
        }
        /// <summary>
        /// 设置角色权限的数据行
        /// </summary>
        /// <param name="ref_row"></param>
        /// <param name="source_row"></param>
        private void SetRolePrivilegeTable_DataRow(ref DataRow ref_row, DataRow source_row)
        {
            object[] item_obj = new object[16];
            item_obj[0] = source_row["moduleid"].ToString();
            item_obj[1] = source_row["ModuleName"].ToString();
            //item_obj[16] = source_row["Description"].ToString();

            int pri_mask = (int)source_row["PrivilegeMask"];

            int dsp_mask = (int)source_row["DisplayPrivilegeMask"];


            if ((pri_mask & Privilege.SELECT_PRIVILEGE_MASK) == Privilege.SELECT_PRIVILEGE_MASK) item_obj[2] = "1"; else item_obj[2] = "0";

            if ((pri_mask & Privilege.CREATE_PRIVILEGE_MASK) == Privilege.CREATE_PRIVILEGE_MASK) item_obj[3] = "1"; else item_obj[3] = "0";

            if ((pri_mask & Privilege.UPDATE_PRIVILEGE_MASK) == Privilege.UPDATE_PRIVILEGE_MASK) item_obj[4] = "1"; else item_obj[4] = "0";

            if ((pri_mask & Privilege.DELETE_PRIVILEGE_MASK) == Privilege.DELETE_PRIVILEGE_MASK) item_obj[5] = "1"; else item_obj[5] = "0";

            if ((pri_mask & Privilege.PRINT_PRIVILEGE_MASK) == Privilege.PRINT_PRIVILEGE_MASK) item_obj[6] = "1"; else item_obj[6] = "0";

            if ((pri_mask & Privilege.EXPORT_PRIVILEGE_MASK) == Privilege.EXPORT_PRIVILEGE_MASK) item_obj[7] = "1"; else item_obj[7] = "0";

            if ((pri_mask & Privilege.APPROVE_PRIVILEGE_MASK) == Privilege.APPROVE_PRIVILEGE_MASK) item_obj[8] = "1"; else item_obj[8] = "0";

            //设置显示

            if ((dsp_mask & Privilege.SELECT_PRIVILEGE_MASK) == Privilege.SELECT_PRIVILEGE_MASK) item_obj[9] = "1"; else item_obj[9] = "0";

            if ((dsp_mask & Privilege.CREATE_PRIVILEGE_MASK) == Privilege.CREATE_PRIVILEGE_MASK) item_obj[10] = "1"; else item_obj[10] = "0";

            if ((dsp_mask & Privilege.UPDATE_PRIVILEGE_MASK) == Privilege.UPDATE_PRIVILEGE_MASK) item_obj[11] = "1"; else item_obj[11] = "0";

            if ((dsp_mask & Privilege.DELETE_PRIVILEGE_MASK) == Privilege.DELETE_PRIVILEGE_MASK) item_obj[12] = "1"; else item_obj[12] = "0";

            if ((dsp_mask & Privilege.PRINT_PRIVILEGE_MASK) == Privilege.PRINT_PRIVILEGE_MASK) item_obj[13] = "1"; else item_obj[13] = "0";

            if ((dsp_mask & Privilege.EXPORT_PRIVILEGE_MASK) == Privilege.EXPORT_PRIVILEGE_MASK) item_obj[14] = "1"; else item_obj[14] = "0";

            if ((dsp_mask & Privilege.APPROVE_PRIVILEGE_MASK) == Privilege.APPROVE_PRIVILEGE_MASK) item_obj[15] = "1"; else item_obj[15] = "0";

            ref_row.ItemArray = item_obj;
        }
        /// <summary>
        /// 设置角色权限的数据表列数据
        /// </summary>
        /// <param name="ref_dt"></param>
        private void SetRolePrivilegeTableColums(ref DataTable ref_dt)
        {
            DataColumn col;

            col = new DataColumn("moduleid");
            ref_dt.Columns.Add(col);

            col = new DataColumn("ModuleName");
            ref_dt.Columns.Add(col);

            col = new DataColumn("c_01");
            ref_dt.Columns.Add(col);

            col = new DataColumn("c_02");
            ref_dt.Columns.Add(col);

            col = new DataColumn("c_03");
            ref_dt.Columns.Add(col);

            col = new DataColumn("c_04");
            ref_dt.Columns.Add(col);

            col = new DataColumn("c_05");
            ref_dt.Columns.Add(col);

            col = new DataColumn("c_06");
            ref_dt.Columns.Add(col);

            col = new DataColumn("c_07");
            ref_dt.Columns.Add(col);

            col = new DataColumn("f_01");
            ref_dt.Columns.Add(col);

            col = new DataColumn("f_02");
            ref_dt.Columns.Add(col);

            col = new DataColumn("f_03");
            ref_dt.Columns.Add(col);

            col = new DataColumn("f_04");
            ref_dt.Columns.Add(col);

            col = new DataColumn("f_05");
            ref_dt.Columns.Add(col);

            col = new DataColumn("f_06");
            ref_dt.Columns.Add(col);

            col = new DataColumn("f_07");
            ref_dt.Columns.Add(col);

        }
        #endregion
    }
}  
