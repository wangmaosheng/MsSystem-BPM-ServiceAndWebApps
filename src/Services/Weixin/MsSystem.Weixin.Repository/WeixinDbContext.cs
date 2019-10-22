using JadeFramework.Dapper.DbContext;
using JadeFramework.Dapper.SqlGenerator;
using MsSystem.Weixin.IRepository;
using MySql.Data.MySqlClient;

namespace MsSystem.Weixin.Repository
{
    public class WeixinDbContext : DapperDbContext, IWeixinDbContext
    {
        private readonly SqlGeneratorConfig _config = new SqlGeneratorConfig
        {
            SqlConnector = ESqlConnector.MySQL,
            UseQuotationMarks = true
        };
        public WeixinDbContext(string connectionString)
            : base(new MySqlConnection(connectionString))
        {

        }

        private IWxAccountRepository _wxAccount;
        public IWxAccountRepository WxAccount => _wxAccount ?? (_wxAccount = new WxAccountRepository(Connection, _config));

        private IWxRuleRepository _wxRule;
        public IWxRuleRepository WxRule => _wxRule ?? (_wxRule = new WxRuleRepository(Connection, _config));

        private IWxTextResponseRepository _wxTextResponse;
        public IWxTextResponseRepository WxTextResponse => _wxTextResponse ?? (_wxTextResponse = new WxTextResponseRepository(Connection, _config));

        private IWxKeywordRepository _keyword;
        public IWxKeywordRepository WxKeyword => _keyword ?? (_keyword = new WxKeywordRepository(Connection, _config));

        private IWxNewsResponseRepository _wxNewsResponse;
        public IWxNewsResponseRepository WxNewsResponse => _wxNewsResponse ?? (_wxNewsResponse = new WxNewsResponseRepository(Connection, _config));

        private IWxMenuRepository _wxMenu;
        public IWxMenuRepository WxMenu => _wxMenu ?? (_wxMenu = new WxMenuRepository(Connection, _config));

        private IWxUserRepository _wxUser;
        public IWxUserRepository WxUser => _wxUser ?? (_wxUser = new WxUserRepository(Connection, _config));

        private IWxMiniprogramUserRepository _wxMiniprogramUser;
        public IWxMiniprogramUserRepository WxMiniprogramUser => _wxMiniprogramUser ?? (_wxMiniprogramUser = new WxMiniprogramUserRepository(Connection, _config));


        private IWxSecKillRepository _wxSecKillRepository;
        public IWxSecKillRepository WxSecKillRepository => _wxSecKillRepository ?? (_wxSecKillRepository = new WxSecKillRepository(Connection, _config));

        private IWxSecKillRecordRepository _wxSecKillrecordRepository;
        public IWxSecKillRecordRepository WxSecKillRecordRepository => _wxSecKillrecordRepository ?? (_wxSecKillrecordRepository = new WxSecKillRecordRepository(Connection, _config));
    }
}
