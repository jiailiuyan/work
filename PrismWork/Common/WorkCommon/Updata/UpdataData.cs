using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkCommon.Updata
{
    public class UpdataData
    {
        public List<PluginData> PluginDatas { get; set; }

        public UpdataData()
        {
            PluginDatas = new List<PluginData>();
        }
    }

    public class PluginData
    {

        public string Name { get; set; }

        public string Path { get; set; }

        public string Description { get; set; }

        public string Version { get; set; }

        public string MD5 { get; set; }

        public bool IsLocal { get; set; }
    }

}
