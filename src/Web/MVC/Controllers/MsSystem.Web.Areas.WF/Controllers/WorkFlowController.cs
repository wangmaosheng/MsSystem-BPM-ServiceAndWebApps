using JadeFramework.Core.Domain.Entities;
using JadeFramework.Core.Domain.Enum;
using JadeFramework.Core.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MsSystem.Utility.Filters;
using MsSystem.Web.Areas.WF.Service;
using MsSystem.Web.Areas.WF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MsSystem.Web.Areas.WF.Controllers
{
    [Area("WF")]
    [Authorize]
    public class WorkFlowController : BaseController
    {
        private readonly IWorkFlowService workFlowService;

        public WorkFlowController(IWorkFlowService workFlowService)
        {
            this.workFlowService = workFlowService;
        }

        [HttpGet]
        [Permission]
        public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 10)
        {
            var page = await workFlowService.GetPageAsync(pageIndex, pageSize);
            return View(page);
        }

        [HttpGet]
        [Permission("/WF/WorkFlow/Index", ButtonType.View, true)]
        public async Task<IActionResult> Show(Guid? id)
        {
            WorkFlowDetailDto workflow = id != null ? await workFlowService.GetByIdAsync(id.Value) : new WorkFlowDetailDto();
            return View(workflow);
        }

        [HttpPost]
        [Permission("/WF/WorkFlow/Index", ButtonType.Add, false)]
        [ActionName("InsertAsync")]
        public async Task<bool> InsertAsync(WorkFlowDetailDto workflow)
        {
            workflow.CreateUserId = UserIdentity.UserId.ToString();
            return await workFlowService.InsertAsync(workflow);
        }

        [HttpPost]
        [Permission("/WF/WorkFlow/Index", ButtonType.Edit, false)]
        [ActionName("UpdateAsync")]
        public async Task<bool> UpdateAsync(WorkFlowDetailDto workflow)
        {
            workflow.CreateUserId = UserIdentity.UserId.ToString();
            return await workFlowService.UpdateAsync(workflow);
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        [HttpPost]
        [Permission("/WF/WorkFlow/Index", ButtonType.Delete, false)]
        [ActionName("DeleteAsync")]
        public async Task<bool> DeleteAsync([FromBody]FlowDeleteDTO dto)
        {
            dto.UserId = UserIdentity.UserId;
            return await workFlowService.DeleteAsync(dto);
        }

        [HttpGet]
        [ActionName("GetWorkFlowStartAsync")]
        public async Task<List<WorkFlowStartDto>> GetWorkFlowStartAsync(Guid? categoryid)
        {
            return await workFlowService.GetWorkFlowStartAsync(categoryid ?? default(Guid));
        }

        [HttpGet]
        [ActionName("GetAllLinesAsync")]
        public async Task<List<ZTree>> GetAllLinesAsync()
        {
            var list = await workFlowService.GetAllLinesAsync();
            return list.Select(m => new ZTree()
            {
                id = m.LineId.ToString(),
                name = m.Name
            }).ToList();
        }

        [HttpGet]
        [ActionName("GetLineAsync")]
        public async Task<WorkFlowLineDto> GetLineAsync(Guid lineid)
        {
            return await workFlowService.GetLineAsync(lineid);
        }
        /// <summary>
        /// new workflow version
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("NewVersionAsync")]
        public async Task<bool> NewVersionAsync([FromBody]WorkFlowDetailDto dto)
        {
            dto.CreateUserId = UserIdentity.UserId.ToString();
            return await workFlowService.NewVersionAsync(dto);
        }
        
    }
}
