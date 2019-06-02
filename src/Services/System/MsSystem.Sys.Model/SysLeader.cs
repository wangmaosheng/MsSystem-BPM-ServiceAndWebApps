using JadeFramework.Core.Dapper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MsSystem.Sys.Model
{
    /// <summary>
    /// 公司领导类型/级别
    /// </summary>
    [Table("sys_leader")]
    public class SysLeader
    {
        /// <summary>
        /// 缩写，公司领导应根据此字段获取
        /// </summary>
        [Key]
        public string Shorter { get; set; }

        /// <summary>
        /// 级别名称
        /// </summary>
        public string LeaderName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
    public class SysLeaderMapper : ClassMapper<SysLeader>
    {
        public SysLeaderMapper()
        {
            Table("sys_leader");
            AutoMap();
        }
    }
}
