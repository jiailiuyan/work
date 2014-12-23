using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Entities 
{    
    /// <summary>
    /// 业务单位类型,该表不做显示维护数据实体类
    /// 生成日期: 2014年10月 27日 21:07
    ///</summary>
    /// <remarks>
    /// 该文件是由代码生成器自动生成的, 请不要随意修改, 你的修改将在代码重新生成时会被覆盖，
    /// 如果要对该类进行修改, 请直接修改该分部类的Sys_BusiUnitType文件(文件名不含.designer)
    /// </remarks>
    public partial class Sys_BusiUnitType : BusinessEntity
    {  
       private int _keyId;
       private string _busiUnitTypeCode;
       private string _busiUnitTypeName;
       private string _busiUnitTypeShortName;
       private int? _parentID;
       private bool _status;
       private int _displayOrder;
       private string _remark;
       private int _createdBy;
       private DateTime _createdOn;
       private int? _modifiedBy;
       private DateTime? _modifiedOn;
      
      #region properties

      
      /// <summary>
      ///  获取或设置一个值, 该值代表业务单位类型内码
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
      ///  获取或设置一个值, 该值代表BusiUnitTypeCode
      /// </summary>
      public string BusiUnitTypeCode
      {
        get
        {
         return string.IsNullOrEmpty(this._busiUnitTypeCode) ? string.Empty : this._busiUnitTypeCode.Trim(); 
        }
        set
        {
          this._busiUnitTypeCode = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表类型名称
      /// </summary>
      public string BusiUnitTypeName
      {
        get
        {
         return string.IsNullOrEmpty(this._busiUnitTypeName) ? string.Empty : this._busiUnitTypeName.Trim(); 
        }
        set
        {
          this._busiUnitTypeName = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表类型简称
      /// </summary>
      public string BusiUnitTypeShortName
      {
        get
        {
         return string.IsNullOrEmpty(this._busiUnitTypeShortName) ? string.Empty : this._busiUnitTypeShortName.Trim(); 
        }
        set
        {
          this._busiUnitTypeShortName = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表ParentID
      /// </summary>
      public int? ParentID
      {
        get
        {
         return this._parentID; 
        }
        set
        {
          this._parentID = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表是否有效
      /// </summary>
      public bool Status
      {
        get
        {
         return this._status; 
        }
        set
        {
          this._status = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表显示顺序
      /// </summary>
      public int DisplayOrder
      {
        get
        {
         return this._displayOrder; 
        }
        set
        {
          this._displayOrder = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表备注
      /// </summary>
      public string Remark
      {
        get
        {
         return string.IsNullOrEmpty(this._remark) ? string.Empty : this._remark.Trim(); 
        }
        set
        {
          this._remark = value;
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