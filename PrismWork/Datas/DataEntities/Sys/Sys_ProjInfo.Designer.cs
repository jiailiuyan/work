using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Entities 
{    
    /// <summary>
    /// 项目基本信息数据实体类
    /// 生成日期: 2014年10月 27日 20:13
    ///</summary>
    /// <remarks>
    /// 该文件是由代码生成器自动生成的, 请不要随意修改, 你的修改将在代码重新生成时会被覆盖，
    /// 如果要对该类进行修改, 请直接修改该分部类的Sys_ProjInfo文件(文件名不含.designer)
    /// </remarks>
    public partial class Sys_ProjInfo : BusinessEntity
    {  
       private int _keyId;
       private string _projName;
       private string _projFullName;
       private string _projStage;
       private string _projCode;
       private string _projDetails;
       private DateTime? _onlineDate;
       private string _director;
       private string _dBIPAdd;
       private string _dBName;
       private string _dBUser;
       private string _dBPwd;
       private bool _status;
       private string _remark;
       private int _createdBy;
       private DateTime _createdOn;
       private int? _modifiedBy;
       private DateTime? _modifiedOn;
      
      #region properties

      
      /// <summary>
      ///  获取或设置一个值, 该值代表项目内码
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
      ///  获取或设置一个值, 该值代表项目名称
      /// </summary>
      public string ProjName
      {
        get
        {
         return string.IsNullOrEmpty(this._projName) ? string.Empty : this._projName.Trim(); 
        }
        set
        {
          this._projName = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表ProjFullName
      /// </summary>
      public string ProjFullName
      {
        get
        {
         return string.IsNullOrEmpty(this._projFullName) ? string.Empty : this._projFullName.Trim(); 
        }
        set
        {
          this._projFullName = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表00：新项目；01：需求分析；02：程序开发；03：试运行；04：正式运行
      /// </summary>
      public string ProjStage
      {
        get
        {
         return string.IsNullOrEmpty(this._projStage) ? string.Empty : this._projStage.Trim(); 
        }
        set
        {
          this._projStage = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表ProjCode
      /// </summary>
      public string ProjCode
      {
        get
        {
         return string.IsNullOrEmpty(this._projCode) ? string.Empty : this._projCode.Trim(); 
        }
        set
        {
          this._projCode = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表项目概况
      /// </summary>
      public string ProjDetails
      {
        get
        {
         return string.IsNullOrEmpty(this._projDetails) ? string.Empty : this._projDetails.Trim(); 
        }
        set
        {
          this._projDetails = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表项目上线时间
      /// </summary>
      public DateTime? OnlineDate
      {
        get
        {
         return this._onlineDate; 
        }
        set
        {
          this._onlineDate = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表项目负责人
      /// </summary>
      public string Director
      {
        get
        {
         return string.IsNullOrEmpty(this._director) ? string.Empty : this._director.Trim(); 
        }
        set
        {
          this._director = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表DBIPAdd
      /// </summary>
      public string DBIPAdd
      {
        get
        {
         return string.IsNullOrEmpty(this._dBIPAdd) ? string.Empty : this._dBIPAdd.Trim(); 
        }
        set
        {
          this._dBIPAdd = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表DBName
      /// </summary>
      public string DBName
      {
        get
        {
         return string.IsNullOrEmpty(this._dBName) ? string.Empty : this._dBName.Trim(); 
        }
        set
        {
          this._dBName = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表DBUser
      /// </summary>
      public string DBUser
      {
        get
        {
         return string.IsNullOrEmpty(this._dBUser) ? string.Empty : this._dBUser.Trim(); 
        }
        set
        {
          this._dBUser = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表DBPwd
      /// </summary>
      public string DBPwd
      {
        get
        {
         return string.IsNullOrEmpty(this._dBPwd) ? string.Empty : this._dBPwd.Trim(); 
        }
        set
        {
          this._dBPwd = value;
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