using JadeFramework.Dapper;
using JadeFramework.Dapper.SqlGenerator;
using MsSystem.Weixin.IRepository;
using MsSystem.Weixin.Model;
using System.Data;

namespace MsSystem.Weixin.Repository
{
    public class WxTextResponseRepository : DapperRepository<WxTextResponse>, IWxTextResponseRepository
    {
        public WxTextResponseRepository(IDbConnection connection, SqlGeneratorConfig config) : base(connection, config)
        {
        }
    }
}
