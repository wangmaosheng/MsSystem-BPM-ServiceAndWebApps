using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MsSystem.Weixin.IService;
using System.Threading.Tasks;

namespace MsSystem.Weixin.API.Controllers
{
    [Authorize]
    [Route("api/User/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class UserController: ControllerBase
    {
        private readonly IWxUserService wxUserService;

        public UserController(IWxUserService wxUserService)
        {
            this.wxUserService = wxUserService;
        }

        [HttpPost]
        [ActionName("SyncWxUserInfoAsync")]
        public async Task SyncWxUserInfoAsync()
        {
            await wxUserService.SyncWxUserInfoAsync();
        }
    }
}
