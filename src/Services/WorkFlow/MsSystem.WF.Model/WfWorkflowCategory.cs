using JadeFramework.Core.Dapper;
using JadeFramework.WorkFlow;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MsSystem.WF.Model
{
    /// <summary>
    /// 流程分类
    /// </summary>
    [Table("wf_workflow_category")]
    public class WfWorkflowCategory : Entity
    {
        public WfWorkflowCategory()
        {
            this.Status = 1;
        }

        /// <summary>
        /// 分类ID
        /// </summary>
        [Key]
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
    }

    internal sealed class WfWorkflowCategoryMapper : ClassMapper<WfWorkflowCategory>
    {
        public WfWorkflowCategoryMapper()
        {
            this.Table("wf_workflow_category");
            this.AutoMap();
        }
    }
}
