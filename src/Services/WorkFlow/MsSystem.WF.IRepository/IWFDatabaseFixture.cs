using JadeFramework.Core.Extensions;
using System;

namespace MsSystem.WF.IRepository
{
    public interface IWFDatabaseFixture: IDisposable, IAutoDenpendencyScoped
    {
        IWFDbContext Db { get; }

    }
}
