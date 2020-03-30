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
        [ActionName("GetPageAsync")]
        public async Task<Page<WfWorkflow>> GetPageAsync(int pageIndex, int pageSize)
        {
            return await workFlowService.GetPageAsync(pageIndex, pageSize);
        }

        [HttpGet]
        [ActionName("GetByIdAsync")]
        public async Task<WorkFlowDetailDto> GetByIdAsync(Guid id)
        {
            return await workFlowService.GetByIdAsync(id);
        }

        [HttpPost]
        [ActionName("InsertAsync")]
        public async Task<bool> InsertAsync([FromBody]WorkFlowDetailDto workflow)
        {
            return await workFlowService.InsertAsync(workflow);
        }

        [HttpPost]
        [ActionName("UpdateAsync")]
        public async Task<bool> UpdateAsync([FromBody]WorkFlowDetailDto workflow)
        {
            return await workFlowService.UpdateAsync(workflow);
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("DeleteAsync")]
        public async Task<bool> DeleteAsync([FromBody]FlowDeleteDTO dto)
        {
            return await workFlowService.DeleteAsync(dto);
        }

        [HttpGet]
        [ActionName("GetWorkFlowStartAsync")]
        public async Task<List<WorkFlowStartDto>> GetWorkFlowStartAsync([FromQuery]Guid categoryid)
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

        /// <summary>
        /// new workflow version
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("NewVersionAsync")]
        public async Task<bool> NewVersionAsync([FromBody]WorkFlowDetailDto dto)
        {
            return await workFlowService.NewVersionAsync(dto);
        }
    }
}
