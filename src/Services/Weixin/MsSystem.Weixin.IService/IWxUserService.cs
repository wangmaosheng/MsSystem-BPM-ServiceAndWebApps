using JadeFramework.Core.Extensions;
using System.Threading.Tasks;

namespace MsSystem.Weixin.IService
{
    public interface IWxUserService : IAutoDenpendencyScoped
    {
        /// <summary>
        /// 同步微信用户基本信息
        /// </summary>
        /// <returns></returns>
        Task<bool> SyncWxUserInfoAsync();
    }
}
