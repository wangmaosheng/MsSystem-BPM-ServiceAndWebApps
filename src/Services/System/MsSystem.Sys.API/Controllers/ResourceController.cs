using JadeFramework.Core.Domain.Entities;
using JadeFramework.Core.Domain.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MsSystem.Sys.IService;
using MsSystem.Sys.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MsSystem.Sys.API.Controllers
{
    [Authorize]
    [Route("api/Resource/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        private ISysResourceService _resourceService;
        public ResourceController(ISysResourceService resourceService)
        {
            _resourceService = resourceService;
        }

        /// <summary>
        /// 根据用户获取左侧菜单列表
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetLeftTreeAsync")]
        public async Task<List<ResourceTreeViewModel>> GetLeftTreeAsync(long userid)
        {
            return await _resourceService.GetLeftTreeAsync(userid);
        }

        /// <summary>
        /// 异步获取用户操作权限
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetUserPermissionAsync")]
        public async Task<UserPermission> GetUserPermissionAsync(long userid)
        {
            return await _resourceService.GetUserPermissionAsync(userid);
        }


        /// <summary>
        /// 获取树形菜单
        /// </summary>
        /// <param name="systemId">系统ID</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetTreeAsync")]
        public async Task<ResourceIndexViewModel> GetTreeAsync(long systemId)
        {
            return await _resourceService.GetTreeAsync(systemId);
        }

        /// <summary>
        /// 获取资源信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="systemid"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetResourceAsync")]
        public async Task<ResourceShowViewModel> GetResourceAsync(long id, long systemid)
        {
            return await _resourceService.GetResourceAsync(id, systemid);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("AddAsync")]
        public async Task<bool> AddAsync([FromBody]ResourceShowDto dto)
        {
            return await _resourceService.AddAsync(dto);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("UpdateAsync")]
        public async Task<bool> UpdateAsync([FromBody]ResourceShowDto dto)
        {
            return await _resourceService.UpdateAsync(dto);
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("DeleteAsync")]
        public async Task<bool> DeleteAsync([FromBody]ResourceDeleteDTO dto)
        {
            return await _resourceService.DeleteAsync(dto.Ids, dto.UserId);
        }


        /// <summary>
        /// 获取树形数据
        /// </summary>
        /// <param name="roleid">角色ID</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetBoxTreeAsync")]
        public async Task<List<ZTree>> GetBoxTreeAsync(long roleid)
        {
            return await _resourceService.GetBoxTreeAsync(roleid);
        }

        /// <summary>
        /// 角色分配资源保存
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("BoxSaveAsync")]
        public async Task<bool> BoxSaveAsync([FromBody]RoleTreeDto dto)
        {
            return await _resourceService.BoxSaveAsync(dto);
        }

    }
}