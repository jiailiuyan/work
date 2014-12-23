using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Entities 
{    
    /// <summary>
    /// Sys_PersonDeskTopConfig数据实体类
    /// 生成日期: 2014年11月 20日 18:49
    ///</summary>
    /// <remarks>
    /// 该文件是由代码生成器自动生成的, 请不要随意修改, 你的修改将在代码重新生成时会被覆盖，
    /// 如果要对该类进行修改, 请直接修改该分部类的Sys_PersonDeskTopConfig文件(文件名不含.designer)
    /// </remarks>
    public partial class Sys_PersonDeskTopConfig : BusinessEntity
    {  
       private int _keyId;
       private int _personId;
       private string _configName;
       private string _configValue;
       private DateTime? _createDate;
       private DateTime? _modifiedDate;
      
      #region properties

      
      /// <summary>
      ///  获取或设置一个值, 该值代表KeyId
      /// </summary>
      public int KeyId
      {
        get
        {
         return this._keyId; 
        }
        set
        {
          this._keyId = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表PersonId
      /// </summary>
      public int PersonId
      {
        get
        {
         return this._personId; 
        }
        set
        {
          this._personId = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表ConfigName
      /// </summary>
      public string ConfigName
      {
        get
        {
         return string.IsNullOrEmpty(this._configName) ? string.Empty : this._configName.Trim(); 
        }
        set
        {
          this._configName = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表ConfigValue
      /// </summary>
      public string ConfigValue
      {
        get
        {
         return string.IsNullOrEmpty(this._configValue) ? string.Empty : this._configValue.Trim(); 
        }
        set
        {
          this._configValue = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表CreateDate
      /// </summary>
      public DateTime? CreateDate
      {
        get
        {
         return this._createDate; 
        }
        set
        {
          this._createDate = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表ModifiedDate
      /// </summary>
      public DateTime? ModifiedDate
      {
        get
        {
         return this._modifiedDate; 
        }
        set
        {
          this._modifiedDate = value;
        }
      }      
      
      
      #endregion properties
      
      
   }
}  