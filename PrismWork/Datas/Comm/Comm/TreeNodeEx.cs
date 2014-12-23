namespace Project.Common
{
    using System;
    using System.Runtime.CompilerServices;

    public class TreeNodeEx : TreeNode
    {
        public string cls { get; set; }

        public int? depth { get; set; }

        public bool expanded { get; set; }

        public string iconCls { get; set; }

        public string qtip { get; set; }

        public string qtitle { get; set; }

        public bool root { get; set; }
    }
}

