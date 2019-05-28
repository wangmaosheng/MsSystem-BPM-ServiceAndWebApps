using JadeFramework.Core.Dapper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MsSystem.Sys.Model
{
    /// <summary>
    /// 角色资源关联表
    /// </summary>
    [Table("sys_role_resource")]
    public class SysRoleResource
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key, Identity]
        public long Id { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public long RoleId { get; set; }

        /// <summary>
        /// 资源ID
        /// </summary>
        public long ResourceId { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public long CreateTime { get; set; }
    }

    public class SysRoleResourceMapper : ClassMapper<SysRoleResource>
    {
        public SysRoleResourceMapper()
        {
            Table("sys_role_resource");
            AutoMap();
        }
    }
}
