using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Entities
{
    /// <summary>
    /// ValueText 名值对数据实体类
    /// 开发人员: 
    /// 开发日期: 2012年06月
    /// </summary>
    public class ValueText : Project.Entities.BusinessEntity
    {
        private string _value;
        private string _text;
                
        /// <summary>
        /// 获取或设置一个值, 该值代表该对象代表的值字符串
        /// </summary>
        public string Value
        {
            get
            {
                return string.IsNullOrEmpty(this._value) ? string.Empty : this._value.Trim();
            }
            set
            {
                this._value = value;
            }
        }

        /// <summary>
        /// 获取或设置一个值, 该值代表该对象代表的文本
        /// </summary>
        public string Text
        {
            get
            {
                return string.IsNullOrEmpty(this._text) ? string.Empty : this._text.Trim();
            }
            set
            {
                this._text = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Text;
        }
    }
}
