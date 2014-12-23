using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Entities 
{    
    /// <summary>
    /// 项目角色数据实体类
    /// 生成日期: 2014年10月 27日 20:45
    ///</summary>
    /// <remarks>
    /// 该文件是由代码生成器自动生成的, 请不要随意修改, 你的修改将在代码重新生成时会被覆盖，
    /// 如果要对该类进行修改, 请直接修改该分部类的Sys_Roles文件(文件名不含.designer)
    /// </remarks>
    public partial class Sys_Roles : BusinessEntity
    {  
       private int _keyId;
       private int _projID;
       private string _roleName;
       private string _roleCode;
       private bool _status;
       private string _remark;
       private int _createdBy;
       private DateTime _createdOn;
       private int? _modifiedBy;
       private DateTime? _modifiedOn;
      
      #region properties

      
      /// <summary>
      ///  获取或设置一个值, 该值代表主键
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
      ///  获取或设置一个值, 该值代表项目内码
      /// </summary>
      public int ProjID
      {
        get
        {
         return this._projID; 
        }
        set
        {
          this._projID = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表角色名称
      /// </summary>
      public string RoleName
      {
        get
        {
         return string.IsNullOrEmpty(this._roleName) ? string.Empty : this._roleName.Trim(); 
        }
        set
        {
          this._roleName = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表RoleCode
      /// </summary>
      public string RoleCode
      {
        get
        {
         return string.IsNullOrEmpty(this._roleCode) ? string.Empty : this._roleCode.Trim(); 
        }
        set
        {
          this._roleCode = value;
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