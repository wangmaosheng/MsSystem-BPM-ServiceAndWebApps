using AutoMapper;
using JadeFramework.Core.Extensions;
using JadeFramework.Weixin.Models.ResponseMsg;
using MsSystem.Weixin.Model;
using System;

namespace MsSystem.Weixin.Service.MapConverter
{
    public class WxTextResponseConverter : ITypeConverter<WxTextResponse, ResponseTextMsg>
    {
        public ResponseTextMsg Convert(WxTextResponse source, ResponseTextMsg destination, ResolutionContext context)
        {
            destination.Content = source.Content;
            destination.CreateTime = DateTime.Now.ToTimeStamp();
            return destination;
        }
    }
}
