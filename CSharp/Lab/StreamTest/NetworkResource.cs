using System.IO;
using System.Net;

namespace Lab.StreamTest
{
    public class NetworkResource : IResource
    {
        public NetworkResource(string absolutePath)
        {
            AbsolutePath = absolutePath;
        }

        public string AbsolutePath { get; }
        public Stream Data { get; private set; }
        public bool SaveToFile(string targetPath)
        {
            var request = WebRequest.CreateHttp(AbsolutePath);
            var response = request.GetResponse();
            var respStream = response.GetResponseStream();
            Data = respStream;

            using (var fs = File.Open(targetPath, FileMode.Create))
            {
                if (Data != null)
                {
                    var buff = new byte[65535];
                    int length;
                    do
                    {
                        length = Data.Read(buff, 0, 65535);
                        if (length > 0)
                        {
                            fs.Write(buff, 0, length);
                        }
                    }
                    while (length > 0);
                    fs.Flush();
                }
            }

            return true;
        }
    }
}
