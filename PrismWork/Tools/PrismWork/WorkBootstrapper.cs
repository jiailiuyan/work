using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Windows;
using ImageView;
using Microsoft.Practices.Prism.MefExtensions;
using Microsoft.Practices.ServiceLocation;
using Modules.BottomToolBar;
using Modules.MainTool;
using WorkCommon.Behaviors;
using WorkCommon.Manager;
using WorkCommon.Manager.LayoutMgr;

namespace PrismWork
{
    public class WorkBootstrapper : MefBootstrapper
    {

        /// <summary> 用于加载起始页面、设置主窗体 </summary>
        protected override void InitializeShell()
        {
            base.InitializeShell();

            ServiceLocator.Current.GetInstance<ILayoutManager>().CombineViewPart();

            Application.Current.MainWindow = (Shell)this.Shell;
            Application.Current.MainWindow.Show();
        }

        /// <summary>
        /// 重写区域加载器,获取区域
        /// </summary>
        /// <returns></returns>
        protected override Microsoft.Practices.Prism.Regions.IRegionBehaviorFactory ConfigureDefaultRegionBehaviors()
        {
            var factory = base.ConfigureDefaultRegionBehaviors();
            factory.AddIfMissing("AutoPopulateExportedViewsBehavior", typeof(AutoPopulateExportedViewsBehavior));
            return factory;
        }

        /// <summary> 导入使用的插件 </summary>
        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();

            GlobalManager.Instance.InitManager(this.AggregateCatalog);

            //导出自身程序集
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(this.GetType().Assembly));

            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(GlobalManager).Assembly));

            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(ImageViewUC).Assembly));

            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(BottomToolBarUC).Assembly));
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(MainToolUC).Assembly));
        }

        protected override DependencyObject CreateShell()
        {
            return (DependencyObject)this.Container.GetExportedValue<IShell>();
        }

    }
}
