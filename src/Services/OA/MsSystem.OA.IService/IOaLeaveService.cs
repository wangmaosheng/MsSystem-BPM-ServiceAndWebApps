using JadeFramework.Core.Domain.Entities;
using JadeFramework.Core.Domain.Result;
using MsSystem.OA.ViewModel;
using System.Threading.Tasks;

namespace MsSystem.OA.IService
{
    public interface IOaLeaveService
    {
        Task<Page<OaLeaveDto>> GetPageAsync(int pageIndex, int pageSize, long userid);
        Task<OaLeaveShowDto> GetAsync(long id);
        Task<AjaxResult> InsertAsync(OaLeaveShowDto entity);
        Task<AjaxResult> UpdateAsync(OaLeaveShowDto entity);
    }
}
