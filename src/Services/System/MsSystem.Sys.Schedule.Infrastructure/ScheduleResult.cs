using MsSystem.Sys.Schedule.Infrastructure.Enums;

namespace MsSystem.Sys.Schedule.Infrastructure
{
    /// <summary>
    /// 调度结果
    /// </summary>
    public class ScheduleResult
    {
        /// <summary>
        /// 结果
        /// </summary>
        public ScheduleDoResult Result { get; set; }
        /// <summary>
        /// 返回消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 返回对象
        /// </summary>
        public object Data { get; set; }
    }
}
