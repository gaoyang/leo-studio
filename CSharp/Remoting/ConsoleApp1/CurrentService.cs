using System;
using Remoting.Common;

namespace ConsoleApp1
{
    public class CurrentService : RemoteService
    {
        public override void Exec(string str)
        {
            Console.WriteLine(str);
        }
    }
}
