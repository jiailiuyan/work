using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Entities 
{    
    /// <summary>
    /// 人员基本信息数据实体类
    /// 生成日期: 2014年10月 27日 23:20
    ///</summary>
    /// <remarks>
    /// 该文件是由代码生成器自动生成的, 请不要随意修改, 你的修改将在代码重新生成时会被覆盖，
    /// 如果要对该类进行修改, 请直接修改该分部类的Sys_Person文件(文件名不含.designer)
    /// </remarks>
    public partial class Sys_Person : BusinessEntity
    {  
       private int _keyId;
       private string _personCode;
       private string _workNo;
       private string _personName;
       private string _domainAcc;
       private string _passWord;
       private int _sexId;
       private DateTime? _birthday;
       private string _iDCard;
       private byte[] _photo;
       private string _nation;
       private string _nativePlace;
       private int? _politicalId;
       private DateTime? _joinDate;
       private int? _educationId;
       private string _graduateFrom;
       private string _perfessional;
       private string _ability;
       private bool? _isFulltime;
       private int? _workType;
       private DateTime? _workStartDate;
       private int? _jobLevel;
       private DateTime? _jobSure;
       private int? _dutyId;
       private DateTime? _dutySure;
       private string _contact;
       private string _officephone;
       private string _homephone;
       private string _mobilephone;
       private string _homeAddress;
       private string _postCode;
       private string _eMail;
       private string _certificate;
       private bool _status;
       private int? _displayOrder;
       private string _remark;
       private string _helperCode;
       private int _createdBy;
       private DateTime _createdOn;
       private int? _modifiedBy;
       private DateTime? _modifiedOn;
      
      #region properties

      
      /// <summary>
      ///  获取或设置一个值, 该值代表员工内码
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
      ///  获取或设置一个值, 该值代表员工编码
      /// </summary>
      public string PersonCode
      {
        get
        {
         return string.IsNullOrEmpty(this._personCode) ? string.Empty : this._personCode.Trim(); 
        }
        set
        {
          this._personCode = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表工号为公司人力资源系统的员工编码
      /// </summary>
      public string WorkNo
      {
        get
        {
         return string.IsNullOrEmpty(this._workNo) ? string.Empty : this._workNo.Trim(); 
        }
        set
        {
          this._workNo = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表员工姓名
      /// </summary>
      public string PersonName
      {
        get
        {
         return string.IsNullOrEmpty(this._personName) ? string.Empty : this._personName.Trim(); 
        }
        set
        {
          this._personName = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表DomainAcc
      /// </summary>
      public string DomainAcc
      {
        get
        {
         return string.IsNullOrEmpty(this._domainAcc) ? string.Empty : this._domainAcc.Trim(); 
        }
        set
        {
          this._domainAcc = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表PassWord
      /// </summary>
      public string PassWord
      {
        get
        {
         return string.IsNullOrEmpty(this._passWord) ? string.Empty : this._passWord.Trim(); 
        }
        set
        {
          this._passWord = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表性别内码
      /// </summary>
      public int SexId
      {
        get
        {
         return this._sexId; 
        }
        set
        {
          this._sexId = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表出生日期
      /// </summary>
      public DateTime? Birthday
      {
        get
        {
         return this._birthday; 
        }
        set
        {
          this._birthday = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表身份证号码
      /// </summary>
      public string IDCard
      {
        get
        {
         return string.IsNullOrEmpty(this._iDCard) ? string.Empty : this._iDCard.Trim(); 
        }
        set
        {
          this._iDCard = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表照片
      /// </summary>
      public byte[] Photo
      {
        get
        {
         return this._photo; 
        }
        set
        {
          this._photo = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表民族
      /// </summary>
      public string Nation
      {
        get
        {
         return string.IsNullOrEmpty(this._nation) ? string.Empty : this._nation.Trim(); 
        }
        set
        {
          this._nation = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表籍贯
      /// </summary>
      public string NativePlace
      {
        get
        {
         return string.IsNullOrEmpty(this._nativePlace) ? string.Empty : this._nativePlace.Trim(); 
        }
        set
        {
          this._nativePlace = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表政治面貌内码
      /// </summary>
      public int? PoliticalId
      {
        get
        {
         return this._politicalId; 
        }
        set
        {
          this._politicalId = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表入党(团)时间
      /// </summary>
      public DateTime? JoinDate
      {
        get
        {
         return this._joinDate; 
        }
        set
        {
          this._joinDate = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表最高学历内码
      /// </summary>
      public int? EducationId
      {
        get
        {
         return this._educationId; 
        }
        set
        {
          this._educationId = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表毕业院校
      /// </summary>
      public string GraduateFrom
      {
        get
        {
         return string.IsNullOrEmpty(this._graduateFrom) ? string.Empty : this._graduateFrom.Trim(); 
        }
        set
        {
          this._graduateFrom = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表所学专业
      /// </summary>
      public string Perfessional
      {
        get
        {
         return string.IsNullOrEmpty(this._perfessional) ? string.Empty : this._perfessional.Trim(); 
        }
        set
        {
          this._perfessional = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表Ability
      /// </summary>
      public string Ability
      {
        get
        {
         return string.IsNullOrEmpty(this._ability) ? string.Empty : this._ability.Trim(); 
        }
        set
        {
          this._ability = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表IsFulltime
      /// </summary>
      public bool? IsFulltime
      {
        get
        {
         return this._isFulltime; 
        }
        set
        {
          this._isFulltime = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表WorkType
      /// </summary>
      public int? WorkType
      {
        get
        {
         return this._workType; 
        }
        set
        {
          this._workType = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表参加工作时间
      /// </summary>
      public DateTime? WorkStartDate
      {
        get
        {
         return this._workStartDate; 
        }
        set
        {
          this._workStartDate = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表职务级别
      /// </summary>
      public int? JobLevel
      {
        get
        {
         return this._jobLevel; 
        }
        set
        {
          this._jobLevel = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表职务确定时间
      /// </summary>
      public DateTime? JobSure
      {
        get
        {
         return this._jobSure; 
        }
        set
        {
          this._jobSure = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表职称内码
      /// </summary>
      public int? DutyId
      {
        get
        {
         return this._dutyId; 
        }
        set
        {
          this._dutyId = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表职称确定时间
      /// </summary>
      public DateTime? DutySure
      {
        get
        {
         return this._dutySure; 
        }
        set
        {
          this._dutySure = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表联系电话
      /// </summary>
      public string Contact
      {
        get
        {
         return string.IsNullOrEmpty(this._contact) ? string.Empty : this._contact.Trim(); 
        }
        set
        {
          this._contact = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表办公电话
      /// </summary>
      public string Officephone
      {
        get
        {
         return string.IsNullOrEmpty(this._officephone) ? string.Empty : this._officephone.Trim(); 
        }
        set
        {
          this._officephone = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表家庭电话
      /// </summary>
      public string Homephone
      {
        get
        {
         return string.IsNullOrEmpty(this._homephone) ? string.Empty : this._homephone.Trim(); 
        }
        set
        {
          this._homephone = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表移动电话
      /// </summary>
      public string Mobilephone
      {
        get
        {
         return string.IsNullOrEmpty(this._mobilephone) ? string.Empty : this._mobilephone.Trim(); 
        }
        set
        {
          this._mobilephone = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表HomeAddress
      /// </summary>
      public string HomeAddress
      {
        get
        {
         return string.IsNullOrEmpty(this._homeAddress) ? string.Empty : this._homeAddress.Trim(); 
        }
        set
        {
          this._homeAddress = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表PostCode
      /// </summary>
      public string PostCode
      {
        get
        {
         return string.IsNullOrEmpty(this._postCode) ? string.Empty : this._postCode.Trim(); 
        }
        set
        {
          this._postCode = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表电子邮件
      /// </summary>
      public string EMail
      {
        get
        {
         return string.IsNullOrEmpty(this._eMail) ? string.Empty : this._eMail.Trim(); 
        }
        set
        {
          this._eMail = value;
        }
      }      
      
      /// <summary>
      ///  获取或设置一个值, 该值代表持证情况
      /// </summary>
      public string Certificate
      {
        get
        {
         return string.IsNullOrEmpty(this._certificate) ? string.Empty : this._certificate.Trim(); 
        }
        set
        {
          this._certificate = value;
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
      ///  获取或设置一个值, 该值代表人员助记码
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