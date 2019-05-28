using JadeFramework.Core.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MsSystem.WF.IService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MsSystem.WF.API.Controllers
{
    [Authorize]
    [Route("api/Config/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        //private readonly IConfigService configService;

        //public ConfigController(IConfigService configService)
        //{
        //    this.configService = configService;
        //}

        //[HttpPost]
        //public async Task<List<ZTree>> GetRoleTreesAsync([FromBody]List<long> ids)
        //{
        //    return await configService.GetRoleTreesAsync(ids);
        //}

        //[HttpPost]
        //public async Task<List<ZTree>> GetUserTreeAsync([FromBody]List<long> ids)
        //{
        //    return await configService.GetUserTreeAsync(ids);
        //}
    }
}
