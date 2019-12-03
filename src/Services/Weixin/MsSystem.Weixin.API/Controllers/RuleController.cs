using JadeFramework.Core.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MsSystem.Weixin.IService;
using MsSystem.Weixin.ViewModel;
using System.Threading.Tasks;

namespace MsSystem.Weixin.API.Controllers
{
    [Authorize]
    [Route("api/Rule/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class RuleController : ControllerBase
    {
        private readonly IWxRuleService wxRuleService;

        public RuleController(IWxRuleService wxRuleService)
        {
            this.wxRuleService = wxRuleService;
        }

        /// <summary>
        /// 获取规则
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetRulePageAsync")]
        public async Task<Page<RuleListDto>> GetRulePageAsync(int pageIndex, int pageSize)
        {
            return await wxRuleService.GetRulePageAsync(pageIndex, pageSize);
        }

        /// <summary>
        /// 获取规则回复明细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetRuleReplyAsync")]
        public async Task<RuleReplyDto> GetRuleReplyAsync(int id)
        {
            return await wxRuleService.GetRuleReplyAsync(id);
        }

        [HttpPost]
        [ActionName("AddAsync")]
        public async Task<bool> AddAsync([FromBody]RuleReplyDto model)
        {
            return await wxRuleService.AddAsync(model);
        }

        [HttpPost]
        [ActionName("UpdateAsync")]
        public async Task<bool> UpdateAsync([FromBody]RuleReplyDto model)
        {
            return await wxRuleService.UpdateAsync(model);
        }

    }
}
