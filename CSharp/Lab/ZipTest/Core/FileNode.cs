using System.Collections.Generic;

namespace Lab.ZipTest.Core
{
    public class FileNode
    {
        public FileNode(string name, FileNodeType type)
        {
            Name = name;
            Type = type;
            Childs = new List<FileNode>();
        }

        public int ParentHashCode { get; set; }

        public string Name { get; }
        public FileNodeType Type { get; }
        public FileNode Parent { get; internal set; }
        public List<FileNode> Childs { get; internal set; }
    }

    public enum FileNodeType
    {
        File,
        Directory
    }
}
