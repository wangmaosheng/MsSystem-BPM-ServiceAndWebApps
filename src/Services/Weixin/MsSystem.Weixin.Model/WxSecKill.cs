using JadeFramework.Core.Dapper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MsSystem.Weixin.Model
{
    /// <summary>
    /// 秒杀
    /// </summary>
    [Table("wx_seckill")]
    public class WxSecKill
    {
        /// <summary>
        /// 商品秒杀id
        /// </summary>
        [Key]
        public long SecKillId { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 库存数量
        /// </summary>
        public long Amount { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public long StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public long EndTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public long CreateTime { get; set; }
    }

    internal class WxSecKillMapper : ClassMapper<WxSecKill>
    {
        public WxSecKillMapper()
        {
            Table("wx_seckill");
            AutoMap();
        }
    }

    /// <summary>
    /// 秒杀记录
    /// </summary>
    [Table("wx_seckill_record")]
    public class WxSecKillRecord
    {
        /// <summary>
        /// 记录id
        /// </summary>
        [Key]
        public long RecordId { get; set; }

        /// <summary>
        /// 秒杀id
        /// </summary>
        public long SecKillId { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 秒杀状态
        /// 状态标识 0成功 1 已付 2未付款 3未付款超时作废
        /// </summary>
        public byte State { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public long CreateTime { get; set; }
    }

    internal class WxSecKillRecordMapper : ClassMapper<WxSecKillRecord>
    {
        public WxSecKillRecordMapper()
        {
            Table("wx_seckill_record");
            AutoMap();
        }
    }

}
