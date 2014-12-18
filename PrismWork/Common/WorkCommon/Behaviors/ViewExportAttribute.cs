//===================================================================================
// Microsoft patterns & practices
// Composite Application Guidance for Windows Presentation Foundation and Silverlight
//===================================================================================
// Copyright (c) Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===================================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===================================================================================
using System;
using System.ComponentModel.Composition;

namespace WorkCommon.Behaviors
{
    /// <summary>
    /// 主界面区域导出属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    [MetadataAttribute]
    public sealed class ViewExportAttribute : ExportAttribute, IViewRegionRegistration
    {
        /// <summary>
        /// 区域名称
        /// </summary>
        public string RegionName { get; set; }
        /// <summary>
        /// 是否需要初始化
        /// 默认值为true,保证启动时,初始化给定模块
        /// </summary>
        public bool IsNeedInitialize { get; set; }

        /// <summary>
        /// 默认构造
        /// </summary>
        public ViewExportAttribute()
            : base(typeof(object))
        {
            this.IsNeedInitialize = true;
        }

        /// <summary>
        /// 构造函数
        /// 使用给定的区域名称初始化,默认进行初始化
        /// </summary>
        /// <param name="regionName"></param>
        public ViewExportAttribute(string regionName)
            : this(regionName, true)
        {
        }
        /// <summary>
        /// 指定视图导出
        /// </summary>
        /// <param name="regionName">区域名称</param>
        /// <param name="isNeedInitialize">是否需要初始化</param>
        public ViewExportAttribute(string regionName, bool isNeedInitialize)
            : this()
        {
            this.RegionName = regionName;
            this.IsNeedInitialize = isNeedInitialize;
        }
    }
}
