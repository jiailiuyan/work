namespace Project.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class TreeNode
    {
        private IList<TreeNode> _children = new List<TreeNode>();

        public static IList<TreeNode> ToTree(IList<TreeNode> list)
        {
            foreach (TreeNode node in list)
            {
                node.leaf = true;
            }
            Dictionary<string, TreeNode> dictionary = list.ToDictionary<TreeNode, string>(b => b.id);
            foreach (TreeNode node2 in list)
            {
                if (!(string.IsNullOrEmpty(node2.parentId) || !dictionary.ContainsKey(node2.parentId)))
                {
                    TreeNode node3 = dictionary[node2.parentId];
                    node3.leaf = false;
                    node3.children.Add(node2);
                }
            }
            IList<TreeNode> list2 = new List<TreeNode>();
            foreach (TreeNode node2 in list)
            {
                if (string.IsNullOrEmpty(node2.parentId) || (node2.parentId == "0"))
                {
                    list2.Add(node2);
                }
            }
            return list2;
        }

        public IList<TreeNode> children
        {
            get
            {
                return this._children;
            }
        }

        public virtual string id { get; set; }

        public virtual bool leaf { get; set; }

        public virtual string parentId { get; set; }

        public virtual string tag { get; set; }

        public virtual string text { get; set; }
    }
}

