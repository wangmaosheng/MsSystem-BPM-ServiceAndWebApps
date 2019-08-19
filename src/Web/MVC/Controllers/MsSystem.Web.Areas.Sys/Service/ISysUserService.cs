using JadeFramework.Core.Domain.Entities;
using JadeFramework.Core.Domain.Result;
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
    /// 用户表接口
    /// </summary>
    public interface ISysUserService
    {

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        Task<UserIndexViewModel> GetUserPageAsync(UserIndexSearch search);

        Task<UserShowDto> GetAsync(long userid);
        /// <summary>
        /// 获取数据权限信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<DataPrivilegesViewModel> GetPrivilegesAsync(DataPrivilegesViewModel model);
        /// <summary>
        /// 用户部门
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        Task<UserDeptViewModel> GetUserDeptAsync(long userid);
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        Task<LoginResult<UserIdentity>> LoginAsync(string account, string password);

        Task<bool> AddAsync(UserShowDto dto);

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(UserShowDto dto);

        Task<bool> SaveUserRoleAsync(RoleBoxDto dto);

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(List<long> ids, long userid);

        /// <summary>
        /// 保存用户数据权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> SaveDataPrivilegesAsync(DataPrivilegesDto model);

        /// <summary>
        /// 保存用户部门
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> SaveUserDeptAsync(UserDeptDto dto);

        /// <summary>
        /// 修改用户头像
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="imgUrl">头像地址</param>
        /// <returns></returns>
        Task<bool> ModifyUserHeadImgAsync(long userid, string imgUrl);
    }
    public class SysUserService : ISysUserService
    {
        private readonly HttpClient _apiClient;
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl;

        public SysUserService(HttpClient httpClient, IConfiguration configuration)
        {
            _apiClient = httpClient;
            _configuration = configuration;
            _baseUrl = configuration["MsApplication:url"] + "/api/sys";
        }
        public async Task<UserIndexViewModel> GetUserPageAsync(UserIndexSearch search)
        {
            var uri = API.SysUser.GetUserPageAsync(_baseUrl, search);
            var responseString = await _apiClient.GetStringAsync(uri);
            return responseString.ToObject<UserIndexViewModel>();
        }
        public async Task<UserShowDto> GetAsync(long userid)
        {
            if (userid == 0)
            {
                return new UserShowDto() { User = new MsSystem.Web.Areas.Sys.Model.SysUser { } };
            }
            var uri = API.SysUser.GetAsync(_baseUrl, userid);
            var responseString = await _apiClient.GetStringAsync(uri);
            return responseString.ToObject<UserShowDto>();
        }
        public async Task<DataPrivilegesViewModel> GetPrivilegesAsync(DataPrivilegesViewModel model)
        {
            throw new System.Exception();
        }
        public async Task<UserDeptViewModel> GetUserDeptAsync(long userid)
        {
            var uri = API.SysUser.GetUserDeptAsync(_baseUrl, userid);
            var responseString = await _apiClient.GetStringAsync(uri);
            return responseString.ToObject<UserDeptViewModel>();
        }
        public async Task<LoginResult<UserIdentity>> LoginAsync(string account, string password)
        {
            var uri = API.SysUser.LoginAsync(_baseUrl);
            var content = new StringContent(JsonConvert.SerializeObject(new LoginDTO { Account = account, Password = password }), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            var httpContent = await response.Content.ReadAsStringAsync();
            return httpContent.ToObject<LoginResult<UserIdentity>>();
        }


        public async Task<bool> AddAsync(UserShowDto dto)
        {
            var uri = API.SysUser.AddAsync(_baseUrl);
            var content = new StringContent(JsonConvert.SerializeObject(dto), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            string res = await response.Content.ReadAsStringAsync();
            return res.ToLower() == bool.TrueString.ToLower();
        }
        public async Task<bool> UpdateAsync(UserShowDto dto)
        {
            var uri = API.SysUser.UpdateAsync(_baseUrl);
            var content = new StringContent(JsonConvert.SerializeObject(dto), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            string res = await response.Content.ReadAsStringAsync();
            return res.ToLower() == bool.TrueString.ToLower();
        }
        public async Task<bool> SaveUserRoleAsync(RoleBoxDto dto)
        {
            var uri = API.SysUser.SaveUserRoleAsync(_baseUrl);
            var content = new StringContent(JsonConvert.SerializeObject(dto), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            string res = await response.Content.ReadAsStringAsync();
            return res.ToLower() == bool.TrueString.ToLower();
        }
        public async Task<bool> DeleteAsync(List<long> ids, long userid)
        {
            var uri = API.SysUser.DeleteAsync(_baseUrl);
            var content = new StringContent(JsonConvert.SerializeObject(new UserDeleteDTO { Ids = ids, UserId = userid }), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            string res = await response.Content.ReadAsStringAsync();
            return res.ToLower() == bool.TrueString.ToLower();
        }
        public async Task<bool> SaveDataPrivilegesAsync(DataPrivilegesDto model)
        {
            var uri = API.SysUser.SaveDataPrivilegesAsync(_baseUrl);
            var content = new StringContent(JsonConvert.SerializeObject(model), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            string res = await response.Content.ReadAsStringAsync();
            return res.ToLower() == bool.TrueString.ToLower();
        }
        public async Task<bool> SaveUserDeptAsync(UserDeptDto dto)
        {
            var uri = API.SysUser.SaveUserDeptAsync(_baseUrl);
            var content = new StringContent(JsonConvert.SerializeObject(dto), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            string res = await response.Content.ReadAsStringAsync();
            return res.ToLower() == bool.TrueString.ToLower();
        }
        public async Task<bool> ModifyUserHeadImgAsync(long userid, string imgUrl)
        {
            var uri = API.SysUser.ModifyUserHeadImgAsync(_baseUrl);
            var content = new StringContent(JsonConvert.SerializeObject(new ModifyUserHeadImgDTO { UserId = userid, ImgUrl = imgUrl }), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            string res = await response.Content.ReadAsStringAsync();
            return res.ToLower() == bool.TrueString.ToLower();
        }

    }

    public interface IScanningLoginService
    {
        /// <summary>
        /// 扫码登录
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        Task<LoginResult<UserIdentity>> ScanningLoginAsync(string account, string accessToken);
    }
    public class ScanningLoginService : IScanningLoginService
    {

        private readonly HttpClient _apiClient;
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl;

        public ScanningLoginService(HttpClient httpClient, IConfiguration configuration)
        {
            _apiClient = httpClient;
            _configuration = configuration;
            _baseUrl = configuration["MsApplication:url"] + "/api/sys";
        }

        public async Task<LoginResult<UserIdentity>> ScanningLoginAsync(string account, string accessToken)
        {
            var uri = API.SysUser.ScanningLoginAsync(_baseUrl);
            var content = new StringContent(JsonConvert.SerializeObject(new LoginDTO { Account = account }), System.Text.Encoding.UTF8, "application/json");
            //_apiClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _apiClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            var httpContent = await response.Content.ReadAsStringAsync();
            return httpContent.ToObject<LoginResult<UserIdentity>>();
        }
    }
}
