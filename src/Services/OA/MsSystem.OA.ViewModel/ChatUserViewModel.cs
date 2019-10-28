namespace MsSystem.OA.ViewModel
{
    public class ChatUserViewModel
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string HeadImg { get; set; }

        /// <summary>
        /// 是否正在会话中
        /// </summary>
        public int IsChatting { get; set; }

        /// <summary>
        /// 是否在线
        /// </summary>
        public int IsOnline { get; set; }

        /// <summary>
        /// 用户创建时间
        /// </summary>
        public long CreateTime { get; set; }
    }

    public class SysUser
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        public string JobNumber { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 头像地址
        /// </summary>
        public string HeadImg { get; set; }

        /// <summary>
        /// 是否删除 1:是，0：否
        /// </summary>
        public byte IsDel { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        public long CreateUserId { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public long CreateTime { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public long? UpdateUserId { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public long? UpdateTime { get; set; }
    }

    public class ChatUserListDto
    {
        public long Id { get; set; }
        public long Sender { get; set; }
        public string Message { get; set; }
        public long Receiver { get; set; }
        public byte IsRead { get; set; }
        public long CreateTime { get; set; }
    }
    public class ChatUserListSearchDto
    {
        public long Sender { get; set; }
        public long Receiver { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; } = 20;
    }


    public class ChatUserInitLoadDto
    {
        
    }
}
