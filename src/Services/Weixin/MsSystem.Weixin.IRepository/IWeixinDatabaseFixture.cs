using System;

namespace MsSystem.Weixin.IRepository
{
    public interface IWeixinDatabaseFixture : IDisposable
    {
        IWeixinDbContext Db { get; }
    }
}