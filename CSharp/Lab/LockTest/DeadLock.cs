using System;
using System.Collections.Generic;
using System.Text;

namespace Lab.LockTest
{
    public class DeadLock
    {

        private readonly object _lockA = new object();
        private readonly object _lockB = new object();
        private readonly object _lockC = new object();



        public object A
        {
            get
            {
                lock (_lockB)
                {
                    Console.WriteLine("A 方法");

                   return B;
                }
            }
        }


        public object B
        {
            get
            {
                lock (_lockC)
                {
                    Console.WriteLine("B 方法");

                    return C;
                }
            }
        }

        public object C
        {
            get
            {
                lock (_lockA)
                {
                    Console.WriteLine("C 方法");

                    return A;
                }
            }
        }

    }
}
