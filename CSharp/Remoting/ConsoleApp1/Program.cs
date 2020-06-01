using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 注册信道
            var serverChannel = new IpcChannel("ConsoleApp1");
            ChannelServices.RegisterChannel(serverChannel, false);
            var service = new CurrentService();
            RemotingServices.Marshal(service, "Main");
        }
    }
}
