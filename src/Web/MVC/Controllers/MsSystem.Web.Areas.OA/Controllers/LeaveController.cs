using JadeFramework.Core.Domain.Enum;
using JadeFramework.Core.Domain.Result;
using JadeFramework.Core.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MsSystem.Utility.Filters;
using MsSystem.Web.Areas.OA.Service;
using MsSystem.Web.Areas.OA.ViewModel;
using System;
using System.Threading.Tasks;

namespace MsSystem.Web.Areas.OA.Controllers
{
    /// <summary>
    /// 员工请假
    /// </summary>
    [Area("OA")]
    [Authorize]
    public class LeaveController : BaseController
    {
        private readonly IOaLeaveService leaveService;

        public LeaveController(IOaLeaveService leaveService)
        {
            this.leaveService = leaveService;
        }

        /// <summary>
        /// 请假列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Permission]
        public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 10)
        {
            var page = await leaveService.GetPageAsync(pageIndex, pageSize, UserIdentity.UserId);
            return View(page);
        }

        [HttpGet]
        [Permission("/OA/Leave/Index", ButtonType.View)]
        public async Task<IActionResult> Show(int? id)
        {
            OaLeaveShowDto model;
            if (id == null || id == 0)
            {
                model = new OaLeaveShowDto()
                {
                    StartTime = DateTime.Now.AddDays(1),
                    EndTime = DateTime.Now.AddDays(2)
                };
            }
            else
            {
                model = await leaveService.GetAsync(id.Value);
            }
            return View(model);
        }

        [HttpPost]
        [Permission("/OA/Leave/Index", ButtonType.Add, false)]
        [ActionName("InsertAsync")]
        public async Task<AjaxResult> InsertAsync([FromBody]OaLeaveShowDto entity)
        {
            entity.CreateUserId = (int)UserIdentity.UserId;
            return await leaveService.InsertAsync(entity);
        }

        [HttpPost]
        [Permission("/OA/Leave/Index", ButtonType.Edit, false)]
        [ActionName("UpdateAsync")]
        public async Task<AjaxResult> UpdateAsync([FromBody]OaLeaveShowDto entity)
        {
            return await leaveService.UpdateAsync(entity);
        }
    }
}
