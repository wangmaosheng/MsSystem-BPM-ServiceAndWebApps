using System.Threading.Tasks;

namespace MsSystem.Weixin.IService
{
    public interface IWxUserService
    {
        /// <summary>
        /// 同步微信用户基本信息
        /// </summary>
        /// <returns></returns>
        Task<bool> SyncWxUserInfoAsync();
    }
}
