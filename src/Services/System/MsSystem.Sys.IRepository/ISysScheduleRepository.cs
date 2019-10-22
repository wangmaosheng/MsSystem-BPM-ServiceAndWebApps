using JadeFramework.Core.Domain.Entities;
using JadeFramework.Dapper;
using MsSystem.Sys.Models;
using System.Threading.Tasks;

namespace MsSystem.Sys.IRepository
{
    public interface ISysScheduleRepository : IDapperRepository<SysSchedule>
    {
        /// <summary>
        /// 分页获取调度
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<Page<SysSchedule>> GetPageListAsync(int pageIndex, int pageSize);
        Task<bool> AddSceduleAsync(SysSchedule schedule);
        Task<bool> UpdateScheduleAsync(SysSchedule schedule);

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        Task<bool> UpdateStatusAsync(long id, int status);

        /// <summary>
        /// 更新任务状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="jobstatus"></param>
        /// <returns></returns>
        Task<bool> UpdateJobStatusAsync(long id, int jobstatus);
    }
}
