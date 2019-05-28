using JadeFramework.Dapper;
using JadeFramework.Dapper.SqlGenerator;
using MsSystem.Sys.IRepository;
using MsSystem.Sys.Model;
using System.Data;

namespace MsSystem.Sys.Repository
{
    public class SysDataPrivilegesRepository: DapperRepository<SysDataPrivileges>, ISysDataPrivilegesRepository
    {
        public SysDataPrivilegesRepository(IDbConnection connection, SqlGeneratorConfig config) : base(connection, config)
        {
        }
    }
}
