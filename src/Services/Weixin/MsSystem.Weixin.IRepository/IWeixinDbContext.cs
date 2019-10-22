using JadeFramework.Dapper.DbContext;

namespace MsSystem.Weixin.IRepository
{
    public interface IWeixinDbContext : IDapperDbContext
    {
        IWxAccountRepository WxAccount { get; }
        IWxRuleRepository WxRule { get; }
        IWxTextResponseRepository WxTextResponse { get; }
        IWxKeywordRepository WxKeyword { get; }
        IWxNewsResponseRepository WxNewsResponse { get; }
        IWxMenuRepository WxMenu { get; }
        IWxUserRepository WxUser { get; }
        IWxMiniprogramUserRepository WxMiniprogramUser { get; }
        IWxSecKillRepository WxSecKillRepository { get; }
        IWxSecKillRecordRepository WxSecKillRecordRepository { get; }
    }
}
