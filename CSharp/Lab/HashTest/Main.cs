using System;
using System.Collections.Generic;
using System.Data.HashFunction.MurmurHash;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Murmur;

namespace Lab.HashTest
{
    public class Main
    {
        public static void Run()
        {
            var str = "asdasdasd";

            {
                var hash = MurmurHash3Factory.Instance.Create(new MurmurHash3Config
                {
                    HashSizeInBits = 128
                }).ComputeHash(Encoding.UTF8.GetBytes(str)).Hash;
                Console.WriteLine(string.Join(" ", hash));
                Console.WriteLine(BitConverter.ToInt32(hash, 0));
            }

            {
                var hash = MurmurHash.Create128().ComputeHash(Encoding.UTF8.GetBytes(str));
                Console.WriteLine(string.Join(" ", hash));
                Console.WriteLine(BitConverter.ToInt32(hash, 0));
            }


            {
                var code = MurMurHash3.Hash(str);
                Console.WriteLine(string.Join(" ", BitConverter.GetBytes(code)));
                Console.WriteLine(code);
            }


            // 碰撞率
            var count = 5000000;
            var datas = new List<string>();
            for (var i = 0; i < count; i++)
                datas.Add(Guid.NewGuid().ToString("N"));

            {
                var sw = new Stopwatch();
                sw.Start();
                var list = new List<int>();
                // for (int i = 0; i < count; i++)
                //     list.Add(i.ToString().GetHashCode());

                foreach (var item in datas)
                    list.Add(item.GetHashCode());
                sw.Stop();
                Console.WriteLine($"GetHashCode {count - list.Distinct().Count()}/{count}\t耗时{sw.ElapsedMilliseconds}ms");
            }

            {
                var murmurHash3 = MurmurHash3Factory.Instance.Create(new MurmurHash3Config
                {
                    HashSizeInBits = 128,
                    Seed = 0xe17a1465
                });
                var sw = new Stopwatch();
                sw.Start();
                var list = new List<long>();
                // for (int i = 0; i < count; i++)
                // {
                //     var hash = murmurHash3.ComputeHash(Encoding.UTF8.GetBytes(i.ToString())).Hash;
                //     list.Add(BitConverter.ToInt32(hash, 0));
                // }
                
                foreach (var item in datas)
                {
                    var hash = murmurHash3.ComputeHash(Encoding.UTF8.GetBytes(item)).Hash;
                    list.Add(BitConverter.ToInt64(hash, 0));
                }

                sw.Stop();
                Console.WriteLine($"System.Data.HashFunction.MurmurHash {count - list.Distinct().Count()}/{count}\t耗时{sw.ElapsedMilliseconds}ms");
            }

            {
                var murmurHash3 = MurmurHash.Create128(0xe17a1465);
                var sw = new Stopwatch();
                sw.Start();
                var list = new List<long>();
                // for (int i = 0; i < count; i++)
                // {
                //     var hash = murmurHash3.ComputeHash(Encoding.UTF8.GetBytes(i.ToString()));
                //     list.Add(BitConverter.ToInt32(hash, 0));
                // }
                
                foreach (var item in datas)
                {
                    var hash = murmurHash3.ComputeHash(Encoding.UTF8.GetBytes(item));
                    list.Add(BitConverter.ToInt64(hash, 0));
                }

                sw.Stop();
                Console.WriteLine($"Murmur-net {count - list.Distinct().Count()}/{count}\t耗时{sw.ElapsedMilliseconds}ms");
            }

            {
                var sw = new Stopwatch();
                sw.Start();
                var list = new List<int>();
                foreach (var item in datas)
                    list.Add(MurMurHash3.Hash(item));
                sw.Stop();
                Console.WriteLine($"MurMurHash3 {count - list.Distinct().Count()}/{count}\t耗时{sw.ElapsedMilliseconds}ms");
            }
        }
    }
}