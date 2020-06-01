using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using ICSharpCode.SharpZipLib.Tar;

namespace Lab.ZipTest.Core
{
    public class FileSystem
    {
        private FileSystem()
        {
            map = new Hashtable();
            Childs = new List<FileNode>();
        }

        private readonly Hashtable map;

        public int Count { get => map.Count; }

        public List<FileNode> Childs { get; }

        public static FileSystem Build(string path)
        {
            var fileSystem = new FileSystem();
            var fileStream = new FileStream(path, FileMode.Open);
            var tarInputStream = new TarInputStream(fileStream);
            TarEntry tarEntry;
            while ((tarEntry = tarInputStream.GetNextEntry()) != null)
            {
                if (fileSystem.map[tarEntry.Name.GetHashCode()] != null) continue;
                var fileNodeType = tarEntry.IsDirectory ? FileNodeType.Directory : FileNodeType.File;
                var dir = tarEntry.Name.TrimEnd('/');
                var index = dir.LastIndexOf('/');
                var name = dir.Substring(index + 1);
                fileSystem.map.Add(tarEntry.Name.GetHashCode(), new FileNode(name, fileNodeType)
                {
                    ParentHashCode = dir.GetHashCode()
                });
            }
            return fileSystem;
        }
    }
}
