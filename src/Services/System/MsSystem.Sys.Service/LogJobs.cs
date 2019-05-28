using JadeFramework.Core.Extensions;
using MsSystem.Sys.IRepository;
using MsSystem.Sys.IService;
using NLog;
using System;

namespace MsSystem.Sys.Service
{
    /// <summary>
    /// 日志Job
    /// </summary>
    public class LogJobs : ILogJobs
    {
        private readonly Logger nlog = LogManager.GetCurrentClassLogger(); //获得日志实;
        private readonly ISysDatabaseFixture databaseFixture;

        public LogJobs(ISysDatabaseFixture databaseFixture)
        {
            this.databaseFixture = databaseFixture;
        }

        /// <summary>
        /// 登录日志记录
        /// </summary>
        public void LoginLog(long userid,string username)
        {
            string message = $"用户名：【{username}】,用户ID:【{userid}】登录成功";
            //nlog.Log(LogLevel.Info, message);
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
            nlog.Log(LogLevel.Error, exception, message);
        }

    }
}
