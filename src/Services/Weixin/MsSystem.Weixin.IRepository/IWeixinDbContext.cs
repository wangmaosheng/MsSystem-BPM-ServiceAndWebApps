using JadeFramework.Dapper.DbContext;
using JadeFramework.Core.Extensions;

namespace MsSystem.Weixin.IRepository
{
    public interface IWeixinDbContext : IDapperDbContext, IAutoDenpendencyScoped
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
