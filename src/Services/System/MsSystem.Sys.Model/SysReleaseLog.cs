using JadeFramework.Core.Dapper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MsSystem.Sys.Model
{
    /// <summary>
    /// 系统发布日志
    /// </summary>
    [Table("sys_release_log")]
    public class SysReleaseLog
    {
        /// <summary>
        /// 发布
        /// </summary>
        [Key, Identity]
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
    public class SysReleaseLogMapper : ClassMapper<SysReleaseLog>
    {
        public SysReleaseLogMapper()
        {
            Table("sys_release_log");
            AutoMap();
        }
    }
}
