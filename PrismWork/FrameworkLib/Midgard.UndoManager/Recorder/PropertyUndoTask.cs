using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Work.UndoManager.TaskModel;
using System.Reflection;
using System.Diagnostics.Contracts;
using Work.UndoManager.Recorder;

namespace Work.UndoManager
{
    /// <summary>
    /// 封装单个属性更改任务处理
    /// </summary>
    public class PropertyUndoTask : UndoTask
    {
        #region 字段,属性
        /// <summary>
        /// 绑定对象
        /// </summary>
        private object objectItem;
        /// <summary>
        /// 属性
        /// </summary>
        private PropertyInfo prop;
        /// <summary>
        /// 新的值
        /// </summary>
        public object NewValue { get; set; }
        /// <summary>
        /// 旧的值
        /// </summary>
        public object OldValue { get; set; }
        /// <summary>
        /// 当前属性名称
        /// </summary>
        public string PropertyName
        {
            get { return prop.Name; }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 父类通过事件通知到子类
        /// </summary>
        public PropertyUndoTask(DefaultRecorder recorder, object sender, PropertyInfo prop, object oldValue, object newValue)
            : base(recorder)
        {
            this.objectItem = sender;
            this.prop = prop;
            this.OldValue = oldValue;
            this.NewValue = newValue;
            this.Action = EnumUndoAction.SetProperty;
            this.execute = Redoing;
            this.unExecute = Undoing;
        }
        #endregion

        private void Undoing(object temp)
        {
            SetPropertyValue(OldValue);
        }

        private void Redoing(object temp)
        {
            SetPropertyValue(NewValue);
        }

        /// <summary>
        /// 设置属性值
        /// </summary>
        /// <param name="value"></param>
        private void SetPropertyValue(object value)
        {
            prop.SetValue(objectItem, value, DefaultRecorder.EmptyArray);
        }

        /// <summary>
        /// 当前对象的描述
        /// </summary>
        public override string DescriptionForUser
        {
            get { return "PropertyUndoTask"; }
        }
    }
}
