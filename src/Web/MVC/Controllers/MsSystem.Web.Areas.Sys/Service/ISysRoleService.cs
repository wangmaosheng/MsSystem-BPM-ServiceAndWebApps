using JadeFramework.Core.Domain.Entities;
using Microsoft.Extensions.Configuration;
using MsSystem.Web.Areas.Sys.Model;
using MsSystem.Web.Areas.Sys.ViewModel;
using MsSystem.Web.Areas.Sys.Infrastructure;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using JadeFramework.Core.Extensions;

namespace MsSystem.Web.Areas.Sys.Service
{
    /// <summary>
    /// 角色
    /// </summary>
    public interface ISysRoleService
    {
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        Task<RoleIndexViewModel> GetListAsync(RoleIndexSearch search);

        /// <summary>
        /// 获取角色基本信息
        /// </summary>
        /// <param name="roleid">角色ID</param>
        /// <returns></returns>
        Task<SysRole> GetAsync(long roleid);

        /// <summary>
        /// 获取该角色下的用户
        /// </summary>
        /// <param name="roleid">角色ID</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        Task<Page<SysUser>> GetRoleUserAsync(long roleid, int pageIndex, int pageSize);

        /// <summary>
        /// 获取角色树
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        Task<List<ZTree>> GetTreeAsync(long userid);

        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        Task<bool> AddAsync(SysRole role);

        /// <summary>
        /// 编辑角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(SysRole role);

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(List<long> ids, long userid);

        /// <summary>
        /// 删除角色下的用户
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> DeleteUserAsync(RoleToUserDto dto);

        /// <summary>
        /// 角色加入用户
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> AddUserAsync(RoleToUserDto dto);

    }
    public class SysRoleService: ISysRoleService
    {
        private readonly HttpClient _apiClient;
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl;

        public SysRoleService(HttpClient httpClient, IConfiguration configuration)
        {
            _apiClient = httpClient;
            _configuration = configuration;
            _baseUrl = configuration["MsApplication:url"] + "/api/sys";
        }
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public async Task<RoleIndexViewModel> GetListAsync(RoleIndexSearch search)
        {
            var uri = API.SysRole.GetListAsync(_baseUrl, search);
            var responseString = await _apiClient.GetStringAsync(uri);
            return responseString.ToObject<RoleIndexViewModel>();
        }

        /// <summary>
        /// 获取角色基本信息
        /// </summary>
        /// <param name="roleid">角色ID</param>
        /// <returns></returns>
        public async Task<SysRole> GetAsync(long roleid)
        {
            var uri = API.SysRole.GetAsync(_baseUrl, roleid);
            var responseString = await _apiClient.GetStringAsync(uri);
            return responseString.ToObject<SysRole>();
        }

        /// <summary>
        /// 获取该角色下的用户
        /// </summary>
        /// <param name="roleid">角色ID</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public async Task<Page<SysUser>> GetRoleUserAsync(long roleid, int pageIndex, int pageSize)
        {
            var uri = API.SysRole.GetRoleUserAsync(_baseUrl, roleid, pageIndex, pageSize);
            var responseString = await _apiClient.GetStringAsync(uri);
            return responseString.ToObject<Page<SysUser>>();
        }

        /// <summary>
        /// 获取角色树
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        public async Task<List<ZTree>> GetTreeAsync(long userid)
        {
            var uri = API.SysRole.GetTreeAsync(_baseUrl, userid);
            var responseString = await _apiClient.GetStringAsync(uri);
            return responseString.ToObject<List<ZTree>>();
        }

        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task<bool> AddAsync(SysRole role)
        {
            var uri = API.SysRole.AddAsync(_baseUrl);
            var content = new StringContent(JsonConvert.SerializeObject(role), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            string res = await response.Content.ReadAsStringAsync();
            return res.ToLower() == bool.TrueString.ToLower();
        }

        /// <summary>
        /// 编辑角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(SysRole role)
        {
            var uri = API.SysRole.UpdateAsync(_baseUrl);
            var content = new StringContent(JsonConvert.SerializeObject(role), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            string res = await response.Content.ReadAsStringAsync();
            return res.ToLower() == bool.TrueString.ToLower();
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(List<long> ids, long userid)
        {
            var uri = API.SysRole.DeleteAsync(_baseUrl);
            var content = new StringContent(JsonConvert.SerializeObject(new RoleDeleteDTO { Ids = ids,UserId = userid }), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            string res = await response.Content.ReadAsStringAsync();
            return res.ToLower() == bool.TrueString.ToLower();
        }

        /// <summary>
        /// 删除角色下的用户
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<bool> DeleteUserAsync(RoleToUserDto dto)
        {
            var uri = API.SysRole.DeleteUserAsync(_baseUrl);
            var content = new StringContent(JsonConvert.SerializeObject(dto), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            string res = await response.Content.ReadAsStringAsync();
            return res.ToLower() == bool.TrueString.ToLower();
        }

        /// <summary>
        /// 角色加入用户
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<bool> AddUserAsync(RoleToUserDto dto)
        {
            var uri = API.SysRole.AddUserAsync(_baseUrl);
            var content = new StringContent(JsonConvert.SerializeObject(dto), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            string res = await response.Content.ReadAsStringAsync();
            return res.ToLower() == bool.TrueString.ToLower();
        }

    }
}
