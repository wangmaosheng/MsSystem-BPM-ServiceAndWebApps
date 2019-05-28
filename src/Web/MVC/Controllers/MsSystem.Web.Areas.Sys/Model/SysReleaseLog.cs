namespace MsSystem.Web.Areas.Sys.Model
{
    /// <summary>
    /// 系统发布日志
    /// </summary>
    public class SysReleaseLog
    {
        /// <summary>
        /// 发布
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public string VersionNumber { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public long CreateTime { get; set; }
    }
}
