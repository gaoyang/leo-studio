using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Lab._Models;

namespace Lab.SetTest
{
    public class Main
    {
        public static void Run()
        {
            HashtableTest();

            DictionaryTest();

            Console.WriteLine("End");
        }


        private static void HashSetTest()
        {
            var hashSet = new HashSet<People>();
            var p1 = new People { Name = "AAA", Age = 11 };
            var p2 = new People { Name = "BBB", Age = 22 };
            Console.WriteLine(hashSet.Add(p1));
            Console.WriteLine(hashSet.Add(p2));
            Console.WriteLine(hashSet.Add(p2));
        }

        private static void HashtableTest()
        {
            var hashtable = new Hashtable();
            hashtable.Add("AAA","-----");
            Console.WriteLine(hashtable["AAA"]);
            Console.WriteLine(hashtable["AAAA"]);
        }

        private static void DictionaryTest()
        {
            var dictionary = new Dictionary<string,object>();
            dictionary.Add("AAA", "-----");
            Console.WriteLine(dictionary["AAA"]);
            Console.WriteLine(dictionary["AAAA"]);
        }
    }
}
