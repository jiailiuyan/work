using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Entities 
{    
    /// <summary>
    /// Log_SqlError数据实体类
    /// 生成日期: 2014年10月 27日 20:39
    ///</summary>
    /// <remarks>
    /// 该文件是由代码生成器自动生成的, 请不要随意修改, 你的修改将在代码重新生成时会被覆盖，
    /// 如果要对该类进行修改, 请直接修改该分部类的Log_SqlError文件(文件名不含.designer)
    /// </remarks>
    public partial class Log_SqlError : BusinessEntity
    {  
       private int _keyId;
       private string _sPName;
       private string _description;
       private DateTime _logTime;
      
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
      ///  获取或设置一个值, 该值代表SPName
      /// </summary>
      public string SPName
      {
        get
        {
         return string.IsNullOrEmpty(this._sPName) ? string.Empty : this._sPName.Trim(); 
        }
        set
        {
          this._sPName = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表Description
      /// </summary>
      public string Description
      {
        get
        {
         return string.IsNullOrEmpty(this._description) ? string.Empty : this._description.Trim(); 
        }
        set
        {
          this._description = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表LogTime
      /// </summary>
      public DateTime LogTime
      {
        get
        {
         return this._logTime; 
        }
        set
        {
          this._logTime = value;
        }
      }      
      
      
      #endregion properties
      
      
   }
}  