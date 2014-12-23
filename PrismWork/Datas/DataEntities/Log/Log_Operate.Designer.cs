using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Entities 
{    
    /// <summary>
    /// Log_Operate数据实体类
    /// 生成日期: 2014年10月 27日 20:24
    ///</summary>
    /// <remarks>
    /// 该文件是由代码生成器自动生成的, 请不要随意修改, 你的修改将在代码重新生成时会被覆盖，
    /// 如果要对该类进行修改, 请直接修改该分部类的Log_Operate文件(文件名不含.designer)
    /// </remarks>
    public partial class Log_Operate : BusinessEntity
    {  
       private int _keyId;
       private int _personId;
       private DateTime _operateTime;
       private int _operateModule;
       private string _operateType;
       private string _operateFunction;
      
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
      ///  获取或设置一个值, 该值代表OperateTime
      /// </summary>
      public DateTime OperateTime
      {
        get
        {
         return this._operateTime; 
        }
        set
        {
          this._operateTime = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表OperateModule
      /// </summary>
      public int OperateModule
      {
        get
        {
         return this._operateModule; 
        }
        set
        {
          this._operateModule = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表OperateType
      /// </summary>
      public string OperateType
      {
        get
        {
         return string.IsNullOrEmpty(this._operateType) ? string.Empty : this._operateType.Trim(); 
        }
        set
        {
          this._operateType = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表OperateFunction
      /// </summary>
      public string OperateFunction
      {
        get
        {
         return string.IsNullOrEmpty(this._operateFunction) ? string.Empty : this._operateFunction.Trim(); 
        }
        set
        {
          this._operateFunction = value;
        }
      }      
      
      
      #endregion properties
      
      
   }
}  