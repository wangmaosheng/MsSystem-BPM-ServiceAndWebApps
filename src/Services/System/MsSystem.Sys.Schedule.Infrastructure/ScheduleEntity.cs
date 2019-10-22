using MsSystem.Sys.Schedule.Infrastructure.Enums;
using System;

namespace MsSystem.Sys.Schedule.Infrastructure
{
    /// <summary>
    /// 任务调度实体
    /// </summary>
    public class ScheduleEntity
    {
        /// <summary>
        /// 任务编号
        /// </summary>
        public long JobId { get; set; }
        /// <summary>
        /// 任务分组
        /// </summary>
        public string JobGroup { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        public string JobName { get; set; }
        /// <summary>
        /// 执行周期表达式
        /// </summary>
        public string Cron { get; set; }
        /// <summary>
        /// 外部地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 任务状态
        /// </summary>
        public JobStatus Status { get; set; }
        /// <summary>
        /// 任务运行状态
        /// </summary>
        public JobRunStatus RunStatus { get; set; }
        /// <summary>
        /// 下次执行时间
        /// </summary>
        public DateTime? NextTime { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BeginTime { get; set; }
        /// <summary>
        /// 任务所在DLL对应的程序集名称
        /// </summary>
        public string AssemblyName { get; set; }
        /// <summary>
        /// 任务所在类
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 停止时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 执行次数
        /// </summary>
        public int RunTimes { get; set; }
        /// <summary>
        /// 执行间隔时间, 秒为单位
        /// </summary>
        public int IntervalSecond { get; set; }
    }
}
