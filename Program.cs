using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspNetCoreSingletonControllerHttpContext
{
    class Program
    {
        static void Main(string[] args)
        {
            int port = 8071;
            if (args.Length == 1)
            {
                port = Int32.Parse(args[0]);
            }

            Startup startup = new Startup();
            WebHost webHost = new WebHost(IPAddress.Any, port, startup);
            webHost.Start();

            Parallel.For(0, 500, delegate (int index)
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = client.GetAsync("http://localhost:8071/Ping/Echo").GetAwaiter().GetResult();
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception($"status {response.StatusCode} was returned");
                }
            });
        }
    }
}
