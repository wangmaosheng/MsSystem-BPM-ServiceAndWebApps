using JadeFramework.Core.Dapper;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MsSystem.Sys.Models
{
    /// <summary>
    /// 任务调度表
    /// </summary>
    [Table("sys_schedule")]
    public class SysSchedule
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key, Identity]
        public long JobId { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string JobName { get; set; }

        /// <summary>
        /// 任务分组
        /// </summary>
        public string JobGroup { get; set; }

        /// <summary>
        /// 任务状态， 0 暂停任务；1 启用任务
        /// </summary>
        public byte JobStatus { get; set; }

        /// <summary>
        /// 触发器类型（0、simple 1、cron）
        /// </summary>
        public byte TriggerType { get; set; }

        /// <summary>
        /// 任务运行时间表达式
        /// </summary>
        public string Cron { get; set; }

        /// <summary>
        /// 任务所在DLL对应的程序集名称
        /// </summary>
        public string AssemblyName { get; set; }

        /// <summary>
        /// 任务所在类
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 任务描述
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 执行次数
        /// </summary>
        public int RunTimes { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 执行间隔时间, 秒为单位
        /// </summary>
        public int IntervalSecond { get; set; }

        /// <summary>
        /// job调用外部的url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 状态 0删除,1未删除
        /// </summary>
        public byte Status { get; set; } = 1;
    }
    internal class SysScheduleMapper : ClassMapper<SysSchedule>
    {
        public SysScheduleMapper()
        {
            Table("sys_schedule");
            AutoMap();
        }
    }

}
