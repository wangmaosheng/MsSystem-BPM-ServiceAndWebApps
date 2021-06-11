using JadeFramework.Core.Extensions;
using MsSystem.Sys.ViewModel;
using System.Threading.Tasks;

namespace MsSystem.Sys.IService
{
    public interface ISysDeptService: IAutoDenpendencyScoped
    {
        Task<DeptIndexViewModel> GetTreeAsync();
        Task<DeptShowViewModel> GetDeptAsync(long deptid);
        Task<bool> AddAsync(DeptShowDto dto);
        Task<bool> UpdateAsync(DeptShowDto dto);
        Task<bool> DeleteAsync(long[] ids, long userid);
    }
}
