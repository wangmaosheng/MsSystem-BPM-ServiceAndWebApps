using JadeFramework.Core.Domain.Entities;
using JadeFramework.Core.Domain.Enum;
using MsSystem.Sys.IRepository;
using MsSystem.Sys.IService;
using MsSystem.Sys.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using MsSystem.Sys.ViewModel;

namespace MsSystem.Sys.Service
{
    public class SysLogService : ISysLogService
    {
        private ISysDatabaseFixture _databaseFixture;
        private ILogJobs _logJobs;
        public SysLogService(ISysDatabaseFixture databaseFixture, ILogJobs logJobs)
        {
            _databaseFixture = databaseFixture;
            _logJobs = logJobs;
        }

        public async Task<Page<SysLog>> GetPageAsync(LogSearchDto model)
        {
            return await _databaseFixture.LogDb.SysLog.GetPageAsync(model);
        }
        public async Task<Dictionary<object, object>> GetChartsAsync(LogLevel level)
        {
            var list = await _databaseFixture.LogDb.SysLog.GetChartsAsync(level);
            return list;
        }

        public async Task<HeartBeatData> GetLasterDataAsync(int recentMinutes, Application application)
        {
            return await _databaseFixture.LogDb.SysLog.GetLasterDataAsync(recentMinutes, application);
        }

    }

}
