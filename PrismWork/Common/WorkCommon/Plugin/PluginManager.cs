using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using WorkCommon.Manager;

namespace WorkCommon.Plugin
{
    public class PluginManager
    {
        public const string PluginsFloder = "Plugins";

        private static PluginManager instance;
        public static PluginManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PluginManager();
                }
                return instance;
            }
        }

        static PluginManager()
        {
            AppDomain.CurrentDomain.SetShadowCopyFiles();
        }

        private ObservableCollection<IPluginObject> pluginObjects;
        public ObservableCollection<IPluginObject> PluginObjects
        {
            get
            {
                return pluginObjects;
            }
            private set
            {
                pluginObjects = value;
            }
        }

        public string GetPlginsFloder()
        {
            return Path.Combine((new FileInfo(this.GetType().Assembly.Location)).Directory.FullName, PluginsFloder);
        }

        public void LoadPlugin()
        {
            var plugindirecroty = new DirectoryInfo(GetPlginsFloder());
            if (plugindirecroty.Exists)
            {
                var files = plugindirecroty.GetFiles().ToList();
                PluginObjects = new ObservableCollection<IPluginObject>(LoadAssembly(files));
            }
        }

        public List<IPluginObject> LoadAssembly(List<FileInfo> fileList)
        {
            List<IPluginObject> objectList = new List<IPluginObject>();
            {
                if (fileList != null && fileList.Count > 0)
                {
                    foreach (var path in fileList)
                    {
                        try
                        {
                            if ((path.FullName.EndsWith(".dll") || path.FullName.EndsWith(".exe")) &&
                                !path.FullName.Contains("Microsoft")
                                )
                            {
                                bool isloaded = false;
                                var type = Assembly.LoadFrom(path.FullName);
                                var ac = new AssemblyCatalog(type);
                                foreach (var item in ac.Parts)
                                {
                                    foreach (var ed in item.ExportDefinitions)
                                    {
                                        if (ed.ContractName.Equals(typeof(IPluginObject).FullName))
                                        {
                                            var po = item.CreatePart().GetExportedValue(ed) as IPluginObject;
                                            if (po != null)
                                            {
                                                var ipo = po.Plugin as IPluginObject;
                                                if (ipo != null)
                                                {
                                                    objectList.Add(ipo);
                                                    isloaded = true;
                                                }
                                            }
                                        }
                                    }
                                }
                                if (isloaded)
                                {
                                    InitPlugins(type);
                                }
                            }
                        }
                        catch
                        {

                        }
                    }
                }
                return objectList;
            }
        }

        public void InitPlugins(Assembly assembly)
        {
            GlobalManager.Instance.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(assembly));
        }

        public static IEnumerable<Type> GetType(Type interfaceType)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    foreach (var t in type.GetInterfaces())
                    {
                        if (t == interfaceType)
                        {
                            yield return type;
                            break;
                        }
                    }
                }
            }
        }

    }
}
