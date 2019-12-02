using JadeFramework.Core.Domain.Entities;
using JadeFramework.Core.Extensions;
using JadeFramework.WorkFlow;
using Microsoft.Extensions.Configuration;
using MsSystem.Web.Areas.WF.Infrastructure;
using MsSystem.Web.Areas.WF.ViewModel;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MsSystem.Web.Areas.WF.Service
{
    public interface IWorkFlowInstanceService
    {
        /// <summary>
        /// 开始用户流程实例
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<WorkFlowResult> CreateInstanceAsync(WorkFlowProcessTransition model);

        /// <summary>
        /// 获取用户待办事项
        /// </summary>
        /// <param name="searchDto"></param>
        /// <returns></returns>
        Task<Page<UserWorkFlowDto>> GetUserTodoListAsync(WorkFlowTodoSearchDto searchDto);

        /// <summary>
        /// 获取用户操作历史记录
        /// </summary>
        /// <param name="searchDto"></param>
        /// <returns></returns>
        Task<Page<WorkFlowOperationHistoryDto>> GetUserOperationHistoryAsync(WorkFlowOperationHistorySearchDto searchDto);

        /// <summary>
        /// get workflow process
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        Task<WorkFlowProcess> GetProcessAsync(WorkFlowProcess process);

        /// <summary>
        /// 系统定制流程获取
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        Task<WorkFlowProcess> GetProcessForSystemAsync(SystemFlowDto process);

        /// <summary>
        /// 获取用户发起的流程
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<Page<UserWorkFlowDto>> GetUserWorkFlowPageAsync(int pageIndex, int pageSize, string userId);

        /// <summary>
        /// 流程过程流转处理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<WorkFlowResult> ProcessTransitionFlowAsync(WorkFlowProcessTransition model);

        /// <summary>
        /// 获取流程审批意见
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<WorkFlowResult> GetFlowApprovalAsync(WorkFlowProcessTransition model);

        /// <summary>
        /// 添加或修改自定义表单数据
        /// </summary>
        /// <param name="addProcess"></param>
        /// <returns></returns>
        Task<WorkFlowResult> AddOrUpdateCustomFlowFormAsync(WorkFlowProcess addProcess);

        /// <summary>
        /// 获取我的审批历史记录
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<Page<UserWorkFlowDto>> GetMyApprovalHistoryAsync(int pageIndex, int pageSize, string userId);

        /// <summary>
        /// 获取流程图信息
        /// </summary>
        /// <param name="id">流程ID</param>
        /// <param name="instanceId">实例ID</param>
        /// <returns></returns>
        Task<WorkFlowImageDto> GetFlowImageAsync(Guid flowid, Guid? instanceId);

        Task<WorkFlowResult> UrgeAsync(UrgeDto urge);
    }

    public class WorkFlowInstanceService : IWorkFlowInstanceService
    {
        private readonly HttpClient _apiClient;
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl;

        public WorkFlowInstanceService(HttpClient httpClient, IConfiguration configuration)
        {
            _apiClient = httpClient;
            _configuration = configuration;
            _baseUrl = configuration["MsApplication:url"] + "/api/wf";
        }

        public async Task<WorkFlowResult> CreateInstanceAsync(WorkFlowProcessTransition model)
        {
            var uri = API.WorkFlowInstance.CreateInstanceAsync(_baseUrl);
            var content = new StringContent(JsonConvert.SerializeObject(model), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            string res = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<WorkFlowResult>(res);
        }

        /// <summary>
        /// 添加或修改自定义表单数据
        /// </summary>
        /// <param name="addProcess"></param>
        /// <returns></returns>
        public async Task<WorkFlowResult> AddOrUpdateCustomFlowFormAsync(WorkFlowProcess addProcess)
        {
            var uri = API.WorkFlowInstance.AddOrUpdateCustomFlowFormAsync(_baseUrl);
            var content = new StringContent(JsonConvert.SerializeObject(addProcess), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            string res = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<WorkFlowResult>(res);
        }

        public async Task<WorkFlowResult> GetFlowApprovalAsync(WorkFlowProcessTransition model)
        {
            var uri = API.WorkFlowInstance.GetFlowApprovalAsync(_baseUrl, model);
            var responseString = await _apiClient.GetStringAsync(uri);
            return responseString.ToObject<WorkFlowResult>();
        }

        public async Task<Page<UserWorkFlowDto>> GetMyApprovalHistoryAsync(int pageIndex, int pageSize, string userId)
        {
            var uri = API.WorkFlowInstance.GetMyApprovalHistoryAsync(_baseUrl, pageIndex, pageSize, userId);
            var responseString = await _apiClient.GetStringAsync(uri);
            return responseString.ToObject<Page<UserWorkFlowDto>>();
        }

        public async Task<WorkFlowProcess> GetProcessAsync(WorkFlowProcess process)
        {
            var uri = API.WorkFlowInstance.GetProcessAsync(_baseUrl, process);
            var responseString = await _apiClient.GetStringAsync(uri);
            return responseString.ToObject<WorkFlowProcess>();
        }

        public async Task<WorkFlowProcess> GetProcessForSystemAsync(SystemFlowDto process)
        {
            var uri = API.WorkFlowInstance.GetProcessForSystemAsync(_baseUrl, process);
            var responseString = await _apiClient.GetStringAsync(uri);
            return responseString.ToObject<WorkFlowProcess>();
        }


        public async Task<Page<WorkFlowOperationHistoryDto>> GetUserOperationHistoryAsync(WorkFlowOperationHistorySearchDto searchDto)
        {
            var uri = API.WorkFlowInstance.GetUserOperationHistoryAsync(_baseUrl, searchDto);
            var responseString = await _apiClient.GetStringAsync(uri);
            return responseString.ToObject<Page<WorkFlowOperationHistoryDto>>();
        }

        public async Task<Page<UserWorkFlowDto>> GetUserTodoListAsync(WorkFlowTodoSearchDto searchDto)
        {
            var uri = API.WorkFlowInstance.GetUserTodoListAsync(_baseUrl, searchDto);
            var responseString = await _apiClient.GetStringAsync(uri);
            return responseString.ToObject<Page<UserWorkFlowDto>>();
        }

        public async Task<Page<UserWorkFlowDto>> GetUserWorkFlowPageAsync(int pageIndex, int pageSize, string userId)
        {
            var uri = API.WorkFlowInstance.GetUserWorkFlowPageAsync(_baseUrl, pageIndex, pageSize, userId);
            var responseString = await _apiClient.GetStringAsync(uri);
            return responseString.ToObject<Page<UserWorkFlowDto>>();
        }

        public async Task<WorkFlowResult> ProcessTransitionFlowAsync(WorkFlowProcessTransition model)
        {
            var uri = API.WorkFlowInstance.ProcessTransitionFlowAsync(_baseUrl);
            var content = new StringContent(JsonConvert.SerializeObject(model), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            string res = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<WorkFlowResult>(res);
        }

        /// <summary>
        /// 获取流程图信息
        /// </summary>
        /// <param name="instanceId">实例ID</param>
        /// <returns></returns>
        public async Task<WorkFlowImageDto> GetFlowImageAsync(Guid flowid, Guid? instanceId)
        {
            var uri = API.WorkFlowInstance.GetFlowImageAsync(_baseUrl, flowid, instanceId);
            var responseString = await _apiClient.GetStringAsync(uri);
            return responseString.ToObject<WorkFlowImageDto>();
        }

        public async Task<WorkFlowResult> UrgeAsync(UrgeDto urge)
        {
            var uri = API.WorkFlowInstance.UrgeAsync(_baseUrl);
            var content = new StringContent(JsonConvert.SerializeObject(urge), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            string res = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<WorkFlowResult>(res);
        }

    }
}
