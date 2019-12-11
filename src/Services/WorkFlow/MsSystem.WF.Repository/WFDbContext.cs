using JadeFramework.Dapper.DbContext;
using JadeFramework.Dapper.SqlGenerator;
using MsSystem.WF.IRepository;
using MySql.Data.MySqlClient;

namespace MsSystem.WF.Repository
{
    public class WFDbContext : DapperDbContext, IWFDbContext
    {
        private readonly SqlGeneratorConfig _config = new SqlGeneratorConfig
        {
            SqlConnector = ESqlConnector.MySQL,
            UseQuotationMarks = true
        };
        public WFDbContext(string connectionString)
            : base(new MySqlConnection(connectionString))
        {
        }

        private IWfWorkflowRepository _workflow;
        public IWfWorkflowRepository Workflow => _workflow ?? (_workflow = new WfWorkflowRepository(Connection, _config));

        private IWfWorkflowFormRepository _workflowForm;
        public IWfWorkflowFormRepository WorkflowForm => _workflowForm ?? (_workflowForm = new WfWorkflowFormRepository(Connection, _config));

        private IWfWorkflowInstanceRepository _workflowInstance;
        public IWfWorkflowInstanceRepository WorkflowInstance => _workflowInstance ?? (_workflowInstance = new WfWorkflowInstanceRepository(Connection, _config));

        private IWfWorkflowTransitionHistoryRepository _workflowTransitionHistory;
        public IWfWorkflowTransitionHistoryRepository WorkflowTransitionHistory => _workflowTransitionHistory
            ?? (_workflowTransitionHistory = new WfWorkflowTransitionHistoryRepository(Connection, _config));

        private IWfWorkflowOperationHistoryRepository _workflowOperationHistory;
        public IWfWorkflowOperationHistoryRepository WorkflowOperationHistory => _workflowOperationHistory
            ?? (_workflowOperationHistory = new WfWorkflowOperationHistoryRepository(Connection, _config));

        private IWfWorkflowCategoryRepository _workflowCategory;
        public IWfWorkflowCategoryRepository WorkflowCategory => _workflowCategory
            ?? (_workflowCategory = new WfWorkflowCategoryRepository(Connection, _config));

        private IWfWorkflowInstanceFormRepository _workflowInstanceForm;
        public IWfWorkflowInstanceFormRepository WorkflowInstanceForm => _workflowInstanceForm
            ?? (_workflowInstanceForm = new WfWorkflowInstanceFormRepository(Connection, _config));

        private IWfWorkflowsqlRepository _wfWorkflowsql;
        public IWfWorkflowsqlRepository WfWorkflowsql => _wfWorkflowsql ?? (_wfWorkflowsql = new WfWorkflowsqlRepository(Connection, _config));


        private IWfWorkflowNoticeRepository _wfWorkflowNotice;
        public IWfWorkflowNoticeRepository WfWorkflowNotice => _wfWorkflowNotice ?? (_wfWorkflowNotice = new WfWorkflowNoticeRepository(Connection, _config));


        private IWfWorkflowUrgeRepository _wfWorkflowUrge;
        public IWfWorkflowUrgeRepository WfWorkflowUrge => _wfWorkflowUrge ?? (_wfWorkflowUrge = new WfWorkflowUrgeRepository(Connection, _config));


        private IWfWorkflowAssignRepository _wfWorkflowAssign;
        public IWfWorkflowAssignRepository WfWorkflowAssign => _wfWorkflowAssign ?? (_wfWorkflowAssign = new WfWorkflowAssignRepository(Connection, _config));
    }
}
