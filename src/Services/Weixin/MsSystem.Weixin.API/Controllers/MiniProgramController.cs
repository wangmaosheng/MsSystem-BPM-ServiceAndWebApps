using JadeFramework.Weixin.MiniProgram;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MsSystem.Weixin.IService;
using MsSystem.Weixin.ViewModel;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace MsSystem.Weixin.API.Controllers
{
    /// <summary>
    /// 小程序接口
    /// </summary>
    [Authorize]
    [Route("api/MiniProgram/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class MiniProgramController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWxMiniprogramUserService _userService;
        private readonly IHttpClientFactory _clientFactory;

        public MiniProgramController(IHttpClientFactory clientFactory, IConfiguration configuration, IWxMiniprogramUserService userService)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
            _userService = userService;
        }

        [HttpPost]
        [ActionName("Login")]
        public async Task<MiniprogramLoginResult> Login([FromBody]LoginDTO login)
        {
            jscode2session data = await GetOpenIdAsync(login.Code);
            if (data == null)
            {
                return new MiniprogramLoginResult
                {
                    StatusCode = MiniProgramResultCode.error
                };
            }
            var dbuser = await _userService.GetByOpenIdAsync(data.openid);
            if (dbuser == null)
            {
                return new MiniprogramLoginResult()
                {
                    StatusCode = MiniProgramResultCode.unregistered,
                    Message = "用户未注册"
                };
            }
            else
            {
                return new MiniprogramLoginResult
                {
                    StatusCode = MiniProgramResultCode.ok,
                    Data = new MiniprogramLoginResultData
                    {
                        Id = dbuser.Id,
                        SessionId = data.session_key
                    }
                };
            }
        }
        [HttpPost]
        [ActionName("Register")]
        public async Task<MiniprogramRegisterResult> Register([FromBody]RegisterDTO register)
        {
            jscode2session data = await GetOpenIdAsync(register.Code);
            if (data == null)
            {
                return new MiniprogramRegisterResult
                {
                    StatusCode = MiniProgramResultCode.error
                };
            }
            var res = await _userService.RegisterAsync(data, register.RawData);
            return res;
        }
        private async Task<jscode2session> GetOpenIdAsync(string code)
        {
            var minprogram = _configuration.GetSection("WxMiniProgram");
            string url = $"https://api.weixin.qq.com/sns/jscode2session?appid={minprogram["AppId"]}&secret={minprogram["AppSecret"]}&js_code={code}&grant_type=authorization_code";
            var responseString = await _clientFactory.CreateClient().GetStringAsync(url);

            if (responseString.Contains("errcode"))
            {
                return null;
            }
            else
            {
                jscode2session data = JsonConvert.DeserializeObject<jscode2session>(responseString);
                return data;
            }

        }
    }
}
