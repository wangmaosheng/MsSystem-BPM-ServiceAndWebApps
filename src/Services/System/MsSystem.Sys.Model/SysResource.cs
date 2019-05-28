using JadeFramework.Core.Dapper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MsSystem.Sys.Model
{
    /// <summary>
    /// 资源表（按钮）
    /// 资源从属于系统表
    /// </summary>
    [Table("sys_resource")]
    public class SysResource
    {
        /// <summary>
        /// 资源ID
        /// </summary>
        [Key, Identity]
        public long ResourceId { get; set; }

        /// <summary>
        /// 所属系统
        /// </summary>
        public long SystemId { get; set; }

        /// <summary>
        /// 资源名称
        /// </summary>
        public string ResourceName { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        public long ParentId { get; set; }

        /// <summary>
        /// 资源地址
        /// </summary>
        public string ResourceUrl { get; set; }

        /// <summary>
        /// 同级排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 按钮样式
        /// </summary>
        public string ButtonClass { get; set; }

        /// <summary>
        /// 样式图标ICON
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 是否显示到菜单
        /// </summary>
        public byte IsShow { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        public long CreateUserId { get; set; }

        /// <summary>
        /// 创建时间戳
        /// </summary>
        public long CreateTime { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public byte IsDel { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 是否是按钮
        /// </summary>
        public byte IsButton { get; set; }

        /// <summary>
        /// 按钮类型
        /// </summary>
        public byte? ButtonType { get; set; }

        /// <summary>
        /// 父节点集
        /// </summary>
        public string Path { get; set; }

    }

    public class SysResourceMapper : ClassMapper<SysResource>
    {
        public SysResourceMapper()
        {
            Table("sys_resource");
            AutoMap();
        }
    }
}
