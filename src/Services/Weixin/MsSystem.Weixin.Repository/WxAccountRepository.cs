using Dapper;
using JadeFramework.Core.Domain.Entities;
using JadeFramework.Dapper;
using JadeFramework.Dapper.SqlGenerator;
using MsSystem.Weixin.IRepository;
using MsSystem.Weixin.Model;
using MsSystem.Weixin.ViewModel;
using System.Data;
using System.Threading.Tasks;

namespace MsSystem.Weixin.Repository
{
    public class WxAccountRepository : DapperRepository<WxAccount>, IWxAccountRepository
    {
        public WxAccountRepository(IDbConnection connection, SqlGeneratorConfig config) : base(connection, config)
        {
        }


        public async Task<Page<WxAccountListDto>> GetPageAsync(int pageIndex, int pageSize)
        {
            Page<WxAccountListDto> page = new Page<WxAccountListDto>()
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            int offset = (pageIndex - 1) * pageSize;
            string sql = $"SELECT * FROM wx_account LIMIT {offset},{pageSize}";
            page.Items = await this.Connection.QueryAsync<WxAccountListDto>(sql);
            page.TotalItems = await this.Connection.ExecuteScalarAsync<int>($"SELECT COUNT(1) FROM wx_account ");
            return page;
        }

    }
}
