using System;

namespace MsSystem.WF.IRepository
{
    public interface IWFDatabaseFixture: IDisposable
    {
        IWFDbContext Db { get; }

    }
}
