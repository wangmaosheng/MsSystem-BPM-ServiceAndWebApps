﻿using JadeFramework.Core.Extensions;
using MsSystem.Weixin.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MsSystem.Weixin.IService
{
    public interface IWxMenuService : IAutoDenpendencyScoped
    {
        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <returns></returns>
        Task<List<WxMenuDto>> GetTreesAsync();
    }
}
