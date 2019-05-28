using Dapper;
using JadeFramework.Core.Domain.Entities;
using JadeFramework.Dapper;
using JadeFramework.Dapper.SqlGenerator;
using MsSystem.OA.IRepository;
using MsSystem.OA.Model;
using MsSystem.OA.ViewModel;
using System.Data;
using System.Threading.Tasks;

namespace MsSystem.OA.Repository
{
    /// <summary>
    /// 员工请假仓储
    /// </summary>
    public class OaLeaveRepository : DapperRepository<OaLeave>, IOaLeaveRepository
	{
		public OaLeaveRepository(IDbConnection connection, SqlGeneratorConfig config) : base(connection, config)
		{
		}

        public async Task<Page<OaLeaveDto>> GetPageAsync(int pageIndex, int pageSize, long userid)
        {
            Page<OaLeaveDto> page = new Page<OaLeaveDto>
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            int offset = pageSize * (pageIndex - 1);
            string sql = $"SELECT ol.`Id`,ol.`LeaveCode`,ol.`Title`,ol.`UserId`,su.`UserName`,ol.`CreateTime`,ol.`FlowStatus` FROM `oa_leave` ol  " +
                $"INNER JOIN sys_user su ON su.`UserId`= ol.`CreateUserId` " +
                $"WHERE ol.`CreateUserId`= @userid ORDER BY ol.`Id` DESC LIMIT @offset, @pageSize ";
            page.Items = await this.Connection.QueryAsync<OaLeaveDto>(sql, new { userid=userid, offset = offset, pageSize = pageSize });
            page.TotalItems = await this.Connection.ExecuteScalarAsync<int>("SELECT COUNT(1) FROM `oa_leave` ol INNER JOIN sys_user su ON su.`UserId`= ol.`CreateUserId` WHERE ol.`CreateUserId`= @userid ", new { userid = userid });
            return page;
        }
    }
}
