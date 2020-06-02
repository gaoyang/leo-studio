namespace Lab.HotKeyTest
{
    public class GlobalHotkey
    {
        private static GlobalHotkey instance;


        public static GlobalHotkey Instance
        {
            get => instance ??= new GlobalHotkey();
        }

        private GlobalHotkey()
        {
        }
    }
}
