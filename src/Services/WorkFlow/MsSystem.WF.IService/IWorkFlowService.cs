using JadeFramework.Core.Domain.Entities;
using MsSystem.WF.Model;
using MsSystem.WF.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MsSystem.WF.IService
{
    public interface IWorkFlowService
    {
        Task<Page<WfWorkflow>> GetPageAsync(int pageIndex, int pageSize);
        Task<WorkFlowDetailDto> GetByIdAsync(Guid id);
        Task<bool> InsertAsync(WorkFlowDetailDto workflow);
        Task<bool> UpdateAsync(WorkFlowDetailDto workflow);
        Task<bool> DeleteAsync(FlowDeleteDTO dto);
        Task<List<WorkFlowStartDto>> GetWorkFlowStartAsync(Guid categoryid);
        //Task<List<WorkFlowLineDto>> GetAllLinesAsync();
        //Task<WorkFlowLineDto> GetLineAsync(Guid lineid);
        Task<bool> NewVersionAsync(WorkFlowDetailDto dto);
    }
}
