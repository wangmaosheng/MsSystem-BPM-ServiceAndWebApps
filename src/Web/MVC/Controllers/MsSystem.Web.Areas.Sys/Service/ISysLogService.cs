using JadeFramework.Core.Domain.Entities;
using JadeFramework.Core.Domain.Enum;
using JadeFramework.Core.Extensions;
using Microsoft.Extensions.Configuration;
using MsSystem.Web.Areas.Sys.Infrastructure;
using MsSystem.Web.Areas.Sys.Model;
using MsSystem.Web.Areas.Sys.ViewModel;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MsSystem.Web.Areas.Sys.Service
{
    public interface ISysLogService
    {
        Task<Page<SysLog>> GetPageAsync(LogSearchDto model);
        Task<Dictionary<object, object>> GetChartsAsync(LogLevel level);
    }
    public class SysLogService : ISysLogService
    {
        private readonly HttpClient _apiClient;
        private readonly IConfiguration _configuration;
        private readonly string _logUrl;

        public SysLogService(HttpClient httpClient, IConfiguration configuration)
        {
            _apiClient = httpClient;
            _configuration = configuration;
            _logUrl = configuration["MsApplication:url"] + "/api/sys";
        }

        public async Task<Dictionary<object, object>> GetChartsAsync(LogLevel level)
        {
            var uri = API.SysLog.GetChartsAsync(_logUrl, level);
            var responseString = await _apiClient.GetStringAsync(uri);
            return responseString.ToObject<Dictionary<object, object>>();
        }

        public async Task<Page<SysLog>> GetPageAsync(LogSearchDto model)
        {
            var uri = API.SysLog.GetPageAsync(_logUrl, model);
            var responseString = await _apiClient.GetStringAsync(uri);
            return responseString.ToObject<Page<SysLog>>();
        }
    }
}
