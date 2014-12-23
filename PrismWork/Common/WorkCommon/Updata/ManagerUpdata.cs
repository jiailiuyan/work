using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Jisons;
using WorkCommon.Plugin;

namespace WorkCommon.Updata
{
    public class ManagerUpdata
    {
        private static ManagerUpdata instance;
        public static ManagerUpdata Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ManagerUpdata();
                }
                return instance;
            }
        }

        public void WriteUpdataData()
        {
            UpdataData data = new UpdataData();

            data.DllDatas.Add(new DllData() { Version = "1.0.0.0", Name = "SentStream.dll", Description = "即时通讯", Path = "plugin/SentStream.dll" });

            data.WriteDataToXml<UpdataData>(@"D:/1.xml");
        }

        public void CheckUpdata()
        {

        }

        public List<IPluginObject> DownPlugins()
        {
            List<IPluginObject> ips = new List<IPluginObject>();
            var dud = DownUpdataData();
            if (dud != null)
            {
                foreach (var item in dud.DllDatas)
                {
                    var url = testUrl + item.Name;
                    var file = Jisons.HttpHelper.GetStreamResponse(url);

                    byte[] datas = new byte[file.Length];
                    file.Read(datas, 0, datas.Length);

                    var path = Path.Combine(PluginManager.Instance.GetPlginsFloder(), item.Name);
                    var fileinfo = new FileInfo(path);
                    if (!(fileinfo).Exists)
                    {
                        fileinfo.Delete();
                    }

                    var f = File.Create(path);
                    f.Write(datas, 0, datas.Length);
                    f.Close();
                    ips.AddRange(PluginManager.Instance.LoadAssembly(new List<FileInfo>() { fileinfo }));

                }
            }
            return ips;
        }

        string testUrl = "http://www.jiailiuyan.com/";

        public UpdataData DownUpdataData()
        {
            var url = testUrl + "test.xml";
            var stream = Jisons.HttpHelper.GetStreamResponse(url);
            return Jisons.XmlClassData.ReadDataFromXml<UpdataData>(stream);
        }
    }
}
