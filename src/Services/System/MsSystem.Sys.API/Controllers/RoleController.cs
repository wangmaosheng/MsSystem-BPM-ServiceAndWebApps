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
    [Route("api/Role/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private ISysRoleService _roleService;
        public RoleController(ISysRoleService roleService)
        {
            _roleService = roleService;
        }
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetListAsync")]
        public async Task<RoleIndexViewModel> GetListAsync([FromQuery]RoleIndexSearch search)
        {
            return await _roleService.GetListAsync(search);
        }
        /// <summary>
        /// 获取角色基本信息
        /// </summary>
        /// <param name="roleid">角色ID</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetAsync")]
        public async Task<SysRole> GetAsync(long roleid)
        {
            return await _roleService.GetAsync(roleid);
        }

        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("AddAsync")]
        public async Task<bool> AddAsync([FromBody]SysRole role)
        {
            return await _roleService.AddAsync(role);
        }

        /// <summary>
        /// 编辑角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("UpdateAsync")]
        public async Task<bool> UpdateAsync([FromBody]SysRole role)
        {
            return await _roleService.UpdateAsync(role);
        }

        /// <summary>
        /// 获取该角色下的用户
        /// </summary>
        /// <param name="roleid">角色ID</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetRoleUserAsync")]
        public async Task<Page<SysUser>> GetRoleUserAsync(long roleid, int pageIndex, int pageSize)
        {
            return await _roleService.GetRoleUserAsync(roleid, pageIndex, pageSize);
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("DeleteAsync")]
        public async Task<bool> DeleteAsync([FromBody]RoleDeleteDTO dto)
        {
            return await _roleService.DeleteAsync(dto.Ids, dto.UserId);
        }

        /// <summary>
        /// 获取角色树
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetTreeAsync")]
        public async Task<List<ZTree>> GetTreeAsync(long userid)
        {
            return await _roleService.GetTreeAsync(userid);
        }

        /// <summary>
        /// 删除角色下的用户
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("DeleteUserAsync")]
        public async Task<bool> DeleteUserAsync([FromBody]RoleToUserDto dto)
        {
            return await _roleService.DeleteUserAsync(dto);
        }

        /// <summary>
        /// 角色加入用户
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("AddUserAsync")]
        public async Task<bool> AddUserAsync([FromBody]RoleToUserDto dto)
        {
            return await _roleService.AddUserAsync(dto);
        }

        /// <summary>
        /// 获取角色树
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("GetRoleTreesAsync")]
        public async Task<List<ZTree>> GetRoleTreesAsync([FromBody]List<long> ids)
        {
            return await _roleService.GetRoleTreesAsync(ids);
        }

    }
}