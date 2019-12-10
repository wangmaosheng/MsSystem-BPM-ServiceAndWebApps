using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace MsSystem.Sys.API.Filters
{
    /// <summary>
    /// 全局异常过滤
    /// </summary>
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private ILogger<HttpGlobalExceptionFilter> _logger;
        public HttpGlobalExceptionFilter(ILogger<HttpGlobalExceptionFilter> logger)
        {
            _logger = logger;
        }
        public void OnException(ExceptionContext context)
        {
            if (!context.ExceptionHandled)
            {
                var excep = context.Exception;
                var controllerName = context.RouteData.Values["controller"];
                var actionName = context.RouteData.Values["action"];
                string errorMsg = $"在请求controller[{controllerName}] 的 action[{actionName}] 时产生异常[{excep.Message}]";

                _logger.LogError(errorMsg);
                context.ExceptionHandled = true;//Tag it is handled.
            }
        }
    }
}
