using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MsSystem.Sys.API.Controllers
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

        public static Dictionary<string, byte> healthbeat = new Dictionary<string, byte>();

        [HttpGet]
        public async Task<string> GetData(string clientid)
        {
            return null;
        }
    }


}