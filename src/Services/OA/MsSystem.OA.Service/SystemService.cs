using Microsoft.Extensions.Options;
using MsSystem.OA.IService;
using MsSystem.OA.ViewModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MsSystem.OA.Service
{
    public class SystemService : ISystemService
    {
        private readonly HttpClient _apiClient;
        private readonly IOptions<AppSettings> _appSettings;
        public SystemService(HttpClient apiClient, IOptions<AppSettings> appSettings)
        {
            _apiClient = apiClient;
            _appSettings = appSettings;
        }
        public async Task<List<SysUser>> GetAllUserAsync()
        {
            string url = _appSettings.Value.MsApplication.url + _appSettings.Value.SystemAPI.GetAllUserAsync;
            var responseString = await _apiClient.GetStringAsync(url);
            return JsonConvert.DeserializeObject<List<SysUser>>(responseString);
        }

    }
}
