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
    /// ���������򵼳�����
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    [MetadataAttribute]
    public sealed class ViewExportAttribute : ExportAttribute, IViewRegionRegistration
    {
        /// <summary>
        /// ��������
        /// </summary>
        public string RegionName { get; set; }
        /// <summary>
        /// �Ƿ���Ҫ��ʼ��
        /// Ĭ��ֵΪtrue,��֤����ʱ,��ʼ������ģ��
        /// </summary>
        public bool IsNeedInitialize { get; set; }

        /// <summary>
        /// Ĭ�Ϲ���
        /// </summary>
        public ViewExportAttribute()
            : base(typeof(object))
        {
            this.IsNeedInitialize = true;
        }

        /// <summary>
        /// ���캯��
        /// ʹ�ø������������Ƴ�ʼ��,Ĭ�Ͻ��г�ʼ��
        /// </summary>
        /// <param name="regionName"></param>
        public ViewExportAttribute(string regionName)
            : this(regionName, true)
        {
        }
        /// <summary>
        /// ָ����ͼ����
        /// </summary>
        /// <param name="regionName">��������</param>
        /// <param name="isNeedInitialize">�Ƿ���Ҫ��ʼ��</param>
        public ViewExportAttribute(string regionName, bool isNeedInitialize)
            : this()
        {
            this.RegionName = regionName;
            this.IsNeedInitialize = isNeedInitialize;
        }
    }
}
