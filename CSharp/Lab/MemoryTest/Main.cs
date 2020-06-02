using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lab.MemoryTest
{
    public class Main
    {
        public static void Run()
        {
            File.Delete("string_test.txt");

            var sw = new StreamWriter("string_test.txt");
            sw.WriteLine("这是测试字符串！！！");

            sw.Close();

            var sr = new StreamReader("string_test.txt");

            var strs = new List<string>();

            var str = sr.ReadLine();
            sr.BaseStream.Seek(0, SeekOrigin.Begin);

            var str2 = sr.ReadLine();
            sr.BaseStream.Seek(0, SeekOrigin.Begin);

            for (int i = 0; i < 10000; i++)
            {
                strs.Add(sr.ReadLine());
                sr.BaseStream.Seek(0, SeekOrigin.Begin);
            }

            sr.Close();


            var strs2 = new List<string>();

            foreach (var item in strs)
            {
                strs2.Add(item);
            }

            Console.WriteLine(object.ReferenceEquals(strs[0], strs2[1]));

            Console.WriteLine("End");
        }
    }
}
