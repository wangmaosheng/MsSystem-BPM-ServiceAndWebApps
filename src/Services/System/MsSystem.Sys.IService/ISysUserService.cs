using JadeFramework.Core.Domain.Entities;
using JadeFramework.Core.Domain.Result;
using MsSystem.Sys.Model;
using MsSystem.Sys.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MsSystem.Sys.IService
{
    /// <summary>
    /// 用户表接口
    /// </summary>
    public interface ISysUserService
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        Task<ILoginResult<UserIdentity>> LoginAsync(string account, string password);

        /// <summary>
        /// 扫码登录
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        Task<ILoginResult<UserIdentity>> ScanningLoginAsync(string account);

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        Task<UserIndexViewModel> GetUserPageAsync(UserIndexSearch search);


        Task<UserShowDto> GetAsync(long userid);

        Task<bool> AddAsync(UserShowDto dto);

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(UserShowDto dto);

        Task<bool> SaveUserRoleAsync(RoleBoxDto dto);

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(List<long> ids, long userid);

        /// <summary>
        /// 获取数据权限信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<DataPrivilegesViewModel> GetPrivilegesAsync(DataPrivilegesViewModel model);

        /// <summary>
        /// 保存用户数据权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> SaveDataPrivilegesAsync(DataPrivilegesDto model);

        /// <summary>
        /// 用户部门
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        Task<UserDeptViewModel> GetUserDeptAsync(long userid);

        /// <summary>
        /// 保存用户部门
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> SaveUserDeptAsync(UserDeptDto dto);

        /// <summary>
        /// 修改用户头像
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="imgUrl">头像地址</param>
        /// <returns></returns>
        Task<bool> ModifyUserHeadImgAsync(long userid, string imgUrl);

        Task<List<ZTree>> GetUserTreeAsync(List<long> ids);

        /// <summary>
        /// 根据角色ID获取用户ID集合
        /// </summary>
        /// <param name="roleids"></param>
        /// <returns></returns>
        Task<List<long>> GetUserIdsByRoleIdsAsync(List<long> roleids);
        Task<List<SysUser>> GetAllUserAsync();
    }
}
