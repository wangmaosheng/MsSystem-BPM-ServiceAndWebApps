using JadeFramework.Core.Domain.Entities;
using JadeFramework.Core.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MsSystem.Utility.Filters;
using MsSystem.Web.Areas.WF.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MsSystem.Web.Areas.WF.Controllers
{
    [Area("WF")]
    [Authorize]
    public class CategoryController: BaseController
    {
        private readonly IWorkflowCategoryService categoryService;
        private readonly IWorkFlowService workFlowService;

        public CategoryController(IWorkflowCategoryService categoryService, IWorkFlowService workFlowService)
        {
            this.categoryService = categoryService;
            this.workFlowService = workFlowService;
        }
        [HttpGet]
        [Permission("/WF/Category/Index")]
        public async Task<IActionResult> Index()
        {
            var res = await categoryService.GetTreeListAsync();
            return View(res);
        }
        [HttpGet]
        public IActionResult Show()
        {
            return View();
        }

        [HttpGet]
        public async Task<List<ZTree>> GetCategoryTreeAsync()
        {
            var trees = await categoryService.GetCategoryTreeAsync();
            trees.Add(new ZTree
            {
                id = default(Guid).ToString(),
                name = "流程分类",
                open = true,
                pId = Guid.NewGuid().ToString()
            });
            return trees;
        }

    }
}
