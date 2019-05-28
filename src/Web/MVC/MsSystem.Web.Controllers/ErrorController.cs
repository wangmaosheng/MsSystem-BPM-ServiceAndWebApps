using Microsoft.AspNetCore.Mvc;

namespace MsSystem.Web.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult IE()
        {
            return View();
        }

        /// <summary>
        /// 没有权限
        /// </summary>
        /// <returns></returns>
        public IActionResult NoAuth()
        {
            return View();
        }

        /// <summary>
        /// 没有菜单权限
        /// </summary>
        /// <returns></returns>
        public IActionResult NoMenu()
        {
            return View();
        }
    }
}