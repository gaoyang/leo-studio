using System.Collections.Generic;
using System.Threading;

namespace Lab.TreeTest
{
    public class TreeNode
    {
        public TreeNode()
        {
        }

        public TreeNode(string header)
        {
            Header = header;
        }

        public TreeNode(string header, object icon)
        {
            Header = header;
            Icon = icon;
        }

        public TreeNode SetIcon(object icon)
        {
            Icon = icon;
            return this;
        }

        public string Name { get; set; }
        public string Header { get; set; }
        public object Icon { get; private set; }
        public object ViewService { get; set; }
        public object Command { get; set; }
        public TreeNode Parent { get; private set; }
        public List<TreeNode> Items { get; private set; }

        public void AddItem(TreeNode item)
        {
            if (Items == null) Items = new List<TreeNode>();
            item.Parent = this;
            Items.Add(item);
        }
    }
}
