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
using System.Threading.Tasks;

namespace MsSystem.Web.Areas.WF.Controllers
{
    [Area("WF")]
    [Authorize]
    public class FormController : BaseController
    {
        private readonly IFormService formService;

        public FormController(IFormService formService)
        {
            this.formService = formService;
        }

        [Permission]
        public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 10)
        {
            var page = await formService.GetPageAsync(pageIndex, pageSize);
            return View(page);
        }

        [Permission]
        public async Task<IActionResult> Show(Guid? id)
        {
            FormDetailDto model;
            if (id == null || id == default(Guid))
            {
                model = new FormDetailDto();
            }
            else
            {
                model = await formService.GetFormDetailAsync(id.Value);
            }
            return View(model);
        }


        [HttpPost]
        [Permission("/WF/Form/Index", ButtonType.Add, false)]
        [ActionName("InsertAsync")]
        public async Task<bool> InsertAsync(FormDetailDto model)
        {
            model.CreateUserId = UserIdentity.UserId.ToString();
            return await formService.InsertAsync(model);
        }

        [HttpPost]
        [Permission("/WF/Form/Index", ButtonType.Edit, false)]
        [ActionName("UpdateAsync")]
        public async Task<bool> UpdateAsync(FormDetailDto model)
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
