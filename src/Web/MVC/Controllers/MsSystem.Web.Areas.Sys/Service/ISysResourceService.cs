using JadeFramework.Core.Domain.Entities;
using JadeFramework.Core.Domain.Permission;
using JadeFramework.Core.Extensions;
using Microsoft.Extensions.Configuration;
using MsSystem.Web.Areas.Sys.Infrastructure;
using MsSystem.Web.Areas.Sys.ViewModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MsSystem.Web.Areas.Sys.Service
{
    /// <summary>
    /// 资源服务接口
    /// </summary>
    public interface ISysResourceService
    {
        /// <summary>
        /// 根据用户获取左侧菜单列表
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        Task<List<ResourceTreeViewModel>> GetLeftTreeAsync(long userid);

        /// <summary>
        /// 异步获取用户操作权限
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        Task<UserPermission> GetUserPermissionAsync(long userid);

        /// <summary>
        /// 获取树形菜单
        /// </summary>
        /// <param name="systemId">系统ID</param>
        /// <returns></returns>
        Task<ResourceIndexViewModel> GetTreeAsync(long systemId);

        /// <summary>
        /// 获取资源信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="systemid"></param>
        /// <returns></returns>
        Task<ResourceShowViewModel> GetResourceAsync(long id, long systemid);

        /// <summary>
        /// 获取树形数据
        /// </summary>
        /// <param name="roleid">角色ID</param>
        /// <returns></returns>
        Task<List<ZTree>> GetBoxTreeAsync(long roleid);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> AddAsync(ResourceShowDto dto);

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(ResourceShowDto dto);

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(long[] ids, long userid);

        /// <summary>
        /// 角色分配资源保存
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> BoxSaveAsync(RoleTreeDto dto);
    }
    public class SysResourceService : ISysResourceService
    {
        private readonly HttpClient _apiClient;
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl;

        public SysResourceService(HttpClient httpClient, IConfiguration configuration)
        {
            _apiClient = httpClient;
            _configuration = configuration;
            _baseUrl = configuration["MsApplication:url"] + "/api/sys";
        }
        public async Task<List<ResourceTreeViewModel>> GetLeftTreeAsync(long userid)
        {
            var uri = API.SysResource.GetLeftTreeAsync(_baseUrl, userid);
            var responseString = await _apiClient.GetStringAsync(uri);
            return responseString.ToObject<List<ResourceTreeViewModel>>();
        }
        public async Task<UserPermission> GetUserPermissionAsync(long userid)
        {
            var uri = API.SysResource.GetUserPermissionAsync(_baseUrl, userid);
            var responseString = await _apiClient.GetStringAsync(uri);
            return responseString.ToObject<UserPermission>();
        }

        public async Task<ResourceIndexViewModel> GetTreeAsync(long systemId)
        {
            var uri = API.SysResource.GetTreeAsync(_baseUrl, systemId);
            var responseString = await _apiClient.GetStringAsync(uri);
            return responseString.ToObject<ResourceIndexViewModel>();
        }
        public async Task<ResourceShowViewModel> GetResourceAsync(long id, long systemid)
        {
            var uri = API.SysResource.GetResourceAsync(_baseUrl, id, systemid);
            var responseString = await _apiClient.GetStringAsync(uri);
            return responseString.ToObject<ResourceShowViewModel>();
        }
        public async Task<List<ZTree>> GetBoxTreeAsync(long roleid)
        {
            var uri = API.SysResource.GetBoxTreeAsync(_baseUrl, roleid);
            var responseString = await _apiClient.GetStringAsync(uri);
            return responseString.ToObject<List<ZTree>>();
        }

        public async Task<bool> AddAsync(ResourceShowDto dto)
        {
            var uri = API.SysResource.AddAsync(_baseUrl);
            var content = new StringContent(JsonConvert.SerializeObject(dto), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            string res = await response.Content.ReadAsStringAsync();
            return res.ToLower() == bool.TrueString.ToLower();
        }
        public async Task<bool> UpdateAsync(ResourceShowDto dto)
        {
            var uri = API.SysResource.UpdateAsync(_baseUrl);
            var content = new StringContent(JsonConvert.SerializeObject(dto), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            string res = await response.Content.ReadAsStringAsync();
            return res.ToLower() == bool.TrueString.ToLower();
        }
        public async Task<bool> DeleteAsync(long[] ids, long userid)
        {
            var uri = API.SysResource.DeleteAsync(_baseUrl);
            var content = new StringContent(JsonConvert.SerializeObject(new ResourceDeleteDTO { Ids = ids,UserId = userid }), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            string res = await response.Content.ReadAsStringAsync();
            return res.ToLower() == bool.TrueString.ToLower();
        }
        public async Task<bool> BoxSaveAsync(RoleTreeDto dto)
        {
            var uri = API.SysResource.BoxSaveAsync(_baseUrl);
            var content = new StringContent(JsonConvert.SerializeObject(dto), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            string res = await response.Content.ReadAsStringAsync();
            return res.ToLower() == bool.TrueString.ToLower();
        }

    }
}
