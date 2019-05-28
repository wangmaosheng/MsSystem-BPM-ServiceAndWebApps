using MsSystem.Weixin.Model;
using System.Threading.Tasks;

namespace MsSystem.Weixin.IService
{
    public interface IWxAccountService
    {
        WxAccount GetAccount();
        Task<WxAccount> GetAccountAsync();
        /// <summary>
        /// 同步AccessToken数据
        /// </summary>
        /// <returns></returns>
        Task<string> GetAccessTokenAsync();
    }
}
