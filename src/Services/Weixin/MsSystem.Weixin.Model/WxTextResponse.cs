using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JadeFramework.Core.Dapper;
using JadeFramework.Core.Extensions;

namespace MsSystem.Weixin.Model
{
    /// <summary>
    /// 文本回复
    /// </summary>
    [Table("wx_text_response")]
    public class WxTextResponse
    {
        public WxTextResponse()
        {
            this.CreateTime = DateTime.Now.ToTimeStamp();
        }

        /// <summary>
        /// 主键
        /// </summary>
        [Key,Identity]
        public int Id { get; set; }

        /// <summary>
        /// 规则ID
        /// </summary>
        public long RuleId { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public long CreateTime { get; set; }

    }
    internal class WxTextResponseMapper : ClassMapper<WxTextResponse>
    {
        public WxTextResponseMapper()
        {
            Table("wx_text_response");
            AutoMap();
        }
    }

}
