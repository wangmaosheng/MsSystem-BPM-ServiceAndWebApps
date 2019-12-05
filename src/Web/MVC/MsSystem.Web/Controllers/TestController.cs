using Microsoft.AspNetCore.Mvc;
using MsSystem.Web.Areas.Sys.Service;
using System;
using System.Threading.Tasks;

namespace MsSystem.Web.Controllers
{
    public class TestController:Controller
    {
        private readonly ISysLogService logService;

        public TestController(ISysLogService logService)
        {
            this.logService = logService;
        }

        public async Task<IActionResult> Index()
        {
            var res = await logService.GetChartsAsync(JadeFramework.Core.Domain.Enum.LogLevel.Error);
            return Json(res);
        }


        public IActionResult SignalR()
        {

            return View();
        }
    }
}
