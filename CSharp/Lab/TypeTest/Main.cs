using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Lab.TypeTest
{
    public class Main
    {
        public static void Run()
        {
            var list = new List<string>();
            var iList = list as IList;
            Console.WriteLine(iList.Count);

            Console.WriteLine("End");
        }
    }
}
