using JadeFramework.Core.Extensions;
using System;

namespace MsSystem.Weixin.IRepository
{
    public interface IWeixinDatabaseFixture : IDisposable, IAutoDenpendencyScoped
    {
        IWeixinDbContext Db { get; }
    }
}