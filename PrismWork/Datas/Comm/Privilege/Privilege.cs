using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Common
{
    /// <summary>
    /// Privilege 模块内权限对象
    ///开发人员: 
    ///开发日期: 2012年06月
    /// </summary>
    public class Privilege
    {
        #region 权限常量定义

        /// <summary>
        /// 查询权限掩码
        /// </summary>
        public const int SELECT_PRIVILEGE_MASK = 1;

        /// <summary>
        /// 新增权限掩码
        /// </summary>
        public const int CREATE_PRIVILEGE_MASK = 2;

        /// <summary>
        /// 修改权限掩码
        /// </summary>
        public const int UPDATE_PRIVILEGE_MASK = 4;

        /// <summary>
        /// 删除权限掩码
        /// </summary>
        public const int DELETE_PRIVILEGE_MASK = 8;

        /// <summary>
        /// 打印权限掩码
        /// </summary>
        public const int PRINT_PRIVILEGE_MASK = 16;

        /// <summary>
        /// 导出权限掩码
        /// </summary>
        public const int EXPORT_PRIVILEGE_MASK = 32;

        /// <summary>
        /// 审核权限掩码
        /// </summary>
        public const int APPROVE_PRIVILEGE_MASK = 64;

        /// <summary>
        /// 提交权限掩码
        /// </summary>
        public const int SUBMIT_PRIVILEGE_MASK = 128;        

        #endregion 权限常量定义
        
        private int _privilegeMask;

        public Privilege(int privilegeMask)
        {
            this._privilegeMask = privilegeMask;
        }

        /// <summary>
        /// 获取一个值, 该值代表是否具有该模块的查询权限
        /// </summary>
        public bool SelectPrivilege
        {
            get
            {
                return (this._privilegeMask & SELECT_PRIVILEGE_MASK) == SELECT_PRIVILEGE_MASK;
            }
        }

        /// <summary>
        /// 获取一个值, 该值代表是否具有该模块的新增权限
        /// </summary>
        public bool CreatePrivilege
        {
            get
            {
                return (this._privilegeMask & CREATE_PRIVILEGE_MASK) == CREATE_PRIVILEGE_MASK;
            }
        }

        /// <summary>
        /// 获取一个值, 该值代表是否具有该模块的修改权限
        /// </summary>
        public bool UpdatePrivilege
        {
            get
            {
                return (this._privilegeMask & UPDATE_PRIVILEGE_MASK) == UPDATE_PRIVILEGE_MASK;
            }
        }

        /// <summary>
        /// 获取一个值, 该值代表是否具有该模块的删除权限
        /// </summary>
        public bool DeletePrivilege
        {
            get
            {
                return (this._privilegeMask & DELETE_PRIVILEGE_MASK) == DELETE_PRIVILEGE_MASK;
            }
        }

        /// <summary>
        /// 获取一个值, 该值代表是否具有该模块的打印权限
        /// </summary>
        public bool PrintPrivilege
        {
            get
            {
                return (this._privilegeMask & PRINT_PRIVILEGE_MASK) == PRINT_PRIVILEGE_MASK;
            }
        }

        /// <summary>
        /// 获取一个值, 该值代表是否具有该模块的导出权限
        /// </summary>
        public bool ExportPrivilege
        {
            get
            {
                return (this._privilegeMask & EXPORT_PRIVILEGE_MASK) == EXPORT_PRIVILEGE_MASK;
            }
        }

        /// <summary>
        /// 获取一个值, 该值代表是否具有该模块的审核权限
        /// </summary>
        public bool ApprovePrivilege
        {
            get
            {
                return (this._privilegeMask & APPROVE_PRIVILEGE_MASK) == APPROVE_PRIVILEGE_MASK;
            }
        }
        /// <summary>
        /// 获取一个值, 该值代表是否具有该模块的提交权限
        /// </summary>
        public bool SubmitPrivilege
        {
            get
            {
                return (this._privilegeMask & SUBMIT_PRIVILEGE_MASK) == SUBMIT_PRIVILEGE_MASK;
            }
        }

    }
}