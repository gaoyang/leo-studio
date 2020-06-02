using System.Collections.Generic;
using System.Threading;

namespace Lab.TreeTest
{
    public class TreeNode
    {
        public TreeNode()
        {
            Id = Interlocked.Increment(ref _idFlag);
        }

        private static int _idFlag;
        public int Id { get; }
        public string Name { get; set; }
        public string Header { get; set; }
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
