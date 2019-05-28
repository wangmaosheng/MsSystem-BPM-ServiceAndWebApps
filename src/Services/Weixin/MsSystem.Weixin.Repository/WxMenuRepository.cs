using JadeFramework.Dapper;
using JadeFramework.Dapper.SqlGenerator;
using MsSystem.Weixin.IRepository;
using MsSystem.Weixin.Model;
using System.Data;

namespace MsSystem.Weixin.Repository
{
    public class WxMenuRepository : DapperRepository<WxMenu>, IWxMenuRepository
    {
        public WxMenuRepository(IDbConnection connection, SqlGeneratorConfig config) : base(connection, config)
        {
        }
    }
}
