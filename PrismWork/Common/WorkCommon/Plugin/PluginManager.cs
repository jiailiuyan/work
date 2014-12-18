using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace WorkCommon.Plugin
{
    public class PluginManager
    {

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

        public void LoadPlugin()
        {
            var path = Path.Combine((new FileInfo(this.GetType().Assembly.Location)).Directory.FullName, "Plugins");
            var plugindirecroty = new DirectoryInfo(path);
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
                                var type = Assembly.LoadFile(path.FullName);
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
                                                objectList.Add(po);
                                            }
                                        }
                                    }
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
