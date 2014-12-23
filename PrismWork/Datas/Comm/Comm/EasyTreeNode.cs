using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Comm.Comm
{
    public class EasyTreeNode
    {
        public string id { get; set; }

        public string text { get; set; }

        public string parentId { get; set; }

        public string tag { get; set; }

        public IList<EasyTreeNode> children { get; set; }

        public void AddChildren(EasyTreeNode node)
        {
            if (children == null)
            {
                this.children = new List<EasyTreeNode>();
            }
            this.children.Add(node);
        }
    }
}
