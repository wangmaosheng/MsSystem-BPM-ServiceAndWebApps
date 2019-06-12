using JadeFramework.Core.Dapper;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MsSystem.WF.Model
{
    /// <summary>
    /// 流程通知节点表
    /// </summary>
    [Table("wf_workflow_notice")]
    public class WfWorkflowNotice
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
        /// 通知节点ID
        /// </summary>
        public Guid NodeId { get; set; }

        /// <summary>
        /// 通知节点名称
        /// </summary>
        public string NodeName { get; set; }

        /// <summary>
        /// 执行人
        /// </summary>
        public string Maker { get; set; }

        /// <summary>
        /// 是否已经流转过
        /// </summary>
        public byte IsTransition { get; set; }

        /// <summary>
        /// 状态，退回时候用
        /// </summary>
        public byte Status { get; set; }

        /// <summary>
        /// 是否已阅
        /// </summary>
        public byte IsRead { get; set; }
    }

    internal sealed class WfWorkflowNoticeMapper : ClassMapper<WfWorkflowNotice>
    {
        public WfWorkflowNoticeMapper()
        {
            Table("wf_workflow_notice");
            AutoMap();
        }
    }
}
