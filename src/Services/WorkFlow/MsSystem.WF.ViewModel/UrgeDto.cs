using System;
using System.Collections.Generic;

namespace MsSystem.WF.ViewModel
{
    public class UrgeDto
    {
        /// <summary>
        /// 流程实例id
        /// </summary>
        public Guid InstanceId { get; set; }

        /// <summary>
        /// 发送人
        /// </summary>
        public string Sender { get; set; }

        /// <summary>
        /// 催办类型
        /// </summary>
        public string UrgeType { get; set; }

        /// <summary>
        /// 催办信息
        /// </summary>
        public string UrgeContent { get; set; }
        public string Link { get; set; }
        public string Title { get; set; }
    }


    public class MessagePushSomBodyDTO
    {
        public List<long> UserIds { get; set; }

        /// <summary>
        /// 发送人
        /// </summary>
        public long Sender { get; set; }

        public string Title { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string MsgJson { get; set; }
        /// <summary>
        /// 跳转地址
        /// </summary>
        public string Link { get; set; }
    }
}
