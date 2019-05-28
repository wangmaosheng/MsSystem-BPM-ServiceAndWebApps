using JadeFramework.Core.Domain.Entities;
using JadeFramework.Dapper;
using MsSystem.Sys.Model;
using MsSystem.Sys.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MsSystem.Sys.IRepository
{
    public interface ISysRoleRepository : IDapperRepository<SysRole>
    {
        Task<List<SysRole>> GetListAsync(IEnumerable<long> roleids);
        Task<Page<SysRole>> GetPageAsync(RoleIndexSearch search);

        /// <summary>
        /// 根据用户ID获取该用户的角色
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        Task<IEnumerable<SysRole>> GetByUserIdAsync(long userid);
    }
}
