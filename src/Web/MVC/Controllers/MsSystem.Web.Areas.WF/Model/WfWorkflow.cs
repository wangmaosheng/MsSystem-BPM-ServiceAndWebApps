using JadeFramework.WorkFlow;
using System;
using System.ComponentModel.DataAnnotations;

namespace MsSystem.Web.Areas.WF.Model
{
    /// <summary>
    /// 工作流表
    /// </summary>
    public class WfWorkflow : Entity
    {
        /// <summary>
        /// 工作流ID
        /// </summary>
        public Guid FlowId { get; set; }

        /// <summary>
        /// 分类ID
        /// </summary>
        public Guid CategoryId { get; set; }

        /// <summary>
        /// 表单ID
        /// </summary>
        public Guid FormId { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string FlowCode { get; set; }

        /// <summary>
        /// 流程名称
        /// </summary>
        public string FlowName { get; set; }

        /// <summary>
        /// 流程JSON内容
        /// </summary>
        public string FlowContent { get; set; }

        /// <summary>
        /// 流程版本
        /// </summary>
        public int FlowVersion { get; set; }

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
        public int IsOld { get; set; }
    }
}
