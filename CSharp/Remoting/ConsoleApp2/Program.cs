using System;
using Remoting.Common;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var consoleApp1Service = (RemoteService)Activator.GetObject(typeof(RemoteService), "ipc://ConosleApp1/Main");

            consoleApp1Service.Exec("Test");

            Console.ReadKey();
        }
    }
}
