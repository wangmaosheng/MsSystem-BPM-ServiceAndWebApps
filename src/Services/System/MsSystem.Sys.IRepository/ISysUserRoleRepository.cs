using JadeFramework.Dapper;
using MsSystem.Sys.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MsSystem.Sys.IRepository
{
    public interface ISysUserRoleRepository : IDapperRepository<SysUserRole>
    {
        /// <summary>
        /// 根据角色ID获取用户ID
        /// </summary>
        /// <param name="roleid">角色ID</param>
        /// <returns></returns>
        Task<List<int>> GetUserIdByRoleIdAsync(long roleid);

        /// <summary>
        /// 根据用户ID获取角色ID
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        Task<IEnumerable<int>> GetRoleIdByUserIdAsync(long userid);

        /// <summary>
        /// 根据用户ID集合获取
        /// </summary>
        /// <param name="roleid">角色ID</param>
        /// <param name="userids">用户ID集合</param>
        /// <returns></returns>
        Task<IEnumerable<SysUserRole>> GetRoleIdByUserIdsAsync(long roleid, List<long> userids);
    }
}
