using JadeFramework.Core.Domain.Entities;
using JadeFramework.Core.Domain.Enum;
using JadeFramework.Core.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MsSystem.Utility.Filters;
using MsSystem.Web.Areas.WF.Service;
using MsSystem.Web.Areas.WF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MsSystem.Web.Areas.WF.Controllers
{
    [Area("WF")]
    [Authorize]
    public class CategoryController : BaseController
    {
        private readonly IWorkflowCategoryService categoryService;
        private readonly IWorkFlowService workFlowService;

        public CategoryController(IWorkflowCategoryService categoryService, IWorkFlowService workFlowService)
        {
            this.categoryService = categoryService;
            this.workFlowService = workFlowService;
        }
        [HttpGet]
        [Permission]
        public async Task<IActionResult> Index()
        {
            var res = await categoryService.GetTreeListAsync();
            return View(res);
        }
        [HttpGet]
        [Permission("/WF/Category/Index", ButtonType.View, true)]
        public async Task<IActionResult> Show(Guid? id)
        {
            CategoryDetailDto model;
            if (id != null && id != default(Guid))
            {
                model = await categoryService.GetCategoryDetailAsync(id.Value);
            }
            else
            {
                model = new CategoryDetailDto();
            }
            return View(model);
        }

        [HttpGet]
        [ActionName("GetCategoryTreeAsync")]
        public async Task<List<ZTree>> GetCategoryTreeAsync(Guid? id)
        {
            if (id != null && id != default(Guid))
            {
                var trees = await categoryService.GetCategoryTreeAsync();
                var res = trees.Where(m => m.id != id.ToString()).ToList();
                res.Add(new ZTree
                {
                    id = default(Guid).ToString(),
                    name = "无",
                    open = true,
                    pId = default(Guid).ToString(),
                });
                return res;
            }
            else
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

        [HttpPost]
        [Permission("/WF/Category/Index", ButtonType.Add, false)]
        [ActionName("InsertAsync")]
        public async Task<bool> InsertAsync([FromBody]CategoryDetailDto model)
        {
            model.UserId = UserIdentity.UserId.ToString();
            return await categoryService.InsertAsync(model);
        }

        [HttpPost]
        [Permission("/WF/Category/Index", ButtonType.Edit, false)]
        [ActionName("UpdateAsync")]
        public async Task<bool> UpdateAsync([FromBody]CategoryDetailDto model)
        {
            model.UserId = UserIdentity.UserId.ToString();
            return await categoryService.UpdateAsync(model);
        }

        [HttpPost]
        [Permission("/WF/Category/Index", ButtonType.Delete, false)]
        [ActionName("DeleteAsync")]
        public async Task<bool> DeleteAsync([FromBody]CategoryDeleteDto model)
        {
            model.UserId = UserIdentity.UserId.ToString();
            return await categoryService.DeleteAsync(model);
        }

    }
}
