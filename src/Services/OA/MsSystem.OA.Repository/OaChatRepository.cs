using JadeFramework.Dapper;
using JadeFramework.Dapper.SqlGenerator;
using MsSystem.OA.IRepository;
using MsSystem.OA.Model;
using System.Data;

namespace MsSystem.OA.Repository
{
    public class OaChatRepository : DapperRepository<OaChat>, IOaChatRepository
    {
        public OaChatRepository(IDbConnection connection, SqlGeneratorConfig config) : base(connection, config)
        {
        }
    }
}
