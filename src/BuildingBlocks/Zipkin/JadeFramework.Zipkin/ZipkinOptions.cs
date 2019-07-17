using System;

namespace JadeFramework.Zipkin
{
    public class ZipkinOptions
    {
        public ZipkinOptions()
        {
            HttpClientServiceName = Guid.NewGuid().ToString();
            LoggerName = "zipkin4net";
            ZipkinCollectorUrl = "http://localhost:9441";
            ContentType = "application/json";
        }

        public string HttpClientServiceName { get; set; }
        /// <summary>
        /// 应用程序名称
        /// </summary>
        public string ApplicationName { get; set; }
        /// <summary>
        /// 日志组件的名称,默认名称zipkin4net
        /// </summary>
        public string LoggerName { get; set; }
        /// <summary>
        /// Zipkin收集地址,默认http://localhost:9441
        /// </summary>
        public string ZipkinCollectorUrl { get; set; }

        /// <summary>
        /// 内容类型，默认是 application/json
        /// </summary>
        public string ContentType { get; set; }
    }
}
