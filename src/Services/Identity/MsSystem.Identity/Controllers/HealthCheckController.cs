using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Ping()
        {
            return Ok();
        }
    }
}