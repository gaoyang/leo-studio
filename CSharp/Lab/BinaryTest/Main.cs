using System;
using System.IO;
using System.Linq;

namespace Lab.BinaryTest
{
    public class Main
    {
        public static void Run()
        {
            //SaveAndRead();

            SizeTest();

            Console.WriteLine("End");
        }

        public static void SaveAndRead()
        {
            var intArray = new[] { 111, 333, 6, 644, 5, int.MaxValue, int.MinValue };

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
        }

        /// <summary>
        /// 测试不同类型不同数量的数值 存入二进制文件的大小
        /// </summary>
        public static void SizeTest()
        {
            // Int32
            {
                Console.WriteLine("写入文件");
                var file = File.Open("size_int32.bin", File.Exists("size_int32.bin") ? FileMode.Append : FileMode.Create);
                using (var writer = new BinaryWriter(file))
                {
                    for (int i = 0; i < 1000000; i++)
                    {
                        writer.Write(i);
                    }
                }
                file.Dispose();
            }

            // UInt32
            {
                Console.WriteLine("写入文件");
                var file = File.Open("size_uint32.bin", File.Exists("size_uint32.bin") ? FileMode.Append : FileMode.Create);
                using (var writer = new BinaryWriter(file))
                {
                    for (uint i = 0; i < 1000000; i++)
                    {
                        writer.Write(i);
                    }
                }
                file.Dispose();
            }
        }
    }
}
