using JadeFramework.WorkFlow;
using Microsoft.AspNetCore.Mvc;
using MsSystem.Web.Areas.WF.Service;
using System.Threading.Tasks;

namespace MsSystem.Web.Components
{
    public class WorkFlowMenuViewComponent : ViewComponent
    {
        private readonly IWorkFlowInstanceService workFlowInstanceService;

        public WorkFlowMenuViewComponent(IWorkFlowInstanceService workFlowInstanceService)
        {
            this.workFlowInstanceService = workFlowInstanceService;
        }

        public async Task<IViewComponentResult> InvokeAsync(WorkFlowProcess process)
        {
            return View(process);
        }
    }
}
