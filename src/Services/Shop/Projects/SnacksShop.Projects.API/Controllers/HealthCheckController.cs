using Microsoft.AspNetCore.Mvc;
using NLog;

namespace SnacksShop.Projects.API.Controllers
{
    /// <summary>
    /// 心跳检查
    /// </summary>
    [Produces("application/json")]
    [Route("api/HealthCheck/[action]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet]
        public IActionResult Ping()
        {
            Logger nlog = LogManager.GetCurrentClassLogger();
            LogEventInfo logEvent = new LogEventInfo(LogLevel.Info, "consul", "心跳检测");
            nlog.Log(logEvent);
            return Ok();
        }
    }
}