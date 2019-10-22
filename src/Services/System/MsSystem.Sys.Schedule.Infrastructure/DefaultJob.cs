using System;
using System.Collections.Generic;

namespace MsSystem.Sys.Schedule.Infrastructure
{
    /// <summary>
    /// 默认要执行的Job
    /// </summary>
    public class DefaultJob
    {
        //public string WxSyncAccessTokenJob = "微信AccessToken自动同步任务";
        //public string WxSyncUserInfoJob = "微信用户信息同步任务";
        public DefaultJob()
        {
            this.Jobs = new List<ScheduleEntity>()
            {
                //new ScheduleEntity
                //{
                //    JobId = 0,
                //    JobGroup = "DefaultJob",
                //    JobName = WxSyncAccessTokenJob,
                //    Cron = "3 * * * * ? ",//每隔3秒钟执行一次
                //    Status = Enums.JobStatus.已启用,
                //    RunStatus = Enums.JobRunStatus.执行中,
                //    BeginTime = DateTime.Now,
                //    AssemblyName = "MsSystem.Sys.Schedule.Infrastructure.Job",
                //    ClassName = "WxAccessTokenJob"
                //},
                //new ScheduleEntity
                //{
                //    JobId = 0,
                //    JobGroup = "DefaultJob",
                //    JobName = WxSyncUserInfoJob,
                //    Cron = "0 0 0/1 * * ? ",//每隔一小时执行
                //    Status = Enums.JobStatus.已启用,
                //    RunStatus = Enums.JobRunStatus.执行中,
                //    BeginTime = DateTime.Now,
                //    AssemblyName = "MsSystem.Sys.Schedule.Infrastructure.Job",
                //    ClassName = "WxUserInfoJob"
                //}
            };
        }
        public List<ScheduleEntity> Jobs { get; set; }
    }
}
