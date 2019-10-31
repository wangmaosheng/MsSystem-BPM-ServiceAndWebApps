using JadeFramework.Zipkin;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using zipkin4net;
using zipkin4net.Middleware;
using zipkin4net.Tracers.Zipkin;
using zipkin4net.Transport.Http;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ApplicationBuilderExtension
    {
        /// <summary>
        /// 注册分布式追踪埋点 zipkin
        /// </summary>
        /// <param name="lifetime"></param>
        /// <param name="loggerFactory"></param>
        public static IApplicationBuilder UseZipkin(this IApplicationBuilder app)
        {
            var lifetime = app.ApplicationServices.GetService<IApplicationLifetime>();
            var zipkinOptions = app.ApplicationServices.GetRequiredService<IOptions<ZipkinOptions>>().Value;
            lifetime.ApplicationStarted.Register(() => {
                var loggerFactory = app.ApplicationServices.GetService<ILoggerFactory>();

                TraceManager.SamplingRate = 1.0f;
                var logger = new TracingLogger(loggerFactory, zipkinOptions.LoggerName);
                var httpSender = new HttpZipkinSender(zipkinOptions.ZipkinCollectorUrl, zipkinOptions.ContentType);

                var tracer = new ZipkinTracer(httpSender, new JSONSpanSerializer());
                TraceManager.RegisterTracer(tracer);
                TraceManager.Start(logger);
            });

            lifetime.ApplicationStopped.Register(() => {
                TraceManager.Stop();
            });

            app.UseTracing(zipkinOptions.ApplicationName);
            return app;
        }
    }
}
