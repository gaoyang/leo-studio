using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Lab.ZipTest.Core;

namespace Lab.ZipTest
{
    public class Main
    {
        public static void Run()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var fileSystem = FileSystem.Build(@"D:\案件物证\IOS\3G 3\Data_5012906926512.iso");
            sw.Stop();
            Console.WriteLine($"耗时:{sw.ElapsedMilliseconds}ms");

            Console.WriteLine($"共 {fileSystem.Count}");
        }
    }
}
