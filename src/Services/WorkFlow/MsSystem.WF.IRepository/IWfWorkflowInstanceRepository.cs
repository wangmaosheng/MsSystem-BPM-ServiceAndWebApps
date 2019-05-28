using JadeFramework.Core.Domain.Entities;
using JadeFramework.Dapper;
using MsSystem.WF.Model;
using MsSystem.WF.ViewModel;
using System.Threading.Tasks;

namespace MsSystem.WF.IRepository
{
    public interface IWfWorkflowInstanceRepository : IDapperRepository<WfWorkflowInstance>
    {
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
        /// 获取用户发起的流程
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<Page<UserWorkFlowDto>> GetUserWorkFlowPageAsync(int pageIndex, int pageSize, string userId);

        /// <summary>
        /// 获取我的审批历史记录
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<Page<UserWorkFlowDto>> GetMyApprovalHistoryAsync(int pageIndex, int pageSize, string userId);
    }
}
