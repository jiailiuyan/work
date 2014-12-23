using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Common
{
    /// <summary>
    /// 操作类型的枚举变量
    /// </summary>
    public enum EnumLogOperateType
    {
        /// <summary>
        /// 增加
        /// </summary>
        INSERT,
        /// <summary>
        /// 更新
        /// </summary>
        UPDATE,
        /// <summary>
        /// 删除
        /// </summary>
        DELETE,
    }
    /// <summary>
    /// 数据的状态
    /// </summary>
    public enum ModDataStatus
    {
        /// <summary>
        /// 模块数据为新增状态
        /// </summary>
        IsAdd = 0,
        /// <summary>
        /// 模块数据为修改状态
        /// </summary>
        IsModify = 1,
    }
}
