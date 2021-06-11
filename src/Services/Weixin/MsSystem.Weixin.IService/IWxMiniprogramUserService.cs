﻿using JadeFramework.Core.Extensions;
using JadeFramework.Weixin.MiniProgram;
using MsSystem.Weixin.Model;
using MsSystem.Weixin.ViewModel;
using System.Threading.Tasks;

namespace MsSystem.Weixin.IService
{
    public interface IWxMiniprogramUserService: IAutoDenpendencyScoped
    {
        Task<WxMiniprogramUser> GetByOpenIdAsync(string openId);
        Task<MiniprogramRegisterResult> RegisterAsync(jscode2session data,string rowData);
    }
}
