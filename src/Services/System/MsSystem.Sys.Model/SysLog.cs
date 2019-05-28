using JadeFramework.Core.Dapper;
using JadeFramework.Core.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MsSystem.Sys.Model
{
    /// <summary>
    /// 日志表
    /// </summary>
    [Table("log")]
    public class SysLog : IEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key, Identity]
        public long Id { get; set; }

        /// <summary>
        /// 应用程序
        /// </summary>
        public string Application { get; set; }

        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime Logged { get; set; }

        /// <summary>
        /// 日志级别
        /// </summary>
        public string Level { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 日志记录开始地方
        /// </summary>
        public string Logger { get; set; }

        public string Callsite { get; set; }
        /// <summary>
        /// 异常信息
        /// </summary>
        public string Exception { get; set; }
    }

    public class SysLogMapper : ClassMapper<SysLog>
    {
        public SysLogMapper()
        {
            Table("log");
            AutoMap();
        }
    }
}
