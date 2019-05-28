using JadeFramework.Core.Domain.Entities;
using JadeFramework.Dapper;
using MsSystem.Sys.Model;
using System.Threading.Tasks;

namespace MsSystem.Sys.IRepository
{
    public interface ISysReleaseLogRepository: IDapperRepository<SysReleaseLog>
    {
        Task<Page<SysReleaseLog>> GetPageAsync(int pageIndex, int pageSize);
    }
}
