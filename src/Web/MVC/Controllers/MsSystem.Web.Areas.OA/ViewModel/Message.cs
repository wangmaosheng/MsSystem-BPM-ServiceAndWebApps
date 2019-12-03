using System;
using System.Collections.Generic;
using JadeFramework.Core.Domain.Entities;
using MsSystem.Web.Areas.OA.Model;

namespace MsSystem.Web.Areas.OA.ViewModel
{
    public class MessageDeleteDTO
    {
        public List<long> Ids { get; set; }
        public long UserId { get; set; }
    }

    public class MessageEnableDTO
    {
        public List<long> Ids { get; set; }
        public long UserId { get; set; }
    }

    public class MessageShowDTO
    {
        /// <summary>
        /// 消息ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 消息类型
        /// <see cref="SysMessageType"/>
        /// </summary>
        public int MsgType { get; set; }

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
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 是否立即生效
        /// </summary>
        public byte IsEnable { get; set; }
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

        /// <summary>
        /// 面向人员类型
        /// <see cref="OaMessageFaceUserType"/>
        /// </summary>
        public byte FaceUserType { get; set; }

        /// <summary>
        /// 用户ID集合
        /// </summary>
        public List<long> UserIds { get; set; }
    }

    public class OaMessageMyListSearch : BaseSearch
    {
        /// <summary>
        /// 当前人
        /// </summary>
        public long UserId { get; set; }
        public int? IsRead { get; set; }
    }


    public class OaMessageReadDto
    {
        public int MessageId { get; set; }
        public long UserId { get; set; }

    }
}
