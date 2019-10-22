using JadeFramework.Dapper;
using JadeFramework.Dapper.SqlGenerator;
using MsSystem.Weixin.IRepository;
using MsSystem.Weixin.Model;
using System;
using System.Data;
using System.Threading.Tasks;

namespace MsSystem.Weixin.Repository
{
    public class WxSecKillRepository : DapperRepository<WxSecKill>, IWxSecKillRepository
    {

        public WxSecKillRepository(IDbConnection connection, SqlGeneratorConfig config) : base(connection, config)
        {
        }

    }

}
