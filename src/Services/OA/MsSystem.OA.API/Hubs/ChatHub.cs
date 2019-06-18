using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using MsSystem.OA.IService;
using MsSystem.OA.ViewModel;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MsSystem.OA.API.Hubs
{
    [Authorize]
    public class ChatHub: Hub
    {
        private readonly IOaChatService _chatService;

        public ChatHub(IOaChatService chatService)
        {
            this._chatService = chatService;
        }

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

        /// <summary>
        /// 信息发送
        /// </summary>
        /// <param name="receiver">接收人</param>
        /// <param name="sender">发送人</param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendMessage(long receiver,long sender, string message)
        {
            //判断接收的人是否在线
            var receiveUser = SignalRMessageGroups.UserGroups.FirstOrDefault(m => m.UserId == receiver && m.GroupName == "ChatHubGroup");
            if (receiveUser != null)
            {
                await Clients.Client(receiveUser.ConnectionId).SendAsync("ReceiveChater", new
                {
                    sender,
                    message,
                    time = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")
                });
                await _chatService.InsertAsync(new Model.OaChat
                {
                    Receiver = receiver,
                    Sender = sender,
                    Message = message
                });
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
