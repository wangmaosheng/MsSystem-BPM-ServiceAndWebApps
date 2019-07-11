using Microsoft.AspNetCore.Mvc.Filters;
using NLog;

namespace SnacksShop.Projects.API.Filters
{
    /// <summary>
    /// 全局异常过滤
    /// </summary>
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly Logger nlog = LogManager.GetCurrentClassLogger(); //获得日志实;

        public void OnException(ExceptionContext context)
        {
            if (!context.ExceptionHandled)
            {
                var excep = context.Exception;
                var controllerName = context.RouteData.Values["controller"];
                var actionName = context.RouteData.Values["action"];
                string errorMsg = $"在请求controller[{controllerName}] 的 action[{actionName}] 时产生异常[{excep.Message}]";

                nlog.Log(LogLevel.Error, context.Exception, errorMsg);
                context.ExceptionHandled = true;//Tag it is handled.
            }
        }
    }
}
