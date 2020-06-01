using System.Diagnostics;
using Remoting.Common;

namespace App1
{
    public class CurrTestService : TestService
    {
        public override void WriteLine(string str)
        {
            Debug.WriteLine($"App1 -> {str}");
        }
    }
}
