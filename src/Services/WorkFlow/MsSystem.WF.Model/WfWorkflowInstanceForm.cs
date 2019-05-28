using JadeFramework.Core.Dapper;
using JadeFramework.WorkFlow;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MsSystem.WF.Model
{
    /// <summary>
    /// 流程实例表单关联表
    /// </summary>
    [Table("wf_workflow_instance_form")]
    public class WfWorkflowInstanceForm : Entity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// 流程实例ID
        /// </summary>
        public Guid InstanceId { get; set; }

        /// <summary>
        /// 表单ID
        /// </summary>
        public Guid FormId { get; set; }

        /// <summary>
        /// 表单内容
        /// </summary>
        public string FormContent { get; set; }

        /// <summary>
        /// 表单类型
        /// <see cref="WorkFlowFormType"/>
        /// </summary>
        public int FormType { get; set; }

        /// <summary>
        /// 表单地址
        /// </summary>
        public string FormUrl { get; set; }

        /// <summary>
        /// 表单数据JSON
        /// </summary>
        public string FormData { get; set; }
    }
    internal sealed class WfWorkflowInstanceFormMapper : ClassMapper<WfWorkflowInstanceForm>
    {
        public WfWorkflowInstanceFormMapper()
        {
            this.Table("wf_workflow_instance_form");
            this.AutoMap();
        }
    }
}
