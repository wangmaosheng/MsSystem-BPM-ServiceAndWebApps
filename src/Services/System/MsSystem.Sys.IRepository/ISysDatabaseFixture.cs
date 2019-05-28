using System;

namespace MsSystem.Sys.IRepository
{
    public interface ISysDatabaseFixture : IDisposable
    {
        ISysDbContext Db { get; }
        ISysLogDbContext LogDb { get; }
    }
}
