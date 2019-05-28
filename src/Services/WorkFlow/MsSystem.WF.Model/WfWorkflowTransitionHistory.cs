using JadeFramework.Core.Dapper;
using JadeFramework.WorkFlow;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MsSystem.WF.Model
{
    /// <summary>
    /// 流程流转历史表
    /// </summary>
    [Table("wf_workflow_transition_history")]
    public class WfWorkflowTransitionHistory: Entity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public Guid TransitionId { get; set; }

        /// <summary>
        /// 流程实例ID
        /// </summary>
        public Guid InstanceId { get; set; }

        /// <summary>
        /// 开始节点Id
        /// </summary>
        public Guid FromNodeId { get; set; }

        /// <summary>
        /// 开始节点类型
        /// <see cref="WorkFlowInstanceNodeType"/>
        /// </summary>
        public int? FromNodeType { get; set; }

        /// <summary>
        /// 开始节点名称
        /// </summary>
        public string FromNodName { get; set; }

        /// <summary>
        /// 结束节点Id
        /// </summary>
        public Guid ToNodeId { get; set; }

        /// <summary>
        /// 结束节点类型
        /// <see cref="WorkFlowInstanceNodeType"/>
        /// </summary>
        public int? ToNodeType { get; set; }

        /// <summary>
        /// 结束节点名称
        /// </summary>
        public string ToNodeName { get; set; }

        /// <summary>
        /// 转化状态
        /// </summary>
        public int? TransitionState { get; set; }

        /// <summary>
        /// 是否结束
        /// <see cref="WorkFlowInstanceStatus"/>
        /// </summary>
        public int? IsFinish { get; set; }
        /// <summary>
        /// 创建人姓名
        /// </summary>
        public string CreateUserName { get; set; }
    }

    internal sealed class WfWorkflowTransitionHistoryMapper : ClassMapper<WfWorkflowTransitionHistory>
    {
        public WfWorkflowTransitionHistoryMapper()
        {
            this.Table("wf_workflow_transition_history");
            this.AutoMap();
        }
    }
}
