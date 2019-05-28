using System;
using System.Collections.Generic;

namespace MsSystem.Web.Areas.WF.ViewModel
{
    public class WorkFlowStartDto
    {
        public Guid FlowId { get; set; }
        public string FlowCode { get; set; }
        public string FlowName { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public Guid FormId { get; set; }
        public string FormName { get; set; }
        public int FormType { get; set; }
        public string FormUrl { get; set; }
    }

    /// <summary>
    /// 流程明细
    /// </summary>
    public class WorkFlowDetailDto
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
        /// 表单名称
        /// </summary>
        public string FormName { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        public string CategoryName { get; set; }

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

        public string CreateUserId { get; set; }
    }

    public class WorkFlowLineDto
    {
        public Guid LineId { get; set; }
        public string Name { get; set; }
    }

    public class WorkFlowImageDto
    {
        /// <summary>
        /// 工作流ID
        /// </summary>
        public Guid FlowId { get; set; }

        /// <summary>
        /// 实例ID
        /// </summary>
        public Guid InstanceId { get; set; }

        /// <summary>
        /// 当前节点ID
        /// </summary>
        public Guid CurrentNodeId { get; set; }

        /// <summary>
        /// 流程JSON内容
        /// </summary>
        public string FlowContent { get; set; }
    }

    public class FlowDeleteDTO
    {
        public List<Guid> Ids { get; set; }
        public long UserId { get; set; }
    }
}
