using AutoMapper;
using MsSystem.Weixin.IRepository;
using MsSystem.Weixin.IService;
using MsSystem.Weixin.Model;
using MsSystem.Weixin.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MsSystem.Weixin.Service
{
    public class WxMenuService : IWxMenuService
    {
        private readonly IWeixinDatabaseFixture databaseFixture;
        private readonly IMapper mapper;

        public WxMenuService(IWeixinDatabaseFixture databaseFixture,IMapper mapper)
        {
            this.databaseFixture = databaseFixture;
            this.mapper = mapper;
        }

        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <returns></returns>
        public async Task<List<WxMenuDto>> GetTreesAsync()
        {
            var dbmenus = await databaseFixture.Db.WxMenu.FindAllAsync(m => m.IsDel == 0);
            var dblist = dbmenus.OrderBy(m => m.Sort);
            List<WxMenuDto> list = new List<WxMenuDto>();

            foreach (var item in dblist.Where(m => m.ParentId == 0))
            {
                WxMenuDto tree = mapper.Map<WxMenu, WxMenuDto>(item);
                tree.Children = mapper.Map<List<WxMenu>, List<WxMenuDto>>(dblist.Where(m => m.ParentId == item.Id).ToList());
                list.Add(tree);
            }
            return list;
        }

    }
}
