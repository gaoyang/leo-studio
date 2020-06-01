using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace Lab.WindowTest.Helper
{
    public static class WindowHelper
    {
        //[DllImport("user32.dll", EntryPoint = "WindowFromPoint")]
        //public static extern int WindowFromPoint(int xPoint, int yPoint);

        [DllImport("user32.dll", EntryPoint = "WindowFromPoint")]
        public static extern int WindowFromPoint(Point point);

        [DllImport("user32.dll", EntryPoint = "GetWindowThreadProcessId")]
        public static extern uint GetWindowThreadProcessId(int hWnd, ref uint lpdwProcessId);

        [DllImport("user32.dll", EntryPoint = "GetClassName")]
        public static extern int GetClassName(int hWnd, StringBuilder lpString, int nMaxCont);
    }
}
