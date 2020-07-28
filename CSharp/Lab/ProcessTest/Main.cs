using System;
using System.Diagnostics;

namespace Lab.ProcessTest
{
    public class Main
    {
        public static void Run()
        {
            long total = 0;
            foreach (var process in Process.GetProcesses())
            {
                var size = process.WorkingSet64;
                total += size;
                Console.WriteLine($"{process.ProcessName.PadRight(20)} {size / 1024 / 1024} MB");
            }

            Console.WriteLine($"共使用内存:{total / 1024 / 1024} MB");
        }
    }
}