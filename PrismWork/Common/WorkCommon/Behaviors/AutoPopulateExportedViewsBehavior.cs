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
using Microsoft.Practices.Prism.Regions;

namespace WorkCommon.Behaviors
{
    /// <summary>
    /// ���򵼳�����
    /// </summary>
    [Export(typeof(AutoPopulateExportedViewsBehavior))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AutoPopulateExportedViewsBehavior : RegionBehavior, IPartImportsSatisfiedNotification
    {
        /// <summary>
        /// ����������ʱ
        /// </summary>
        protected override void OnAttach()
        {
            AddRegisteredViews();
        }

        /// <summary>
        /// ʵ��IPartImportsSatisfiedNotification�ӿ�
        /// </summary>
        public void OnImportsSatisfied()
        {
            AddRegisteredViews();
        }

        private void AddRegisteredViews()
        {
            if (this.Region != null)
            {
                foreach (var viewEntry in this.RegisteredViews)
                {
                    if (viewEntry.Metadata.RegionName == this.Region.Name)
                    {
                        var view = viewEntry.Value;
                        if (!this.Region.Views.Contains(view))
                        {
                            this.Region.Add(view);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// ���ڵ�����������
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "MEF injected values"), ImportMany(AllowRecomposition = true)]
        public Lazy<object, IViewRegionRegistration>[] RegisteredViews { get; set; }

        /// <summary>
        /// ��ȡָ���������ƶ�Ӧ����ͼ���
        /// </summary>
        /// <param name="regionName"></param>
        /// <returns></returns>
        public object GetRegionView(string regionName)
        {
            foreach (var item in RegisteredViews)
            {
                if (item.Metadata.RegionName.Equals(regionName))
                {
                    return item.Value;
                }
            }
            return null;
        }

    }
}
