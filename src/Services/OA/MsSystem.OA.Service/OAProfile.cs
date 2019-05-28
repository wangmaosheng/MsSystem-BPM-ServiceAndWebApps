using AutoMapper;
using MsSystem.OA.Model;
using MsSystem.OA.ViewModel;

namespace MsSystem.OA.Service
{
    public class OAProfile : Profile
    {
        public OAProfile()
        {
            this.CreateMap<OaLeaveShowDto, OaLeave>();
            this.CreateMap<OaLeave, OaLeaveShowDto>();
        }
    }
}
