using System.Diagnostics;
using System.Windows.Controls;
using Remoting.Common;

namespace App1
{
    public class CurrentService : RemoteService
    {

        public CurrentService()
        {
        }

        public override void Exec(string str)
        {
            Debug.WriteLine(str);
        }
    }
}
