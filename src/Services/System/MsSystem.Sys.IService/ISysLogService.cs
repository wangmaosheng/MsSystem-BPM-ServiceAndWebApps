using JadeFramework.Core.Domain.Entities;
using JadeFramework.Core.Domain.Enum;
using MsSystem.Sys.Model;
using MsSystem.Sys.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MsSystem.Sys.IService
{
    public interface ISysLogService
    {
        Task<Page<SysLog>> GetPageAsync(LogSearchDto model);
        Task<Dictionary<object, object>> GetChartsAsync(LogLevel level);
        Task<HeartBeatData> GetLasterDataAsync(int recentMinutes, Application application);
    }
}
