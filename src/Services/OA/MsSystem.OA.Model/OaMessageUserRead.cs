using JadeFramework.Core.Dapper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MsSystem.OA.Model
{
    /// <summary>
    /// 用户消息已读表
    /// </summary>
    [Table("oa_message_user_read")]
    public class OaMessageUserRead
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
    }
    public sealed class OaMessageUserReadMapper : ClassMapper<OaMessageUserRead>
    {
        public OaMessageUserReadMapper()
        {
            Table("oa_message_user_read");
            AutoMap();
        }
    }
}