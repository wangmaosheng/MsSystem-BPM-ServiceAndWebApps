using JadeFramework.Core.Dapper;
using JadeFramework.WorkFlow;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MsSystem.WF.Model
{
    /// <summary>
    /// 流程操作历史记录
    /// </summary>
    [Table("wf_workflow_operation_history")]
    public class WfWorkflowOperationHistory : Entity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public Guid OperationId { get; set; }

        /// <summary>
        /// 实例进程Id
        /// </summary>
        public Guid InstanceId { get; set; }

        /// <summary>
        /// 节点ID
        /// </summary>
        public Guid NodeId { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        public string NodeName { get; set; }

        /// <summary>
        /// 流转类型
        /// <see cref="WorkFlowMenu"/>
        /// </summary>
        public int? TransitionType { get; set; }

        /// <summary>
        /// 操作内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 创建人姓名
        /// </summary>
        public string CreateUserName { get; set; }
    }

    internal sealed class WfWorkflowOperationHistoryMapper : ClassMapper<WfWorkflowOperationHistory>
    {
        public WfWorkflowOperationHistoryMapper()
        {
            this.Table("wf_workflow_operation_history");
            this.AutoMap();
        }
    }
}
