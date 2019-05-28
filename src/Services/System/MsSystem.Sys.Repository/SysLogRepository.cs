using Dapper;
using JadeFramework.Core.Domain.Entities;
using JadeFramework.Core.Domain.Enum;
using JadeFramework.Core.Extensions;
using JadeFramework.Dapper;
using JadeFramework.Dapper.SqlGenerator;
using MsSystem.Sys.IRepository;
using MsSystem.Sys.Model;
using MsSystem.Sys.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSystem.Sys.Repository
{
    public class SysLogRepository : DapperRepository<SysLog>, ISysLogRepository
    {
        public SysLogRepository(IDbConnection connection, SqlGeneratorConfig config) : base(connection, config)
        {
        }

        public async Task<Page<SysLog>> GetPageAsync(LogSearchDto model)
        {
            Page<SysLog> page = new Page<SysLog>()
            {
                PageIndex = model.PageIndex,
                PageSize = model.PageSize
            };
            string sqlwhere = "WHERE 1=1 ";
            if (model.logLevel != null)
            {
                sqlwhere += $"AND level = '{model.logLevel.GetDescription()}' ";
            }
            if (model.Application != null)
            {
                sqlwhere += $" AND Application = '{model.Application.GetDescription()}' ";
            }
            string sql = $"SELECT * FROM log {sqlwhere} ORDER BY Id DESC  LIMIT {model.OffSet()},{model.PageSize}";
            page.Items = await this.QueryAsync(sql);

            string sqlcount = $"SELECT COUNT(1) FROM log {sqlwhere}";
            page.TotalItems = await this.Connection.ExecuteScalarAsync<int>(sqlcount);
            return page;
        }

        /// <summary>
        /// 根据日志级别获取日志统计信息
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public async Task<Dictionary<object, object>> GetChartsAsync(LogLevel level)
        {
            return await Task.Run(() =>
            {
                string sql = $@"SELECT DATE_FORMAT(Logged,'%Y-%m-%d %H') AS CreateDate,COUNT(Logged) AS Count  FROM  `log` WHERE LEVEL='{level.ToString()}' GROUP BY CreateDate ORDER BY CreateDate ASC";
                var list = Connection.Query(sql, null).ToDictionary(row => row.CreateDate, row => row.Count);
                return list;
            });
        }

        public async Task<HeartBeatData> GetLasterDataAsync(int recentMinutes, Application application)
        {

            int pollSeconds = 20;
            int length = recentMinutes * 60 / pollSeconds; //确定多少区段

            TimeSection[] times = new TimeSection[length];

            for (int i = length - 1; i >= 0; i--)
            {
                if (i == length - 1)
                {
                    TimeSection firsSection = new TimeSection
                    {
                        EndTime = DateTime.Now
                    };
                    firsSection.StartTime = firsSection.EndTime.AddSeconds(-1 * pollSeconds);
                    times[i] = firsSection;
                }
                else
                {
                    TimeSection timeSection = new TimeSection();
                    timeSection.EndTime = times[i + 1].StartTime;
                    timeSection.StartTime = timeSection.EndTime.AddSeconds(-1 * pollSeconds);
                    times[i] = timeSection;
                }

            }


            StringBuilder sql = new StringBuilder();
            sql.Append($" SELECT logged FROM `log` WHERE LEVEL='Info' AND message='心跳检测' AND Application='{application.GetDescription().ToString()}' AND ");

            List<string> timeList = new List<string>();

            for (int i = 0; i < times.Length; i++)
            {
                timeList.Add($" (logged >'{times[i].StartTime}' AND logged <= '{times[i].EndTime}') ");
            }
            string skipsql = timeList.Join(" OR ");
            sql.Append(" ( " + skipsql + " ) ");

            sql.Append(" ORDER BY logged DESC ");
            var loggedlist = Connection.Query<DateTime>(sql.ToString(), null).ToList();
            HeartBeatData data = new HeartBeatData();
            data.XData = new string[times.Length + 1];
            data.YData = new decimal[times.Length + 1];
            for (int i = 0; i < times.Length; i++)
            {
                var item = times[i];
                var first = loggedlist.FirstOrDefault(m => m > item.StartTime && m <= item.EndTime);
                data.XData[i] = item.StartTime.ToString("HH:mm:ss");
                if (first != null)
                {
                    if ((i + 1) % 2 == 1)
                    {
                        data.YData[i] = 1;
                    }
                    else
                    {
                        data.YData[i] = -1;
                    }
                }
                else
                {
                    data.YData[i] = 0;
                }
            }
            data.XData[times.Length] = times.Last().EndTime.ToString("HH:mm:ss");
            data.YData[times.Length] = data.YData[times.Length-1] * -1;
            return data;
        }
    }
}
