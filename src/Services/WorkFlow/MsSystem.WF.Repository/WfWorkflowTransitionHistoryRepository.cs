using System.Data;
using JadeFramework.Dapper;
using JadeFramework.Dapper.SqlGenerator;
using MsSystem.WF.IRepository;
using MsSystem.WF.Model;

namespace MsSystem.WF.Repository
{
    public class WfWorkflowTransitionHistoryRepository : DapperRepository<WfWorkflowTransitionHistory>, IWfWorkflowTransitionHistoryRepository
    {
        public WfWorkflowTransitionHistoryRepository(IDbConnection connection, SqlGeneratorConfig config) : base(connection, config)
        {
        }
    }
}
