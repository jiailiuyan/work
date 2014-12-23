using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Entities 
{    
    /// <summary>
    /// Sys_PersonDeskTopIconSet数据实体类
    /// 生成日期: 2014年11月 20日 18:47
    ///</summary>
    /// <remarks>
    /// 该文件是由代码生成器自动生成的, 请不要随意修改, 你的修改将在代码重新生成时会被覆盖，
    /// 如果要对该类进行修改, 请直接修改该分部类的Sys_PersonDeskTopIconSet文件(文件名不含.designer)
    /// </remarks>
    public partial class Sys_PersonDeskTopIconSet : BusinessEntity
    {  
       private int _keyId;
       private int _moduleId;
       private int _personId;
       private string _iconFileName;
      
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
      ///  获取或设置一个值, 该值代表ModuleId
      /// </summary>
      public int ModuleId
      {
        get
        {
         return this._moduleId; 
        }
        set
        {
          this._moduleId = value;
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
      ///  获取或设置一个值, 该值代表IconFileName
      /// </summary>
      public string IconFileName
      {
        get
        {
         return string.IsNullOrEmpty(this._iconFileName) ? string.Empty : this._iconFileName.Trim(); 
        }
        set
        {
          this._iconFileName = value;
        }
      }      
      
      
      #endregion properties
      
      
   }
}  