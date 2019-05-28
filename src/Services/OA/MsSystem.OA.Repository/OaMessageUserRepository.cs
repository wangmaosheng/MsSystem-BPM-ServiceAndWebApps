using JadeFramework.Dapper;
using JadeFramework.Dapper.SqlGenerator;
using MsSystem.OA.IRepository;
using MsSystem.OA.Model;
using System.Data;

namespace MsSystem.OA.Repository
{
    public class OaMessageUserRepository : DapperRepository<OaMessageUser>, IOaMessageUserRepository
    {
        public OaMessageUserRepository(IDbConnection connection, SqlGeneratorConfig config) : base(connection, config)
        {
        }
    }
}
