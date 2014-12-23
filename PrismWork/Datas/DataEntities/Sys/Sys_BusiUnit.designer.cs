using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Entities 
{    
    /// <summary>
    /// 业务单位信息数据实体类
    /// 生成日期: 2014年10月 27日 21:07
    ///</summary>
    /// <remarks>
    /// 该文件是由代码生成器自动生成的, 请不要随意修改, 你的修改将在代码重新生成时会被覆盖，
    /// 如果要对该类进行修改, 请直接修改该分部类的Sys_BusiUnit文件(文件名不含.designer)
    /// </remarks>
    public partial class Sys_BusiUnit : BusinessEntity
    {  
       private int _keyId;
       private int _busiUnitTypeID;
       private string _regionCode;
       private string _busiUnitCode;
       private string _busiUnitName;
       private string _shortName;
       private string _helperCode;
       private int? _orderId;
       private string _address;
       private int? _parentId;
       private string _webSiteUrl;
       private string _ftpSiteUrl;
       private string _telephone1;
       private string _telephone2;
       private string _fax;
       private string _e_Mail;
       private decimal? _x;
       private decimal? _y;
       private int _isSaleCount;
       private bool _status;
       private string _remark;
       private int _createdBy;
       private DateTime _createdOn;
       private int? _modifiedBy;
       private DateTime? _modifiedOn;
      
      #region properties

      
      /// <summary>
      ///  获取或设置一个值, 该值代表业务单位内码
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
      ///  获取或设置一个值, 该值代表业务单位类型内码
      /// </summary>
      public int BusiUnitTypeID
      {
        get
        {
         return this._busiUnitTypeID; 
        }
        set
        {
          this._busiUnitTypeID = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表行政区域编码
      /// </summary>
      public string RegionCode
      {
        get
        {
         return string.IsNullOrEmpty(this._regionCode) ? string.Empty : this._regionCode.Trim(); 
        }
        set
        {
          this._regionCode = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表业务单位编码
      /// </summary>
      public string BusiUnitCode
      {
        get
        {
         return string.IsNullOrEmpty(this._busiUnitCode) ? string.Empty : this._busiUnitCode.Trim(); 
        }
        set
        {
          this._busiUnitCode = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表业务单位名称
      /// </summary>
      public string BusiUnitName
      {
        get
        {
         return string.IsNullOrEmpty(this._busiUnitName) ? string.Empty : this._busiUnitName.Trim(); 
        }
        set
        {
          this._busiUnitName = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表业务单位简称
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
      ///  获取或设置一个值, 该值代表业务单位助记码
      /// </summary>
      public string HelperCode
      {
        get
        {
         return string.IsNullOrEmpty(this._helperCode) ? string.Empty : this._helperCode.Trim(); 
        }
        set
        {
          this._helperCode = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表OrderId
      /// </summary>
      public int? OrderId
      {
        get
        {
         return this._orderId; 
        }
        set
        {
          this._orderId = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表Address
      /// </summary>
      public string Address
      {
        get
        {
         return string.IsNullOrEmpty(this._address) ? string.Empty : this._address.Trim(); 
        }
        set
        {
          this._address = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表上级业务单位内码
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
      ///  获取或设置一个值, 该值代表网站地址
      /// </summary>
      public string WebSiteUrl
      {
        get
        {
         return string.IsNullOrEmpty(this._webSiteUrl) ? string.Empty : this._webSiteUrl.Trim(); 
        }
        set
        {
          this._webSiteUrl = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表FTP服务器
      /// </summary>
      public string FtpSiteUrl
      {
        get
        {
         return string.IsNullOrEmpty(this._ftpSiteUrl) ? string.Empty : this._ftpSiteUrl.Trim(); 
        }
        set
        {
          this._ftpSiteUrl = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表联系电话1
      /// </summary>
      public string Telephone1
      {
        get
        {
         return string.IsNullOrEmpty(this._telephone1) ? string.Empty : this._telephone1.Trim(); 
        }
        set
        {
          this._telephone1 = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表联系电话2
      /// </summary>
      public string Telephone2
      {
        get
        {
         return string.IsNullOrEmpty(this._telephone2) ? string.Empty : this._telephone2.Trim(); 
        }
        set
        {
          this._telephone2 = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表传真
      /// </summary>
      public string Fax
      {
        get
        {
         return string.IsNullOrEmpty(this._fax) ? string.Empty : this._fax.Trim(); 
        }
        set
        {
          this._fax = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表电子邮件
      /// </summary>
      public string E_Mail
      {
        get
        {
         return string.IsNullOrEmpty(this._e_Mail) ? string.Empty : this._e_Mail.Trim(); 
        }
        set
        {
          this._e_Mail = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表X
      /// </summary>
      public decimal? X
      {
        get
        {
         return this._x; 
        }
        set
        {
          this._x = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表Y
      /// </summary>
      public decimal? Y
      {
        get
        {
         return this._y; 
        }
        set
        {
          this._y = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表IsSaleCount
      /// </summary>
      public int IsSaleCount
      {
        get
        {
         return this._isSaleCount; 
        }
        set
        {
          this._isSaleCount = value;
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