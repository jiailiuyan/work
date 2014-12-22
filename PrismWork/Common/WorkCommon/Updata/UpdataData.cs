using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkCommon.Updata
{
    public class UpdataData
    {
        public List<DllData> DllDatas { get; set; }

        public UpdataData()
        {
            DllDatas = new List<DllData>();
        }

    }

    public class DllData
    {

        public string Name { get; set; }

        public string Path { get; set; }

        public string Description { get; set; }

        public string Version { get; set; }
    }

}
