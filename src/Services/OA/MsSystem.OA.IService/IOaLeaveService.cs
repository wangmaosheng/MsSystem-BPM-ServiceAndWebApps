using JadeFramework.Core.Domain.Entities;
using JadeFramework.Core.Domain.Result;
using JadeFramework.Core.Extensions;
using MsSystem.OA.ViewModel;
using System.Threading.Tasks;

namespace MsSystem.OA.IService
{
    public interface IOaLeaveService: IAutoDenpendencyScoped
    {
        Task<Page<OaLeaveDto>> GetPageAsync(int pageIndex, int pageSize, long userid);
        Task<OaLeaveShowDto> GetAsync(long id);
        Task<AjaxResult> InsertAsync(OaLeaveShowDto entity);
        Task<AjaxResult> UpdateAsync(OaLeaveShowDto entity);
    }
}
