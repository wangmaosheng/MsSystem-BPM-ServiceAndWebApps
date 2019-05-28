using JadeFramework.Core.Domain.Authorization;
using JadeFramework.Core.Domain.Entities;
using Microsoft.Extensions.Configuration;
using MsSystem.WF.IService;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MsSystem.WF.Service
{
    public class ConfigService : IConfigService
    {
        private readonly IConfiguration _configuration;
        private string identityUrl;
        private string tokenurl;
        private readonly IdentityModel identityModel;
        public ConfigService(IConfiguration configuration)
        {
            this._configuration = configuration;
            var identitySection = _configuration.GetSection("MsApplication");
            identityModel = new IdentityModel
            {
                client_id = identitySection["client_id"],
                client_secret = identitySection["client_secret"],
                grant_type = identitySection["grant_type"],
                scopes = identitySection["scopes"]
            };
            identityUrl = identitySection["url"];
            tokenurl = identitySection["tokenurl"];
        }

        public async Task<List<ZTree>> GetRoleTreesAsync(List<long> ids)
        {
            var token = await this.GetTokenAsync();
            var client = new RestClient(this.identityUrl + _configuration["WorkFlow:Roles"]);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddJsonBody(ids);
            var respons = await client.ExecuteTaskAsync(request);
            var content = respons.Content;
            return JsonConvert.DeserializeObject<List<ZTree>>(content);
        }

        public async Task<List<ZTree>> GetUserTreeAsync(List<long> ids)
        {
            var token = await this.GetTokenAsync();
            var client = new RestClient(this.identityUrl + _configuration["WorkFlow:Users"]);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddJsonBody(ids);
            var respons = await client.ExecuteTaskAsync(request);
            var content = respons.Content;
            return JsonConvert.DeserializeObject<List<ZTree>>(content);
        }

        /// <summary>
        /// 根据角色ID获取用户ID
        /// </summary>
        /// <param name="roleids"></param>
        /// <returns></returns>
        public async Task<List<long>> GetUserIdsByRoleIdsAsync(List<long> roleids)
        {
            var token = await this.GetTokenAsync();
            var client = new RestClient(this.identityUrl + _configuration["WorkFlow:GetUserIds"]);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddJsonBody(roleids);
            var respons = await client.ExecuteTaskAsync(request);
            var content = respons.Content;
            return JsonConvert.DeserializeObject<List<long>>(content);
        }

        private async Task<string> GetTokenAsync()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict["client_id"] = identityModel.client_id;
            dict["client_secret"] = identityModel.client_secret;
            dict["grant_type"] = identityModel.grant_type;
            dict["scopes"] = identityModel.scopes;
            var token = string.Empty;
            using (HttpClient http = new HttpClient())
            using (var content = new FormUrlEncodedContent(dict))
            {
                var msg = await http.PostAsync($"{identityUrl + tokenurl}", content);
                string result = await msg.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<IdentityResult>(result);
                token = model.access_token;
                return token;
            }
        }
    }
}
