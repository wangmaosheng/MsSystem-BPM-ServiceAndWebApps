using JadeFramework.Core.Domain.Entities;
using MsSystem.Sys.Model;
using System.Threading.Tasks;

namespace MsSystem.Sys.IService
{
    /// <summary>
    /// 发布日志服务接口
    /// </summary>
    public interface ISysReleaseLogService
    {
        /// <summary>
        /// 发布日志分页获取
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        Task<Page<SysReleaseLog>> GetPageAsync(int pageIndex, int pageSize);
    }
}
