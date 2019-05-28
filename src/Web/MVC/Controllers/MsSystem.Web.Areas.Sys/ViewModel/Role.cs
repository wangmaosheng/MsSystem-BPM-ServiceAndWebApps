using JadeFramework.Core.Domain.Entities;
using MsSystem.Web.Areas.Sys.Model;
using System.Collections.Generic;

namespace MsSystem.Web.Areas.Sys.ViewModel
{
    /// <summary>
    /// 角色列表
    /// </summary>
    public class RoleIndexViewModel
    {
        /// <summary>
        /// 系统
        /// </summary>
        public List<SelectListItem> Systems { get; set; }

        /// <summary>
        /// 角色分页
        /// </summary>
        public Page<SysRole> Page { get; set; }

        /// <summary>
        /// 搜索实体
        /// </summary>
        public RoleIndexSearch RoleSearch { get; set; }
    }

    /// <summary>
    /// 角色搜索实体
    /// </summary>
    public class RoleIndexSearch : BaseSearch
    {
        /// <summary>
        /// 系统ID
        /// </summary>
        public long SystemId { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }

        public override string ToString()
        {
            return $"SystemId={SystemId}&RoleName={RoleName}&PageIndex={PageIndex}&PageSize={PageSize}";
        }
    }

    public class RoleUserDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public long RoleId { get; set; }

        /// <summary>
        /// 是否选中
        /// </summary>
        public byte IsChose { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
    }

    /// <summary>
    /// 树
    /// </summary>
    public class RoleTreeDto
    {
        public long RoleId { get; set; }

        public List<string> ResourceIds { get; set; }

        public long CreateUserId { get; set; }
    }

    /// <summary>
    /// 角色分配用户DTO
    /// </summary>
    public class RoleToUserDto
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public long RoleId { get; set; }
        /// <summary>
        /// 用户ID集合
        /// </summary>
        public List<long> UserIds { get; set; }
        /// <summary>
        /// 当前用户ID
        /// </summary>
        public long CurrentUserId { get; set; }
    }
    public class RoleDeleteDTO
    {
        public List<long> Ids { get; set; }
        public long UserId { get; set; }
    }
}
