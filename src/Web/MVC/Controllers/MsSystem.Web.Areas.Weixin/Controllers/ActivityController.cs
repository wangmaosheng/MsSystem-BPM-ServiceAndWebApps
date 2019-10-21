using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace MsSystem.Web.Areas.Weixin.Controllers
{
    /// <summary>
    /// 活动
    /// </summary>
    [Area("Weixin")]
    [AllowAnonymous]
    public class ActivityController : Controller
    {
        /// <summary>
        /// 转盘
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Turntable(long id)
        {
            return View();
        }

    }
}
