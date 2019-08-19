using JadeFramework.Core.Extensions;
using Microsoft.AspNetCore.SignalR;
using MsSystem.Utility;
using System;

namespace MsSystem.Web.Areas.Sys.Hubs
{
    public class ScanningLoginHub : Hub
    {
        public void InitMessage(string code)
        {
            SignalRMessageGroups.UserGroups.Add(new SignalRMessageGroups
            {
                ConnectionId = Context.ConnectionId,
                GroupName = "ScanningLoginHubGroup",
                QrCode = code,
                CreateTime = DateTime.Now.ToTimeStamp()
            });
        }
    }
}
