using JadeFramework.Core.Extensions;
using JadeFramework.Dapper;
using JadeFramework.Dapper.SqlGenerator;
using MsSystem.Sys.IRepository;
using MsSystem.Sys.Model;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MsSystem.Sys.Repository
{
    public class SysRoleResourceRepository : DapperRepository<SysRoleResource>, ISysRoleResourceRepository
    {
        public SysRoleResourceRepository(IDbConnection connection, SqlGeneratorConfig config) : base(connection, config)
        {
        }

        public async Task<IEnumerable<SysRoleResource>> GetListByRoleIdAsync(IEnumerable<long> roleids)
        {
            string sql = $"SELECT * FROM sys_role_resource WHERE roleid IN({roleids.Join()})";
            return await this.QueryAsync(sql);
        }
    }
}
