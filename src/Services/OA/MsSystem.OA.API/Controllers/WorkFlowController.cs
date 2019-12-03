using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MsSystem.OA.IService;
using MsSystem.OA.ViewModel;
using System;
using System.Collections.Generic;
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


        /// <summary>
        /// 获取节点人员信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("GetFlowNodeInfo")]
        public async Task<List<long>> GetFlowNodeInfo([FromBody]FlowViewModel model)
        {
            return await flowService.GetFlowNodeInfo(model);
        }

        /// <summary>
        /// 获取最终的节点ID
        /// </summary>
        /// <param name="model">连线条件字典集合</param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("GetFinalNodeId")]
        public async Task<Guid?> GetFinalNodeId([FromBody]FlowLineFinalNodeDto model)
        {
            return await flowService.GetFinalNodeId(model);
        }
    }
}
