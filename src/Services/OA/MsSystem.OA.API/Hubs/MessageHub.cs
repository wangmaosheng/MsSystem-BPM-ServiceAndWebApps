using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using MsSystem.OA.API.Infrastructure;
using MsSystem.OA.ViewModel;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MsSystem.OA.API.Hubs
{
    [Authorize]
    public class MessageHub : Hub
    {
        public MessageHub()
        {
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, MessageDefault.GroupName);
            var curUser = SignalRMessageGroups.UserGroups.FirstOrDefault(m => m.ConnectionId == Context.ConnectionId && m.GroupName == MessageDefault.GroupName);
            if (curUser != null)
            {
                SignalRMessageGroups.UserGroups.Remove(curUser);
            }
            await base.OnDisconnectedAsync(ex);
        }


        public async Task InitMessage(long userid)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, MessageDefault.GroupName);
            var curUser = SignalRMessageGroups.UserGroups.FirstOrDefault(m => m.UserId == userid && m.GroupName == MessageDefault.GroupName);
            if (curUser != null)
            {
                SignalRMessageGroups.UserGroups.Remove(curUser);
            }
            SignalRMessageGroups.UserGroups.Add(new SignalRMessageGroups
            {
                ConnectionId = Context.ConnectionId,
                GroupName = MessageDefault.GroupName,
                UserId = userid
            });
        }

    }
}
