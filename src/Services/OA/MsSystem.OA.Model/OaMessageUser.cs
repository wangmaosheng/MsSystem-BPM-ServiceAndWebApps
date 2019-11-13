using JadeFramework.Core.Dapper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MsSystem.OA.Model
{
    /// <summary>
    /// 消息用户关系表
    /// </summary>
    [Table("oa_message_user")]
    public class OaMessageUser
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key, Identity]
        public long Id { get; set; }

        /// <summary>
        /// 消息ID
        /// </summary>
        public long MessageId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 是否已读
        /// </summary>
        public byte IsRead { get; set; }
    }
    public sealed class OaMessageUserMapper : ClassMapper<OaMessageUser>
    {
        public OaMessageUserMapper()
        {
            Table("oa_message_user");
            AutoMap();
        }
    }

}