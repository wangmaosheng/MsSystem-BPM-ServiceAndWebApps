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
        /// <summary>
        /// 配置id
        /// </summary>
        public long ConfigId { get; set; }
        /// <summary>
        /// 活动名称
        /// </summary>
        public string ActivityName { get; set; }
        /// <summary>
        ///活动规则说明
        /// </summary>
        public string Rule { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public long StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
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
        /// <summary>
        /// 活动id
        /// </summary>
        public long ActivityId { get; set; }
        /// <summary>
        /// 奖品名称
        /// </summary>
        public string PrizeName { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public long Quantity { get; set; }
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
    /// 活动记录
    /// </summary>
    [Table("wx_activity_records")]
    public class WxActivityRecords
    {
        [Key]
        public long Id { get; set; }
        /// <summary>
        /// 活动id
        /// </summary>
        public long ActivityId { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        public long RecordTime { get; set; }
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
