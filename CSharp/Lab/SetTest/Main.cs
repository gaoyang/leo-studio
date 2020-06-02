using System;
using System.Collections.Generic;
using System.Text;
using Lab._Models;

namespace Lab.SetTest
{
    public class Main
    {
        public static void Run()
        {
            var  hashSet = new HashSet<People>();
            var p1 = new People {Name = "AAA", Age = 11};
            var p2 = new People {Name = "BBB", Age = 22 };
            Console.WriteLine(hashSet.Add(p1));
            Console.WriteLine(hashSet.Add(p2));
            Console.WriteLine(hashSet.Add(p2));

            Console.WriteLine("End");
        }
    }
}
