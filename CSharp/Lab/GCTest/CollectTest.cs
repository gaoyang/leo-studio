using System;
using System.Runtime.InteropServices;

namespace Lab.GCTest
{
    public class CollectTest
    {
        public void Run()
        {
            Test(out var ptr);

            //通过句柄获取对象
            var obj = GCHandle.FromIntPtr(ptr).Target;

            GC.Collect();
            GC.Collect();
        }

        private void Test(out IntPtr ptr)
        {
            var myClass = new MyClass("name");
            myClass.Self = myClass;
            //获取myClass的句柄
            var handle = GCHandle.Alloc(myClass, GCHandleType.Weak);
            ptr = GCHandle.ToIntPtr(handle);
            myClass = null;
        }

        private class MyClass
        {
            public MyClass(string name)
            {
                Name = name;
            }

            ~MyClass()
            {
                Console.WriteLine("MyClass 被释放了!!!");
            }

            public string Name { get; set; }
            public MyClass Self { get; set; }
        }
    }
}
