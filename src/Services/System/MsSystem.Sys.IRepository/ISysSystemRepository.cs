using JadeFramework.Core.Domain.Entities;
using JadeFramework.Dapper;
using MsSystem.Sys.Model;
using System.Threading.Tasks;

namespace MsSystem.Sys.IRepository
{
    public interface ISysSystemRepository: IDapperRepository<SysSystem>
    {
        Task<Page<SysSystem>> GetPageAsync(int pageIndex, int pageSize);
    }
}
