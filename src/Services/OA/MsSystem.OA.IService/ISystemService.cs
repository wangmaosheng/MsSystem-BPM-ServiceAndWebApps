using MsSystem.OA.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MsSystem.OA.IService
{
    public interface ISystemService
    {
        Task<List<SysUser>> GetAllUserAsync();
    }
}
