using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CocoStudio.UndoManager
{
    /// <summary>
    /// 选中项代理接口
    /// 因为当前项目作为底层项目,无法反向依赖Common项目
    /// 提供该接口,由Common项目实现,提供对选择对象切换事件的代理
    /// 使得分组命令管理类可以使用该接口获取事件通知
    /// </summary>
    public interface ISelectedServiceProxy
    {
        /// <summary>
        /// 选中项发生改变
        /// </summary>
        event Action<List<object>> SelectedObjectsChange;

        /// <summary>
        /// 是否为动画编辑器
        /// 在此增加标识是为了在自动记录撤销时，不会因为自动记录所选中的数量进行自动打包而作。。。 
        /// PS ： 暂时处理 2013 12 19
        /// </summary>
        event Action<bool> IsAnimationProject;
    }
}
