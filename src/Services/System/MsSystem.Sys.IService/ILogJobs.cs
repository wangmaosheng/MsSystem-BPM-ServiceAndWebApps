using System;

namespace MsSystem.Sys.IService
{
    public interface ILogJobs
    {
        /// <summary>
        /// 登录日志
        /// </summary>
        /// <returns></returns>
        void LoginLog(long userid,string username);

        /// <summary>
        /// 异常记录
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="exception">异常</param>
        /// <returns></returns>
        void ExceptionLog(long userid, Exception exception);

    }
}
