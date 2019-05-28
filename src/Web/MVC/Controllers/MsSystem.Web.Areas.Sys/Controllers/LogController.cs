using JadeFramework.Core.Domain.Enum;
using Microsoft.AspNetCore.Mvc;
using MsSystem.Web.Areas.Sys.ViewModel;
using MsSystem.Utility.Filters;
using MsSystem.Web.Areas.Sys.Service;
using System.Threading.Tasks;

namespace MsSystem.Web.Areas.Sys.Controllers
{
    /// <summary>
    /// »’÷æ
    /// </summary>
    [Area("Sys")]
    public class LogController : Controller
    {
        private ISysLogService _logService;
        public LogController(ISysLogService logService)
        {
            _logService = logService;
        }

        [HttpGet]
        [Permission]
        public async Task<IActionResult> Index(LogSearchDto model)
        {
            if (model.PageIndex==0)
            {
                model.PageIndex = 1;
            }
            if (model.PageSize==0)
            {
                model.PageSize = 10;
            }
            var res = await _logService.GetPageAsync(model);
            ViewBag.LogSearch = model;
            return View(res);
        }
    }
}