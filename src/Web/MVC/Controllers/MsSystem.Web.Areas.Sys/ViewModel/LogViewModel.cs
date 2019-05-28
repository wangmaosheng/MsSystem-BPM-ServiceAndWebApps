using JadeFramework.Core.Domain.Entities;
using JadeFramework.Core.Domain.Enum;
using System;
using System.ComponentModel;

namespace MsSystem.Web.Areas.Sys.ViewModel
{

    public class LogSearchDto: BaseSearch
    {
        public LogLevel? logLevel { get; set; }

        public Application? Application { get; set; }
    }

    public class TimeSection
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }

    public class HeartBeatData
    {
        public string[] XData { get; set; }
        public decimal[] YData { get; set; }
    }



    public enum Application
    {
        [Description("MsSystem.Identity")]
        Identity = 0,
        [Description("MsSystem.Sys.API")]
        Sys = 1,
        [Description("MsSystem.OA.API")]
        OA = 2,
        [Description("MsSystem.WF.API")]
        WorkFlow = 3,
        [Description("MsSystem.WeiXin.API")]
        Weixin = 4,
        [Description("MsSystem.Schedule.API")]
        Schedule = 5,
        [Description("MsSystem.Web")]
        Web
    }
}
