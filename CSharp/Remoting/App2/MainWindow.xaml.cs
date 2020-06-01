using System;
using System.Diagnostics;
using System.Runtime.Remoting.Channels;
using System.Windows;
using LightKit.Services;
using Remoting.Common;

namespace App2
{
    public partial class MainWindow : Window
    {
        Lazy<TestService> testService = new Lazy<TestService>(() =>
        {
            return null;
            //var testService = Remote.GetInstance<TestService>("AAA");
            //testService.Writed += (sender, e) =>
            //{

            //    Debug.WriteLine("App2 Event -> ");
            //};
            //return testService;
        });
        public MainWindow()
        {
            InitializeComponent();
        }

        void CallBack_OnClick(object sender, RoutedEventArgs e)
        {
            var consoleApp1Service = (RemoteService)Activator.GetObject(typeof(RemoteService), "ipc://App1/Main");
            consoleApp1Service.Exec("Test");
            testService.Value.WriteLine("ADDDSADADASD");
        }
    }
}
