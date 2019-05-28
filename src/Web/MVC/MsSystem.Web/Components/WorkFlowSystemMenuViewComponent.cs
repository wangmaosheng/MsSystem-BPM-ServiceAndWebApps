using JadeFramework.Core.Mvc.Extensions;
using JadeFramework.WorkFlow;
using Microsoft.AspNetCore.Mvc;
using MsSystem.Web.Areas.WF.Service;
using System.Threading.Tasks;

namespace MsSystem.Web.Components
{
    public class WorkFlowSystemMenuViewComponent : ViewComponent
    {
        private readonly IWorkFlowInstanceService workFlowInstanceService;

        public WorkFlowSystemMenuViewComponent(IWorkFlowInstanceService workFlowInstanceService)
        {
            this.workFlowInstanceService = workFlowInstanceService;
        }

        public async Task<IViewComponentResult> InvokeAsync(SystemFlowDto model)
        {
            model.UserId = HttpContext.User.ToUserIdentity().UserId.ToString();
            var process = await workFlowInstanceService.GetProcessForSystemAsync(model);
            process.FormContent = model.PageId;
            return View(process);
        }
    }
}
