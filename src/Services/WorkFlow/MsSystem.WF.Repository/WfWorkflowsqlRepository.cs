using JadeFramework.Dapper;
using JadeFramework.Dapper.SqlGenerator;
using MsSystem.WF.IRepository;
using MsSystem.WF.Model;
using System.Data;

namespace MsSystem.WF.Repository
{
    public class WfWorkflowsqlRepository : DapperRepository<WfWorkflowsql>, IWfWorkflowsqlRepository
    {
        public WfWorkflowsqlRepository(IDbConnection connection, SqlGeneratorConfig config) : base(connection, config)
        {
        }
    }
}
