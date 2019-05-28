using System.Collections.Generic;
using JadeFramework.Core.Domain.Entities;
using MsSystem.Sys.Model;

namespace MsSystem.Sys.ViewModel
{
    public class DeptIndexViewModel
    {
        public List<DeptTreeViewModel> DeptTree { get; set; }
    }

    public class DeptTreeViewModel
    {
        /// <summary>
        /// 部门ID
        /// </summary>
        public long DeptId { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 部门编码
        /// </summary>
        public string DeptCode { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        public long ParentId { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public byte IsDel { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 子部门
        /// </summary>
        public List<DeptTreeViewModel> Children { get; set; }
    }

    public class DeptShowViewModel
    {
        /// <summary>
        /// 部门
        /// </summary>
        public SysDept SysDept { get; set; }

        /// <summary>
        /// 父节点选择菜单下拉
        /// </summary>
        public List<ZTree> ParentMenus { get; set; }
    }

    public class DeptShowDto
    {
        public SysDept SysDept { get; set; }
    }

    public class DeptDeleteDTO
    {
        public long[] Ids { get; set; }
        public long UserId { get; set; }
    }
}
