using JadeFramework.Core.Domain.Entities;
using MsSystem.Sys.IRepository;
using MsSystem.Sys.IService;
using MsSystem.Sys.Models;
using MsSystem.Sys.Schedule.Infrastructure;
using MsSystem.Sys.Schedule.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MsSystem.Sys.Service
{
    public class SysScheduleService : ISysScheduleService
    {
        private readonly ISysDatabaseFixture databaseFixture;

        public SysScheduleService(ISysDatabaseFixture databaseFixture)
        {
            this.databaseFixture = databaseFixture;
        }

        /// <summary>
        /// 根据id获取调度
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<SysSchedule> GetScheduleAsync(long id)
        {
            return await databaseFixture.Db.SysSchedule.FindByIdAsync(id);
        }

        /// <summary>
        /// 添加或修改调度
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        public async Task<bool> AddOrUpdateAsync(SysSchedule schedule)
        {
            return schedule.JobId > 0
                ? await databaseFixture.Db.SysSchedule.UpdateScheduleAsync(schedule)//编辑
                : await databaseFixture.Db.SysSchedule.AddSceduleAsync(schedule);//新增
        }

        /// <summary>
        /// 获取全部调度
        /// </summary>
        /// <returns></returns>
        public async Task<List<ScheduleEntity>> GetEntityListAsync()
        {
            var list = await databaseFixture.Db.SysSchedule.FindAllAsync();
            return list.Select(m => new ScheduleEntity
            {
                JobId = m.JobId,
                JobGroup = m.JobGroup,
                JobName = m.JobName,
                Cron = m.Cron,
                Url = m.Url,
                BeginTime = m.BeginTime,
                AssemblyName = m.AssemblyName,
                ClassName = m.ClassName,
                EndTime = m.EndTime,
                RunTimes = m.RunTimes,
                IntervalSecond = m.IntervalSecond
            }).ToList();
        }

        /// <summary>
        /// 分页获取调度
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<Page<SysSchedule>> GetPageListAsync(int pageIndex, int pageSize)
        {
            return await databaseFixture.Db.SysSchedule.GetPageListAsync(pageIndex, pageSize);
        }

        /// <summary>
        /// 初始化调度
        /// </summary>
        /// <returns></returns>
        public async Task Initialize()
        {
            List<ScheduleEntity> list = await this.GetEntityListAsync();
            foreach (var item in list)
            {
                //加载调度
                ScheduleManage.Instance.AddScheduleList(item);
                ///运行
                await SchedulerCenter.Instance.RunScheduleJob<ScheduleManage>(item.JobGroup, item.JobName);
            }
            //判断默认任务是否存在
            DefaultJob defaultJob = new DefaultJob();
            if (list.Select(m => m.JobName).Intersect(defaultJob.Jobs.Select(n => n.JobName)).Count() <= 0)//判断是否有交集
            {
                foreach (var item in defaultJob.Jobs)
                {
                    if (list.Select(m => m.JobName).Contains(item.JobName) == false)
                    {
                        await this.AddOrUpdateAsync(new SysSchedule
                        {
                            JobId = item.JobId,
                            JobGroup = item.JobGroup,
                            JobName = item.JobName,
                            Cron = item.Cron,
                            Status = (byte)item.Status,
                            BeginTime = item.BeginTime,
                            AssemblyName = item.AssemblyName,
                            ClassName = item.ClassName,
                            CreateTime = DateTime.Now
                        });
                    }
                }
            }
            await AddDefaultJob(defaultJob);
        }

        public async Task AddDefaultJob(DefaultJob defaultJob)
        {
            foreach (var item in defaultJob.Jobs)
            {
                ScheduleManage.Instance.AddScheduleList(item);
                //if (item.JobName == defaultJob.WxSyncAccessTokenJob)
                //{
                //    await SchedulerCenter.Instance.RunScheduleJob<ScheduleManage, WxAccessTokenJob>(item.JobGroup, item.JobName);
                //}
                //else if (item.JobName == defaultJob.WxSyncUserInfoJob)
                //{
                //    await SchedulerCenter.Instance.RunScheduleJob<ScheduleManage, WxUserInfoJob>(item.JobGroup, item.JobName);
                //}
            }
        }

        /// <summary>
        /// 启动调度（不执行）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> StartAsync(long id)
        {
            return await databaseFixture.Db.SysSchedule.UpdateStatusAsync(id, 1);
        }

        /// <summary>
        /// 停止调度
        /// </summary>
        /// <param name="id">任务ID</param>
        /// <returns></returns>
        public async Task<bool> StopAsync(long id)
        {
            return await databaseFixture.Db.SysSchedule.UpdateStatusAsync(id, 0);
        }

        /// <summary>
        /// 立即执行任务
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> ExecuteJobAsync(long id)
        {
            var entity = await GetEntityById(id);
            ScheduleManage.Instance.AddScheduleList(entity);
            var result = await SchedulerCenter.Instance.RunScheduleJob<ScheduleManage>(entity.JobGroup, entity.JobName);
            if (result.Result == ScheduleDoResult.Success)
            {
                //更新任务执行状态
                await databaseFixture.Db.SysSchedule.UpdateJobStatusAsync(id, (int)JobStatus.已启用);
                return true;
            }
            else
            {
                return false;
            }
        }

        private async Task<ScheduleEntity> GetEntityById(long id)
        {
            var detail = await databaseFixture.Db.SysSchedule.FindByIdAsync(id);
            var entity = new ScheduleEntity
            {
                JobId = detail.JobId,
                JobGroup = detail.JobGroup,
                JobName = detail.JobName,
                Cron = detail.Cron,
                Url = detail.Url,
                BeginTime = detail.BeginTime,
                AssemblyName = detail.AssemblyName,
                ClassName = detail.ClassName,
                EndTime = detail.EndTime,
                RunTimes = detail.RunTimes,
                IntervalSecond = detail.IntervalSecond
            };
            return entity;
        }

        /// <summary>
        /// 暂停任务
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> SuspendAsync(long id)
        {
            var entity = await GetEntityById(id);
            var result = await SchedulerCenter.Instance.StopScheduleJob<ScheduleManage>(entity.JobGroup, entity.JobName);
            if (result.Result != ScheduleDoResult.Error)
            {
                return false;
            }
            else
            {
                await databaseFixture.Db.SysSchedule.UpdateJobStatusAsync(id, (int)JobStatus.已停止);
                return true;
            }
        }
    }
}
