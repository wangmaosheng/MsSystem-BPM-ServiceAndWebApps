using JadeFramework.Dapper;
using JadeFramework.Dapper.SqlGenerator;
using MsSystem.OA.IRepository;
using MsSystem.OA.Model;
using System.Data;

namespace MsSystem.OA.Repository
{
    public class OaWorkflowsqlRepository : DapperRepository<OaWorkflowsql>, IOaWorkflowsqlRepository
    {
        public OaWorkflowsqlRepository(IDbConnection connection, SqlGeneratorConfig config) : base(connection, config)
        {
        }
    }
}
