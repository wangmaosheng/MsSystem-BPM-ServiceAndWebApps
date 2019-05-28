using JadeFramework.Core.Domain.Entities;
using JadeFramework.Dapper;
using MsSystem.OA.Model;
using MsSystem.OA.ViewModel;
using System.Threading.Tasks;

namespace MsSystem.OA.IRepository
{
    /// <summary>
    /// 员工请假仓储接口
    /// </summary>
    public interface IOaLeaveRepository: IDapperRepository<OaLeave>
	{
        Task<Page<OaLeaveDto>> GetPageAsync(int pageIndex, int pageSize, long userid);
    }
}
