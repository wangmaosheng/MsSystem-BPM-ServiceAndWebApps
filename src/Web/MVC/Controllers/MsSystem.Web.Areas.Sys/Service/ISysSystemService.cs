using JadeFramework.Core.Domain.Entities;
using JadeFramework.Core.Extensions;
using Microsoft.Extensions.Configuration;
using MsSystem.Web.Areas.Sys.Infrastructure;
using MsSystem.Web.Areas.Sys.Model;
using MsSystem.Web.Areas.Sys.ViewModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MsSystem.Web.Areas.Sys.Service
{
    /// <summary>
    /// 系统
    /// </summary>
    public interface ISysSystemService
    {
        /// <summary>
        /// 根据ID获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<SysSystem> GetByIdAsync(long id);

        Task<List<SysSystem>> ListAsync();

        Task<Page<SysSystem>> GetPageAsync(int pageIndex, int pageSize);

        /// <summary>
        /// 新增系统
        /// </summary>
        /// <param name="system"></param>
        /// <returns></returns>
        Task<bool> InsertAsync(SysSystem system);

        /// <summary>
        /// 更新系统
        /// </summary>
        /// <param name="system"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(SysSystem system);

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(List<long> ids, long userid);
    }
    public class SysSystemService : ISysSystemService
    {
        private readonly HttpClient _apiClient;
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl;

        public SysSystemService(HttpClient httpClient, IConfiguration configuration)
        {
            _apiClient = httpClient;
            _configuration = configuration;
            _baseUrl = configuration["MsApplication:url"] + "/api/sys";
        }
        public async Task<SysSystem> GetByIdAsync(long id)
        {
            var uri = API.SysSystem.GetByIdAsync(_baseUrl, id);
            var responseString = await _apiClient.GetStringAsync(uri);
            return responseString.ToObject<SysSystem>();
        }
        public async Task<List<SysSystem>> ListAsync()
        {
            var uri = API.SysSystem.ListAsync(_baseUrl);
            var responseString = await _apiClient.GetStringAsync(uri);
            return responseString.ToObject<List<SysSystem>>();
        }
        public async Task<Page<SysSystem>> GetPageAsync(int pageIndex, int pageSize)
        {
            var uri = API.SysSystem.GetPageAsync(_baseUrl, pageIndex, pageSize);
            var responseString = await _apiClient.GetStringAsync(uri);
            return responseString.ToObject<Page<SysSystem>>();
        }
        public async Task<bool> InsertAsync(SysSystem system)
        {
            var uri = API.SysSystem.InsertAsync(_baseUrl);
            var content = new StringContent(JsonConvert.SerializeObject(system), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            string res = await response.Content.ReadAsStringAsync();
            return res.ToLower() == bool.TrueString.ToLower();
        }
        public async Task<bool> UpdateAsync(SysSystem system)
        {
            var uri = API.SysSystem.UpdateAsync(_baseUrl);
            var content = new StringContent(JsonConvert.SerializeObject(system), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            string res = await response.Content.ReadAsStringAsync();
            return res.ToLower() == bool.TrueString.ToLower();
        }
        public async Task<bool> DeleteAsync(List<long> ids, long userid)
        {
            var uri = API.SysSystem.DeleteAsync(_baseUrl);
            var content = new StringContent(JsonConvert.SerializeObject(new SystemDeleteDTO { Ids = ids,UserId = userid }), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            string res = await response.Content.ReadAsStringAsync();
            return res.ToLower() == bool.TrueString.ToLower();
        }
    }
}
