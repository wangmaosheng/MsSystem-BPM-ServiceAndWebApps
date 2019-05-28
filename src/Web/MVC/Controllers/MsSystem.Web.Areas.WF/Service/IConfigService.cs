using JadeFramework.Core.Domain.Entities;
using Microsoft.Extensions.Configuration;
using MsSystem.Web.Areas.WF.Infrastructure;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MsSystem.Web.Areas.WF.Service
{
    public interface IConfigService
    {
        Task<List<ZTree>> GetRoleTreesAsync(List<long> ids);
        Task<List<ZTree>> GetUserTreeAsync(List<long> ids);
    }

    public class ConfigService : IConfigService
    {
        private readonly HttpClient _apiClient;
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl;

        public ConfigService(HttpClient httpClient, IConfiguration configuration)
        {
            _apiClient = httpClient;
            _configuration = configuration;
            _baseUrl = configuration["MsApplication:url"] + "/api/sys";
        }
        public async Task<List<ZTree>> GetRoleTreesAsync(List<long> ids)
        {
            var uri = API.Config.GetRoleTreesAsync(_baseUrl);
            var content = new StringContent(JsonConvert.SerializeObject(ids), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            string res = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ZTree>>(res);
        }

        public async Task<List<ZTree>> GetUserTreeAsync(List<long> ids)
        {
            var uri = API.Config.GetUserTreeAsync(_baseUrl);
            var content = new StringContent(JsonConvert.SerializeObject(ids), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            string res = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ZTree>>(res);
        }
    }
}
