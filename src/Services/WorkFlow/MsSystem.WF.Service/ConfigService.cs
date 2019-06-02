using JadeFramework.Core.Domain.Entities;
using JadeFramework.Core.Extensions;
using Microsoft.Extensions.Options;
using MsSystem.WF.IService;
using MsSystem.WF.ViewModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MsSystem.WF.Service
{
    public class ConfigService : IConfigService
    {
        private readonly HttpClient _apiClient;
        private readonly IOptions<AppSettings> _appSettings;
        //private readonly IConfiguration _configuration;
        //private string identityUrl;
        public ConfigService(HttpClient apiClient, IOptions<AppSettings> appSettings)
        {
            //_configuration = configuration;
            _apiClient = apiClient;
            _appSettings = appSettings;
            //var identitySection = _configuration.GetSection("MsApplication");
            //identityUrl = identitySection["url"];
        }

        public async Task<List<ZTree>> GetRoleTreesAsync(List<long> ids)
        {
            //var token = await this.GetTokenAsync();
            //var client = new RestClient(this.identityUrl + _configuration["WorkFlow:Roles"]);
            //var request = new RestRequest(Method.POST);
            //request.AddHeader("Authorization", "Bearer " + token);
            //request.AddJsonBody(ids);
            //var respons = await client.ExecuteTaskAsync(request);
            //var content = respons.Content;
            //return JsonConvert.DeserializeObject<List<ZTree>>(content);
            string url = _appSettings.Value.MsApplication.url + _appSettings.Value.WorkFlow.Roles;
            var content = new StringContent(JsonConvert.SerializeObject(ids), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            string res = await response.Content.ReadAsStringAsync();
            return res.ToObject<List<ZTree>>();
        }

        public async Task<List<ZTree>> GetUserTreeAsync(List<long> ids)
        {
            //var token = await this.GetTokenAsync();
            //var client = new RestClient(this.identityUrl + _configuration["WorkFlow:Users"]);
            //var request = new RestRequest(Method.POST);
            //request.AddHeader("Authorization", "Bearer " + token);
            //request.AddJsonBody(ids);
            //var respons = await client.ExecuteTaskAsync(request);
            //var content = respons.Content;
            //return JsonConvert.DeserializeObject<List<ZTree>>(content);
            string url = _appSettings.Value.MsApplication.url + _appSettings.Value.WorkFlow.Users;// + _configuration["WorkFlow:Users"];
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
            //var token = await this.GetTokenAsync();
            //var client = new RestClient(this.identityUrl + _configuration["WorkFlow:GetUserIds"]);
            //var request = new RestRequest(Method.POST);
            //request.AddHeader("Authorization", "Bearer " + token);
            //request.AddJsonBody(roleids);
            //var respons = await client.ExecuteTaskAsync(request);
            //var content = respons.Content;
            //return JsonConvert.DeserializeObject<List<long>>(content);

            string url = _appSettings.Value.MsApplication.url + _appSettings.Value.WorkFlow.GetUserIds;// _configuration["WorkFlow:GetUserIds"];
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

        //private async Task<string> GetTokenAsync()
        //{
        //    Dictionary<string, string> dict = new Dictionary<string, string>();
        //    dict["client_id"] = identityModel.client_id;
        //    dict["client_secret"] = identityModel.client_secret;
        //    dict["grant_type"] = identityModel.grant_type;
        //    dict["scopes"] = identityModel.scopes;
        //    var token = string.Empty;
        //    using (HttpClient http = new HttpClient())
        //    using (var content = new FormUrlEncodedContent(dict))
        //    {
        //        var msg = await http.PostAsync($"{identityUrl + tokenurl}", content);
        //        string result = await msg.Content.ReadAsStringAsync();
        //        var model = JsonConvert.DeserializeObject<IdentityResult>(result);
        //        token = model.access_token;
        //        return token;
        //    }
        //}
    }
}
