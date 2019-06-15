using JadeFramework.Core.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MsSystem.Utility.Filters;
using MsSystem.Web.Areas.Weixin.Service;
using System.Threading.Tasks;

namespace MsSystem.Web.Areas.Weixin.Controllers
{
    [Area("Weixin")]
    [Authorize]
    public class AccountController: BaseController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            this._accountService = accountService;
        }

        [HttpGet]
        [Permission]
        public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 10)
        {
            var res = await _accountService.GetPageAsync(pageIndex, pageSize);
            return View(res);
        }
    }
}
