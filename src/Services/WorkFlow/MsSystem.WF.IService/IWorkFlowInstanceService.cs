using JadeFramework.Core.Domain.Entities;
using JadeFramework.WorkFlow;
using MsSystem.WF.ViewModel;
using System;
using System.Threading.Tasks;

namespace MsSystem.WF.IService
{
    public interface IWorkFlowInstanceService
    {
        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<WorkFlowResult> CreateInstanceAsync(WorkFlowProcessTransition model);

        /// <summary>
        /// 获取用户待办事项
        /// </summary>
        /// <param name="searchDto"></param>
        /// <returns></returns>
        Task<Page<UserWorkFlowDto>> GetUserTodoListAsync(WorkFlowTodoSearchDto searchDto);

        /// <summary>
        /// 获取用户流程操作历史记录
        /// </summary>
        /// <param name="searchDto"></param>
        /// <returns></returns>
        Task<Page<WorkFlowOperationHistoryDto>> GetUserOperationHistoryAsync(WorkFlowOperationHistorySearchDto searchDto);

        /// <summary>
        /// 添加或修改自定义表单数据
        /// </summary>
        /// <param name="addProcess"></param>
        /// <returns></returns>
        Task<WorkFlowResult> AddOrUpdateCustomFlowFormAsync(WorkFlowProcess addProcess);

        /// <summary>
        /// get workflow process
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        Task<WorkFlowProcess> GetProcessAsync(WorkFlowProcess process);

        /// <summary>
        /// 系统定制流程获取
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<WorkFlowProcess> GetProcessForSystemAsync(SystemFlowDto model);

        /// <summary>
        /// 获取用户发起的流程
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<Page<UserWorkFlowDto>> GetUserWorkFlowPageAsync(int pageIndex, int pageSize, string userId);

        /// <summary>
        /// 流程过程流转处理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<WorkFlowResult> ProcessTransitionFlowAsync(WorkFlowProcessTransition model);

        /// <summary>
        /// 获取审批意见
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<WorkFlowResult> GetFlowApprovalAsync(WorkFlowProcessTransition model);

        /// <summary>
        /// 获取我的审批历史记录
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<Page<UserWorkFlowDto>> GetMyApprovalHistoryAsync(int pageIndex, int pageSize, string userId);

        /// <summary>
        /// 获取流程图信息
        /// </summary>
        /// <param name="instanceId">实例ID</param>
        /// <returns></returns>
        Task<WorkFlowImageDto> GetFlowImageAsync(Guid flowid, Guid? instanceId);

        /// <summary>
        /// 流程催办
        /// </summary>
        /// <param name="urge"></param>
        /// <returns></returns>
        Task<WorkFlowResult> UrgeAsync(UrgeDto urge);
    }
}
