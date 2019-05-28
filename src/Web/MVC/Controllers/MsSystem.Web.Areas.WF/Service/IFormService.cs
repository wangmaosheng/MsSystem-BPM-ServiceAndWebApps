using JadeFramework.Core.Domain.Entities;
using JadeFramework.Core.Extensions;
using Microsoft.Extensions.Configuration;
using MsSystem.Web.Areas.WF.Infrastructure;
using MsSystem.Web.Areas.WF.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MsSystem.Web.Areas.WF.Service
{
    public interface IFormService
    {
        Task<Page<FormPageDto>> GetPageAsync(int pageIndex, int pageSize);
        Task<FormDetailDto> GetFormDetailAsync(Guid id);
        Task<bool> InsertAsync(FormDetailDto model);
        Task<bool> UpdateAsync(FormDetailDto model);
        Task<List<ZTree>> GetFormTreeAsync();
    }
    public class FormService : IFormService
    {
        private readonly HttpClient _apiClient;
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl;

        public FormService(HttpClient httpClient, IConfiguration configuration)
        {
            _apiClient = httpClient;
            _configuration = configuration;
            _baseUrl = configuration["MsApplication:url"] + "/api/wf";
        }

        public async Task<FormDetailDto> GetFormDetailAsync(Guid id)
        {
            var uri = API.Form.GetFormDetailAsync(_baseUrl, id);
            var responseString = await _apiClient.GetStringAsync(uri);
            return responseString.ToObject<FormDetailDto>();
        }

        public async Task<List<ZTree>> GetFormTreeAsync()
        {
            var uri = API.Form.GetFormTreeAsync(_baseUrl);
            var responseString = await _apiClient.GetStringAsync(uri);
            return responseString.ToObject<List<ZTree>>();
        }

        public async Task<Page<FormPageDto>> GetPageAsync(int pageIndex, int pageSize)
        {
            var uri = API.Form.GetPageAsync(_baseUrl, pageIndex, pageSize);
            var responseString = await _apiClient.GetStringAsync(uri);
            return responseString.ToObject<Page<FormPageDto>>();
        }

        public async Task<bool> InsertAsync(FormDetailDto model)
        {
            var uri = API.Form.InsertAsync(_baseUrl);
            var content = new StringContent(JsonConvert.SerializeObject(model), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            string res = await response.Content.ReadAsStringAsync();
            return res.ToLower() == bool.TrueString.ToLower();
        }

        public async Task<bool> UpdateAsync(FormDetailDto model)
        {
            var uri = API.Form.UpdateAsync(_baseUrl);
            var content = new StringContent(JsonConvert.SerializeObject(model), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            string res = await response.Content.ReadAsStringAsync();
            return res.ToLower() == bool.TrueString.ToLower();
        }
    }
}
