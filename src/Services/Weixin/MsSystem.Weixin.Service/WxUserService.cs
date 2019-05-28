using MsSystem.Weixin.IRepository;
using MsSystem.Weixin.IService;
using MsSystem.Weixin.Model;
using Newtonsoft.Json;
using RestSharp;
using System.Threading.Tasks;

namespace MsSystem.Weixin.Service
{
    public class WxUserService : IWxUserService
    {
        private readonly IWeixinDatabaseFixture databaseFixture;

        public WxUserService(IWeixinDatabaseFixture databaseFixture)
        {
            this.databaseFixture = databaseFixture;
        }

        /// <summary>
        /// 同步微信用户基本信息
        /// </summary>
        /// <returns></returns>
        public async Task<bool> SyncWxUserInfoAsync()
        {
            var dbuser = await databaseFixture.Db.WxUser.FindAllAsync(m => m.Subscribe == 1 && m.IsSync == 0);
            var account = await databaseFixture.Db.WxAccount.FindAsync();
            foreach (var item in dbuser)
            {
                string userinfoUrl = "https://api.weixin.qq.com/cgi-bin/user/info";
                var request = new RestRequest(Method.GET);
                request.AddParameter("access_token", account.AccessToken);
                request.AddParameter("openid", item.OpenId);
                request.AddParameter("lang", "zh_CN");
                var client = new RestClient(userinfoUrl);
                IRestResponse response = await client.ExecuteTaskAsync(request);
                WxUser wxUser = JsonConvert.DeserializeObject<WxUser>(response.Content);
                wxUser.IsSync = 1;
                await databaseFixture.Db.WxUser.UpdateAsync(wxUser);
            }
            return true;
        }

    }
}
