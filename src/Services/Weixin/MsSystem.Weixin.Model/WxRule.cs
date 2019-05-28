using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JadeFramework.Core.Dapper;
using JadeFramework.Core.Extensions;

namespace MsSystem.Weixin.Model
{
    /// <summary>
    /// 响应微信消息规则
    /// </summary>
    [Table("wx_rule")]
    public class WxRule
    {
        public WxRule()
        {
            this.CreateTime = DateTime.Now.ToTimeStamp();
        }

        /// <summary>
        /// 主键
        /// </summary>
        [Key,Identity]
        public int Id { get; set; }

        /// <summary>
        /// 规则名称
        /// </summary>
        public string RuleName { get; set; }

        /// <summary>
        /// 规则类型0:普通，1:未匹配到回复规则
        /// </summary>
        public int RuleType { get; set; }

        /// <summary>
        /// 规则类型
        /// TODO：<see cref="JadeFramework.Weixin.Enums.RequestMsgType"/>请求类型
        /// </summary>
        public int RequestMsgType { get; set; }

        /// <summary>
        /// 响应消息类型
        /// TODO：<see cref="JadeFramework.Weixin.Enums.ResponseMsgType"/>响应类型
        /// </summary>
        public int ResponseMsgType { get; set; }

        /// <summary>
        /// 规则创建时间
        /// </summary>
        public long CreateTime { get; set; }
    }

    internal class WxRuleMapper : ClassMapper<WxRule>
    {
        public WxRuleMapper()
        {
            Table("wx_rule");
            AutoMap();
        }
    }
}
