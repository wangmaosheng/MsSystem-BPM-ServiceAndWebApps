using JadeFramework.Core.Extensions;
using System;

namespace MsSystem.OA.IRepository
{

    public interface IOaDatabaseFixture : IDisposable, IAutoDenpendencyScoped
    {
        IOaDbContext Db { get; }
    }
}
