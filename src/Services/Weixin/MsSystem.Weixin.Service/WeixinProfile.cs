using AutoMapper;
using JadeFramework.Weixin.Models.ResponseMsg;
using MsSystem.Weixin.Model;
using MsSystem.Weixin.Service.MapConverter;
using MsSystem.Weixin.ViewModel;

namespace MsSystem.Weixin.Service
{
    public class WeixinProfile : Profile
    {
        public WeixinProfile()
        {
            this.CreateMap<WxTextResponse, ResponseTextMsg>().ConvertUsing<WxTextResponseConverter>();


            this.CreateMap<WxMenu, WxMenuDto>();
            this.CreateMap<WxMenuDto, WxMenu>();
        }
    }
}
