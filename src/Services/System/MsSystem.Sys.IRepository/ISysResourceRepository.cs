using JadeFramework.Dapper;
using MsSystem.Sys.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MsSystem.Sys.IRepository
{
    public interface ISysResourceRepository : IDapperRepository<SysResource>
    {
        Task<List<SysResource>> GetListAsync(IEnumerable<long> resids);
        Task<List<SysResource>> GetAllListAsync(IEnumerable<long> resids, byte? isdel = 0);
        Task<IEnumerable<SysResource>> GetAllListAsync(IEnumerable<long> resids);

        /// <summary>
        /// 根据用户ID获取该用户可用的菜单
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        Task<IEnumerable<SysResource>> GetListByUserIdAsync(long userid);

        /// <summary>
        /// 获取菜单按钮
        /// </summary>
        /// <param name="parentid"></param>
        /// <returns></returns>
        Task<IEnumerable<SysResource>> GetChildButtonsAsync(long parentid);
    }
}
