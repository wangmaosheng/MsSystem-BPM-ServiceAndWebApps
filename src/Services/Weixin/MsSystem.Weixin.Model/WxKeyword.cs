using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JadeFramework.Core.Dapper;
using JadeFramework.Core.Extensions;

namespace MsSystem.Weixin.Model
{
    /// <summary>
    /// 关键字
    /// </summary>
    [Table("wx_keyword")]
    public class WxKeyword
    {
        public WxKeyword()
        {
            this.CreateTime = DateTime.Now.ToTimeStamp();
        }

        [Key,Identity]
        public int Id { get; set; }
        public int RuleId { get; set; }
        public string Keyword { get; set; }
        public long CreateTime { get; set; }
    }
    internal class WxKeywordMapper : ClassMapper<WxKeyword>
    {
        public WxKeywordMapper()
        {
            Table("wx_keyword");
            AutoMap();
        }
    }
}
