using System;
using System.Collections.Generic;
using System.Text;

namespace Lab.ReflectionTest
{
    public class Main
    {
        public static void Run()
        {
            var type = typeof(MyClass).GetProperties()[0];

            Console.WriteLine(type.CanWrite);
        }

        public class MyClass
        {
            public string Name { get; protected set; }
        }
    }
}
