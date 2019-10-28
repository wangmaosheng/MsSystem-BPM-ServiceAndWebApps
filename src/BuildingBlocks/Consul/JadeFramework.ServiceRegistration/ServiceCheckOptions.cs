namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// 服务检查
    /// </summary>
    public class ServiceCheckOptions
    {
        /// <summary>
        /// 健康检查地址
        /// </summary>
        public string HealthCheckUrl { get; set; }
    }
}
