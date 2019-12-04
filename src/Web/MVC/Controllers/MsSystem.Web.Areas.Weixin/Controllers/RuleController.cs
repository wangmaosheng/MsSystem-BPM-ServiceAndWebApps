using JadeFramework.Core.Domain.Enum;
using JadeFramework.Core.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MsSystem.Utility.Filters;
using MsSystem.Web.Areas.Weixin.Service;
using MsSystem.Web.Areas.Weixin.ViewModel;
using System.Threading.Tasks;

namespace MsSystem.Web.Areas.Weixin.Controllers
{
    [Area("Weixin")]
    [Authorize]
    public class RuleController : BaseController
    {
        private readonly IRuleService ruleService;

        public RuleController(IRuleService ruleService)
        {
            this.ruleService = ruleService;
        }

        [HttpGet]
        [Permission]
        public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 10)
        {
            var res = await ruleService.GetRulePageAsync(pageIndex, pageSize);
            return View(res);
        }

        [HttpGet]
        [Permission]
        public async Task<IActionResult> Show(int? id)
        {
            if (id > 0)
            {
                var res = await ruleService.GetRuleReplyAsync(id.Value);
                return View(res);
            }
            else
            {
                return View(new RuleReplyDto());
            }
        }

        [HttpPost]
        [Permission("/Weixin/Rule/Index", ButtonType.Add, false)]
        [ActionName("Add")]
        public async Task<IActionResult> Add([FromBody]RuleReplyDto model)
        {
            var res = await ruleService.AddAsync(model);
            return Ok(res);
        }

        [HttpPost]
        [Permission("/Weixin/Rule/Index", ButtonType.Edit, false)]
        [ActionName("Update")]
        public async Task<IActionResult> Update([FromBody]RuleReplyDto model)
        {
            var res = await ruleService.UpdateAsync(model);
            return Ok(res);
        }


    }
}
