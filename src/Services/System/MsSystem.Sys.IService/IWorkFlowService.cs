using MsSystem.Sys.ViewModel;
using System.Threading.Tasks;

namespace MsSystem.Sys.IService
{
    public interface IWorkFlowService
    {
        Task<string> WorkFlowSelectInfoAsync(FlowViewModel model);
    }
}
