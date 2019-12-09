using JadeFramework.Core.Domain.Container;
using JadeFramework.Core.Domain.Entities;
using JadeFramework.Core.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MsSystem.Web.Areas.OA.Service;
using MsSystem.Web.Areas.OA.ViewModel;
using MsSystem.Web.Areas.Sys.Service;
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
            _messageService = messageService;
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
    }
}
