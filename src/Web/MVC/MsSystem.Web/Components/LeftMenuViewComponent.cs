using JadeFramework.Core.Domain.Container;
using JadeFramework.Core.Mvc.Extensions;
using Microsoft.AspNetCore.Mvc;
using MsSystem.Web.Areas.Sys.Service;
using MsSystem.Web.Areas.Sys.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MsSystem.Web.Components
{
    /// <summary>
    /// 左侧菜单
    /// </summary>
    public class LeftMenuViewComponent : ViewComponent
    {
        private ISysResourceService _resourceService;
        private IPermissionStorageContainer _permissionStorage;

        public LeftMenuViewComponent(ISysResourceService resourceService, IPermissionStorageContainer permissionStorage)
        {
            _resourceService = resourceService;
            _permissionStorage = permissionStorage;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = HttpContext.User.ToUserIdentity();
            ViewBag.User = user;
            var resources = await _resourceService.GetLeftTreeAsync(user.UserId);
            if (resources.Any())
            {
                //读取该用户全部操作权限并缓存
                await _permissionStorage.InitAsync();
                return View(resources);
            }
            else
            {
                return View(new List<ResourceTreeViewModel>());
            }
        }
    }
}
