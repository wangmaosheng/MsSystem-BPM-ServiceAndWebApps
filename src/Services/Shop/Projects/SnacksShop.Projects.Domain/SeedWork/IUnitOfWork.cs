using System;
using System.Threading;
using System.Threading.Tasks;

namespace SnacksShop.Projects.Domain.SeedWork
{
    /// <summary>
    /// 工作单元
    /// </summary>
    public interface IUnitOfWork:IDisposable
    {
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken)); 
    }
}