namespace MsSystem.Weixin.Model
{
    public abstract class WxBaseModel
    {
        public byte IsDelete { get; set; }
        /// <summary>
        /// 创建人ID
        /// </summary>
        public long CreateUserId { get; set; }
        /// <summary>
        /// 创建时间戳
        /// </summary>
        public long CreateTime { get; set; }
        /// <summary>
        /// 更新人
        /// </summary>
        public long UpdateUserId { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public long UpdateTime { get; set; }
    }
}
