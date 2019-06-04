using JadeFramework.Core.Dapper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MsSystem.WF.Model
{
    /// <summary>
    /// 用于工作流获取权限系统数据
    /// </summary>
    [Table("wf_workflowsql")]
    public class WfWorkflowsql
    {
        /// <summary>
        /// 流程sql名称,必须是以wf_为开头，用于判断属于哪个系统，方便调用接口
        /// </summary>
        [Key]
        public string Name { get; set; }

        /// <summary>
        /// 流程SQL，执行结果必须是一行一列
        /// </summary>
        public string FlowSQL { get; set; }

        /// <summary>
        /// 参数。以英文 , 分割
        /// </summary>
        public string Param { get; set; }

        /// <summary>
        /// 类型，0：选人节点，必须返回的是用户ID，1：连线条件，必须返回的是1或0
        /// </summary>
        public byte SQLType { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        public long CreateUserId { get; set; }

        /// <summary>
        /// 创建时间戳
        /// </summary>
        public long CreateTime { get; set; }
    }
    internal sealed class WfWorkflowsqlMapper : ClassMapper<WfWorkflowsql>
    {
        public WfWorkflowsqlMapper()
        {
            Table("wf_workflowsql");
            AutoMap();
        }
    }
}
