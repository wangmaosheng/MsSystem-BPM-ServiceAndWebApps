using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace MsSystem.Web
{
    public class Program
    {

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    .UseUrls("http://*:8000")
                    .UseKestrel();
                });
    }
}
