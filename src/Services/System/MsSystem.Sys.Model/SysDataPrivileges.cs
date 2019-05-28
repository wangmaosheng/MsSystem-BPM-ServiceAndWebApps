using JadeFramework.Core.Dapper;
using JadeFramework.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MsSystem.Sys.Model
{
    /// <summary>
    /// 数据权限表
    /// </summary>
    [Table("sys_data_privileges")]
    public class SysDataPrivileges : IEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key, Identity]
        public long Id { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        public long DeptId { get; set; }

        /// <summary>
        /// 系统ID
        /// </summary>
        public long SystemId { get; set; }
    }

    public sealed class SysDataPrivilegesMapper : ClassMapper<SysDataPrivileges>
    {
        public SysDataPrivilegesMapper()
        {
            Table("sys_data_privileges");
            AutoMap();
        }
    }
}
