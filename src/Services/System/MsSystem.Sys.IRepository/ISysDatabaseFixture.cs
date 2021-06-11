using JadeFramework.Core.Extensions;
using System;

namespace MsSystem.Sys.IRepository
{
    public interface ISysDatabaseFixture : IDisposable, IAutoDenpendencyScoped
    {
        ISysDbContext Db { get; }
        ISysLogDbContext LogDb { get; }
    }
}
