using JadeFramework.Core.Domain.Enum;
using JadeFramework.Core.Extensions;
using JadeFramework.Core.Mvc;
using Microsoft.AspNetCore.Mvc;
using MsSystem.Web.Areas.Sys.ViewModel;
using MsSystem.Utility;
using MsSystem.Utility.Filters;
using MsSystem.Web.Areas.Sys.Service;
using System.Threading.Tasks;

namespace MsSystem.Web.Areas.Sys.Controllers
{
    /// <summary>
    /// 部门
    /// </summary>
    [Area("Sys")]
    public class DeptController : BaseController
    {
        public ISysDeptService _deptService;
        public DeptController(ISysDeptService deptService)
        {
            _deptService = deptService;
        }
        #region 页面
        [HttpGet]
        [Permission("/Sys/Dept/Index")]
        public async Task<IActionResult> Index()
        {
            var res = await _deptService.GetTreeAsync();
            return View(res);
        }
        [HttpGet]
        public IActionResult Show()
        {
            return View();
        }
        #endregion

        #region 权限控制

        /// <summary>
        /// 编辑数据获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Permission("/Sys/Dept/Index", ButtonType.View, false)]
        [ActionName("Get")]
        public async Task<IActionResult> Get([FromQuery]long id)
        {
            DeptShowViewModel domain = await _deptService.GetDeptAsync(id);
            return Ok(domain);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Permission("/Sys/Dept/Index", ButtonType.Add, false)]
        [ActionName("Add")]
        public async Task<IActionResult> Add([FromBody]DeptShowDto dto)
        {
            dto.SysDept.CreateUserId = UserIdentity.UserId;
            bool res = await _deptService.AddAsync(dto);
            return Ok(res);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Permission("/Sys/Dept/Index", ButtonType.Edit, false)]
        [ActionName("Edit")]
        public async Task<IActionResult> Edit([FromBody]DeptShowDto dto)
        {
            dto.SysDept.CreateUserId = UserIdentity.UserId;
            bool res = await _deptService.UpdateAsync(dto);
            return Ok(res);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        [Permission("/Sys/Dept/Index", ButtonType.Delete, false)]
        [ActionName("Delete")]
        public async Task<IActionResult> Delete([FromBody]long[] ids)
        {
            long userid = UserIdentity.UserId;
            var res = await _deptService.DeleteAsync(ids, userid);
            return Ok(res);
        }

        #endregion

    }
}
