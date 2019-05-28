using JadeFramework.Core.Extensions;
using Microsoft.Extensions.Configuration;
using MsSystem.Web.Areas.Weixin.Infrastructure;
using MsSystem.Web.Areas.Weixin.ViewModel;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MsSystem.Web.Areas.Weixin.Service
{
    public interface IWxMenuService
    {
        Task<List<WxMenuDto>> GetTreesAsync();
    }

    public class WxMenuService: IWxMenuService
    {
        private readonly HttpClient _apiClient;
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl;

        public WxMenuService(HttpClient httpClient, IConfiguration configuration)
        {
            _apiClient = httpClient;
            _configuration = configuration;
            _baseUrl = configuration["MsApplication:url"] + "/api/weixin";
        }

        public async Task<List<WxMenuDto>> GetTreesAsync()
        {
            var uri = API.WxMenu.GetTreesAsync(_baseUrl);
            var responseString = await _apiClient.GetStringAsync(uri);
            return responseString.ToObject<List<WxMenuDto>>();
        }
    }
}
