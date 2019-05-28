using JadeFramework.Core.Dapper;
using JadeFramework.Core.Extensions;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MsSystem.Weixin.Model
{
    [Table("wx_miniprogram_user")]
    public class WxMiniprogramUser
    {
        public WxMiniprogramUser()
        {
            this.CreateTime = DateTime.Now.ToTimeStamp();
        }

        /// <summary>
        /// 自增主键
        /// </summary>
        [Key,Identity]
        public long Id { get; set; }

        /// <summary>
        /// 小程序对应该用户的OpenId
        /// </summary>
        [Key]
        public string OpenId { get; set; }

        /// <summary>
        /// 微信开放平台的唯一标识符
        /// </summary>
        public string UnionId { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 用户所在城市
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 用户所在国家
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// 用户所在的省份
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 语言
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public byte Gender { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string AvatarUrl { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public long CreateTime { get; set; }
    }
    internal class WxMiniprogramUserMapper : ClassMapper<WxMiniprogramUser>
    {
        public WxMiniprogramUserMapper()
        {
            Table("wx_miniprogram_user");
            AutoMap();
        }
    }
}
