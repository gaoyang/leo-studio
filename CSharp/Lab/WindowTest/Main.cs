using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Threading;
using Lab.WindowTest.Helper;

namespace Lab.WindowTest
{
    public class Main
    {
        public static void Run()
        {
            Point p = new Point(-500, 500);
            var hwnd = WindowHelper.WindowFromPoint(p);
            Console.WriteLine(hwnd);
            var className = new StringBuilder(256);
            WindowHelper.GetClassName(hwnd, className, 256);
            Console.WriteLine(className);
        }
    }
}
