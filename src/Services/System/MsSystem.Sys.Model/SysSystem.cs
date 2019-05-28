using JadeFramework.Core.Dapper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MsSystem.Sys.Model
{
    /// <summary>
    /// 系统表
    /// </summary>
    [Table("sys_system")]
    public class SysSystem
    {
        /// <summary>
        /// 系统ID
        /// </summary>
        [Key, Identity]
        public long SystemId { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        public string SystemName { get; set; }

        /// <summary>
        /// 系统编码 GUID
        /// </summary>
        public string SystemCode { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public byte IsDel { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 创建人ID
        /// </summary>
        public long CreateUserId { get; set; }
        /// <summary>
        /// 创建时间戳
        /// </summary>
        public long CreateTime { get; set; }
        /// <summary>
        /// 更新人
        /// </summary>
        public long UpdateUserId { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public long UpdateTime { get; set; }
    }

    internal class SysSystemMapper : ClassMapper<SysSystem>
    {
        public SysSystemMapper()
        {
            Table("sys_system");
            AutoMap();
        }
    }
}
