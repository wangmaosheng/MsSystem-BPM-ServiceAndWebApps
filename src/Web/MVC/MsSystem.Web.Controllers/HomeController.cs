using System.Collections.Generic;
using JadeFramework.Core.Domain.Container;
using Microsoft.AspNetCore.Mvc;
using MsSystem.Sys.IService;
using MsSystem.Sys.Model;
using MsSystem.Utility;
using MsSystem.Web.Filters;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MsSystem.Web.Controllers
{
    public class HomeController : Controller
    {
        private ISysResourceService _resourceService;
        private IPermissionStorageContainer _permissionStorage;
        private ISysReleaseLogService _releaseLogService;
        public HomeController(
            ISysResourceService resourceService, IPermissionStorageContainer permissionStorage, 
            ISysReleaseLogService releaseLogService)
        {
            _resourceService = resourceService;
            _permissionStorage = permissionStorage;
            _releaseLogService = releaseLogService;
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ServiceFilter(typeof(UserAuthAttribute))]
        public async Task<IActionResult> Index()
        {
            SysUser user = HttpContext.Session.User();
            ViewBag.User = user;
            //读取左侧菜单
            var resources = await _resourceService.GetLeftTreeAsync(user.UserId);
            if (resources.Any())
            {
                //读取该用户全部操作权限并缓存
                await _permissionStorage.InitAsync();
                return View(resources);
            }
            else
            {
                return Redirect("/error/nomenu");
            }
        }

        /// <summary>
        /// 默认打开页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ServiceFilter(typeof(UserAuthAttribute))]
        public IActionResult Default()
        {
            return View();
        }
        /// <summary>
        /// 获取发布日志
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ServiceFilter(typeof(UserAuthAttribute))]
        public IActionResult ReleaseLog([FromQuery]int pageIndex, [FromQuery]int pageSize)
        {
            var res = _releaseLogService.GetPage(pageIndex, pageSize);
            return Ok(res);
        }

    }
}
