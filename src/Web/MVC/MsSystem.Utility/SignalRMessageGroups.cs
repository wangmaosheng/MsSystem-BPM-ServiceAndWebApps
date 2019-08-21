using JadeFramework.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MsSystem.Utility
{
    public class SignalRMessageGroups
    {
        public string ConnectionId { get; set; }
        public string QrCode { get; set; }
        public string GroupName { get; set; }
        public string JSON { get; set; }
        public long UserId { get; set; }
        public long CreateTime { get; set; }
        public static List<SignalRMessageGroups> UserGroups = new List<SignalRMessageGroups>();

        public static void Clear(string currentCode,long userid)
        {
            Clear();
            //var list = UserGroups.Where(m => m.QrCode != currentCode && m.UserId == userid);
            for (int i = 0; i < UserGroups.Count(); i++)
            {
                var item = UserGroups[i];
                if (item.QrCode != currentCode && item.UserId == userid)
                {
                    UserGroups.Remove(item);
                }
            }
        }

        /// <summary>
        /// 删除过期
        /// </summary>
        public static void Clear()
        {
            for (int i = 0; i < UserGroups.Count; i++)
            {
                var item = UserGroups[i];
                var time = item.CreateTime.ToDateTime().AddMinutes(5);
                if (time < DateTime.Now)
                {
                    UserGroups.Remove(item);
                }
            }
            //foreach (var item in UserGroups)
            //{
            //    var time = item.CreateTime.ToDateTime().AddMinutes(5);
            //    if (time < DateTime.Now)
            //    {
            //        UserGroups.Remove(item);
            //    }
            //}
        }
    }
}
