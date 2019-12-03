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
        [ActionName("GetTreeListAsync")]
        public async Task<List<CategoryTreeListDto>> GetTreeListAsync()
        {
            //await capPublisher.PublishAsync("WorkFlowStatusChanged", DateTime.Now);
            return await categoryService.GetTreeListAsync();
        }

        [HttpGet]
        [ActionName("GetCategoryTreeAsync")]
        public async Task<List<ZTree>> GetCategoryTreeAsync()
        {
            return await categoryService.GetCategoryTreeAsync();
        }

        [HttpGet]
        [ActionName("GetCategoryDetailAsync")]
        public async Task<CategoryDetailDto> GetCategoryDetailAsync(Guid id)
        {
            return await categoryService.GetCategoryDetailAsync(id);
        }

        [HttpPost]
        [ActionName("InsertAsync")]
        public async Task<bool> InsertAsync([FromBody]CategoryDetailDto model)
        {
            return await categoryService.InsertAsync(model);
        }

        [HttpPost]
        [ActionName("UpdateAsync")]
        public async Task<bool> UpdateAsync([FromBody]CategoryDetailDto model)
        {
            return await categoryService.UpdateAsync(model);
        }

        [HttpPost]
        [ActionName("DeleteAsync")]
        public async Task<bool> DeleteAsync([FromBody]CategoryDeleteDto model)
        {
            return await categoryService.DeleteAsync(model);
        }

    }
}
