using JadeFramework.Core.Domain.Container;
using JadeFramework.Core.Domain.Enum;
using JadeFramework.Core.Domain.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MsSystem.Utility.Filters
{

    public class PermissionAuthorizationRequirement : IAuthorizationRequirement
    {
        public UrlAndButtonType UrlAndButtonType { get; }

        public PermissionAuthorizationRequirement(string url, ButtonType buttonType, bool isPage)
        {
            UrlAndButtonType = new UrlAndButtonType()
            {
                Url = url,
                ButtonType = (byte)buttonType,
                IsPage = isPage
            };
        }
        public PermissionAuthorizationRequirement(string url, byte buttonType, bool isPage)
        {
            UrlAndButtonType = new UrlAndButtonType()
            {
                Url = url,
                ButtonType = buttonType,
                IsPage = isPage
            };
        }
    }
    /// <summary>
    /// 权限过滤器
    /// </summary>
    [Authorize]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class PermissionAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="buttonType">按钮类型</param>
        /// <param name="isPage">是否是页面</param>
        public PermissionAttribute(string url = default(string), ButtonType buttonType = ButtonType.View, bool isPage = true) :
            base(typeof(RequiresPermissionAttributeExecutor))
        {
            Arguments = new object[] { new PermissionAuthorizationRequirement(url, buttonType, isPage) };
        }
        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="buttonType">按钮类型</param>
        /// <param name="isPage">是否是页面</param>
        public PermissionAttribute(string url, byte buttonType, bool isPage = true) :
            base(typeof(RequiresPermissionAttributeExecutor))
        {
            Arguments = new object[] { new PermissionAuthorizationRequirement(url, buttonType, isPage) };
        }

        private class RequiresPermissionAttributeExecutor : Attribute, IAsyncResourceFilter
        {
            private IPermissionStorageContainer _permissionStorage;
            private PermissionAuthorizationRequirement _requiredPermissions;

            public RequiresPermissionAttributeExecutor(
                IPermissionStorageContainer permissionStorage, PermissionAuthorizationRequirement requiredPermissions)
            {
                _permissionStorage = permissionStorage;
                _requiredPermissions = requiredPermissions;
            }

            public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
            {
                string menuUrl = _requiredPermissions.UrlAndButtonType.Url;
                //判断用户权限
                if (string.IsNullOrEmpty(menuUrl))
                {
                    //区域判断
                    string area = context.RouteData.Values["area"].ToString();
                    if (string.IsNullOrEmpty(area))
                    {
                        menuUrl = "/" + context.RouteData.Values["controller"] + "/" + context.RouteData.Values["action"];
                    }
                    else
                    {
                        menuUrl = "/" + area + "/" + context.RouteData.Values["controller"] + "/" + context.RouteData.Values["action"];
                    }
                }
                menuUrl = menuUrl.Trim().ToLower();
                var dbpermission = await _permissionStorage.GetPermissionAsync();
                var menu = dbpermission.Menus.FirstOrDefault(m => m.MenuUrl != null && m.MenuUrl.Trim().ToLower() == menuUrl);
                if (menu != null)//地址存在
                {
                    if (_requiredPermissions.UrlAndButtonType.ButtonType == default(byte))
                    {
                        await next();
                    }
                    else
                    {
                        byte buttonType = (byte)_requiredPermissions.UrlAndButtonType.ButtonType;
                        if (menu.MenuButton.Select(m => m.ButtonType).Contains(buttonType))//拥有操作权限
                        {
                            await next();
                        }
                        else
                        {
                            //没有操作权限
                            if (_requiredPermissions.UrlAndButtonType.IsPage)
                            {
                                context.Result = new RedirectResult("/error/noauth");
                            }
                            else
                            {
                                context.Result = new ContentResult()
                                {
                                    Content = PermissionStatusCodes.Status2Unauthorized.ToString()
                                };
                            }
                            await context.Result.ExecuteResultAsync(context);
                        }
                    }
                }
                else
                {
                    //没有操作权限
                    if (_requiredPermissions.UrlAndButtonType.IsPage)
                    {
                        context.Result = new RedirectResult("/error/noauth");
                    }
                    else
                    {
                        context.Result = new ContentResult()
                        {
                            Content = PermissionStatusCodes.Status2Unauthorized.ToString()
                        };
                    }
                    await context.Result.ExecuteResultAsync(context);
                }
            }
        }

    }
}
