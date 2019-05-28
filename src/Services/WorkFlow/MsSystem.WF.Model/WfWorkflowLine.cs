using JadeFramework.Core.Dapper;
using JadeFramework.WorkFlow;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MsSystem.WF.Model
{
    /// <summary>
    /// 工作流line通用条件值
    /// </summary>
    [Table("wf_workflow_line")]
    public class WfWorkflowLine : Entity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// 分组ID
        /// </summary>
        public Guid GroupId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 类型 <see cref="JadeFramework.WorkFlow.FlowLineSetInfoType"/>
        /// </summary>
        public string LineType { get; set; }

        /// <summary>
        /// 执行SQL
        /// </summary>
        public string ExecuteSQL { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public byte IsDel { get; set; }
    }

    internal sealed class WfWorkflowLineMapper : ClassMapper<WfWorkflowLine>
    {
        public WfWorkflowLineMapper()
        {
            Table("wf_workflow_line");
            AutoMap();
        }
    }
}
