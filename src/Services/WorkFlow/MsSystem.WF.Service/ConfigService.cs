using JadeFramework.Core.Domain.Entities;
using JadeFramework.Core.Extensions;
using Microsoft.Extensions.Options;
using MsSystem.WF.IService;
using MsSystem.WF.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MsSystem.WF.Service
{
    public class ConfigService : IConfigService
    {
        private readonly HttpClient _apiClient;
        private readonly IOptions<AppSettings> _appSettings;

        public ConfigService(HttpClient apiClient, IOptions<AppSettings> appSettings)
        {
            _apiClient = apiClient;
            _appSettings = appSettings;
        }

        public async Task<List<ZTree>> GetRoleTreesAsync(List<long> ids)
        {
            //Consul服务之间调用



            string url = _appSettings.Value.MsApplication.url + _appSettings.Value.WorkFlow.Roles;
            var content = new StringContent(JsonConvert.SerializeObject(ids), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            string res = await response.Content.ReadAsStringAsync();
            return res.ToObject<List<ZTree>>();
        }

        public async Task<List<ZTree>> GetUserTreeAsync(List<long> ids)
        {
            string url = _appSettings.Value.MsApplication.url + _appSettings.Value.WorkFlow.Users;
            var content = new StringContent(JsonConvert.SerializeObject(ids), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            string res = await response.Content.ReadAsStringAsync();
            return res.ToObject<List<ZTree>>();
        }

        /// <summary>
        /// 根据角色ID获取用户ID
        /// </summary>
        /// <param name="roleids"></param>
        /// <returns></returns>
        public async Task<List<long>> GetUserIdsByRoleIdsAsync(List<long> roleids)
        {
            string url = _appSettings.Value.MsApplication.url + _appSettings.Value.WorkFlow.GetUserIds;
            var content = new StringContent(JsonConvert.SerializeObject(roleids), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            string res = await response.Content.ReadAsStringAsync();
            return res.ToObject<List<long>>();
        }

        /// <summary>
        /// SQL获取节点信息
        /// </summary>
        /// <param name="sysname"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<List<long>> GetFlowNodeInfo(string sysname, FlowViewModel model)
        {
            string url = string.Format(_appSettings.Value.MsApplication.url + _appSettings.Value.WorkFlow.GetFlowNodeInfo, sysname);
            var content = new StringContent(model.ToJson(), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            string res = await response.Content.ReadAsStringAsync();
            return res.ToObject<List<long>>();
        }
        /// <summary>
        /// 获取最终的节点ID
        /// </summary>
        /// <param name="model">连线条件字典集合</param>
        /// <returns></returns>
        public async Task<Guid?> GetFinalNodeId(string sysname, FlowLineFinalNodeDto model)
        {
            string url = string.Format(_appSettings.Value.MsApplication.url + _appSettings.Value.WorkFlow.GetFinalNodeId, sysname);
            var content = new StringContent(model.ToJson(), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            string res = await response.Content.ReadAsStringAsync();
            Guid? finalnodeid = JsonConvert.DeserializeObject<Guid?>(res);
            return finalnodeid;
        }

        /// <summary>
        /// //对某些人进行消息推送并入库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task UrgeSendSignalR(MessagePushSomBodyDTO model)
        {
            string url = string.Format(_appSettings.Value.MsApplication.url + _appSettings.Value.WorkFlow.UrgeSendSignalR);
            var content = new StringContent(model.ToJson(), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
        }
    }
}
