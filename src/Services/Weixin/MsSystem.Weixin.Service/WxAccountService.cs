using JadeFramework.Core.Domain.Entities;
using JadeFramework.Weixin.Models;
using MsSystem.Weixin.IRepository;
using MsSystem.Weixin.IService;
using MsSystem.Weixin.Model;
using MsSystem.Weixin.ViewModel;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace MsSystem.Weixin.Service
{
    public class WxAccountService : IWxAccountService
    {
        private readonly IWeixinDatabaseFixture databaseFixture;

        public WxAccountService(IWeixinDatabaseFixture databaseFixture)
        {
            this.databaseFixture = databaseFixture;
        }

        public async Task<Page<WxAccountListDto>> GetPageAsync(int pageIndex, int pageSize)
        {
            return await databaseFixture.Db.WxAccount.GetPageAsync(pageIndex, pageSize);
        }

















        public WxAccount GetAccount()
        {
            return databaseFixture.Db.WxAccount.Find();
        }
        public async Task<WxAccount> GetAccountAsync()
        {
            return await databaseFixture.Db.WxAccount.FindAsync();
        }

        /// <summary>
        /// 同步AccessToken
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetAccessTokenAsync()
        {
            try
            {
                WxAccount account = await databaseFixture.Db.WxAccount.FindAsync();
                if (account.AccessTokenCreateTime == null || account.AccessTokenCreateTime.Value.AddSeconds(6000) < DateTime.Now)
                {
                    try
                    {
                        string accessTokenUrl = "https://api.weixin.qq.com/cgi-bin/token";
                        var request = new RestRequest(Method.GET);
                        request.AddParameter("grant_type", "client_credential");
                        request.AddParameter("appid", account.AppId);
                        request.AddParameter("secret", account.AppSecret);
                        var client = new RestClient(accessTokenUrl);
                        IRestResponse response = await client.ExecuteTaskAsync(request);
                        AccessToken accessToken = JsonConvert.DeserializeObject<AccessToken>(response.Content);
                        account.AccessToken = accessToken.access_token;
                        account.AccessTokenCreateTime = DateTime.Now;
                        await databaseFixture.Db.WxAccount.UpdateAsync(account);
                        return accessToken.access_token;
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
                else
                {
                    return account.AccessToken;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
