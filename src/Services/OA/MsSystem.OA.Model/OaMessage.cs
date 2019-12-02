using JadeFramework.Core.Dapper;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MsSystem.OA.Model
{
    /// <summary>
    /// 系统消息表
    /// </summary>
    [Table("oa_message")]
    public class OaMessage
    {
        /// <summary>
        /// 消息ID
        /// </summary>
        [Key, Identity]
        public long Id { get; set; }

        /// <summary>
        /// 消息类型
        /// <see cref="OaMessageType"/>
        /// </summary>
        public int MsgType { get; set; }

        /// <summary>
        /// 面向人员类型
        /// <see cref="OaMessageFaceUserType"/>
        /// </summary>
        public byte FaceUserType { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 是否是本地消息
        /// </summary>
        public byte IsLocal { get; set; }

        /// <summary>
        /// 是否是系统自己创建消息
        /// </summary>
        public byte IsSystem { get; set; }

        /// <summary>
        /// 跳转方式
        /// blank tab
        /// </summary>
        public string TargetType { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public long StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public long EndTime { get; set; }
        /// <summary>
        /// 是否立即生效
        /// </summary>
        public byte IsEnable { get; set; }
        /// <summary>
        /// 是否被执行过
        /// </summary>
        public byte IsExecuted { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public byte IsDel { get; set; }
        /// <summary>
        /// 编制人ID
        /// </summary>
        public long MakerUserId { get; set; }
        /// <summary>
        /// 创建人Id
        /// </summary>
        public long CreateUserId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public long CreateTime { get; set; }

    }
    public sealed class OaMessageMapper : ClassMapper<OaMessage>
    {
        public OaMessageMapper()
        {
            Table("oa_message");
            AutoMap();
        }
    }

    /// <summary>
    /// 消息类型
    /// </summary>
    public enum OaMessageType
    {
        [Description("系统消息")]
        System = 0,
        [Description("推送消息")]
        Push = 1
    }
    /// <summary>
    /// 面向人员类型
    /// </summary>
    public enum OaMessageFaceUserType
    {
        [Description("全部人员")]
        All = 0,
        [Description("某些人")]
        Users = 1,
        //[Description("某些角色")]
        //Roles = 2
    }
}