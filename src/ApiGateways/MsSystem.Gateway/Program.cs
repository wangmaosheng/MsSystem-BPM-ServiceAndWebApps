using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace MsSystem.Gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }


        private static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, builder) =>
                {
                    builder.SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                    .AddJsonFile("configuration.json", false, true);
                })
                .UseStartup<Startup>()
                .UseUrls("http://*:5000")
                .UseKestrel()
                .Build();
    }
}
