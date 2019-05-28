using JadeFramework.Core.Domain.Container;
using JadeFramework.Core.Domain.Entities;
using JadeFramework.Core.Domain.Permission;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MsSystem.Web.Components
{
    /// <summary>
    /// 菜单区分布视图
    /// </summary>
    public class MenuViewComponent : ViewComponent
    {
        private IPermissionStorageContainer _permissionStorage;

        public MenuViewComponent(IPermissionStorageContainer permissionStorage)
        {
            _permissionStorage = permissionStorage;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isShowPage"></param>
        /// <param name="routeName"></param>
        /// <returns></returns>
        public async Task<IViewComponentResult> InvokeAsync(bool isShowPage = false)
        {

            string area = this.RouteData.Values["area"].ToString();
            string controller = this.RouteData.Values["controller"].ToString();
            string action = this.RouteData.Values["action"].ToString();
            RouteName routeName = new RouteName()
            {
                Action = action,
                Areas = area,
                Controller = controller
            };

            string url = string.IsNullOrEmpty(routeName.Areas)
                ? "/" + routeName.Controller + "/" + routeName.Action
                : "/" + routeName.Areas + "/" + routeName.Controller + "/" + routeName.Action;
            url = url.Trim().ToLower();
            var permission = await _permissionStorage.GetPermissionAsync();
            var menu = permission.Menus.FirstOrDefault(m => m.MenuUrl != null && m.MenuUrl.Trim().ToLower() == url);
            if (menu != null && isShowPage == false)
            {
                var list = CreateBtn(routeName, menu);
                return View(list);
            }
            else
            {
                if (isShowPage == true)
                {
                    List<string> list = new List<string>()
                    {
                        "<div class=\"btn-group btn-group-sm\">",
                        "<button type=\"button\" id=\"formSave\" class=\"btn btn-primary btn-sm btn-permission btn-permission-save\"><i class=\"fa fa-save\"></i><span class=\"ml5\">保存</span></button>",
                        "<button type=\"button\" id=\"formReturn\" class=\"btn btn-default btn-sm btn-permission\"><i class=\"fa fa-mail-reply\"></i><span class=\"ml5\">返回</span></button>",
                        "</div>"
                    };
                    return View(list);
                }
                else
                {
                    return View(new List<string>());
                }
            }
        }

        /// <summary>
        /// 创建菜单HTML
        /// </summary>
        /// <param name="routeName"></param>
        /// <param name="menu"></param>
        /// <returns></returns>
        private List<string> CreateBtn(RouteName routeName, Menu menu)
        {
            var buttons = menu.MenuButton;
            string btnId = routeName.Controller + "_" + routeName.Action + "_";
            var comBtns = buttons.OrderBy(m => m.ButtonType);
            List<string> list = new List<string>();
            List<string> list2 = new List<string>();
            list2.Add("<div class=\"btn-group btn-group-sm\">");
            foreach (var item in comBtns)
            {
                if (item.ButtonType == 1)//查看
                {
                    list.Add($"<div class=\"btn-group btn-group-sm\"><button class=\"btn btn-sm btn-permission btn-permission-search {item.ButtonClass}\" type=\"button\" id=\"{btnId + item.ButtonType}\"><i class=\"{item.Icon}\"></i><span class=\"ml5\">查询</span></button></div>" +
                        $"<div class=\"btn-group btn-group-sm\"><a id=\"ms_refresh\" class=\"btn btn-sm btn-permission\" title=\"刷新\"><i class=\"fa fa-refresh\"></i></a></div>");
                }
                else
                {
                    list2.Add($"<button class=\"btn btn-sm btn-permission {item.ButtonClass}\" type=\"button\" id=\"{btnId + item.ButtonType}\"><i class=\"{item.Icon}\"></i><span class=\"ml5\">{item.ButtonName}</span></button>");
                }
            }
            list2.Add("</div>");
            return list.Union(list2).ToList();
        }
    }
}
