using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MsSystem.Weixin.IService;
using System.Threading.Tasks;

namespace MsSystem.Weixin.API.Controllers
{
    [Authorize]
    [Route("api/Account/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IWxAccountService wxAccountService;

        public AccountController(IWxAccountService wxAccountService)
        {
            this.wxAccountService = wxAccountService;
        }
        [HttpPost]
        public async Task<string> GetAccessTokenAsync()
        {
            return await wxAccountService.GetAccessTokenAsync();
        }
    }
}
