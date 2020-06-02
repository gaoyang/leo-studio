using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab.ListTest
{
    public class Main
    {
        public static void Run()
        {
            var list1 = new List<string> { "aaa", "vvv", "ppp" };
            var list2 = new List<string> { "aaa", "vvv", "ppp" };
            var result = list2.Except(list1).ToArray();
            foreach (var str in result)
            {
                Console.WriteLine(str);
            }

            Console.WriteLine(result.Any());

            Console.WriteLine("==================");

            Console.WriteLine(list1.SequenceEqual(list2));
        }
    }
}
