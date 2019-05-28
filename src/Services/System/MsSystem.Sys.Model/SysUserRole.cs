using JadeFramework.Core.Dapper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MsSystem.Sys.Model
{
    /// <summary>
    /// 用户角色关联表
    /// </summary>
    [Table("sys_user_role")]
    public class SysUserRole
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key, Identity]
        public long Id { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public long RoleId { get; set; }

        /// <summary>
        /// 创建时间戳
        /// </summary>
        public long CreateTime { get; set; }
    }

    public class SysUserRoleMapper : ClassMapper<SysUserRole>
    {
        public SysUserRoleMapper()
        {
            Table("sys_user_role");
            AutoMap();
        }
    }
}
