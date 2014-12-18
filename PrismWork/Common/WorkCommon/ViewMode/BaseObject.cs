using System;
using System.ComponentModel;
using System.Reflection;
using Microsoft.Practices.Prism.ViewModel;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace WorkCommon.ViewModel
{
    /// <summary>
    /// 所有游戏引擎对象的父类
    /// 重写父类的PropertyChange事件,提供对撤销重做的支持
    /// </summary>
    [DataContract]
    public abstract class BaseObject : NotificationObject
    {
        #region 属性
        /// <summary>
        /// 是否自动触发状态更改属性,
        /// 控制调用PropertyChanged时,是否同步触发StateChanged
        /// 默认值为True
        /// </summary>
        [Browsable(false)]
        public bool IsRaiseStateChanged { get; set; }
      
        /// <summary>
        /// 名字地段
        /// </summary>
        protected string name;
        /// <summary>
        /// 当前对象的名字,
        /// </summary>
        [DisplayName("Display_Name")]
        [Category("Group_Routine")]
        public virtual string Name
        {
            get { return name; }
            set
            {
                name = value;
                RaisePropertyChanged("Name");
                RaisePropertyChanged("DisplayName");
            }
        }

        /// <summary>
        /// 用来显示的名字,与Name属性同步,优先显示Name属性
        /// 默认显示类名
        /// </summary>
        [Browsable(false)]
        public virtual string DisplayName
        {
            get
            {
                if (String.IsNullOrEmpty(Name) == false)
                {
                    return Name;
                }
                else
                    return "[Node]";
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        public BaseObject()
        {
            this.IsRaiseStateChanged = true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public BaseObject(string name)
            : this()
        {
            this.name = name;
        }
        #endregion

     
        #region 方法
        /// <summary>
        /// 重写父类的属性通知事件,默认通知撤销回退记录
        /// </summary>
        /// <param name="propertyName"></param>
        protected override void RaisePropertyChanged(string propertyName)
        {
            this.RaisePropertyChanged(propertyName, IsRaiseStateChanged);
        }
        /// <summary>
        /// 批量通知属性更改,并指定是否生成撤销回退记录
        /// </summary>
        /// <param name="isNotifyStateChanged">是否通知属性更改</param>
        /// <param name="propertyNames">需要通知的属性</param>
        protected void RaisePropertyChanged(bool isNotifyStateChanged, params string[] propertyNames)
        {
            foreach (var item in propertyNames)
            {
                this.RaisePropertyChanged(item, isNotifyStateChanged);
            }
        }
        /// <summary>
        /// 触发PropertyChanged事件,并控制是否通知StateChanged事件
        /// 调用该方法时,由参数isNotifyStateChanged显示控制是否触发StateChanged事件,
        /// 不再受 IsAutoRaiseStateChange 属性控制
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="isNotifyStateChanged"></param>
        protected virtual void RaisePropertyChanged(string propertyName, bool isNotifyStateChanged)
        {
            base.RaisePropertyChanged(propertyName);
        }
        /// <summary>
        /// 触发PropertyChanged事件,并控制是否通知StateChanged事件
        /// 调用该方法时,由参数isNotifyStateChanged显示控制是否触发StateChanged事件,
        /// 不再受 IsAutoRaiseStateChange 属性控制
        /// </summary>
        /// <param name="propertyExpression">属性名称表达式</param>
        /// <param name="isNotifyStateChanged">控制是否通知产生撤销回退记录</param>
        protected virtual void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression, bool isNotifyStateChanged)
        {
            string propertyName = PropertySupport.ExtractPropertyName(propertyExpression);
            this.RaisePropertyChanged(propertyName, isNotifyStateChanged);
        }
          
        #endregion
    }
}
