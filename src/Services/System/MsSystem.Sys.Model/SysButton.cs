using JadeFramework.Core.Dapper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MsSystem.Sys.Model
{
    /// <summary>
    /// 操作按钮表
    /// </summary>
    [Table("sys_button")]
    public class SysButton
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key, Identity]
        public long ButtonId { get; set; }

        /// <summary>
        /// 按钮名称
        /// </summary>
        public string ButtonName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }
    }
    public class SysButtonMapper : ClassMapper<SysButton>
    {
        public SysButtonMapper()
        {
            Table("sys_button");
            AutoMap();
        }
    }
}
