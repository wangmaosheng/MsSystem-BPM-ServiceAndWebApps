using JadeFramework.Core.Domain.Entities;
using JadeFramework.Core.Domain.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MsSystem.OA.IService;
using MsSystem.OA.ViewModel;
using System.Threading.Tasks;

namespace MsSystem.OA.API.Controllers
{
    /// <summary>
    /// 员工请假
    /// </summary>
    [Authorize]
    [Route("api/Leave/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class LeaveController : ControllerBase
    {
        private readonly IOaLeaveService leaveService;

        public LeaveController(IOaLeaveService leaveService)
        {
            this.leaveService = leaveService;
        }

        [HttpGet]
        [ActionName("GetPageAsync")]
        public async Task<Page<OaLeaveDto>> GetPageAsync(int pageIndex, int pageSize, long userid)
        {
            return await leaveService.GetPageAsync(pageIndex, pageSize, userid);
        }
        [HttpGet]
        [ActionName("GetAsync")]
        public async Task<OaLeaveShowDto> GetAsync(long id)
        {
            return await leaveService.GetAsync(id);
        }
        [HttpPost]
        [ActionName("InsertAsync")]
        public async Task<AjaxResult> InsertAsync([FromBody]OaLeaveShowDto entity)
        {
            return await leaveService.InsertAsync(entity);
        }
        [HttpPost]
        [ActionName("UpdateAsync")]
        public async Task<AjaxResult> UpdateAsync([FromBody]OaLeaveShowDto entity)
        {
            return await leaveService.UpdateAsync(entity);
        }
    }
}
