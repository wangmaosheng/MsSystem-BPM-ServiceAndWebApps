using System;

namespace MsSystem.OA.IRepository
{

    public interface IOaDatabaseFixture : IDisposable
    {
        IOaDbContext Db { get; }
    }
}
