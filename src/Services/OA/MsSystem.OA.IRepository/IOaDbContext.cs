using JadeFramework.Dapper.DbContext;
using JadeFramework.Core.Extensions;

namespace MsSystem.OA.IRepository
{
    public interface IOaDbContext : IDapperDbContext, IAutoDenpendencyScoped
    {
        IOaLeaveRepository OaLeaveRepository { get; }

        IOaMessageRepository OaMessage { get; }
        IOaMessageUserRepository OaMessageUser { get; }
        IOaWorkflowsqlRepository OaWorkflowsql { get; }
        IOaChatRepository OaChat { get; }
    }
}
