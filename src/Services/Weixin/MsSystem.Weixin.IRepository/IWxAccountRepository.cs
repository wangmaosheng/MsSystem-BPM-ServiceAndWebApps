using JadeFramework.Core.Domain.Entities;
using JadeFramework.Dapper;
using MsSystem.Weixin.Model;
using MsSystem.Weixin.ViewModel;
using System.Threading.Tasks;

namespace MsSystem.Weixin.IRepository
{
    public interface IWxAccountRepository : IDapperRepository<WxAccount>
    {
        Task<Page<WxAccountListDto>> GetPageAsync(int pageIndex, int pageSize);
    }
}
