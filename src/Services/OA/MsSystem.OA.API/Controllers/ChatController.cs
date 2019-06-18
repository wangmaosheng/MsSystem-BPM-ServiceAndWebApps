using JadeFramework.Core.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using MsSystem.OA.API.Hubs;
using MsSystem.OA.IService;
using MsSystem.OA.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MsSystem.OA.API.Controllers
{
    [Authorize]
    [Route("api/Chat/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private ISystemService _systemService;
        private IHubContext<ChatHub> _hubContext;

        public ChatController(IServiceProvider serviceProvider, ISystemService systemService)
        {
            _hubContext = serviceProvider.GetService<IHubContext<ChatHub>>();
            this._systemService = systemService;
        }

        /// <summary>
        /// 获取全部聊天用户
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<ChatUserViewModel>> GetChatUserAsync([FromBody]List<long> chattinguserids)
        {
            //获取用户
            var allUsers = await _systemService.GetAllUserAsync();
            List<ChatUserViewModel> chatUsers = allUsers.Select(m => new ChatUserViewModel
            {
                UserId = m.UserId,
                UserName = m.UserName,
                HeadImg = m.HeadImg,
                CreateTime = m.CreateTime,
                IsChatting = 0,
                IsOnline = 0
            }).ToList();
            var userids = SignalRMessageGroups.UserGroups.Where(m => m.GroupName == "ChatHubGroup").Select(m => m.UserId);

            foreach (var item in chatUsers)
            {
                if (userids.Contains(item.UserId))
                {
                    item.IsOnline = 1;
                }
                if (chattinguserids.HasItems() && chattinguserids.Contains(item.UserId))
                {
                    item.IsChatting = 1;
                }
            }
            return chatUsers.OrderByDescending(m => m.IsChatting).ThenByDescending(m => m.IsOnline).ThenBy(m => m.CreateTime).ToList(); ;
        }

    }
}
