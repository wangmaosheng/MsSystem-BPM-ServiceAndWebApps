using JadeFramework.Core.Domain.Entities;
using MsSystem.Sys.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MsSystem.Sys.IService
{
    /// <summary>
    /// 系统
    /// </summary>
    public interface ISysSystemService
    {
        /// <summary>
        /// 根据ID获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<SysSystem> GetByIdAsync(long id);
        /// <summary>
        /// 新增系统
        /// </summary>
        /// <param name="system"></param>
        /// <returns></returns>
        Task<bool> InsertAsync(SysSystem system);
        /// <summary>
        /// 更新系统
        /// </summary>
        /// <param name="system"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(SysSystem system);

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(List<long> ids, long userid);

        Task<List<SysSystem>> ListAsync();
        Task<Page<SysSystem>> GetPageAsync(int pageIndex, int pageSize);
        /// <summary>
        /// 禁用系统
        /// </summary>
        /// <returns></returns>
        Task<bool> DisableSystemAsync(long ids);
        /// <summary>
        /// 启用系统
        /// </summary>
        /// <param name="id">系统ID</param>
        /// <returns></returns>
        Task<bool> EnableSystemAsync(long id);
    }
}
