using System;
using System.Runtime.InteropServices;

namespace Lab.GCTest
{
    public class Main
    {
        public static void Run()
        {
            new CollectTest().Run();

            //new HeapTest().Run();

            Console.WriteLine("GCTest end");
            Console.ReadLine();
        }

       
    }
}
