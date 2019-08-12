using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace MsSystem.Identity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Run();
        }

        public static IWebHost CreateWebHostBuilder(string[] args)
        {
            //var configuration = new ConfigurationBuilder()
            //    .AddJsonFile("appsettings.json", false, true)
            //    .Build();
            //var options = new JadeFramework.Core.Consul.ServiceDiscoveryOptions();
            //configuration.Bind("ServiceDiscovery", options);

            return WebHost.CreateDefaultBuilder(args)
                    .UseStartup<Startup>()
                    .UseUrls("http://*:5200")
                    .Build();
        }

    }
}
