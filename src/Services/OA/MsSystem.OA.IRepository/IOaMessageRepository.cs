using JadeFramework.Core.Domain.Entities;
using JadeFramework.Dapper;
using MsSystem.OA.Model;
using MsSystem.OA.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MsSystem.OA.IRepository
{
    public interface IOaMessageRepository : IDapperRepository<OaMessage>
    {
        Task<Page<OaMessage>> GetPageAsync(int pageIndex, int pageSize);
        Task<Page<OaMessageMyList>> MyListAsync(OaMessageMyListSearch search);

        /// <summary>
        /// 根据id集合获取可用消息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<IEnumerable<OaMessage>> GetByIds(List<long> ids);
    }
}
