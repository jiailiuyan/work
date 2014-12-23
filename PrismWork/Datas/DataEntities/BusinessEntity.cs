using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.ComponentModel;

namespace Project.Entities
{
    /// <summary>
    /// BusinessEntity 业务实体基类
    /// 开发人员: 
    /// 开发日期: 2012年06月
    /// </summary>
    [Serializable()]
    public abstract class BusinessEntity : ICloneable
    {
        private bool _isNew = false;
        private bool _isDeleted = false;
        private bool _isDirty = false;
        private object _tag;

        /// <summary>
        /// 获取一个值, 该值表示是否是新创建的对象, 该对象还没有插入数据库中
        /// </summary>
        [Browsable(false)]
        public bool IsNew
        {
            get
            {
                return this._isNew;
            }
        }

        /// <summary>
        /// 获取一个值, 该值表示是否是一个删除的对象, 该对象还没有从数据库中删除
        /// </summary>
        [Browsable(false)]
        public bool IsDeleted
        {
            get
            {
                return this._isDeleted;
            }
        }

        /// <summary>
        /// 获取一个值, 该值表示业务实体对象是否被修改
        /// </summary>
        [Browsable(false)]
        public virtual bool IsDirty
        {
            get
            {
                return this._isDirty;
            }
        }

        /// <summary>
        /// 获取一个值, 该值表示该对象是否有效
        /// </summary>
        [Browsable(false)]
        public virtual bool IsValid
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// 获取一个值, 该值表示该对象是否可以进行保存
        /// </summary>
        [Browsable(false)]
        public virtual bool IsSaveable
        {
            get
            {
                return this.IsDirty && this.IsValid;
            }
        }

        [Browsable(false)]
        public object Tag
        {
            get
            {
                return this._tag;
            }
            set
            {
                this._tag = value;
            }
        }

        /// <summary>
        /// 标记一个对象为新创建的对象
        /// </summary>
        public virtual void MarkNew()
        {
            this._isNew = true;
            this._isDeleted = false;
            this._isDirty = false;
        }

        /// <summary>
        /// 标记一个对象被删除
        /// </summary>
        public virtual void MarkDeleted()
        {
            this._isDeleted = true;
        }

        /// <summary>
        /// 标记一个对象是干净的对象
        /// </summary>
        public virtual void MarkClean()
        {
            this._isDirty = false;
            this._isNew = false;
            this._isDeleted = false;
        }

        /// <summary>
        /// 标记一个对象为脏对象
        /// </summary>
        public virtual void MarkDirty()
        {
            this._isDirty = true;
        }

        #region ICloneable 成员
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            object newObject = Activator.CreateInstance(this.GetType());

            FieldInfo[] fields = newObject.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

            int i = 0;

            foreach (FieldInfo fi in this.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                Type ICloneType = fi.FieldType.GetInterface("ICloneable", true);

                if (ICloneType != null)
                {
                    ICloneable IClone = (ICloneable)fi.GetValue(this);

                    fields[i].SetValue(newObject, IClone.Clone());
                }
                else
                {
                    fields[i].SetValue(newObject, fi.GetValue(this));
                }

                Type IEnumerableType = fi.FieldType.GetInterface("IEnumerable", true);
                if (IEnumerableType != null)
                {
                    IEnumerable IEnum = (IEnumerable)fi.GetValue(this);

                    Type IListType = fields[i].FieldType.GetInterface("IList", true);
                    Type IDicType = fields[i].FieldType.GetInterface("IDictionary", true);

                    int j = 0;
                    if (IListType != null)
                    {
                        IList list = (IList)fields[i].GetValue(newObject);

                        foreach (object obj in IEnum)
                        {
                            ICloneType = obj.GetType().
                                GetInterface("ICloneable", true);

                            if (ICloneType != null)
                            {
                                ICloneable clone = (ICloneable)obj;

                                list[j] = clone.Clone();
                            }
                            j++;
                        }
                    }
                    else if (IDicType != null)
                    {
                        IDictionary dic = (IDictionary)fields[i].GetValue(newObject);
                        j = 0;

                        foreach (DictionaryEntry de in IEnum)
                        {
                            ICloneType = de.Value.GetType().GetInterface("ICloneable", true);

                            if (ICloneType != null)
                            {
                                ICloneable clone = (ICloneable)de.Value;

                                dic[de.Key] = clone.Clone();
                            }
                            j++;
                        }
                    }
                }
                i++;
            }
            return newObject;
        }

        #endregion
    }
}