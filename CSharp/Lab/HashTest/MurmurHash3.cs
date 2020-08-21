using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Lab.HashTest
{
    public static class MurMurHash3
    {
        //Change to suit your needs
        const uint seed = 0xe17a1465;

        public static int Hash(string data)
        {
            return Hash(Encoding.UTF8.GetBytes(data));
        }

        public static int Hash(Stream stream)
        {
            var data = new BinaryReader(stream).ReadBytes((int)stream.Length);
            stream.Seek(0, SeekOrigin.Begin);
            Console.WriteLine(string.Join(" ", data.Select(o => o.ToString("X"))));


            const uint c1 = 0xcc9e2d51;
            const uint c2 = 0x1b873593;

            var h1 = seed;
            uint k1 = 0;
            uint streamLength = 0;

            using (var reader = new BinaryReader(stream))
            {
                var chunk = reader.ReadBytes(4);
                while (chunk.Length > 0)
                {
                    streamLength += (uint)chunk.Length;
                    switch (chunk.Length)
                    {
                        case 4:
                            /* Get four bytes from the input into an uint */
                            k1 = (uint)
                                (chunk[0]
                                 | (chunk[1] << 8)
                                 | (chunk[2] << 16)
                                 | (chunk[3] << 24));

                            /* bitmagic hash */
                            k1 *= c1;
                            k1 = rotl32(k1, 15);
                            k1 *= c2;

                            h1 ^= k1;
                            h1 = rotl32(h1, 13);
                            h1 = h1 * 5 + 0xe6546b64;
                            break;
                        case 3:
                            k1 = (uint)
                                (chunk[0]
                                 | (chunk[1] << 8)
                                 | (chunk[2] << 16));
                            k1 *= c1;
                            k1 = rotl32(k1, 15);
                            k1 *= c2;
                            h1 ^= k1;
                            break;
                        case 2:
                            k1 = (uint)
                                (chunk[0]
                                 | (chunk[1] << 8));
                            k1 *= c1;
                            k1 = rotl32(k1, 15);
                            k1 *= c2;
                            h1 ^= k1;
                            break;
                        case 1:
                            k1 = chunk[0];
                            k1 *= c1;
                            k1 = rotl32(k1, 15);
                            k1 *= c2;
                            h1 ^= k1;
                            break;
                    }

                    chunk = reader.ReadBytes(4);
                }
            }

            // finalization, magic chants to wrap it all up
            h1 ^= streamLength;
            h1 = fmix(h1);

            unchecked //ignore overflow
            {
                return (int)h1;
            }
        }

        public static int Hash(byte[] data)
        {
            var length = data.Length;
            if (length == 0)
                return 0;

            const uint c1 = 0xcc9e2d51;
            const uint c2 = 0x1b873593;
            var h1 = seed;
            uint k1 = 0;

            var currentIndex = 0;
            while (length >= 4)
            {
                /* Get four bytes from the input into an uint */
                k1 = (uint)
                    (data[currentIndex++]
                     | (data[currentIndex++] << 8)
                     | (data[currentIndex++] << 16)
                     | (data[currentIndex++] << 24));

                /* bitmagic hash */
                k1 *= c1;
                k1 = rotl32(k1, 15);
                k1 *= c2;

                h1 ^= k1;
                h1 = rotl32(h1, 13);
                h1 = h1 * 5 + 0xe6546b64;
                length -= 4;
            }


            switch (length)
            {
                case 3:
                    k1 = (uint)
                        (data[currentIndex++]
                         | (data[currentIndex++] << 8)
                         | (data[currentIndex] << 16));
                    k1 *= c1;
                    k1 = rotl32(k1, 15);
                    k1 *= c2;
                    h1 ^= k1;
                    break;
                case 2:
                    k1 = (uint)
                        (data[currentIndex++]
                         | (data[currentIndex] << 8));
                    k1 *= c1;
                    k1 = rotl32(k1, 15);
                    k1 *= c2;
                    h1 ^= k1;
                    break;
                case 1:
                    k1 = data[currentIndex];
                    k1 *= c1;
                    k1 = rotl32(k1, 15);
                    k1 *= c2;
                    h1 ^= k1;
                    break;
            }

            // finalization, magic chants to wrap it all up
            h1 ^= (uint)data.Length;
            h1 = fmix(h1);

            unchecked //ignore overflow
            {
                return (int)h1;
            }
        }

        static uint rotl32(uint x, byte r)
        {
            return (x << r) | (x >> (32 - r));
        }

        static uint fmix(uint h)
        {
            h ^= h >> 16;
            h *= 0x85ebca6b;
            h ^= h >> 13;
            h *= 0xc2b2ae35;
            h ^= h >> 16;
            return h;
        }
    }
}