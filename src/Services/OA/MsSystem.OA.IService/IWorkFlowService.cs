using JadeFramework.WorkFlow;
using System.Threading.Tasks;

namespace MsSystem.OA.IService
{
    public interface IWorkFlowService
    {
        Task<bool> ChangeTableStatusAsync(WorkFlowStatusChange statusChange);
    }
}
