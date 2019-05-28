using JadeFramework.Core.Dapper;
using JadeFramework.Core.Extensions;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MsSystem.Weixin.Model
{
    /// <summary>
    /// 微信用户表
    /// </summary>
    [Table("wx_user")]
    public class WxUser 
    {
        public WxUser()
        {
            this.CreateTime = DateTime.Now.ToTimeStamp();
        }

        /// <summary>
        /// 用户OpenId主键
        /// </summary>
        [Key]
        public string OpenId { get; set; }

        /// <summary>
        /// 用户是否关注该公众号1：关注了，0：没关注
        /// </summary>
        public int Subscribe { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 用户的性别，值为1时是男性，值为2时是女性，值为0时是未知
        /// </summary>
        public int Sex { get; set; }

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
        /// 用户头像，最后一个数值代表正方形头像大小（有0、46、64、96、132数值可选，0代表640*640正方形头像），
        /// 用户没有头像时该项为空。若用户更换头像，原有头像URL将失效。
        /// </summary>
        public string Headimgurl { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public string Birthday { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public long CreateTime { get; set; }

        /// <summary>
        /// 用户基本信息是否同步过
        /// </summary>
        public int IsSync { get; set; }

    }
    internal class WxUserMapper : ClassMapper<WxUser>
    {
        public WxUserMapper()
        {
            Table("wx_user");
            AutoMap();
        }
    }
}
