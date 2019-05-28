using JadeFramework.Core.Dapper;
using JadeFramework.WorkFlow;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MsSystem.OA.Model
{
    /// <summary>
    /// 员工请假
    /// </summary>
    [Table("oa_leave")]
    public class OaLeave: OtherEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key, Identity]
        public int Id { get; set; }

        /// <summary>
        /// 请假编号
        /// </summary>
	    public string LeaveCode { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 请假人
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 工作代理人
        /// </summary>
        public int AgentId { get; set; }

        /// <summary>
        /// 请假类型
        /// </summary>
        public byte LeaveType { get; set; }

        /// <summary>
        /// 请假原因
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// 请假天数
        /// </summary>
        public int Days { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public long StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public long EndTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public int CreateUserId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public long CreateTime { get; set; }

    }

    /// <summary>
    /// 映射
    /// </summary>
    public sealed class OaLeaveMapper : ClassMapper<OaLeave>
    {
        public OaLeaveMapper()
        {
            Table("oa_leave");
            AutoMap();
        }
    }
}
