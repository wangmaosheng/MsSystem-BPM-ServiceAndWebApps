using JadeFramework.Dapper;
using JadeFramework.Dapper.SqlGenerator;
using MsSystem.OA.IRepository;
using MsSystem.OA.Model;
using System.Data;

namespace MsSystem.OA.Repository
{
    public class OaMessageUserReadRepository : DapperRepository<OaMessageUserRead>, IOaMessageUserReadRepository
    {
        public OaMessageUserReadRepository(IDbConnection connection, SqlGeneratorConfig config) : base(connection, config)
        {
        }
    }
}
