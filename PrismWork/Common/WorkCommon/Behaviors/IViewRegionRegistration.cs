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

namespace WorkCommon.Behaviors
{
    /// <summary>
    /// 标识主界面区域的接口
    /// 用来实现主界面的动态组合
    /// </summary>
    public interface IViewRegionRegistration
    {
        /// <summary>
        /// 区域名称
        /// </summary>
        string RegionName { get; }
        /// <summary>
        /// 是否需要初始化
        /// </summary>
        bool IsNeedInitialize { get; }
    }
}
