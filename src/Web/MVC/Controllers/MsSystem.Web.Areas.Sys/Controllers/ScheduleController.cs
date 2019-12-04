using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MsSystem.Web.Areas.Sys.Service;
using MsSystem.Web.Areas.Sys.ViewModel;
using System.Threading.Tasks;

namespace MsSystem.Web.Areas.Sys.Controllers
{
    [Area("Sys")]
    public class ScheduleController : Controller
    {
        private readonly IScheduleService scheduleService;

        public ScheduleController(IScheduleService scheduleService)
        {
            this.scheduleService = scheduleService;
        }


        public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 10)
        {
            var page = await scheduleService.GetPageListAsync(pageIndex, pageSize);
            return View(page);
        }

        public async Task<IActionResult> Show(long? id)
        {
            ScheduleDto schedule;
            if (id == null || id.Value <= 0)
            {
                schedule = new ScheduleDto();
            }
            else
            {
                schedule = await scheduleService.GetScheduleAsync(id.Value);
            }
            return View(schedule);
        }

        /// <summary>
        /// 保存调度更新
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveAsync")]
        public async Task<bool> SaveAsync(ScheduleDto data)
        {
            return await scheduleService.AddOrUpdateAsync(data);
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


        /// <summary>
        /// CRON选择
        /// </summary>
        /// <returns></returns>
        public IActionResult Cron()
        {
            return View();
        }
    }
}
