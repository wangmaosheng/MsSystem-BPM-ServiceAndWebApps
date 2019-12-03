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
    [Route("api/Form/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class FormController: ControllerBase
    {
        private readonly IFormService formService;

        public FormController(IFormService formService)
        {
            this.formService = formService;
        }

        [HttpGet]
        [ActionName("GetPageAsync")]
        public async Task<Page<FormPageDto>> GetPageAsync(int pageIndex, int pageSize)
        {
            return await formService.GetPageAsync(pageIndex, pageSize);
        }

        [HttpGet]
        [ActionName("GetFormDetailAsync")]
        public async Task<FormDetailDto> GetFormDetailAsync(Guid id)
        {
            return await formService.GetFormDetailAsync(id);
        }

        [HttpPost]
        [ActionName("InsertAsync")]
        public async Task<bool> InsertAsync([FromBody]FormDetailDto model)
        {
            return await formService.InsertAsync(model);
        }

        [HttpPost]
        [ActionName("UpdateAsync")]
        public async Task<bool> UpdateAsync([FromBody]FormDetailDto model)
        {
            return await formService.UpdateAsync(model);
        }

        [HttpGet]
        [ActionName("GetFormTreeAsync")]
        public async Task<List<ZTree>> GetFormTreeAsync()
        {
            return await formService.GetFormTreeAsync();
        }
    }
}
