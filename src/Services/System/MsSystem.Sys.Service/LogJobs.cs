using JadeFramework.Core.Extensions;
using Microsoft.Extensions.Logging;
using MsSystem.Sys.IRepository;
using MsSystem.Sys.IService;
using System;

namespace MsSystem.Sys.Service
{
    /// <summary>
    /// 日志Job
    /// </summary>
    public class LogJobs : ILogJobs
    {
        private readonly ISysDatabaseFixture databaseFixture;
        private readonly ILogger<LogJobs> logger;

        public LogJobs(ISysDatabaseFixture databaseFixture,ILogger<LogJobs> logger)
        {
            this.databaseFixture = databaseFixture;
            this.logger = logger;
        }

        /// <summary>
        /// 登录日志记录
        /// </summary>
        public void LoginLog(long userid,string username)
        {
            string message = $"用户名：【{username}】,用户ID:【{userid}】登录成功";
            databaseFixture.LogDb.SysLog.InsertAsync(new Model.SysLog()
            {
                Application = "MsSystem.Sys.API",
                Logged = DateTime.Now,
                Level = JadeFramework.Core.Domain.Enum.LogLevel.Login.ToString(),
                Message = message,
                Logger = "System"
            });
        }

        /// <summary>
        /// 异常记录
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="exception">异常</param>
        /// <returns></returns>
        public void ExceptionLog(long userid, Exception exception)
        {
            string message = "【系统主动记录】" + exception.ToLogMessage();
            logger.LogError(message);
        }

    }
}
