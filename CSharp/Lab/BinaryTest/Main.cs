using System;
using System.IO;
using System.Linq;

namespace Lab.BinaryTest
{
    public class Main
    {
        public static void Run()
        {
            var intArray = new[] { 111, 333, 6, 644, 5, Int32.MaxValue, Int32.MinValue };

            {
                Console.WriteLine("写入文件");
                var file = File.Open("test.bin", File.Exists("test.bin") ? FileMode.Append : FileMode.Create);
                using (var writer = new BinaryWriter(file))
                {
                    intArray.Select(BitConverter.GetBytes).ToList().ForEach(writer.Write);
                }
            }

            Console.WriteLine("读取文件");

            {
                var file = File.Open("test.bin", FileMode.Open);
                using (var reader = new BinaryReader(file))
                {
                    byte[] bytes = null;
                    do
                    {
                        bytes = reader.ReadBytes(4);
                        Console.WriteLine(BitConverter.ToInt32(bytes));
                    }
                    while (file.Position != file.Length);
                }
            }

            Console.WriteLine("End");
        }
    }
}
