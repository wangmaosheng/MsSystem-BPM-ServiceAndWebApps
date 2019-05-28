using JadeFramework.Core.Domain.Entities;
using JadeFramework.Core.Extensions;
using Microsoft.Extensions.Configuration;
using MsSystem.Web.Areas.Sys.Infrastructure;
using MsSystem.Web.Areas.Sys.Model;
using System.Net.Http;
using System.Threading.Tasks;

namespace MsSystem.Web.Areas.Sys.Service
{
    /// <summary>
    /// 发布日志服务接口
    /// </summary>
    public interface ISysReleaseLogService
    {
        /// <summary>
        /// 发布日志分页获取
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        Task<Page<SysReleaseLog>> GetPageAsync(int pageIndex, int pageSize);
    }
    public class SysReleaseLogService : ISysReleaseLogService
    {
        private readonly HttpClient _apiClient;
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl;

        public SysReleaseLogService(HttpClient httpClient, IConfiguration configuration)
        {
            _apiClient = httpClient;
            _configuration = configuration;
            _baseUrl = configuration["MsApplication:url"] + "/api/sys";
        }

        public async Task<Page<SysReleaseLog>> GetPageAsync(int pageIndex, int pageSize)
        {
            var uri = API.SysReleaseLog.GetPageAsync(_baseUrl, pageIndex, pageSize);
            var responseString = await _apiClient.GetStringAsync(uri);
            return responseString.ToObject<Page<SysReleaseLog>>();
        }

    }
}
