using JadeFramework.Core.Domain.Entities;
using JadeFramework.Core.Extensions;
using Microsoft.Extensions.Configuration;
using MsSystem.Web.Areas.Weixin.Infrastructure;
using MsSystem.Web.Areas.Weixin.ViewModel;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace MsSystem.Web.Areas.Weixin.Service
{
    public interface IRuleService
    {
        Task<Page<RuleListDto>> GetRulePageAsync(int pageIndex, int pageSize);
        Task<RuleReplyDto> GetRuleReplyAsync(int id);
        Task<bool> AddAsync(RuleReplyDto model);
        Task<bool> UpdateAsync(RuleReplyDto model);
    }

    public class RuleService: IRuleService
    {
        private readonly HttpClient _apiClient;
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl;

        public RuleService(HttpClient httpClient, IConfiguration configuration)
        {
            _apiClient = httpClient;
            _configuration = configuration;
            _baseUrl = configuration["MsApplication:url"] + "/api/weixin";
        }

        public async Task<bool> AddAsync(RuleReplyDto model)
        {
            var uri = API.Rule.AddAsync(_baseUrl);
            var content = new StringContent(JsonConvert.SerializeObject(model), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            string res = await response.Content.ReadAsStringAsync();
            return res.ToLower() == bool.TrueString.ToLower();
        }

        public async Task<Page<RuleListDto>> GetRulePageAsync(int pageIndex, int pageSize)
        {
            var uri = API.Rule.GetRulePageAsync(_baseUrl, pageIndex, pageSize);
            var responseString = await _apiClient.GetStringAsync(uri);
            return responseString.ToObject<Page<RuleListDto>>();
        }

        public async Task<RuleReplyDto> GetRuleReplyAsync(int id)
        {
            var uri = API.Rule.GetRuleReplyAsync(_baseUrl, id);
            var responseString = await _apiClient.GetStringAsync(uri);
            return responseString.ToObject<RuleReplyDto>();
        }

        public async Task<bool> UpdateAsync(RuleReplyDto model)
        {
            var uri = API.Rule.UpdateAsync(_baseUrl);
            var content = new StringContent(JsonConvert.SerializeObject(model), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            string res = await response.Content.ReadAsStringAsync();
            return res.ToLower() == bool.TrueString.ToLower();
        }
    }
}
