using System.Collections.Generic;

namespace Lab.GrammarTest
{
    public class Main
    {
        public static void Run()
        {
            var aaa = new Test { "aaa", "aaa", "aaa", "aaa" };
            aaa.AAA = "a";


        }
    }

    public class Test : List<string>
    {
        public string AAA { get; set; }
    }
}
