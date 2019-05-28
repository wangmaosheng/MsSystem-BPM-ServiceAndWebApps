using System.ComponentModel.DataAnnotations.Schema;
using JadeFramework.Core.Dapper;

namespace MsSystem.Weixin.Model
{
    /// <summary>
    /// 图文响应
    /// </summary>
    [Table("wx_news_response")]
    public class WxNewsResponse
    {
        public int Id { get; set; }
        public int RuleId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Picurl { get; set; }
        public string Url { get; set; }
        public int Sort { get; set; }
        public int Isdel { get; set; }
        public long CreateTime { get; set; }
    }

    internal class WxNewsResponseMapper : ClassMapper<WxNewsResponse>
    {
        public WxNewsResponseMapper()
        {
            Table("wx_news_response");
            AutoMap();
        }
    }
}
