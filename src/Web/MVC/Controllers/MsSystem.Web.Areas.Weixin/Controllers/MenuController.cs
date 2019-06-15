using JadeFramework.Core.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MsSystem.Web.Areas.Weixin.Service;
using System.Threading.Tasks;

namespace MsSystem.Web.Areas.Weixin.Controllers
{
    [Area("Weixin")]
    [Authorize]
    public class MenuController : BaseController
    {
        private readonly IWxMenuService menuService;

        public MenuController(IWxMenuService menuService)
        {
            this.menuService = menuService;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }

    }
}
