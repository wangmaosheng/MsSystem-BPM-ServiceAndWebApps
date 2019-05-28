using AutoMapper;
using MsSystem.WF.Model;
using MsSystem.WF.ViewModel;

namespace MsSystem.WF.Service
{
    class WorkFlowProfile : Profile
    {
        public WorkFlowProfile()
        {
            this.CreateMap<WfWorkflow, WorkFlowDetailDto>();
            this.CreateMap<WorkFlowDetailDto, WfWorkflow>();

            this.CreateMap<WfWorkflowForm, FormDetailDto>();
            this.CreateMap<FormDetailDto, WfWorkflowForm>();
        }
    }
}
