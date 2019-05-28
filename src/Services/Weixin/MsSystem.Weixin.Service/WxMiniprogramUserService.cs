using JadeFramework.Core.Extensions;
using JadeFramework.Weixin.MiniProgram;
using MsSystem.Weixin.IRepository;
using MsSystem.Weixin.IService;
using MsSystem.Weixin.Model;
using MsSystem.Weixin.ViewModel;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace MsSystem.Weixin.Service
{
    public class WxMiniprogramUserService : IWxMiniprogramUserService
    {
        private readonly IWeixinDatabaseFixture databaseFixture;

        public WxMiniprogramUserService(IWeixinDatabaseFixture databaseFixture)
        {
            this.databaseFixture = databaseFixture;
        }

        public async Task<WxMiniprogramUser> GetByOpenIdAsync(string openId)
        {
            return await databaseFixture.Db.WxMiniprogramUser.FindAsync(m => m.OpenId == openId);
        }

        public async Task<MiniprogramRegisterResult> RegisterAsync(jscode2session data,string rowData)
        {
            var dbuser = await databaseFixture.Db.WxMiniprogramUser.FindAsync(m => m.OpenId == data.openid);
            if (dbuser != null)
            {
                var res = new MiniprogramRegisterResult()
                {
                    StatusCode = MiniProgramResultCode.error
                };
            }
            RegisterUserModel userModel = JsonConvert.DeserializeObject<RegisterUserModel>(rowData);
            WxMiniprogramUser user = new WxMiniprogramUser
            {
                CreateTime = DateTime.Now.ToTimeStamp(),
                OpenId = data.openid,
                UnionId = data.unionid,
                AvatarUrl = userModel.AvatarUrl,
                City = userModel.City,
                Country = userModel.Country,
                Gender = userModel.Gender,
                Language = userModel.Language,
                NickName = userModel.NickName,
                Province = userModel.Province
            };
            long userid = await databaseFixture.Db.WxMiniprogramUser.InsertReturnIdAsync(user);
            return new MiniprogramRegisterResult
            {
                Data = new MiniprogramRegisterData
                {
                    Id = userid,
                    SessionId = data.session_key
                },
                StatusCode = MiniProgramResultCode.ok
            };
        }
    }
}
