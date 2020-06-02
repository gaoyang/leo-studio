using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Lab.PerformanceTest
{
    public class Main
    {
        public static void Run()
        {
            {
                var list = new List<int>();
                var sw = new Stopwatch();
                sw.Start();
                for (var i = 0; i <= 45000; i++)
                {
                    list.Add(i);
                    list.Contains(i);
                }

                sw.Stop();
                Console.WriteLine($"List<int> 耗时{sw.ElapsedMilliseconds}ms");
            }
        }
    }
}
