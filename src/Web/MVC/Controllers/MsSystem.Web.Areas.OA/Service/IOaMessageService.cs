using JadeFramework.Core.Domain.Entities;
using JadeFramework.Core.Extensions;
using Microsoft.Extensions.Configuration;
using MsSystem.Web.Areas.OA.Infrastructure;
using MsSystem.Web.Areas.OA.Model;
using MsSystem.Web.Areas.OA.ViewModel;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace MsSystem.Web.Areas.OA.Service
{
    public interface IOaMessageService
    {
        Task<Page<OaMessage>> GetPageAsync(int pageIndex, int pageSize);
        Task<MessageShowDTO> GetByIdAsync(long id);
        Task<bool> InsertAsync(MessageShowDTO model);
        Task<bool> UpdateAsync(MessageShowDTO model);
        Task<bool> DeleteAsync(MessageDeleteDTO dto);
        Task<bool> EnableMessageAsync(MessageEnableDTO dto);
        Task<Page<OaMessageMyList>> MyListAsync(OaMessageMyListSearch search);
        Task<OaMessageMyListDetail> MyListDetailAsync(long id, long userid);
        Task<bool> ReadMessageAsync(OaMessageReadDto message);
    }
    public class OaMessageService : IOaMessageService
    {
        private readonly HttpClient _apiClient;
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl;

        public OaMessageService(HttpClient httpClient, IConfiguration configuration)
        {
            _apiClient = httpClient;
            _configuration = configuration;
            _baseUrl = configuration["MsApplication:url"] + "/api/oa";
        }

        public async Task<bool> DeleteAsync(MessageDeleteDTO dto)
        {
            var uri = API.OaMessage.DeleteAsync(_baseUrl);
            var content = new StringContent(JsonConvert.SerializeObject(dto), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            string res = await response.Content.ReadAsStringAsync();
            return res.ToLower() == bool.TrueString.ToLower();
        }

        public async Task<MessageShowDTO> GetByIdAsync(long id)
        {
            var uri = API.OaMessage.GetByIdAsync(_baseUrl, id);
            var responseString = await _apiClient.GetStringAsync(uri);
            return responseString.ToObject<MessageShowDTO>();
        }

        public async Task<Page<OaMessage>> GetPageAsync(int pageIndex, int pageSize)
        {
            var uri = API.OaMessage.GetPageAsync(_baseUrl, pageIndex,pageSize);
            var responseString = await _apiClient.GetStringAsync(uri);
            return responseString.ToObject<Page<OaMessage>>();
        }

        public async Task<bool> InsertAsync(MessageShowDTO model)
        {
            var uri = API.OaMessage.InsertAsync(_baseUrl);
            var content = new StringContent(JsonConvert.SerializeObject(model), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            string res = await response.Content.ReadAsStringAsync();
            return res.ToLower() == bool.TrueString.ToLower();
        }

        public async Task<bool> UpdateAsync(MessageShowDTO model)
        {
            var uri = API.OaMessage.UpdateAsync(_baseUrl);
            var content = new StringContent(JsonConvert.SerializeObject(model), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            string res = await response.Content.ReadAsStringAsync();
            return res.ToLower() == bool.TrueString.ToLower();
        }
        public async Task<bool> EnableMessageAsync(MessageEnableDTO dto)
        {
            var uri = API.OaMessage.EnableMessageAsync(_baseUrl);
            var content = new StringContent(JsonConvert.SerializeObject(dto), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            string res = await response.Content.ReadAsStringAsync();
            return res.ToLower() == bool.TrueString.ToLower();
        }
        public async Task<Page<OaMessageMyList>> MyListAsync(OaMessageMyListSearch search)
        {
            var uri = API.OaMessage.MyListAsync(_baseUrl, search);
            var responseString = await _apiClient.GetStringAsync(uri);
            return responseString.ToObject<Page<OaMessageMyList>>();
        }

        public async Task<OaMessageMyListDetail> MyListDetailAsync(long id, long userid)
        {
            var uri = API.OaMessage.MyListDetailAsync(_baseUrl, id, userid);
            var responseString = await _apiClient.GetStringAsync(uri);
            return responseString.ToObject<OaMessageMyListDetail>();
        }

        public async Task<bool> ReadMessageAsync(OaMessageReadDto message)
        {
            var uri = API.OaMessage.ReadMessageAsync(_baseUrl);
            var content = new StringContent(JsonConvert.SerializeObject(message), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            string res = await response.Content.ReadAsStringAsync();
            return res.ToLower() == bool.TrueString.ToLower();
        }

    }
}
