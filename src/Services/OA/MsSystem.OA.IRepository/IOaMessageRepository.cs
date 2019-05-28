using JadeFramework.Core.Domain.Entities;
using JadeFramework.Dapper;
using MsSystem.OA.Model;
using MsSystem.OA.ViewModel;
using System.Threading.Tasks;

namespace MsSystem.OA.IRepository
{
    public interface IOaMessageRepository : IDapperRepository<OaMessage>
    {
        Task<Page<OaMessage>> GetPageAsync(int pageIndex, int pageSize);
        Task<Page<OaMessageMyList>> MyListAsync(OaMessageMyListSearch search);
    }
}
