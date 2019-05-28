using JadeFramework.Core.Domain.Entities;
using JadeFramework.Dapper;
using MsSystem.Sys.Model;
using MsSystem.Sys.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MsSystem.Sys.IRepository
{
    public interface ISysUserRepository : IDapperRepository<SysUser>
    {
        Task<Page<SysUser>> GetPageAsync(UserIndexSearch search);
        Task<Page<SysUser>> GetPageAsync(int pageIndex, int pageSize, List<int> userids);
        /// <summary>
        /// 修改用户头像
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="imgUrl">头像地址</param>
        /// <returns></returns>
        Task<bool> ModifyUserHeadImgAsync(long userid, string imgUrl);

        /// <summary>
        /// 根据角色ID获取用户ID集合
        /// </summary>
        /// <param name="roleids"></param>
        /// <returns></returns>
        Task<List<long>> GetUserIdsByRoleIdsAsync(List<long> roleids);
    }
}
