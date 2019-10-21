using JadeFramework.Core.Dapper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MsSystem.Weixin.Model
{
    /// <summary>
    /// 活动
    /// </summary>
    [Table("wx_activity")]
    public class WxActivity: WxBaseModel
    {
        [Key]
        public long Id { get; set; }
        public long ConfigId { get; set; }
        public string ActivityName { get; set; }
        public string Rule { get; set; }
        public long StartTime { get; set; }
        public long EndTime { get; set; }

    }
    internal class WxActivityMapper : ClassMapper<WxActivity>
    {
        public WxActivityMapper()
        {
            Table("wx_activity");
            AutoMap();
        }
    }
    /// <summary>
    /// 奖品明细
    /// </summary>
    [Table("wx_activity_prize")]
    public class WxActivityPrize
    {
        [Key]
        public long Id { get; set; }
        public long ActivityId { get; set; }
        public string PrizeName { get; set; }
    }
    internal class WxActivityPrizeMapper : ClassMapper<WxActivityPrize>
    {
        public WxActivityPrizeMapper()
        {
            Table("wx_activity_prize");
            AutoMap();
        }
    }

    /// <summary>
    /// 活动配置
    /// </summary>
    [Table("wx_activity_config")]
    public class WxActivityConfig: WxBaseModel
    {
        [Key]
        public long Id { get; set; }
        /// <summary>
        /// 配置名称
        /// </summary>
        public string ConfigName { get; set; }
    }
    internal class WxActivityConfigMapper : ClassMapper<WxActivityConfig>
    {
        public WxActivityConfigMapper()
        {
            Table("wx_activity_config");
            AutoMap();
        }
    }
}
