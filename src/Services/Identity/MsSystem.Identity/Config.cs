using IdentityServer4.Models;
using System.Collections.Generic;

namespace MsSystem.Identity
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>()
            {
                new ApiResource("mssystem_schedule_api","mssystem_schedule_api"),
                new ApiResource("mssystem_api","mssystem_api"),
                new ApiResource("mssystem_weixin_api","mssystem_weixin_api"),
                new ApiResource("mssystem_wf_api","mssystem_wf_api"),
                new ApiResource("mssystem_sys_api","mssystem_sys_api"),
                new ApiResource("gateway_api","gateway_api"),
                new ApiResource("user_identity","user_identity")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>()
            {
                new Client(){
                     ClientId="mssystem",
                     ClientSecrets={ new Secret("123".Sha256())},
                     AllowedScopes={ "mssystem_wf_api", "mssystem_schedule_api", "mssystem_weixin_api", "mssystem_api", "mssystem_sys_api", "gateway_api","user_identity"},
                     AllowedGrantTypes={GrantType.ClientCredentials},
                     AccessTokenLifetime = 60*60*2
                }
            };
        }
    }
}
