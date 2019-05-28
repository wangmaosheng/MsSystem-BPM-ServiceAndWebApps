using System;
using System.ComponentModel;

namespace MsSystem.Web.Areas.OA.ViewModel
{
    public class OaLeaveDto
    {
        public int Id { get; set; }
        public string LeaveCode { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 请假人
        /// </summary>
        public int UserId { get; set; }
        public string UserName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public long CreateTime { get; set; }

        /// <summary>
        /// 流程状态
        /// </summary>
        public int FlowStatus { get; set; }
    }

    public class OaLeaveShowDto
    {
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
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

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
    /// 请假类型
    /// </summary>
    public enum OaLeaveType
    {
        [Description("事假")]
        CompassionateLeave = 0,
        [Description("病假")]
        SickLeave = 1,
        [Description("年假")]
        AnnualLeave = 2,
        [Description("婚假")]
        MarriageLeave = 3,
        [Description("产假/陪产假")]
        MaternityLeave = 4,
        [Description("丧假")]
        FuneralLeave = 5,
        [Description("探亲假")]
        FamilyLeave = 6,
    }
}
