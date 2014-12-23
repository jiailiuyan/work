using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Project.Comm.Comm
{
    public class EasyTree
    {
        public DataTable source { get; set; }
        public string idField { get; set; }
        public string textField { get; set; }
        public string parentField { get; set; }
        public string tagField { get; set; }

        public EasyTree(DataTable source,string idField,string textField,string parentField,string tagField = null)
        {
            this.source = source;
            this.idField = idField;
            this.textField = textField;
            this.parentField = parentField;
            this.tagField = tagField;
        }

        public IList<EasyTreeNode> paserTree()
        {
            IList<EasyTreeNode> list = new List<EasyTreeNode>();
            if (this.source != null && this.source.Rows.Count > 0)
            {
                foreach (DataRow row in source.Rows)
                {
                    if (row[parentField] == DBNull.Value || row[parentField].ToString() == null || row[parentField].ToString().Length == 0 || row[parentField].ToString() == "0")
                    {
                        EasyTreeNode node = new EasyTreeNode()
                        {
                            id = row[idField].ToString(),
                            text = row[textField].ToString(),
                            parentId = "",
                            tag = (tagField != null && tagField.Length > 0) ? row[tagField].ToString() : ""
                        };
                        PaserNode(node);
                        list.Add(node);
                    }
                }

                
            }
            return list;
        }

        public void PaserNode(EasyTreeNode node)
        {
            List<DataRow> rows = source.AsEnumerable().Where(s => s[parentField].ToString() == node.id).ToList();
            if (rows != null && rows.Count > 0)
            {
                foreach (DataRow row in rows)
                {
                    EasyTreeNode temp = new EasyTreeNode()
                    {
                        id = row[idField].ToString(),
                        text = row[textField].ToString(),
                        parentId = row[parentField].ToString(),
                        tag = (tagField != null && tagField.Length > 0) ? row[tagField].ToString() : ""
                    };
                    PaserNode(temp);
                    node.AddChildren(temp);
                }
            }
            else
            {
                return;
            }
        }
    }
}
