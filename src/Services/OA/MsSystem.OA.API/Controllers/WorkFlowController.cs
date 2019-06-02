using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MsSystem.OA.IService;
using System.Threading.Tasks;

namespace MsSystem.OA.API.Controllers
{
    [Authorize]
    [Route("api/WorkFlow/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class WorkFlowController : ControllerBase
    {
        private readonly IWorkFlowService flowService;

        public WorkFlowController(IWorkFlowService flowService)
        {
            this.flowService = flowService;
        }


        [HttpGet]
        public async Task<string> WorkFlowSelectInfoAsync(string sql)
        {
            return await flowService.WorkFlowSelectInfoAsync(sql);
        }
    }
}
