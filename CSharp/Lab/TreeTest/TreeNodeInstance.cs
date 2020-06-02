using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab.TreeTest
{
    public class TreeNodeInstance : IEnumerable
    {
        private readonly Dictionary<string, TreeNodeInstance> _treeNodeInstance;

        protected TreeNodeInstance()
        {
            _treeNodeInstance = new Dictionary<string, TreeNodeInstance>();

        }
        public TreeNodeInstance(string key, Func<TreeNode> createNode) : this()
        {
            Key = key;
            CreateNode = createNode;
            TreeNodeType = TreeNodeType.None;
        }
        public TreeNodeInstance(TreeNodeType treeNodeType) : this()
        {
            TreeNodeType = treeNodeType;
        }

        public string Key { get; }
        public TreeNodeType TreeNodeType { get; }
        public Func<TreeNode> CreateNode { get; }

        public TreeNodeInstance this[string key]
        {
            get => _treeNodeInstance[key];
            set
            {
                if (!_treeNodeInstance.ContainsKey(key))
                    _treeNodeInstance.Add(key, value);
            }
        }

        public void Add(TreeNodeInstance treeDictionary)
        {

        }

        public void Add(string key, Func<TreeNode> createNode)
        {

        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
