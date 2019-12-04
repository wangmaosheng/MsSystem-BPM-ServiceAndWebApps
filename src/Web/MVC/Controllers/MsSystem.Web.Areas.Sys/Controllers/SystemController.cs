using JadeFramework.Core.Domain.Enum;
using JadeFramework.Core.Extensions;
using JadeFramework.Core.Mvc;
using Microsoft.AspNetCore.Mvc;
using MsSystem.Web.Areas.Sys.Model;
using MsSystem.Web.Areas.Sys.ViewModel;
using MsSystem.Utility.Filters;
using MsSystem.Web.Areas.Sys.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MsSystem.Web.Areas.Sys.Controllers
{
    /// <summary>
    /// 系统
    /// </summary>
    [Area("Sys")]
    public class SystemController : BaseController
    {
        private ISysSystemService _systemService;
        public SystemController(ISysSystemService systemService)
        {
            _systemService = systemService;
        }


        #region 系统页面

        [HttpGet]
        [Permission]
        public async Task<IActionResult> Index([FromQuery]SystemIndexSearch search)
        {
            if (search.PageIndex.IsDefault())
            {
                search.PageIndex = 1;
            }
            if (search.PageSize.IsDefault())
            {
                search.PageSize = 10;
            }
            var res = await _systemService.GetPageAsync(search.PageIndex, search.PageSize);
            return View(res);
        }
        [HttpGet]
        [Permission("/Sys/System/Index", ButtonType.View)]
        public IActionResult Show()
        {
            return View();
        }

        #endregion

        #region 权限控制

        [HttpGet]
        [Permission("/Sys/System/Index", ButtonType.View, false)]
        [ActionName("Get")]
        public async Task<IActionResult> Get([Bind("Id"), FromQuery]long id)
        {
            if (id > 0)
            {
                var res = await _systemService.GetByIdAsync(id);
                return Ok(res);
            }
            else
            {
                return Ok(new SysSystem());
            }
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Permission("/Sys/System/Index", ButtonType.Add, false)]
        [ActionName("Add")]
        public async Task<IActionResult> Add([FromBody]SysSystem system)
        {
            system.CreateUserId = UserIdentity.UserId;
            bool res = await _systemService.InsertAsync(system);
            return Ok(res);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Permission("/Sys/System/Index", ButtonType.Edit, false)]
        [ActionName("Update")]
        public async Task<IActionResult> Update([FromBody]SysSystem system)
        {
            system.UpdateUserId = UserIdentity.UserId;
            bool res = await _systemService.UpdateAsync(system);
            return Ok(res);
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        [Permission("/Sys/System/Index", ButtonType.Delete, false)]
        [ActionName("Delete")]
        public async Task<IActionResult> Delete([FromBody]List<long> ids)
        {
            long userid = UserIdentity.UserId;
            var res = await _systemService.DeleteAsync(ids, userid);
            return Ok(res);
        }

        #endregion

    }
}