using JadeFramework.Core.Dapper;
using JadeFramework.WorkFlow;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MsSystem.WF.Model
{
    /// <summary>
    /// 流程表单
    /// </summary>
    [Table("wf_workflow_form")]
    public class WfWorkflowForm : Entity
    {
        /// <summary>
        /// 表单ID
        /// </summary>
        [Key]
        public Guid FormId { get; set; }

        /// <summary>
        /// 表单名称
        /// </summary>
        public string FormName { get; set; }

        /// <summary>
        /// 表单类型
        /// <see cref="WorkFlowFormType"/>
        /// </summary>
        public int FormType { get; set; }

        /// <summary>
        /// 表单内容
        /// <see cref="WorkFlowFormType.Custom"/>
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 原始表单内容
        /// </summary>
        public string OriginalContent { get; set; }

        /// <summary>
        /// 表单地址
        /// <see cref="WorkFlowFormType.System"/>
        /// </summary>
        public string FormUrl { get; set; }
    }
    internal sealed class WfWorkflowFormMapper : ClassMapper<WfWorkflowForm>
    {
        public WfWorkflowFormMapper()
        {
            this.Table("wf_workflow_form");
            this.AutoMap();
        }
    }
}
