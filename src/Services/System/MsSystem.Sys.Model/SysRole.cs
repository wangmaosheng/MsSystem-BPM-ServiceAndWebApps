using JadeFramework.Core.Dapper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MsSystem.Sys.Model
{
    /// <summary>
    /// 角色表
    /// 角色从属于系统表
    /// </summary>
    [Table("sys_role")]
    public class SysRole
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        [Key, Identity]
        public long RoleId { get; set; }

        /// <summary>
        /// 系统编码
        /// </summary>
        public long SystemId { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public byte IsDel { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        public long CreateUserId { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public long CreateTime { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public long UpdateUserId { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public long UpdateTime { get; set; }
    }

    internal class SysRoleMapper : ClassMapper<SysRole>
    {
        public SysRoleMapper()
        {
            Table("sys_role");
            AutoMap();
        }
    }
}
