using JadeFramework.Core.Domain.Entities;
using MsSystem.Sys.IRepository;
using MsSystem.Sys.IService;
using MsSystem.Sys.Model;
using System.Threading.Tasks;

namespace MsSystem.Sys.Service
{
    /// <summary>
    /// 发布日志服务
    /// </summary>
    public class SysReleaseLogService : ISysReleaseLogService
    {
        private ISysDatabaseFixture _databaseFixture;
        private ILogJobs _logJobs;
        public SysReleaseLogService(ISysDatabaseFixture databaseFixture, ILogJobs logJobs)
        {
            _databaseFixture = databaseFixture;
            _logJobs = logJobs;
        }

        /// <summary>
        /// 发布日志分页获取
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public async Task<Page<SysReleaseLog>> GetPageAsync(int pageIndex, int pageSize)
        {
            return await _databaseFixture.Db.SysReleaseLog.GetPageAsync(pageIndex, pageSize);
        }


    }
}
