using JadeFramework.Core.Domain.Entities;
using JadeFramework.Core.Extensions;
using Microsoft.Extensions.Configuration;
using MsSystem.Web.Areas.Sys.Infrastructure;
using MsSystem.Web.Areas.Sys.ViewModel;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace MsSystem.Web.Areas.Sys.Service
{
    /// <summary>
    /// 调度服务
    /// </summary>
    public interface IScheduleService
    {
        Task<Page<ScheduleDto>> GetPageListAsync(int pageIndex, int pageSize);
        Task<bool> AddOrUpdateAsync(ScheduleDto schedule);
        Task<ScheduleDto> GetScheduleAsync(long id);

        /// <summary>
        /// 启动调度（不执行）
        /// </summary>
        /// <param name="id">任务ID</param>
        /// <returns></returns>
        Task<bool> StartAsync(long id);

        /// <summary>
        /// 停止调度
        /// </summary>
        /// <param name="id">任务ID</param>
        /// <returns></returns>
        Task<bool> StopAsync(long id);

        /// <summary>
        /// 立即执行任务
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> ExecuteJobAsync(long id);

        /// <summary>
        /// 暂停任务
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> SuspendAsync(long id);
    }

    public class ScheduleService : IScheduleService
    {
        private readonly HttpClient _apiClient;
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl;

        public ScheduleService(HttpClient httpClient, IConfiguration configuration)
        {
            _apiClient = httpClient;
            _configuration = configuration;
            _baseUrl = configuration["MsApplication:url"] + "/api/Sys";
        }
        public async Task<Page<ScheduleDto>> GetPageListAsync(int pageIndex, int pageSize)
        {
            var uri = API.Schedule.GetPageListAsync(_baseUrl, pageIndex, pageSize);
            var responseString = await _apiClient.GetStringAsync(uri);
            return responseString.ToObject<Page<ScheduleDto>>();
        }
        public async Task<bool> AddOrUpdateAsync(ScheduleDto schedule)
        {
            var uri = API.Schedule.AddOrUpdateAsync(_baseUrl);
            var content = new StringContent(JsonConvert.SerializeObject(schedule), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            return true;
        }

        public async Task<ScheduleDto> GetScheduleAsync(long id)
        {
            var uri = API.Schedule.GetScheduleAsync(_baseUrl, id);
            var responseString = await _apiClient.GetStringAsync(uri);
            return responseString.ToObject<ScheduleDto>();
        }
        public async Task<bool> StartAsync(long id)
        {
            var uri = API.Schedule.StartAsync(_baseUrl);
            var content = new StringContent(id.ToString(), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            return true;
        }
        public async Task<bool> StopAsync(long id)
        {
            var uri = API.Schedule.StopAsync(_baseUrl);
            var content = new StringContent(id.ToString(), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            return true;
        }
        public async Task<bool> ExecuteJobAsync(long id)
        {
            var uri = API.Schedule.ExecuteJobAsync(_baseUrl);
            var content = new StringContent(id.ToString(), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            return true;
        }
        public async Task<bool> SuspendAsync(long id)
        {
            var uri = API.Schedule.SuspendAsync(_baseUrl);
            var content = new StringContent(id.ToString(), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            return true;
        }

    }
}
