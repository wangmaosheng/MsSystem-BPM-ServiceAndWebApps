using Dapper;
using JadeFramework.Core.Domain.Entities;
using JadeFramework.Dapper;
using JadeFramework.Dapper.SqlGenerator;
using MsSystem.Sys.IRepository;
using MsSystem.Sys.Model;
using System.Data;
using System.Threading.Tasks;

namespace MsSystem.Sys.Repository
{
    public class SysReleaseLogRepository : DapperRepository<SysReleaseLog>, ISysReleaseLogRepository
    {
        public SysReleaseLogRepository(IDbConnection connection, SqlGeneratorConfig config) : base(connection, config)
        {
        }

        public async Task<Page<SysReleaseLog>> GetPageAsync(int pageIndex, int pageSize)
        {
            Page<SysReleaseLog> page = new Page<SysReleaseLog>()
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            int offset = (pageIndex - 1) * pageSize;
            string sql = $"SELECT * FROM sys_release_log ORDER BY id DESC LIMIT {offset},{pageSize}";
            string count = "SELECT COUNT(1) FROM sys_release_log";

            page.Items = await this.QueryAsync(sql);
            page.TotalItems = await this.Connection.ExecuteScalarAsync<int>(count);
            return page;
        }
    }
}
