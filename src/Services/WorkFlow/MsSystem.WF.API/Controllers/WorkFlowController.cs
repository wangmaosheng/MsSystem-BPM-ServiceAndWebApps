using JadeFramework.Core.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MsSystem.WF.IService;
using MsSystem.WF.Model;
using MsSystem.WF.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MsSystem.WF.API.Controllers
{
    [Authorize]
    [Route("api/WorkFlow/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class WorkFlowController : ControllerBase
    {
        private readonly IWorkFlowService workFlowService;

        public WorkFlowController(IWorkFlowService workFlowService)
        {
            this.workFlowService = workFlowService;
        }

        [HttpGet]
        public async Task<Page<WfWorkflow>> GetPageAsync(int pageIndex, int pageSize)
        {
            return await workFlowService.GetPageAsync(pageIndex, pageSize);
        }

        [HttpGet]
        public async Task<WorkFlowDetailDto> GetByIdAsync(Guid id)
        {
            return await workFlowService.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<bool> InsertAsync([FromBody]WorkFlowDetailDto workflow)
        {
            return await workFlowService.InsertAsync(workflow);
        }

        [HttpPost]
        public async Task<bool> UpdateAsync([FromBody]WorkFlowDetailDto workflow)
        {
            return await workFlowService.UpdateAsync(workflow);
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> DeleteAsync([FromBody]FlowDeleteDTO dto)
        {
            return await workFlowService.DeleteAsync(dto);
        }

        [HttpGet]
        public async Task<List<WorkFlowStartDto>> GetWorkFlowStartAsync(Guid categoryid)
        {
            return await workFlowService.GetWorkFlowStartAsync(categoryid);
        }

        //[HttpGet]
        //public async Task<List<WorkFlowLineDto>> GetAllLinesAsync()
        //{
        //    return await workFlowService.GetAllLinesAsync();
        //}

        //[HttpGet]
        //public async Task<WorkFlowLineDto> GetLineAsync(Guid lineid)
        //{
        //    return await workFlowService.GetLineAsync(lineid);
        //}
    }
}
