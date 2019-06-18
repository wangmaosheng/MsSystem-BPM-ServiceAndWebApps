using IdentityModel.Client;
using JadeFramework.Cache;
using Microsoft.Extensions.Options;
using MsSystem.OA.ViewModel;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MsSystem.OA.API.Infrastructure
{
    public class TokenClientOptions
    {
        public string Address { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string GrantType { get; set; }
    }
    public class TokenClient
    {
        private ICachingProvider _cacheFactory;
        private readonly IOptions<AppSettings> _appSettings;

        public TokenClient(HttpClient client, ICachingProvider cachingProvider, IOptions<AppSettings> appSettings)
        {
            Client = client;
            _cacheFactory = cachingProvider;
            _appSettings = appSettings;
        }

        public HttpClient Client { get; }

        public async Task<string> GetToken()
        {
            string res = _cacheFactory.Get("accessToken") as string;
            if (string.IsNullOrEmpty(res))
            {
                var response = await Client.RequestTokenAsync(new TokenRequest
                {
                    Address = _appSettings.Value.MsApplication.url + _appSettings.Value.MsApplication.tokenurl,
                    ClientId = _appSettings.Value.MsApplication.client_id,
                    ClientSecret = _appSettings.Value.MsApplication.client_secret,
                    GrantType = _appSettings.Value.MsApplication.grant_type
                });
                if (response.AccessToken != null)
                {
                    _cacheFactory.Set("accessToken", response.AccessToken, new TimeSpan(0, 100, 0));
                    return response.AccessToken;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return res;
            }
        }
    }
}
