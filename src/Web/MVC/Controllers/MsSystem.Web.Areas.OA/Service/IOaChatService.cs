using JadeFramework.Core.Extensions;
using Microsoft.Extensions.Configuration;
using MsSystem.Web.Areas.OA.Infrastructure;
using MsSystem.Web.Areas.OA.ViewModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MsSystem.Web.Areas.OA.Service
{
    public interface IOaChatService
    {
        Task<List<ChatUserViewModel>> GetChatUserAsync(List<long> chattinguserids);
        Task<List<ChatUserListDto>> GetChatListAsync(ChatUserListSearchDto model);
    }

    public class OaChatService: IOaChatService
    {
        private readonly HttpClient _apiClient;
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl;

        public OaChatService(HttpClient httpClient, IConfiguration configuration)
        {
            _apiClient = httpClient;
            _configuration = configuration;
            _baseUrl = configuration["MsApplication:url"] + "/api/oa";
        }
        public async Task<List<ChatUserViewModel>> GetChatUserAsync(List<long> chattinguserids)
        {
            var uri = API.OaChat.GetChatUserAsync(_baseUrl);
            var content = new StringContent(JsonConvert.SerializeObject(chattinguserids), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            string res = await response.Content.ReadAsStringAsync();
            return res.ToObject<List<ChatUserViewModel>>();
        }

        public async Task<List<ChatUserListDto>> GetChatListAsync(ChatUserListSearchDto model)
        {
            var uri = API.OaChat.GetChatListAsync(_baseUrl, model);
            var responseString = await _apiClient.GetStringAsync(uri);
            return responseString.ToObject<List<ChatUserListDto>>();
        }

    }
}
