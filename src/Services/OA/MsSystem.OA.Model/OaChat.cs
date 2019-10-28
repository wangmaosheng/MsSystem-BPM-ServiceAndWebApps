using JadeFramework.Core.Dapper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MsSystem.OA.Model
{
    [Table("oa_chat")]
    public class OaChat
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key, Identity]
        public long Id { get; set; }

        /// <summary>
        /// 发送人
        /// </summary>
        public long Sender { get; set; }
        /// <summary>
        /// 接收人
        /// </summary>
        public long Receiver { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 是否已读
        /// 默认 0：未读，1：已读
        /// </summary>
        public byte IsRead { get; set; } = 0;

        /// <summary>
        /// 创建时间戳
        /// </summary>
        public long CreateTime { get; set; }
    }
    public sealed class OaChatMapper : ClassMapper<OaChat>
    {
        public OaChatMapper()
        {
            Table("oa_chat");
            AutoMap();
        }
    }
}