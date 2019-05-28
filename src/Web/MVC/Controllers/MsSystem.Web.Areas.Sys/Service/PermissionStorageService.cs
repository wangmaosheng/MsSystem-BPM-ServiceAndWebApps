using JadeFramework.Cache;
using JadeFramework.Core.Domain.Container;
using JadeFramework.Core.Domain.Entities;
using JadeFramework.Core.Domain.Permission;
using JadeFramework.Core.Mvc.Extensions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace MsSystem.Web.Areas.Sys.Service
{
    /// <summary>
    /// permission storage
    /// </summary>
    public class PermissionStorageService : IPermissionStorageContainer
    {
        private IHttpContextAccessor _httpContextAccessor;
        private ISysResourceService _resourceService;
        private ICachingProvider _cacheFactory;
        public PermissionStorageService(
            ICachingProvider cacheFactory,
            IHttpContextAccessor httpContextAccessor,
            ISysResourceService resourceService)
        {
            _cacheFactory = cacheFactory;
            _httpContextAccessor = httpContextAccessor;
            _resourceService = resourceService;
        }

        private UserIdentity UserIdentity()
        {
            return _httpContextAccessor.HttpContext.User.ToUserIdentity();
        }
        /// <summary>
        /// 权限KEY
        /// </summary>
        public string Key => "PERMISSION_" + UserIdentity().UserId;

        /// <summary>
        /// 存储数据
        /// </summary>
        /// <param name="obj"></param>
        public void Store(object obj)
        {
            string json = JsonConvert.SerializeObject(obj);
            _cacheFactory.Set(this.Key, json);
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <returns></returns>
        public object Get()
        {
            var res = _cacheFactory.Get(this.Key);
            return res;
        }

        /// <summary>
        /// 异步获取用户权限
        /// </summary>
        /// <returns></returns>
        public async Task<UserPermission> GetPermissionAsync()
        {
            long userid = UserIdentity().UserId;
            string res = this.Get() as string;
            if (string.IsNullOrEmpty(res))
            {
                var permission = await _resourceService.GetUserPermissionAsync(userid);
                this.Store(permission);
                return permission;
            }
            //异步
            return await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<UserPermission>(res));
        }



        /// <summary>
        /// 异步初始化权限
        /// </summary>
        /// <returns></returns>
        public async Task InitAsync()
        {
            long userid = UserIdentity().UserId;
            var permission = await _resourceService.GetUserPermissionAsync(userid);
            this.Store(permission);
        }
    }
}
