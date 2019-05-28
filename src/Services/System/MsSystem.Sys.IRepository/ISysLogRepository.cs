using JadeFramework.Core.Domain.Entities;
using JadeFramework.Core.Domain.Enum;
using JadeFramework.Dapper;
using MsSystem.Sys.Model;
using MsSystem.Sys.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MsSystem.Sys.IRepository
{
    public interface ISysLogRepository : IDapperRepository<SysLog>
    {
        Task<Page<SysLog>> GetPageAsync(LogSearchDto model);
        /// <summary>
        /// 根据日志级别获取日志统计信息
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        Task<Dictionary<object, object>> GetChartsAsync(LogLevel level);

        Task<HeartBeatData> GetLasterDataAsync(int recentMinutes, Application application);
    }
}
