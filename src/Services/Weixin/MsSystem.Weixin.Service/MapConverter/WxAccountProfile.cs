using AutoMapper;
using MsSystem.Weixin.Model;
using MsSystem.Weixin.ViewModel;

namespace MsSystem.Weixin.Service.MapConverter
{
    public class WxAccountProfile : Profile
    {
        public WxAccountProfile()
        {
            this.CreateMap<WxAccount, WxAccountListDto>();
            this.CreateMap<WxAccountListDto, WxAccount>();
        }
    }
}
