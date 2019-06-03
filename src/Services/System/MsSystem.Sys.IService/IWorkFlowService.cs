using MsSystem.Sys.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MsSystem.Sys.IService
{
    public interface IWorkFlowService
    {
        Task<List<long>> GetFlowNodeInfo(FlowViewModel model);
    }
}
