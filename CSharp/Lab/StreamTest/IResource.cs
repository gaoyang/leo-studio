using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lab.StreamTest
{
    internal interface IResource
    {
        string AbsolutePath { get; }

        Stream Data { get; }

        bool SaveToFile(string targetPath);
    }
}
