using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Entities 
{    
    /// <summary>
    /// 项目模块数据实体类
    /// 生成日期: 2014年10月 27日 21:50
    ///</summary>
    /// <remarks>
    /// 该文件是由代码生成器自动生成的, 请不要随意修改, 你的修改将在代码重新生成时会被覆盖，
    /// 如果要对该类进行修改, 请直接修改该分部类的Sys_Modules文件(文件名不含.designer)
    /// </remarks>
    public partial class Sys_Modules : BusinessEntity
    {  
       private int _keyId;
       private int _projID;
       private string _moduleCode;
       private string _moduleName;
       private string _shortName;
       private int? _parentId;
       private string _urlString;
       private string _moduleEntry;
       private string _moduleIconS;
       private string _moduleIconB;
       private bool _isShowInDeskTop;
       private int? _openType;
       private bool _status;
       private string _hint;
       private int? _displayOrder;
       private int? _displayPrivilegeMask;
       private string _helpUrlString;
       private string _remark;
       private int _createdBy;
       private DateTime _createdOn;
       private int? _modifiedBy;
       private DateTime? _modifiedOn;
      
      #region properties

      
      /// <summary>
      ///  获取或设置一个值, 该值代表模块内码
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
      ///  获取或设置一个值, 该值代表ModuleCode
      /// </summary>
      public string ModuleCode
      {
        get
        {
         return string.IsNullOrEmpty(this._moduleCode) ? string.Empty : this._moduleCode.Trim(); 
        }
        set
        {
          this._moduleCode = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表模块名称
      /// </summary>
      public string ModuleName
      {
        get
        {
         return string.IsNullOrEmpty(this._moduleName) ? string.Empty : this._moduleName.Trim(); 
        }
        set
        {
          this._moduleName = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表ShortName
      /// </summary>
      public string ShortName
      {
        get
        {
         return string.IsNullOrEmpty(this._shortName) ? string.Empty : this._shortName.Trim(); 
        }
        set
        {
          this._shortName = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表上级模块内码
      /// </summary>
      public int? ParentId
      {
        get
        {
         return this._parentId; 
        }
        set
        {
          this._parentId = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表页面地址
      /// </summary>
      public string UrlString
      {
        get
        {
         return string.IsNullOrEmpty(this._urlString) ? string.Empty : this._urlString.Trim(); 
        }
        set
        {
          this._urlString = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表ModuleEntry
      /// </summary>
      public string ModuleEntry
      {
        get
        {
         return string.IsNullOrEmpty(this._moduleEntry) ? string.Empty : this._moduleEntry.Trim(); 
        }
        set
        {
          this._moduleEntry = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表ModuleIconS
      /// </summary>
      public string ModuleIconS
      {
        get
        {
         return string.IsNullOrEmpty(this._moduleIconS) ? string.Empty : this._moduleIconS.Trim(); 
        }
        set
        {
          this._moduleIconS = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表ModuleIconB
      /// </summary>
      public string ModuleIconB
      {
        get
        {
         return string.IsNullOrEmpty(this._moduleIconB) ? string.Empty : this._moduleIconB.Trim(); 
        }
        set
        {
          this._moduleIconB = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表主要针对只有二级菜单，需要直接打开该二级菜单链接地址而特殊设定
      /// </summary>
      public bool IsShowInDeskTop
      {
        get
        {
         return this._isShowInDeskTop; 
        }
        set
        {
          this._isShowInDeskTop = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表0：以B/S方式打开；1：以C/S方式打开
      /// </summary>
      public int? OpenType
      {
        get
        {
         return this._openType; 
        }
        set
        {
          this._openType = value;
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
      ///  获取或设置一个值, 该值代表提示信息
      /// </summary>
      public string Hint
      {
        get
        {
         return string.IsNullOrEmpty(this._hint) ? string.Empty : this._hint.Trim(); 
        }
        set
        {
          this._hint = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表DisplayOrder
      /// </summary>
      public int? DisplayOrder
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
      ///  获取或设置一个值, 该值代表权限掩码按：查询、新增、修改、删除、打印、导入、导出、审核、提交排列
      /// </summary>
      public int? DisplayPrivilegeMask
      {
        get
        {
         return this._displayPrivilegeMask; 
        }
        set
        {
          this._displayPrivilegeMask = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表HelpUrlString
      /// </summary>
      public string HelpUrlString
      {
        get
        {
         return string.IsNullOrEmpty(this._helpUrlString) ? string.Empty : this._helpUrlString.Trim(); 
        }
        set
        {
          this._helpUrlString = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表描述
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