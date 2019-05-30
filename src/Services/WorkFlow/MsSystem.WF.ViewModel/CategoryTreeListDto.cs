using System;
using System.Collections.Generic;

namespace MsSystem.WF.ViewModel
{
    public class CategoryTreeListDto
    {
        /// <summary>
        /// 分类ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        public Guid ParentId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 子分类
        /// </summary>
        public List<CategoryTreeListDto> Children { get; set; }
    }
    public class CategoryDetailDto
    {
        /// <summary>
        /// 分类ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        public Guid ParentId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 父级姓名
        /// </summary>
        public string ParentName { get; set; }

        public int Status { get; set; }
        public string UserId { get; set; }
    }

    public class CategoryDeleteDto
    {
        public List<Guid> Ids { get; set; }
        public string UserId { get; set; }
    }
}
