using JadeFramework.Dapper;
using JadeFramework.Dapper.SqlGenerator;
using MsSystem.Sys.IRepository;
using MsSystem.Sys.Model;
using System.Data;

namespace MsSystem.Sys.Repository
{
    public class SysDeptLeaderRepository : DapperRepository<SysDeptLeader>, ISysDeptLeaderRepository
    {
        public SysDeptLeaderRepository(IDbConnection connection, SqlGeneratorConfig config) : base(connection, config)
        {
        }
    }
}
