using Dapper;
using JadeFramework.Core.Domain.Entities;
using JadeFramework.Dapper;
using JadeFramework.Dapper.SqlGenerator;
using MsSystem.Sys.IRepository;
using MsSystem.Sys.Models;
using System;
using System.Data;
using System.Threading.Tasks;

namespace MsSystem.Sys.Repository
{
    public class SysScheduleRepository : DapperRepository<SysSchedule>,ISysScheduleRepository
    {
        public SysScheduleRepository(IDbConnection connection, SqlGeneratorConfig config) : base(connection, config)
        {
        }

        /// <summary>
        /// 分页获取调度
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<Page<SysSchedule>> GetPageListAsync(int pageIndex, int pageSize)
        {
            Page<SysSchedule> page = new Page<SysSchedule>
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            int offset = pageSize * (pageIndex - 1);
            string sql = "SELECT * FROM `sys_schedule` LIMIT @offset,@pageSize";
            page.Items = await this.QueryAsync(sql, new { offset = offset, pageSize = pageSize });
            page.TotalItems = this.ExecuteScalar<int>("SELECT COUNT(1) FROM sys_schedule");
            return page;
        }

        public async Task<bool> AddSceduleAsync(SysSchedule schedule)
        {
            schedule.CreateTime = DateTime.Now;
            string sql = @"INSERT INTO `sys_schedule`(`JobName`,`JobGroup`,`JobStatus`,`TriggerType`,`Cron`,`AssemblyName`,`ClassName`,`Remark`,`CreateTime`,`UpdateTime`,`RunTimes`,`BeginTime`,`EndTime`,`IntervalSecond`,`Url`,`Status`)
VALUES(@JobName,@JobGroup,@JobStatus,@TriggerType,@Cron,@AssemblyName,@ClassName,@Remark,@CreateTime,@UpdateTime,@RunTimes,@BeginTime,@EndTime,@IntervalSecond,@Url,@Status)";
            int res = await this.Connection.ExecuteAsync(sql, schedule);
            return res > 0;
        }

        public async Task<bool> UpdateScheduleAsync(SysSchedule schedule)
        {
            schedule.UpdateTime = DateTime.Now;
            string sql = @"UPDATE `sys_schedule` 
                                SET `JobName`=@JobName,
                                `JobGroup`=@JobGroup,
                                `TriggerType`=@TriggerType,
                                `Cron`=@Cron,
                                `AssemblyName`=@AssemblyName,
                                `ClassName`=@ClassName,
                                `Remark`=@Remark,
                                `UpdateTime`=@UpdateTime,
                                `RunTimes`=@RunTimes,
                                `BeginTime`=@BeginTime,
                                `EndTime`=@EndTime,
                                `IntervalSecond`=@IntervalSecond,
                                `Url`=@Url 
                                WHERE `JobId`=@JobId ";
            int res = await this.Connection.ExecuteAsync(sql, schedule);
            return res > 0;
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task<bool> UpdateStatusAsync(long id, int status)
        {
            string sql = @"UPDATE `sys_schedule`  SET `Status`=@status WHERE JobId = @id";
            int res = await this.Connection.ExecuteAsync(sql, new { status = status, id = id });
            return res > 0;
        }

        /// <summary>
        /// 更新任务状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="jobstatus"></param>
        /// <returns></returns>
        public async Task<bool> UpdateJobStatusAsync(long id, int jobstatus)
        {
            string sql = @"UPDATE `sys_schedule`  SET `JobStatus`=@jobstatus WHERE JobId = @id";
            int res = await this.Connection.ExecuteAsync(sql, new { jobstatus = jobstatus, id = id });
            return res > 0;
        }
    }
}
