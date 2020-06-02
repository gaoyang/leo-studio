using System;
using System.Collections.Generic;
using System.Text;

namespace Lab.StringTest
{
    public class Main
    {
        public static void Run()
        {
            Console.Write(Convert.ToChar(0x1E));


            Console.WriteLine();



            var aaa = "ABCDEF";
            var bbb = string.Concat("ABC", "DEF");

            Console.WriteLine(aaa.GetHashCode());
            Console.WriteLine(bbb.GetHashCode());

            var ccc = new StringBuilder("ABC");
            ccc.Append("DEF");
            Console.WriteLine(ccc.ToString().GetHashCode());
        }
    }
}
