using JadeFramework.Weixin.MiniProgram;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MsSystem.Weixin.API.Filters
{
    /// <summary>
    /// 全局异常过滤
    /// </summary>
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _env;
        private ILogger<HttpGlobalExceptionFilter> _logger;

        public HttpGlobalExceptionFilter(IWebHostEnvironment env, ILogger<HttpGlobalExceptionFilter> logger)
        {
            _env = env;
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

                var json = new ErrorResponse("未知错误,请重试");

                if (_env.IsDevelopment()) json.DeveloperMessage = context.Exception;
                context.Result = new ApplicationErrorResult(json);
                context.HttpContext.Response.StatusCode = (int)MiniProgramResultCode.error;
                context.ExceptionHandled = true;//Tag it is handled.
            }
        }
    }
    public class ApplicationErrorResult : ObjectResult
    {
        public ApplicationErrorResult(object value) : base(value)
        {
            StatusCode = (int)MiniProgramResultCode.error;
        }
    }

    public class ErrorResponse
    {
        public ErrorResponse(string msg)
        {
            Message = msg;
        }
        public string Message { get; set; }
        public object DeveloperMessage { get; set; }
    }
}
