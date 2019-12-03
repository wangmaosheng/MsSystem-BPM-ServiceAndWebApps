using JadeFramework.Core.Domain.Entities;
using JadeFramework.Core.Domain.Result;
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
    [Route("api/User/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private ISysUserService _userService;
        public UserController(ISysUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("LoginAsync")]
        public async Task<ILoginResult<UserIdentity>> LoginAsync([FromBody]LoginDTO model)
        {
            return await _userService.LoginAsync(model.Account, model.Password);
        }

        /// <summary>
        /// 扫码登录
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("ScanningLoginAsync")]
        public async Task<ILoginResult<UserIdentity>> ScanningLoginAsync([FromBody]LoginDTO model)
        {
            return await _userService.ScanningLoginAsync(model.Account);
        }


        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetUserPageAsync")]
        public async Task<UserIndexViewModel> GetUserPageAsync([FromQuery]UserIndexSearch search)
        {
            return await _userService.GetUserPageAsync(search);
        }

        [HttpGet]
        [ActionName("GetAsync")]
        public async Task<UserShowDto> GetAsync(long userid)
        {
            return await _userService.GetAsync(userid);
        }

        [HttpPost]
        [ActionName("AddAsync")]
        public async Task<bool> AddAsync([FromBody]UserShowDto dto)
        {
            return await _userService.AddAsync(dto);
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("UpdateAsync")]
        public async Task<bool> UpdateAsync([FromBody]UserShowDto dto)
        {
            return await _userService.UpdateAsync(dto);
        }

        [HttpPost]
        [ActionName("SaveUserRoleAsync")]
        public async Task<bool> SaveUserRoleAsync([FromBody]RoleBoxDto dto)
        {
            return await _userService.SaveUserRoleAsync(dto);
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("DeleteAsync")]
        public async Task<bool> DeleteAsync([FromBody]UserDeleteDTO dto)
        {
            return await _userService.DeleteAsync(dto.Ids, dto.UserId);
        }

        /// <summary>
        /// 获取数据权限信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetPrivilegesAsync")]
        public async Task<DataPrivilegesViewModel> GetPrivilegesAsync([FromQuery]DataPrivilegesViewModel model)
        {
            return await _userService.GetPrivilegesAsync(model);
        }

        /// <summary>
        /// 保存用户数据权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveDataPrivilegesAsync")]
        public async Task<bool> SaveDataPrivilegesAsync([FromBody]DataPrivilegesDto model)
        {
            return await _userService.SaveDataPrivilegesAsync(model);
        }

        /// <summary>
        /// 用户部门
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetUserDeptAsync")]
        public async Task<UserDeptViewModel> GetUserDeptAsync(long userid)
        {
            return await _userService.GetUserDeptAsync(userid);
        }

        /// <summary>
        /// 保存用户部门
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("SaveUserDeptAsync")]
        public async Task<bool> SaveUserDeptAsync([FromBody]UserDeptDto dto)
        {
            return await _userService.SaveUserDeptAsync(dto);
        }

        /// <summary>
        /// 修改用户头像
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="imgUrl">头像地址</param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("ModifyUserHeadImgAsync")]
        public async Task<bool> ModifyUserHeadImgAsync([FromBody]ModifyUserHeadImgDTO dto)
        {
            return await _userService.ModifyUserHeadImgAsync(dto.UserId, dto.ImgUrl);
        }


        [HttpPost]
        [ActionName("GetUserTreeAsync")]
        public async Task<List<ZTree>> GetUserTreeAsync([FromBody]List<long> ids)
        {
            return await _userService.GetUserTreeAsync(ids);
        }

        /// <summary>
        /// 根据角色ID获取用户ID集合
        /// </summary>
        /// <param name="roleids"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("GetUserIdsByRoleIdsAsync")]
        public async Task<List<long>> GetUserIdsByRoleIdsAsync([FromBody]List<long> roleids)
        {
            return await _userService.GetUserIdsByRoleIdsAsync(roleids);
        }

        /// <summary>
        /// 获取全部可用用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetAllUserAsync")]
        public async Task<List<SysUser>> GetAllUserAsync()
        {
            return await _userService.GetAllUserAsync();
        }

    }
}