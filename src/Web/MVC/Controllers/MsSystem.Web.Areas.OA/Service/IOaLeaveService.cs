using JadeFramework.Core.Domain.Entities;
using JadeFramework.Core.Domain.Result;
using JadeFramework.Core.Extensions;
using Microsoft.Extensions.Configuration;
using MsSystem.Web.Areas.OA.ViewModel;
using MsSystem.Web.Areas.OA.Infrastructure;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace MsSystem.Web.Areas.OA.Service
{
    public interface IOaLeaveService
    {
        Task<Page<OaLeaveDto>> GetPageAsync(int pageIndex, int pageSize, long userid);
        Task<OaLeaveShowDto> GetAsync(long id);
        Task<AjaxResult> InsertAsync(OaLeaveShowDto entity);
        Task<AjaxResult> UpdateAsync(OaLeaveShowDto entity);
    }

    public class OaLeaveService : IOaLeaveService
    {
        private readonly HttpClient _apiClient;
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl;

        public OaLeaveService(HttpClient httpClient, IConfiguration configuration)
        {
            _apiClient = httpClient;
            _configuration = configuration;
            _baseUrl = configuration["MsApplication:url"] + "/api/oa";
        }

        public async Task<OaLeaveShowDto> GetAsync(long id)
        {
            var uri = API.OaLeave.GetAsync(_baseUrl, id);
            var responseString = await _apiClient.GetStringAsync(uri);
            return responseString.ToObject<OaLeaveShowDto>();
        }

        public async Task<Page<OaLeaveDto>> GetPageAsync(int pageIndex, int pageSize, long userid)
        {
            var uri = API.OaLeave.GetPageAsync(_baseUrl, pageIndex, pageSize, userid);
            return await _apiClient.GetObjectAsync<Page<OaLeaveDto>>(uri);
        }

        public async Task<AjaxResult> InsertAsync(OaLeaveShowDto entity)
        {
            var uri = API.OaLeave.InsertAsync(_baseUrl);
            var content = new StringContent(JsonConvert.SerializeObject(entity), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            string res = await response.Content.ReadAsStringAsync();
            return res.ToObject<AjaxResult>();
        }

        public async Task<AjaxResult> UpdateAsync(OaLeaveShowDto entity)
        {
            var uri = API.OaLeave.UpdateAsync(_baseUrl);
            var content = new StringContent(JsonConvert.SerializeObject(entity), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            string res = await response.Content.ReadAsStringAsync();
            return res.ToObject<AjaxResult>();
        }
    }
}
