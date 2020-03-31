using JadeFramework.Core.Dapper;
using JadeFramework.WorkFlow;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MsSystem.WF.Model
{
    /// <summary>
    /// 工作流表
    /// </summary>
    [Table("wf_workflow")]
    public class WfWorkflow : Entity
    {
        /// <summary>
        /// 工作流ID
        /// </summary>
        [Key]
        public Guid FlowId { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string FlowCode { get; set; }

        /// <summary>
        /// 分类ID
        /// </summary>
        public Guid CategoryId { get; set; }

        /// <summary>
        /// 表单ID
        /// </summary>
        public Guid FormId { get; set; }


        /// <summary>
        /// 流程名称
        /// </summary>
        public string FlowName { get; set; }

        /// <summary>
        /// 流程JSON内容
        /// </summary>
        public string FlowContent { get; set; }

        /// <summary>
        /// 流程版本 默认值为1
        /// </summary>
        public int FlowVersion { get; set; } = 1;

        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 是否启用 1：是 0: 否
        /// </summary>
        public int Enable { get; set; }

        /// <summary>
        /// 是否是旧版本
        /// </summary>
        public int IsOld { get; set; } = 0;
    }

    internal sealed class WfWorkflowMapper : ClassMapper<WfWorkflow>
    {
        public WfWorkflowMapper()
        {
            Table("wf_workflow");
            AutoMap();
        }
    }
}
