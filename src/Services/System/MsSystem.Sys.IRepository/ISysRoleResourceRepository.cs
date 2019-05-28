using JadeFramework.Dapper;
using MsSystem.Sys.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MsSystem.Sys.IRepository
{
    public interface ISysRoleResourceRepository : IDapperRepository<SysRoleResource>
    {
        Task<IEnumerable<SysRoleResource>> GetListByRoleIdAsync(IEnumerable<long> roleids);
    }
}
