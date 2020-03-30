using JadeFramework.Core.Domain.Entities;
using JadeFramework.Core.Extensions;
using Microsoft.Extensions.Configuration;
using MsSystem.Web.Areas.WF.Infrastructure;
using MsSystem.Web.Areas.WF.Model;
using MsSystem.Web.Areas.WF.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MsSystem.Web.Areas.WF.Service
{
    public interface IWorkFlowService
    {
        Task<Page<WfWorkflow>> GetPageAsync(int pageIndex, int pageSize);
        Task<bool> InsertAsync(WorkFlowDetailDto workflow);
        Task<bool> UpdateAsync(WorkFlowDetailDto workflow);
        Task<bool> DeleteAsync(FlowDeleteDTO dto);
        Task<WorkFlowDetailDto> GetByIdAsync(Guid id);
        Task<List<WorkFlowStartDto>> GetWorkFlowStartAsync(Guid categoryid);
        Task<List<WorkFlowLineDto>> GetAllLinesAsync();
        Task<WorkFlowLineDto> GetLineAsync(Guid lineid);
        /// <summary>
        /// new workflow version
        /// </summary>
        /// <param name="id">flowid</param>
        /// <returns></returns>
        Task<bool> NewVersionAsync(WorkFlowDetailDto dto);
    }

    public class WorkFlowService : IWorkFlowService
    {
        private readonly HttpClient _apiClient;
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl;

        public WorkFlowService(HttpClient httpClient, IConfiguration configuration)
        {
            _apiClient = httpClient;
            _configuration = configuration;
            _baseUrl = configuration["MsApplication:url"] + "/api/wf";
        }

        public async Task<WorkFlowDetailDto> GetByIdAsync(Guid id)
        {
            var uri = API.WorkFlow.GetByIdAsync(_baseUrl, id);
            var responseString = await _apiClient.GetStringAsync(uri);
            return responseString.ToObject<WorkFlowDetailDto>();
        }

        public async Task<List<WorkFlowStartDto>> GetWorkFlowStartAsync(Guid categoryid)
        {
            var uri = API.WorkFlow.GetWorkFlowStartAsync(_baseUrl, categoryid);
            var responseString = await _apiClient.GetStringAsync(uri);
            return responseString.ToObject<List<WorkFlowStartDto>>();
        }

        public async Task<Page<WfWorkflow>> GetPageAsync(int pageIndex, int pageSize)
        {
            var uri = API.WorkFlow.GetPageAsync(_baseUrl, pageIndex, pageSize);
            var responseString = await _apiClient.GetStringAsync(uri);
            return responseString.ToObject<Page<WfWorkflow>>();
        }

        public async Task<bool> InsertAsync(WorkFlowDetailDto workflow)
        {
            var uri = API.WorkFlow.InsertAsync(_baseUrl);
            var content = new StringContent(JsonConvert.SerializeObject(workflow), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            string res = await response.Content.ReadAsStringAsync();
            return res.ToLower() == bool.TrueString.ToLower();
        }

        public async Task<bool> UpdateAsync(WorkFlowDetailDto workflow)
        {
            var uri = API.WorkFlow.UpdateAsync(_baseUrl);
            var content = new StringContent(JsonConvert.SerializeObject(workflow), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            string res = await response.Content.ReadAsStringAsync();
            return res.ToLower() == bool.TrueString.ToLower();
        }

        public async Task<bool> DeleteAsync(FlowDeleteDTO dto)
        {
            var uri = API.WorkFlow.DeleteAsync(_baseUrl);
            return await _apiClient.PostBooleanAsync(uri, dto);
        }

        public async Task<List<WorkFlowLineDto>> GetAllLinesAsync()
        {
            var uri = API.WorkFlow.GetAllLinesAsync(_baseUrl);
            var responseString = await _apiClient.GetStringAsync(uri);
            return responseString.ToObject<List<WorkFlowLineDto>>();
        }

        public async Task<WorkFlowLineDto> GetLineAsync(Guid lineid)
        {
            var uri = API.WorkFlow.GetLineAsync(_baseUrl, lineid);
            var responseString = await _apiClient.GetStringAsync(uri);
            return responseString.ToObject<WorkFlowLineDto>();
        }
        public async Task<bool> NewVersionAsync(WorkFlowDetailDto dto)
        {
            var uri = API.WorkFlow.NewVersionAsync(_baseUrl);
            return await _apiClient.PostBooleanAsync(uri, dto);
        }
    }
}
