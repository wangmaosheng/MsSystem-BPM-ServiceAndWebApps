using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MsSystem.Weixin.IService;
using MsSystem.Weixin.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MsSystem.Weixin.API.Controllers
{
    [Authorize]
    [Route("api/Menu/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IWxMenuService menuService;

        public MenuController(IWxMenuService menuService)
        {
            this.menuService = menuService;
        }

        [HttpGet]
        [ActionName("GetTreesAsync")]
        public async Task<List<WxMenuDto>> GetTreesAsync()
        {
            return await menuService.GetTreesAsync();
        }
    }
}
