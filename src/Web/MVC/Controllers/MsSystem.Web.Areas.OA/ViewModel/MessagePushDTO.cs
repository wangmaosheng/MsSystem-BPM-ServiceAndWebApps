using System;

namespace MsSystem.Web.Areas.OA.ViewModel
{
    public class PushAllDTO
    {
        public long MessageId { get; set; }
        public string Title { get; set; }
        public DateTime PublishTime { get; set; }
    }

    public class MessagePushDTO
    {
        public long UserId { get; set; }
        public string GroupName { get; set; }
        public string MsgJson { get; set; }
    }
    public class MessageGroupPushDTO
    {
        public string GroupName { get; set; }
        public string MsgJson { get; set; }
    }
}
