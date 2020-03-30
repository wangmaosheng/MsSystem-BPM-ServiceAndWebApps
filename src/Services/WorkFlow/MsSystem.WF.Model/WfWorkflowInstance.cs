using JadeFramework.Core.Dapper;
using JadeFramework.WorkFlow;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MsSystem.WF.Model
{
    /// <summary>
    /// 流程实例
    /// </summary>
    [Table("wf_workflow_instance")]
    public class WfWorkflowInstance: Entity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public Guid InstanceId { get; set; }

        /// <summary>
        /// 流程ID
        /// </summary>
        public Guid FlowId { get; set; }

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
        /// 为0表示全部人员
        /// </summary>
        public string MakerList { get; set; }

        /// <summary>
        /// 流程JSON内容
        /// </summary>
        public string FlowContent { get; set; }

        #region 结合起来判断流程是否结束
        /*              流转状态判断 实际情况组合
         * IsFinish=1 & Status=WorkFlowStatus.IsFinish      表示通过
         * IsFinish==null & Status=WorkFlowStatus.UnSubmit  表示未提交
         * IsFinish=0 & Status=WorkFlowStatus.Running       表示运行中
         * IsFinish=0 & Status=WorkFlowStatus.Deprecate     表示不同意 ???
         * IsFinish=0 & Status=WorkFlowStatus.Back          表示流程被退回
         * **/
        /// <summary>
        /// 流程节点是否结束
        /// 注：此字段代表工作流流转过程中运行的状态判断
        /// </summary>
        public int? IsFinish { get; set; }

        /// <summary>
        /// 用户操作状态<see cref="WorkFlowStatus"/>
        /// 注：此字段代表用户操作流程的状态
        /// </summary>
        public int Status { get; set; }

        #endregion

        /// <summary>
        /// 流程版本
        /// </summary>
        public int FlowVersion { get; set; } = 1;

        /// <summary>
        /// 创建人姓名
        /// </summary>
        public string CreateUserName { get; set; }

        public long UpdateTime { get; set; }
    }
    internal class WfWorkflowInstanceMapper : ClassMapper<WfWorkflowInstance>
    {
        public WfWorkflowInstanceMapper()
        {
            this.Table("wf_workflow_instance");
            this.AutoMap();
        }
    }
}
