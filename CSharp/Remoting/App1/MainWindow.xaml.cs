using System.Diagnostics;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using System.Windows;
using LightKit.Services;
using Remoting.Common;

namespace App1
{
    public partial class MainWindow : Window
    {
        CurrTestService currTestService = new CurrTestService();
        public MainWindow()
        {
            InitializeComponent();
            var service = new CurrentService();

            // 注册信道
            var serverChannel = new IpcServerChannel("App1");
            ChannelServices.RegisterChannel(serverChannel, false);
            RemotingServices.Marshal(service, "Main");

            //Remote.RunInstance<TestService>(currTestService, "AAA");
        }

        void StartApp_OnClick(object sender, RoutedEventArgs e)
        {
            currTestService.Write("LLLLL");
        }
    }
}
