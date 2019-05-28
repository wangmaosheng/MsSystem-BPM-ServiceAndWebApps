using DotNetCore.CAP;
using JadeFramework.WorkFlow;
using Microsoft.AspNetCore.Mvc;
using MsSystem.OA.IService;
using System.Threading.Tasks;

namespace MsSystem.OA.API.Controllers
{
    /// <summary>
    /// 消息订阅
    /// </summary>
    public class SubscriberController : ControllerBase
    {
        private readonly IWorkFlowService workFlowService;

        public SubscriberController(IWorkFlowService workFlowService)
        {
            this.workFlowService = workFlowService;
        }

        [NonAction]
        [CapSubscribe("WorkFlowStatusChangedOA")]
        public async Task ChangeTableStatusAsync(WorkFlowStatusChange statusChange)
        {
            await workFlowService.ChangeTableStatusAsync(statusChange);
        }

    }
}
