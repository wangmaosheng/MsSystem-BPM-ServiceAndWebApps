using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using MsSystem.OA.ViewModel;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MsSystem.OA.API.Hubs
{
    [Authorize]
    public class ChatHub: Hub
    {
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "ChatHubGroup");
            var curUser = SignalRMessageGroups.UserGroups.FirstOrDefault(m => m.ConnectionId == Context.ConnectionId && m.GroupName == "ChatHubGroup");
            if (curUser != null)
            {
                SignalRMessageGroups.UserGroups.Remove(curUser);
            }
            await base.OnDisconnectedAsync(ex);
        }

        public async Task SendMessage(long userid, string message)
        {
            //判断接收的人是否在线
            var receiveUser = SignalRMessageGroups.UserGroups.FirstOrDefault(m => m.UserId == userid && m.GroupName == "ChatHubGroup");
            if (receiveUser != null)
            {
                await Clients.Client(receiveUser.ConnectionId).SendAsync("ReceiveChater", message);
            }
            else
            {
                //发送邮件/短信提醒
            }
        }

        public async Task InitMessage(long userid)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "ChatHubGroup");
            var curUser = SignalRMessageGroups.UserGroups.FirstOrDefault(m => m.UserId == userid && m.GroupName == "ChatHubGroup");
            if (curUser != null)
            {
                SignalRMessageGroups.UserGroups.Remove(curUser);
            }
            SignalRMessageGroups.UserGroups.Add(new SignalRMessageGroups
            {
                ConnectionId = Context.ConnectionId,
                GroupName = "ChatHubGroup",
                UserId = userid
            });
            //刷新在线用户列表

            await Clients.All.SendAsync("RefreshOnliner", userid);
        }
    }
}
