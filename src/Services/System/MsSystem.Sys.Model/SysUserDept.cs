using JadeFramework.Core.Dapper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MsSystem.Sys.Model
{
    /// <summary>
    /// 用户部门关联表
    /// </summary>
    [Table("sys_user_dept")]
    public class SysUserDept
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
        /// 创建时间戳
        /// </summary>
        public long CreateTime { get; set; }
    }

    public class SysUserDeptMapper : ClassMapper<SysUserDept>
    {
        public SysUserDeptMapper()
        {
            Table("sys_user_dept");
            AutoMap();
        }
    }
}
