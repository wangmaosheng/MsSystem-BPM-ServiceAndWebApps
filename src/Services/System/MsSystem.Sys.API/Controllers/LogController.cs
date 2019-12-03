using JadeFramework.Core.Domain.Entities;
using JadeFramework.Core.Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MsSystem.Sys.IService;
using MsSystem.Sys.Model;
using MsSystem.Sys.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MsSystem.Sys.API.Controllers
{
    [Authorize]
    [Route("api/Log/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ISysLogService logService;

        public LogController(ISysLogService logService)
        {
            this.logService = logService;
        }

        [HttpGet]
        [ActionName("GetPageAsync")]
        public async Task<Page<SysLog>> GetPageAsync([FromQuery]LogSearchDto model)
        {
            return await logService.GetPageAsync(model);
        }

        [HttpGet]
        [ActionName("GetChartsAsync")]
        public async Task<Dictionary<object, object>> GetChartsAsync(LogLevel level)
        {
            return await logService.GetChartsAsync(level);
        }

        [HttpGet]
        [ActionName("GetLasterDataAsync")]
        public async Task<HeartBeatData> GetLasterDataAsync([FromQuery]int recentMinutes, [FromQuery]Application application)
        {
            return await logService.GetLasterDataAsync(recentMinutes, application);
        }
    }

}
