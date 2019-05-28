using JadeFramework.Core.Extensions;
using Microsoft.Extensions.Configuration;
using MsSystem.Web.Areas.Sys.Infrastructure;
using MsSystem.Web.Areas.Sys.ViewModel;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace MsSystem.Web.Areas.Sys.Service
{
    public interface ISysDeptService
    {
        Task<DeptIndexViewModel> GetTreeAsync();
        Task<DeptShowViewModel> GetDeptAsync(long deptid);
        Task<bool> AddAsync(DeptShowDto dto);
        Task<bool> UpdateAsync(DeptShowDto dto);
        Task<bool> DeleteAsync(long[] ids, long userid);
    }
    public class SysDeptService : ISysDeptService
    {
        private readonly HttpClient _apiClient;
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl;

        public SysDeptService(HttpClient httpClient, IConfiguration configuration)
        {
            _apiClient = httpClient;
            _configuration = configuration;
            _baseUrl = configuration["MsApplication:url"] + "/api/sys";
        }
        public async Task<DeptIndexViewModel> GetTreeAsync()
        {
            var uri = API.SysDept.GetTreeAsync(_baseUrl);
            var responseString = await _apiClient.GetStringAsync(uri);
            return responseString.ToObject<DeptIndexViewModel>();
        }
        public async Task<DeptShowViewModel> GetDeptAsync(long deptid)
        {
            var uri = API.SysDept.GetDeptAsync(_baseUrl, deptid);
            var responseString = await _apiClient.GetStringAsync(uri);
            return responseString.ToObject<DeptShowViewModel>();
        }
        public async Task<bool> AddAsync(DeptShowDto dto)
        {
            var uri = API.SysDept.AddAsync(_baseUrl);
            var content = new StringContent(JsonConvert.SerializeObject(dto), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            string res = await response.Content.ReadAsStringAsync();
            return res.ToLower() == bool.TrueString.ToLower();
        }
        public async Task<bool> UpdateAsync(DeptShowDto dto)
        {
            var uri = API.SysDept.UpdateAsync(_baseUrl);
            var content = new StringContent(JsonConvert.SerializeObject(dto), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            string res = await response.Content.ReadAsStringAsync();
            return res.ToLower() == bool.TrueString.ToLower();
        }
        public async Task<bool> DeleteAsync(long[] ids, long userid)
        {
            var uri = API.SysDept.DeleteAsync(_baseUrl);
            var content = new StringContent(JsonConvert.SerializeObject(new DeptDeleteDTO { Ids = ids,UserId = userid }), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            string res = await response.Content.ReadAsStringAsync();
            return res.ToLower() == bool.TrueString.ToLower();
        }

    }
}
