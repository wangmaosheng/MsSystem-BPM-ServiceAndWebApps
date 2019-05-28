using JadeFramework.WorkFlow;
using System;

namespace MsSystem.Web.Areas.WF.ViewModel
{
    public class WorkFlowInstanceDto
    {
        /// <summary>
        /// 创建人
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 创建人姓名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 流程ID
        /// </summary>
        public Guid FlowId { get; set; }
        /// <summary>
        /// 实例ID
        /// </summary>
        public Guid InstanceId { get; set; }
        /// <summary>
        /// 菜单类型
        /// </summary>
        public WorkFlowMenu MenuType { get; set; }
        /// <summary>
        /// 表单内容
        /// </summary>
        public string FormData { get; set; }

        public WorkFlowStatusChange StatusChange { get; set; }
    }

    /// <summary>
    /// 待办搜索实体
    /// </summary>
    public class WorkFlowTodoSearchDto
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string UserId { get; set; }
    }

    /// <summary>
    /// 用户工作流处理过
    /// </summary>
    public class WorkFlowOperationHistoryDto
    {
        public Guid InstanceId { get; set; }

        /// <summary>
        /// 流程ID
        /// </summary>
        public Guid FlowId { get; set; }

        /// <summary>
        /// 流程名称
        /// </summary>
        public string FlowName { get; set; }

        /// <summary>
        /// 实例编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 当前节点ID
        /// </summary>
        public Guid ActivityId { get; set; }

        /// <summary>
        /// 当前节点类型
        /// <see cref="WorkFlowInstanceNodeType"/>
        /// </summary>
        public int? ActivityType { get; set; }

        /// <summary>
        /// 当前节点名称
        /// </summary>
        public string ActivityName { get; set; }

        /// <summary>
        /// 上一个节点ID
        /// </summary>
        public Guid PreviousId { get; set; }

        /// <summary>
        /// 执行人
        /// </summary>
        public string MakerList { get; set; }
        public long CreateTime { get; set; }

        /// <summary>
        /// 操作内容
        /// </summary>
        public string Content { get; set; }
    }

    public class WorkFlowOperationHistorySearchDto
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string UserId { get; set; }
    }
    //public class SystemFlowDto
    //{
    //    /// <summary>
    //    /// 表单ID
    //    /// </summary>
    //    public string PageId { get; set; }
    //    /// <summary>
    //    /// 表单地址
    //    /// </summary>
    //    public string FormUrl { get; set; }
    //}
}
