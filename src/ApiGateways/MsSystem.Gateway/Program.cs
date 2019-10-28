using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace MsSystem.Gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }
        public static IWebHost BuildWebHost(string[] args)
        {
            return Microsoft.AspNetCore.WebHost.CreateDefaultBuilder(args)
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
}
