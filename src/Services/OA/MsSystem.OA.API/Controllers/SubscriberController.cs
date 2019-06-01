using DotNetCore.CAP;
using JadeFramework.WorkFlow;
using Microsoft.AspNetCore.Authorization;
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

        /// <summary>
        /// CAP 改变OA系统表单流程状态
        /// </summary>
        /// <param name="statusChange"></param>
        /// <returns></returns>
        [NonAction]
        [CapSubscribe("WorkFlowStatusChangedOA")]
        public async Task ChangeTableStatusAsync(WorkFlowStatusChange statusChange)
        {
            await workFlowService.ChangeTableStatusAsync(statusChange);
        }

    }
}
