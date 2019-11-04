using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace MsSystem.Gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureAppConfiguration((hostingContext, builder) =>
                {
                    builder.SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                    .AddJsonFile("configuration.json", false, true);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    .UseUrls("http://*:5000")
                    .UseKestrel();
                });

        //public static void Main(string[] args)
        //{
        //    BuildWebHost(args).Run();
        //}
        //public static IWebHost BuildWebHost(string[] args)
        //{
        //    return Microsoft.AspNetCore.WebHost.CreateDefaultBuilder(args)
        //            .ConfigureAppConfiguration((hostingContext, builder) =>
        //            {
        //                builder.SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
        //                .AddJsonFile("configuration.json", false, true);
        //            })
        //            .UseStartup<Startup>()
        //            .UseUrls("http://*:5000")
        //            .UseKestrel()
        //            .Build();

        //}
    }
}
