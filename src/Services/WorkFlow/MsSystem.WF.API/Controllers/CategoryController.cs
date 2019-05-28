using DotNetCore.CAP;
using JadeFramework.Core.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MsSystem.WF.IService;
using MsSystem.WF.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MsSystem.WF.API.Controllers
{
    [Authorize]
    [Route("api/Category/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IWorkflowCategoryService categoryService;
        private readonly ICapPublisher capPublisher;

        public CategoryController(IWorkflowCategoryService categoryService, ICapPublisher capPublisher)
        {
            this.categoryService = categoryService;
            this.capPublisher = capPublisher;
        }

        [HttpGet]
        public async Task<List<CategoryTreeListDto>> GetTreeListAsync()
        {
            await capPublisher.PublishAsync("WorkFlowStatusChanged", DateTime.Now);
            return await categoryService.GetTreeListAsync();
        }

        [HttpGet]
        public async Task<List<ZTree>> GetCategoryTreeAsync()
        {
            return await categoryService.GetCategoryTreeAsync();
        }

    }
}
