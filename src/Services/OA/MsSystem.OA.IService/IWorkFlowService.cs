using JadeFramework.WorkFlow;
using MsSystem.OA.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MsSystem.OA.IService
{
    public interface IWorkFlowService
    {
        Task<bool> ChangeTableStatusAsync(WorkFlowStatusChange statusChange);
        Task<List<long>> GetFlowNodeInfo(FlowViewModel model);
        Task<Guid?> GetFinalNodeId(FlowLineFinalNodeDto model);
    }
}
