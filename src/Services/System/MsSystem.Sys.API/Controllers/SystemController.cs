using JadeFramework.Core.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MsSystem.Sys.IService;
using MsSystem.Sys.Model;
using MsSystem.Sys.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MsSystem.Sys.API.Controllers
{
    [Authorize]
    [Route("api/System/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class SystemController : ControllerBase
    {
        private ISysSystemService _systemService;
        public SystemController(ISysSystemService systemService)
        {
            _systemService = systemService;
        }

        /// <summary>
        /// 根据ID获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetByIdAsync")]
        public async Task<SysSystem> GetByIdAsync(long id)
        {
            return await _systemService.GetByIdAsync(id);
        }

        /// <summary>
        /// 新增系统
        /// </summary>
        /// <param name="system"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("InsertAsync")]
        public async Task<bool> InsertAsync([FromBody]SysSystem system)
        {
            return await _systemService.InsertAsync(system);
        }

        /// <summary>
        /// 更新系统
        /// </summary>
        /// <param name="system"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("UpdateAsync")]
        public async Task<bool> UpdateAsync([FromBody]SysSystem system)
        {
            return await _systemService.UpdateAsync(system);
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("DeleteAsync")]
        public async Task<bool> DeleteAsync([FromBody]SystemDeleteDTO dto)
        {
            return await _systemService.DeleteAsync(dto.Ids, dto.UserId);
        }

        [HttpGet]
        [ActionName("ListAsync")]
        public async Task<List<SysSystem>> ListAsync()
        {
            return await _systemService.ListAsync();
        }

        [HttpGet]
        [ActionName("GetPageAsync")]
        public async Task<Page<SysSystem>> GetPageAsync(int pageIndex, int pageSize)
        {
            return await _systemService.GetPageAsync(pageIndex, pageSize);
        }
    }
}