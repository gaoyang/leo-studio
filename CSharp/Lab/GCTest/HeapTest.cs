using System;
using System.Runtime.InteropServices;

namespace Lab.GCTest
{
    public class HeapTest
    {
        public void Run()
        {
            var a = "asdasd";
            var gcHandle = GCHandle.Alloc(a);
        }


    }
}
