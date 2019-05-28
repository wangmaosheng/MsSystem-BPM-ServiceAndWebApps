using JadeFramework.Core.Domain.Entities;
using JadeFramework.Core.ViewModel;
using MsSystem.Web.Areas.Sys.Model;
using System.Collections.Generic;

namespace MsSystem.Web.Areas.Sys.ViewModel
{
    #region Index

    /// <summary>
    /// 资源列表实体
    /// </summary>
    public class ResourceIndexViewModel
    {
        /// <summary>
        /// 资源实体
        /// </summary>
        public List<ResourceTreeViewModel> ResourceTree { get; set; }

        /// <summary>
        /// 所属系统
        /// </summary>
        public List<SelectListItem> SystemItems { get; set; }
    }

    /// <summary>
    /// 资源实体
    /// </summary>
    public class ResourceTreeViewModel
    {
        /// <summary>
        /// 资源ID
        /// </summary>
        public long ResourceId { get; set; }

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
        /// 样式图标ICON
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public byte IsDel { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 拥有按钮集合
        /// </summary>
        public List<IdAndValue> Buttons { get; set; }

        /// <summary>
        /// 子菜单
        /// </summary>
        public List<ResourceTreeViewModel> Children { get; set; }
    }

    #endregion

    #region Show

    /// <summary>
    /// 资源展示
    /// </summary>
    public class ResourceShowViewModel
    {
        /// <summary>
        /// 基本信息
        /// </summary>
        public SysResource SysResource { get; set; }

        /// <summary>
        /// 拥有按钮
        /// </summary>
        public List<ButtonViewModel> ButtonViewModels { get; set; }

        /// <summary>
        /// 父节点选择菜单下拉
        /// </summary>
        public List<ZTree> ParentMenus { get; set; }
    }

    #endregion

    #region Update/Add

    /// <summary>
    /// 编辑页DTO
    /// </summary>
    public class ResourceShowDto
    {
        /// <summary>
        /// 基本信息
        /// </summary>
        public SysResource SysResource { get; set; }

        /// <summary>
        /// 按钮DTO
        /// </summary>
        public List<ButtonViewModel> ButtonDto { get; set; }
    }

    #endregion

    /// <summary>
    /// 所属按钮
    /// </summary>
    public class ButtonViewModel
    {
        /// <summary>
        /// 资源主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 按钮类型
        /// </summary>
        public byte ButtonType { get; set; }
        /// <summary>
        /// 按钮类型 ==>用于vue
        /// </summary>
        public byte ButtonModel { get; set; }
        /// <summary>
        ///  按钮名称
        /// </summary>
        public string Name { get; set; }
    }

    public class ResourceDeleteDTO
    {
        public long[] Ids { get; set; }
        public long UserId { get; set; }
    }
}
