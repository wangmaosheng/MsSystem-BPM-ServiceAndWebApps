using JadeFramework.Core.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MsSystem.Web.Areas.WF.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MsSystem.Web.Areas.WF.Controllers
{
    [Area("WF")]
    [Authorize]
    public class ConfigController : Controller
    {
        private readonly IConfigService configService;

        public ConfigController(IConfigService configService)
        {
            this.configService = configService;
        }

        [HttpPost]
        [ActionName("GetRoleTreesAsync")]
        public async Task<List<ZTree>> GetRoleTreesAsync(List<long> ids)
        {
            return await configService.GetRoleTreesAsync(ids);
        }

        [HttpPost]
        [ActionName("GetUserTreeAsync")]
        public async Task<List<ZTree>> GetUserTreeAsync(List<long> ids)
        {
            return await configService.GetUserTreeAsync(ids);
        }
    }
}
