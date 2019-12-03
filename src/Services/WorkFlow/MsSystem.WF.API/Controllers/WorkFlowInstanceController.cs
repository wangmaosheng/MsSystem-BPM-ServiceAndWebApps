using JadeFramework.Core.Domain.Entities;
using JadeFramework.Core.Extensions;
using JadeFramework.WorkFlow;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MsSystem.WF.IService;
using MsSystem.WF.Model;
using MsSystem.WF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MsSystem.WF.API.Controllers
{
    [Authorize]
    [Route("api/WorkFlowInstance/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class WorkFlowInstanceController : ControllerBase
    {
        private readonly IWorkFlowInstanceService _workFlowInstanceService;
        private readonly IConfigService _configService;

        public WorkFlowInstanceController(IWorkFlowInstanceService workFlowInstanceService, IConfigService configService)
        {
            this._workFlowInstanceService = workFlowInstanceService;
            this._configService = configService;
        }

        /// <summary>
        /// 创建流程实例
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("CreateInstanceAsync")]
        public async Task<WorkFlowResult> CreateInstanceAsync([FromBody]WorkFlowProcessTransition model)
        {
            return await _workFlowInstanceService.CreateInstanceAsync(model);
        }

        /// <summary>
        /// 获取用户待办事项
        /// </summary>
        /// <param name="searchDto"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetUserTodoListAsync")]
        public async Task<Page<UserWorkFlowDto>> GetUserTodoListAsync([FromQuery]WorkFlowTodoSearchDto searchDto)
        {
            return await _workFlowInstanceService.GetUserTodoListAsync(searchDto);
        }


        /// <summary>
        /// 获取用户流程操作历史记录
        /// </summary>
        /// <param name="searchDto"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetUserOperationHistoryAsync")]
        public async Task<Page<WorkFlowOperationHistoryDto>> GetUserOperationHistoryAsync([FromQuery]WorkFlowOperationHistorySearchDto searchDto)
        {
            return await _workFlowInstanceService.GetUserOperationHistoryAsync(searchDto);
        }

        /// <summary>
        /// 添加或修改自定义表单数据
        /// </summary>
        /// <param name="addProcess"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("AddOrUpdateCustomFlowFormAsync")]
        public async Task<WorkFlowResult> AddOrUpdateCustomFlowFormAsync([FromBody]WorkFlowProcess addProcess)
        {
            return await _workFlowInstanceService.AddOrUpdateCustomFlowFormAsync(addProcess);
        }

        /// <summary>
        /// get workflow process
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetProcessAsync")]
        public async Task<WorkFlowProcess> GetProcessAsync([FromQuery]WorkFlowProcess process)
        {
            return await _workFlowInstanceService.GetProcessAsync(process);
        }

        /// <summary>
        /// 系统定制流程获取
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetProcessForSystemAsync")]
        public async Task<WorkFlowProcess> GetProcessForSystemAsync([FromQuery]SystemFlowDto model)
        {
            return await _workFlowInstanceService.GetProcessForSystemAsync(model);
        }

        /// <summary>
        /// 获取用户发起的流程
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetUserWorkFlowPageAsync")]
        public async Task<Page<UserWorkFlowDto>> GetUserWorkFlowPageAsync(int pageIndex, int pageSize, string userId)
        {
            return await _workFlowInstanceService.GetUserWorkFlowPageAsync(pageIndex, pageSize, userId);
        }

        /// <summary>
        /// 流程过程流转处理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("ProcessTransitionFlowAsync")]
        public async Task<WorkFlowResult> ProcessTransitionFlowAsync([FromBody]WorkFlowProcessTransition model)
        {
            return await _workFlowInstanceService.ProcessTransitionFlowAsync(model);
        }
        /// <summary>
        /// 获取审批意见
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetFlowApprovalAsync")]
        public async Task<WorkFlowResult> GetFlowApprovalAsync([FromQuery]WorkFlowProcessTransition model)
        {
            return await _workFlowInstanceService.GetFlowApprovalAsync(model);
        }

        /// <summary>
        /// 获取我的审批历史记录
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetMyApprovalHistoryAsync")]
        public async Task<Page<UserWorkFlowDto>> GetMyApprovalHistoryAsync(int pageIndex, int pageSize, string userId)
        {
            return await _workFlowInstanceService.GetMyApprovalHistoryAsync(pageIndex, pageSize, userId);
        }

        /// <summary>
        /// 获取流程图信息
        /// </summary>
        /// <param name="instanceId">实例ID</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetFlowImageAsync")]
        public async Task<WorkFlowImageDto> GetFlowImageAsync(Guid flowid, Guid? instanceId)
        {
            return await _workFlowInstanceService.GetFlowImageAsync(flowid, instanceId);
        }

        /// <summary>
        /// 流程催办
        /// </summary>
        /// <param name="urge"></param>
        [HttpPost]
        [ActionName("UrgeAsync")]
        public async Task<WorkFlowResult> UrgeAsync([FromBody]UrgeDto urge)
        {
            var workflowResult = await _workFlowInstanceService.UrgeAsync(urge);
            if (!workflowResult.Result)
            {
                return workflowResult;
            }
            var urgeTypeArray = urge.UrgeType.Split(',').Where(m => !string.IsNullOrEmpty(m));
            List<long> urgeUserList = workflowResult.Data.ToString().Split(',')
                .Where(m => !string.IsNullOrEmpty(m))
                .Select(m => Convert.ToInt64(m)).ToList();
            MessagePushSomBodyDTO model = new MessagePushSomBodyDTO
            {
                UserIds = urgeUserList,
                Title = urge.Title,
                MsgJson = urge.UrgeContent,
                Sender = urge.Sender.ToInt64(),
                Link = urge.Link
            };
            foreach (var item in urgeTypeArray)
            {
                UrgeType urgeType = (UrgeType)item.ToInt32();
                switch (urgeType)
                {
                    case UrgeType.EMail:
                        UrgeSendEMail();
                        break;
                    case UrgeType.SMS:
                        UrgeSendSMS();
                        break;
                    case UrgeType.WeChat:
                        UrgeSendWeChat();
                        break;
                    case UrgeType.SignalR:
                    default:
                        await UrgeSendSignalR(model);
                        break;
                }
            }


            return WorkFlowResult.Success();
        }

        protected async Task UrgeSendSignalR(MessagePushSomBodyDTO model)
        {
            await _configService.UrgeSendSignalR(model);
        }
        protected void UrgeSendEMail()
        {

        }
        protected void UrgeSendSMS()
        {

        }
        protected void UrgeSendWeChat()
        {

        }
    }
}
