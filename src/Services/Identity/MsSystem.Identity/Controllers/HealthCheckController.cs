using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Threading.Tasks;

namespace MsSystem.Identity.Controllers
{
    /// <summary>
    /// 心跳检查
    /// </summary>
    [Produces("application/json")]
    [Route("api/HealthCheck/[action]")]
    public class HealthCheckController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Ping()
        {
            //await Task.Factory.StartNew(() =>
            //{
            //    Logger nlog = LogManager.GetCurrentClassLogger();
            //    LogEventInfo logEvent = new LogEventInfo(LogLevel.Info, "consul", "心跳检测");
            //    nlog.Log(logEvent);
            //});
            return Ok();
        }
    }
}