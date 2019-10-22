using JadeFramework.Core.Domain.Entities;
using MsSystem.Sys.Models;
using MsSystem.Sys.Schedule.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MsSystem.Sys.IService
{
    public interface ISysScheduleService
    {
        /// <summary>
        /// 根据id获取调度
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<SysSchedule> GetScheduleAsync(long id);

        /// <summary>
        /// 添加或修改调度
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        Task<bool> AddOrUpdateAsync(SysSchedule schedule);

        /// <summary>
        /// 获取全部调度
        /// </summary>
        /// <returns></returns>
        Task<List<ScheduleEntity>> GetEntityListAsync();

        /// <summary>
        /// 分页获取调度
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<Page<SysSchedule>> GetPageListAsync(int pageIndex, int pageSize);

        /// <summary>
        /// 调度初始化
        /// </summary>
        /// <returns></returns>
        Task Initialize();

        /// <summary>
        /// 启动调度（不执行）
        /// </summary>
        /// <param name="id"></param>
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
}
