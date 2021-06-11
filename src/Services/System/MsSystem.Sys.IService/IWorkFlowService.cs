using JadeFramework.Core.Extensions;
using MsSystem.Sys.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MsSystem.Sys.IService
{
    public interface IWorkFlowService: IAutoDenpendencyScoped
    {
        Task<List<long>> GetFlowNodeInfo(FlowViewModel model);
        Task<Guid?> GetFinalNodeId(FlowLineFinalNodeDto model);
    }
}
