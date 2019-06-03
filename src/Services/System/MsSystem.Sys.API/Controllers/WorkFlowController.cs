using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MsSystem.Sys.IService;
using MsSystem.Sys.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MsSystem.Sys.API.Controllers
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

        [HttpPost]
        public async Task<List<long>> GetFlowNodeInfo([FromBody]FlowViewModel model)
        {
            return await flowService.GetFlowNodeInfo(model);
        }
    }
}
