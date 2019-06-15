using JadeFramework.Core.Domain.Entities;
using MsSystem.Weixin.Model;
using MsSystem.Weixin.ViewModel;
using System.Threading.Tasks;

namespace MsSystem.Weixin.IService
{
    public interface IWxAccountService
    {
        Task<Page<WxAccountListDto>> GetPageAsync(int pageIndex, int pageSize);













        WxAccount GetAccount();
        Task<WxAccount> GetAccountAsync();
        /// <summary>
        /// 同步AccessToken数据
        /// </summary>
        /// <returns></returns>
        Task<string> GetAccessTokenAsync();
    }
}
