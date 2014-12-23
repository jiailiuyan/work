using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Entities 
{    
    /// <summary>
    /// Log_Login数据实体类
    /// 生成日期: 2014年10月 28日 14:36
    ///</summary>
    /// <remarks>
    /// 该文件是由代码生成器自动生成的, 请不要随意修改, 你的修改将在代码重新生成时会被覆盖，
    /// 如果要对该类进行修改, 请直接修改该分部类的Log_Login文件(文件名不含.designer)
    /// </remarks>
    public partial class Log_Login : BusinessEntity
    {  
       private int _keyId;
       private int _personId;
       private string _loginIP;
       private string _loginHostName;
       private string _loginMac;
       private DateTime _loginTime;
       private DateTime? _logoutTime;
      
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
      ///  获取或设置一个值, 该值代表LoginIP
      /// </summary>
      public string LoginIP
      {
        get
        {
         return string.IsNullOrEmpty(this._loginIP) ? string.Empty : this._loginIP.Trim(); 
        }
        set
        {
          this._loginIP = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表LoginHostName
      /// </summary>
      public string LoginHostName
      {
        get
        {
         return string.IsNullOrEmpty(this._loginHostName) ? string.Empty : this._loginHostName.Trim(); 
        }
        set
        {
          this._loginHostName = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表LoginMac
      /// </summary>
      public string LoginMac
      {
        get
        {
         return string.IsNullOrEmpty(this._loginMac) ? string.Empty : this._loginMac.Trim(); 
        }
        set
        {
          this._loginMac = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表LoginTime
      /// </summary>
      public DateTime LoginTime
      {
        get
        {
         return this._loginTime; 
        }
        set
        {
          this._loginTime = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表LogoutTime
      /// </summary>
      public DateTime? LogoutTime
      {
        get
        {
         return this._logoutTime; 
        }
        set
        {
          this._logoutTime = value;
        }
      }      
      
      
      #endregion properties
      
      
   }
}  