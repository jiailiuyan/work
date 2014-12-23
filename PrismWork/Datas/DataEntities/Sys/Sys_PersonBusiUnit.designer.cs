using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Entities 
{    
    /// <summary>
    /// 一个人可能属于多个单位数据实体类
    /// 生成日期: 2014年10月 27日 21:08
    ///</summary>
    /// <remarks>
    /// 该文件是由代码生成器自动生成的, 请不要随意修改, 你的修改将在代码重新生成时会被覆盖，
    /// 如果要对该类进行修改, 请直接修改该分部类的Sys_PersonBusiUnit文件(文件名不含.designer)
    /// </remarks>
    public partial class Sys_PersonBusiUnit : BusinessEntity
    {  
       private int _keyId;
       private int _personID;
       private int _busiUnitID;
       private Int16 _isMaster;
       private DateTime _beginDate;
       private DateTime? _endDate;
       private int _createdBy;
       private DateTime _createdOn;
       private int? _modifiedBy;
       private DateTime? _modifiedOn;
      
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
      ///  获取或设置一个值, 该值代表人员内码
      /// </summary>
      public int PersonID
      {
        get
        {
         return this._personID; 
        }
        set
        {
          this._personID = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表业务单位内码
      /// </summary>
      public int BusiUnitID
      {
        get
        {
         return this._busiUnitID; 
        }
        set
        {
          this._busiUnitID = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表主要岗位标志
      /// </summary>
      public Int16 IsMaster
      {
        get
        {
         return this._isMaster; 
        }
        set
        {
          this._isMaster = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表开始时间
      /// </summary>
      public DateTime BeginDate
      {
        get
        {
         return this._beginDate; 
        }
        set
        {
          this._beginDate = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表结束时间
      /// </summary>
      public DateTime? EndDate
      {
        get
        {
         return this._endDate; 
        }
        set
        {
          this._endDate = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表记录创建人
      /// </summary>
      public int CreatedBy
      {
        get
        {
         return this._createdBy; 
        }
        set
        {
          this._createdBy = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表记录创建时间
      /// </summary>
      public DateTime CreatedOn
      {
        get
        {
         return this._createdOn; 
        }
        set
        {
          this._createdOn = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表记录修改人
      /// </summary>
      public int? ModifiedBy
      {
        get
        {
         return this._modifiedBy; 
        }
        set
        {
          this._modifiedBy = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表记录修改时间
      /// </summary>
      public DateTime? ModifiedOn
      {
        get
        {
         return this._modifiedOn; 
        }
        set
        {
          this._modifiedOn = value;
        }
      }      
      
      
      #endregion properties
      
      
   }
}  