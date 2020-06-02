using System;
using System.Threading;
using System.Threading.Tasks;

namespace Lab.LockTest
{
    public class Main
    {
        public static void Run()
        {
            Lock();
        }

        /// <summary>
        /// 互斥锁
        /// </summary>
        public static void Lock()
        {
            Console.WriteLine("=========== Lock ============");
            var _lock = new object();

            var t1 = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("线程1 开始");

                lock (_lock)
                {
                    Console.WriteLine("线程1 进入锁");
                    Thread.Sleep(4000);
                    Console.WriteLine("线程1 退出锁");
                }

                Console.WriteLine("线程1 结束");
            });

            var t2 = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("线程2 开始");

                Thread.Sleep(1000);
                lock (_lock)
                {
                    Console.WriteLine("线程2 进入锁");
                    Console.WriteLine("线程2 退出锁");
                }

                Console.WriteLine("线程2 结束");
            });

            Task.WaitAll(t1, t2);
        }

        /// <summary>
        /// 互斥锁 （对象）
        /// </summary>
        public static void MonitorLock()
        {
            Console.WriteLine("=========== MonitorLock ============");
            var _lock = new object();

            var t1 = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("线程1 开始");

                Monitor.Enter(_lock);
                Console.WriteLine("线程1 进入锁");
                Thread.Sleep(4000);
                Console.WriteLine("线程1 退出锁");
                Monitor.Exit(_lock);
                Console.WriteLine("线程1 结束");
            });

            var t2 = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("线程2 开始");

                Thread.Sleep(1000);
                Monitor.Enter(_lock);
                Console.WriteLine("线程2 进入锁");
                Console.WriteLine("线程2 退出锁");
                Monitor.Exit(_lock);

                Console.WriteLine("线程2 结束");
            });

            Task.WaitAll(t1, t2);
        }

        /// <summary>
        /// 互斥锁 （条件）
        /// </summary>
        public static void MutexLock()
        {
            Console.WriteLine("=========== MutexLock ============");

        }

        /// <summary>
        /// 读写锁
        /// 读读不互斥 读写互斥 写写互斥
        /// </summary>
        public static void ReaderWriterLock()
        {
            Console.WriteLine("=========== ReaderWriterLock ============");

        }
    }
}
