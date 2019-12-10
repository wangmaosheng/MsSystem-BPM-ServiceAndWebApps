using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MsSystem.Weixin.API.Controllers
{
    /// <summary>
    /// 心跳检查
    /// </summary>
    [Produces("application/json")]
    [Route("api/HealthCheck/[action]")]
    public class HealthCheckController : Controller
    {
        private readonly ILogger<HealthCheckController> logger;

        public HealthCheckController(ILogger<HealthCheckController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult Ping()
        {
            logger.LogInformation("心跳检测");
            return Ok();
        }
    }
}
