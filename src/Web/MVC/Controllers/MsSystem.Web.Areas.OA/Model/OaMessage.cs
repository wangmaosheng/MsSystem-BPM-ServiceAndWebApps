using System.ComponentModel;

namespace MsSystem.Web.Areas.OA.Model
{
    /// <summary>
    /// 系统消息表
    /// </summary>
    public class OaMessage
    {
        /// <summary>
        /// 消息ID
        /// </summary>
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
        /// 跳转方式
        /// </summary>
        public string TargetType { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>
        public string Link { get; set; }

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

    public class OaMessageMyListDetail
    {
        /// <summary>
        /// 消息ID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public long CreateTime { get; set; }
    }

    /// <summary>
    /// 我的消息列表
    /// </summary>
    public class OaMessageMyList
    {
        /// <summary>
        /// 消息ID
        /// </summary>
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
        /// 跳转方式
        /// </summary>
        public string TargetType { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// 是否已读
        /// </summary>
        public int IsRead { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public long CreateTime { get; set; }
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