using JadeFramework.Core.Domain.Entities;
using JadeFramework.Core.Domain.Enum;
using JadeFramework.Core.Mvc;
using JadeFramework.WorkFlow;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MsSystem.Utility.Filters;
using MsSystem.Web.Areas.WF.Service;
using MsSystem.Web.Areas.WF.ViewModel;
using System;
using System.Threading.Tasks;

namespace MsSystem.Web.Areas.WF.Controllers
{
    [Area("WF")]
    [Authorize]
    public class WorkFlowInstanceController : BaseController
    {
        private readonly IWorkFlowInstanceService _workFlowInstanceService;
        private readonly IWorkflowCategoryService _categoryService;
        private readonly IWorkFlowService _workFlowService;

        public WorkFlowInstanceController(IWorkFlowInstanceService workFlowInstanceService,
            IWorkflowCategoryService categoryService,
            IWorkFlowService workFlowService)
        {
            this._workFlowInstanceService = workFlowInstanceService;
            this._categoryService = categoryService;
            this._workFlowService = workFlowService;
        }

        #region 我的审批历史记录

        /// <summary>
        /// 我的审批历史记录
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [Permission("/WF/WorkFlowInstance/MyApprovalHistory", ButtonType.View)]
        public async Task<IActionResult> MyApprovalHistory(int pageIndex = 1, int pageSize = 10)
        {
            var page = await _workFlowInstanceService.GetMyApprovalHistoryAsync(pageIndex, pageSize, UserIdentity.UserId.ToString());
            return View(page);
        }

        #endregion


        #region 我的流程

        [HttpGet]
        [Permission("/WF/WorkFlowInstance/MyFlow", ButtonType.View)]
        public async Task<IActionResult> MyFlow(int pageIndex = 1, int pageSize = 10)
        {
            var page = await _workFlowInstanceService.GetUserWorkFlowPageAsync(pageIndex, pageSize, UserIdentity.UserId.ToString());
            return View(page);
        }

        #endregion


        #region 发起流程


        [HttpGet]
        [Permission("/WF/WorkFlowInstance/Start", ButtonType.View)]
        public IActionResult Start()
        {
            return View();
        }

        /// <summary>
        /// 开始流程实例
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("CreateInstanceAsync")]
        public async Task<WorkFlowResult> CreateInstanceAsync([FromBody]WorkFlowProcessTransition model)
        {
            model.UserId = UserIdentity.UserId.ToString();
            model.UserName = UserIdentity.UserName;
            var res = await _workFlowInstanceService.CreateInstanceAsync(model);
            return res;
        }


        public async Task<IActionResult> Process(Guid flowid, Guid? instanceid)
        {
            WorkFlowProcess process = new WorkFlowProcess
            {
                UserId = UserIdentity.UserId.ToString(),
                FlowId = flowid,
                InstanceId = instanceid ?? default(Guid)
            };
            var res = await _workFlowInstanceService.GetProcessAsync(process);
            return View(res);
        }

        [HttpPost]
        [ActionName("ProcessTransitionFlowAsync")]
        public async Task<WorkFlowResult> ProcessTransitionFlowAsync([FromBody]WorkFlowProcessTransition model)
        {
            model.UserId = UserIdentity.UserId.ToString();
            model.UserName = UserIdentity.UserName;
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
            model.UserId = UserIdentity.UserId.ToString();
            return await _workFlowInstanceService.GetFlowApprovalAsync(model);
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
            addProcess.UserId = UserIdentity.UserId.ToString();
            addProcess.UserName = UserIdentity.UserName;
            return await _workFlowInstanceService.AddOrUpdateCustomFlowFormAsync(addProcess);
        }

        #endregion

        #region 流程待办


        /// <summary>
        /// 流程待办
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [Permission("/WF/WorkFlowInstance/TodoList", ButtonType.View)]
        public async Task<IActionResult> TodoList(int pageIndex = 1, int pageSize = 10)
        {
            WorkFlowTodoSearchDto searchDto = new WorkFlowTodoSearchDto
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                UserId = UserIdentity.UserId.ToString()
            };
            var page = await _workFlowInstanceService.GetUserTodoListAsync(searchDto);
            return View(page);
        }

        /// <summary>
        /// 首页待办获取
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<Page<UserWorkFlowDto>> HomeTodoList()
        {
            WorkFlowTodoSearchDto searchDto = new WorkFlowTodoSearchDto
            {
                PageIndex = 1,
                PageSize = 10,
                UserId = UserIdentity.UserId.ToString()
            };
            var page = await _workFlowInstanceService.GetUserTodoListAsync(searchDto);
            return page;
        }

        #endregion

        #region 用户流程操作历史记录

        /// <summary>
        /// 用户流程操作历史记录
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [Permission("/WF/WorkFlowInstance/OperationHistory", ButtonType.View)]
        public async Task<IActionResult> OperationHistory(int pageIndex = 1, int pageSize = 10)
        {
            WorkFlowOperationHistorySearchDto searchDto = new WorkFlowOperationHistorySearchDto
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                UserId = UserIdentity.UserId.ToString()
            };
            var page = await _workFlowInstanceService.GetUserOperationHistoryAsync(searchDto);
            return View(page);
        }
        #endregion


        /// <summary>
        /// 流程图
        /// </summary>
        /// <param name="instanceId">实例ID</param>
        /// <returns></returns>
        public async Task<IActionResult> FlowImage(Guid flowid, Guid? instanceId)
        {
            var instance = await _workFlowInstanceService.GetFlowImageAsync(flowid, instanceId);
            return View(instance);
        }

        /// <summary>
        /// 流程催办
        /// </summary>
        /// <param name="urge"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("UrgeAsync")]
        public async Task<WorkFlowResult> UrgeAsync([FromBody]UrgeDto urge)
        {
            urge.Sender = UserIdentity.UserId.ToString();
            return await _workFlowInstanceService.UrgeAsync(urge);
        }
    }
}
