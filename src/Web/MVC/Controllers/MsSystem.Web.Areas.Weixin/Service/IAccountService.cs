using JadeFramework.Core.Domain.Entities;
using JadeFramework.Core.Extensions;
using Microsoft.Extensions.Configuration;
using MsSystem.Web.Areas.Weixin.Infrastructure;
using MsSystem.Web.Areas.Weixin.ViewModel;
using System.Net.Http;
using System.Threading.Tasks;

namespace MsSystem.Web.Areas.Weixin.Service
{
    public interface IAccountService
    {
        Task<Page<WxAccountListDto>> GetPageAsync(int pageIndex, int pageSize);
    }

    public class AccountService : IAccountService
    {
        private readonly HttpClient _apiClient;
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl;

        public AccountService(HttpClient httpClient, IConfiguration configuration)
        {
            _apiClient = httpClient;
            _configuration = configuration;
            _baseUrl = configuration["MsApplication:url"] + "/api/weixin";
        }

        public async Task<Page<WxAccountListDto>> GetPageAsync(int pageIndex, int pageSize)
        {
            var uri = API.Account.GetPageAsync(_baseUrl, pageIndex, pageSize);
            var responseString = await _apiClient.GetStringAsync(uri);
            return responseString.ToObject<Page<WxAccountListDto>>();
        }
    }
}
