using JadeFramework.Core.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MsSystem.Sys.IService;
using MsSystem.Sys.Models;
using System.Threading.Tasks;

namespace MsSystem.Sys.API.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/Schedule/[action]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly ISysScheduleService scheduleService;

        public ScheduleController(ISysScheduleService scheduleService)
        {
            this.scheduleService = scheduleService;
        }

        [HttpGet]
        [ActionName("GetScheduleAsync")]
        public async Task<SysSchedule> GetScheduleAsync(long id)
        {
            return await scheduleService.GetScheduleAsync(id);
        }

        [HttpGet]
        [ActionName("GetPageListAsync")]
        public async Task<Page<SysSchedule>> GetPageListAsync(int pageIndex, int pageSize)
        {
            return await scheduleService.GetPageListAsync(pageIndex, pageSize);
        }

        [HttpPost]
        [ActionName("AddOrUpdateAsync")]
        public async Task<bool> AddOrUpdateAsync([FromBody]SysSchedule schedule)
        {
            return await scheduleService.AddOrUpdateAsync(schedule);
        }

        /// <summary>
        /// 启动调度（不执行）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("StartAsync")]
        public async Task<bool> StartAsync(long id)
        {
            return await scheduleService.StartAsync(id);
        }

        /// <summary>
        /// 停止调度
        /// </summary>
        /// <param name="id">任务ID</param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("StopAsync")]
        public async Task<bool> StopAsync(long id)
        {
            return await scheduleService.StopAsync(id);
        }

        /// <summary>
        /// 立即执行任务
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("ExecuteJobAsync")]
        public async Task<bool> ExecuteJobAsync(long id)
        {
            return await scheduleService.ExecuteJobAsync(id);
        }

        /// <summary>
        /// 暂停任务
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SuspendAsync")]
        public async Task<bool> SuspendAsync(long id)
        {
            return await scheduleService.SuspendAsync(id);
        }
    }
}
