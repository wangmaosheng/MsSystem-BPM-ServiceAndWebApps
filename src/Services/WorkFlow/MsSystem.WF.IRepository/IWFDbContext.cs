using JadeFramework.Dapper.DbContext;

namespace MsSystem.WF.IRepository
{
    public interface IWFDbContext : IDapperDbContext
    {
        IWfWorkflowRepository Workflow { get; }
        IWfWorkflowFormRepository WorkflowForm { get; }
        IWfWorkflowInstanceRepository WorkflowInstance { get; }
        IWfWorkflowTransitionHistoryRepository WorkflowTransitionHistory { get; }
        IWfWorkflowOperationHistoryRepository WorkflowOperationHistory { get; }
        IWfWorkflowCategoryRepository WorkflowCategory { get; }
        IWfWorkflowInstanceFormRepository WorkflowInstanceForm { get; }
        IWfWorkflowsqlRepository WfWorkflowsql { get; }
        IWfWorkflowNoticeRepository WfWorkflowNotice { get; }
        IWfWorkflowUrgeRepository WfWorkflowUrge { get; }
        IWfWorkflowAssignRepository WfWorkflowAssign { get; }
    }
}
