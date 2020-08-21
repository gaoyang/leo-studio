using System;
using System.Threading;
using System.Threading.Tasks;

namespace Lab.ThreadTest
{
    public class Main
    {
        public static void Run()
        {
            TaskTest();
            // ThreadTest();
        }

        static void TaskTest()
        {
            var cancel = new CancellationTokenSource();

            Task.Run(async () =>
            {
                try
                {
                    while (true)
                    {
                        if (cancel.IsCancellationRequested)
                            Thread.CurrentThread.Abort();
                        Console.WriteLine("正在执行");
                        await Task.Delay(2000);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("拦截到异常");
                    Console.WriteLine(e);
                }

                Console.WriteLine("最后再输出一句话");
            });

            Thread.Sleep(5000);

            cancel.Cancel();
            Console.WriteLine("终止线程");
        }

        static void ThreadTest()
        {
            var thread = new Thread(async () =>
            {
                try
                {
                    try
                    {
                        while (true)
                        {
                            Console.WriteLine("正在执行");
                            await Task.Delay(2000);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("拦截到异常");
                        Console.WriteLine(e);
                    }

                    Console.WriteLine("中间的一段换");
                }
                catch (Exception e)
                {
                    Console.WriteLine("外层又拦截到异常");
                    Console.WriteLine(e);
                }

                Console.WriteLine("最后再输出一句话");
            });
            thread.Start();

            Thread.Sleep(5000);
            thread.Abort();
            Console.WriteLine("终止线程");
        }
    }
}