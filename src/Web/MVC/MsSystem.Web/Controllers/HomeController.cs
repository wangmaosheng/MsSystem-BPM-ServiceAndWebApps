using JadeFramework.Core.Domain.Container;
using JadeFramework.Core.Domain.Entities;
using JadeFramework.Core.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MsSystem.Web.Areas.OA.Service;
using MsSystem.Web.Areas.OA.ViewModel;
using MsSystem.Web.Areas.Sys.Service;
using NLog;
using System.Linq;
using System.Threading.Tasks;

namespace MsSystem.Web.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private ISysResourceService _resourceService;
        private readonly IOaMessageService _messageService;
        private IPermissionStorageContainer _permissionStorage;
        public HomeController(ISysResourceService resourceServicee,
            IOaMessageService messageService,
            IPermissionStorageContainer permissionStorage)
        {
            _resourceService = resourceServicee;
            this._messageService = messageService;
            _permissionStorage = permissionStorage;
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var messagePage = await _messageService.MyListAsync(new OaMessageMyListSearch()
            {
                IsDel = 0,
                IsRead = 0,
                PageIndex = 1,
                PageSize = 10,
                UserId = UserIdentity.UserId
            });
            ViewBag.MessagePage = messagePage;
            return View();
            //ViewBag.User = this.UserIdentity;
            ////读取左侧菜单
            //var resources = await _resourceService.GetLeftTreeAsync(UserIdentity.UserId);
            //if (resources.Any())
            //{
            //    //获取消息
            //    var messagePage = await _messageService.MyListAsync(new OaMessageMyListSearch()
            //    {
            //        IsDel = 0,
            //        IsRead = 0,
            //        PageIndex = 1,
            //        PageSize = 10,
            //        UserId = UserIdentity.UserId
            //    });
            //    ViewBag.MessagePage = messagePage;
            //    //读取该用户全部操作权限并缓存
            //    await _permissionStorage.InitAsync();
            //    return View(resources);
            //}
            //else
            //{
            //    return Redirect("/error/nomenu");
            //}
        }
        /// <summary>
        /// 默认打开页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Default()
        {
            return View();
        }
        private readonly Logger nlog = LogManager.GetCurrentClassLogger(); //获得日志实;

        public IActionResult Test()
        {
            try
            {
                nlog.Info("测试数据");
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
            return Content("测试");
        }
    }
}
