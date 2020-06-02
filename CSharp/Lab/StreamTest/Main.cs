using System;

namespace Lab.StreamTest
{
    public class Main
    {
        public static void Run()
        {
            Console.WriteLine("download network file ...");
            new NetworkResource("https://www.baidu.com/img/dong_a16028f60eed614e4fa191786f32f417.gif")
                .SaveToFile("test.gif");

            Console.WriteLine("End");
        }

    }
}
