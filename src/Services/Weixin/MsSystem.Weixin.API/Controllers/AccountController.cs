using JadeFramework.Core.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MsSystem.Weixin.IService;
using MsSystem.Weixin.ViewModel;
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

        [HttpGet]
        [ActionName("GetPageAsync")]
        public async Task<Page<WxAccountListDto>> GetPageAsync(int pageIndex, int pageSize)
        {
            return await wxAccountService.GetPageAsync(pageIndex, pageSize);
        }




        /// <summary>
        /// 获取最新的accessToken
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetAccessTokenAsync")]
        public async Task<string> GetAccessTokenAsync()
        {
            return await wxAccountService.GetAccessTokenAsync();
        }
    }
}
