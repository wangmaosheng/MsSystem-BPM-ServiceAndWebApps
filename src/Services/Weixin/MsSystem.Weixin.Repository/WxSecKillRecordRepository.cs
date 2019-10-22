using JadeFramework.Dapper;
using JadeFramework.Dapper.SqlGenerator;
using MsSystem.Weixin.IRepository;
using MsSystem.Weixin.Model;
using System;
using System.Data;
using System.Threading.Tasks;

namespace MsSystem.Weixin.Repository
{
    public class WxSecKillRecordRepository : DapperRepository<WxSecKillRecord>, IWxSecKillRecordRepository
    {
        public WxSecKillRecordRepository(IDbConnection connection, SqlGeneratorConfig config) : base(connection, config)
        {
        }
    }

}
