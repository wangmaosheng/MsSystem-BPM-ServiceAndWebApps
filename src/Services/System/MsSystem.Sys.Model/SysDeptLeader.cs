using JadeFramework.Core.Dapper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MsSystem.Sys.Model
{
    /// <summary>
    /// 部门领导关系表
    /// </summary>
    [Table("sys_dept_leader")]
    public class SysDeptLeader
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key, Identity]
        public long Id { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        public long DeptId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 领导类型 <see cref="SysLeader.Shorter"/>
        /// </summary>
        public string LeaderType { get; set; }
    }
    public class SysDeptLeaderMapper : ClassMapper<SysDeptLeader>
    {
        public SysDeptLeaderMapper()
        {
            Table("sys_dept_leader");
            AutoMap();
        }
    }
}
