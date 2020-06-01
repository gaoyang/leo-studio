using System;
using System.Diagnostics;
using LightKit.Services;

namespace Remoting.Common
{
    public abstract class TestService : RemotingService
    {
        public event EventHandler Writed;
        public abstract void WriteLine(string str);

        public void Write(string str)
        {
            Debug.WriteLine("WWW");
            //Writed?.Invoke(this, EventArgs.Empty);
        }
    }
}
