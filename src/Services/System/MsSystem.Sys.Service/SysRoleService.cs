using JadeFramework.Core.Domain.Entities;
using JadeFramework.Core.Extensions;
using MsSystem.Sys.IRepository;
using MsSystem.Sys.IService;
using MsSystem.Sys.Model;
using MsSystem.Sys.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MsSystem.Sys.Service
{
    public class SysRoleService : ISysRoleService
    {
        private ISysDatabaseFixture _databaseFixture;
        private ILogJobs _logJobs;
        public SysRoleService(ISysDatabaseFixture databaseFixture, ILogJobs logJobs)
        {
            _databaseFixture = databaseFixture;
            _logJobs = logJobs;
        }
        public async Task<RoleIndexViewModel> GetListAsync(RoleIndexSearch search)
        {
            var dbsystems = await _databaseFixture.Db.SysSystem.FindAllAsync(m => m.IsDel == 0);
            RoleIndexViewModel dto = new RoleIndexViewModel
            {
                Systems = dbsystems.Select(m => new SelectListItem()
                {
                    Text = m.SystemName,
                    Value = m.SystemId.ToString(),
                    Selected = m.SystemId == search.SystemId
                }).ToList(),
                Page = await _databaseFixture.Db.SysRole.GetPageAsync(search),
                RoleSearch = search
            };
            return dto;
        }

        /// <summary>
        /// 获取角色基本信息
        /// </summary>
        /// <param name="roleid">角色ID</param>
        /// <returns></returns>
        public async Task<SysRole> GetAsync(long roleid)
        {
            return await _databaseFixture.Db.SysRole.FindByIdAsync(roleid);
        }

        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task<bool> AddAsync(SysRole role)
        {
            try
            {
                role.CreateTime = DateTime.Now.ToTimeStamp();
                role.IsDel = 0;
                await _databaseFixture.Db.SysRole.InsertAsync(role);
                return true;
            }
            catch (Exception ex)
            {
                _logJobs.ExceptionLog(role.CreateUserId, ex);
                return false;
            }
        }

        /// <summary>
        /// 编辑角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(SysRole role)
        {
            try
            {
                var dbrole = await _databaseFixture.Db.SysRole.FindByIdAsync(role.RoleId);
                if (dbrole == null)
                {
                    return false;
                }
                dbrole.RoleName = role.RoleName;
                dbrole.Memo = role.Memo;
                dbrole.IsDel = role.IsDel;
                dbrole.UpdateTime = DateTime.Now.ToTimeStamp();
                return await _databaseFixture.Db.SysRole.UpdateAsync(dbrole);
            }
            catch (Exception ex)
            {
                _logJobs.ExceptionLog(role.CreateUserId, ex);
                return false;
            }
        }

        /// <summary>
        /// 获取该角色下的用户
        /// </summary>
        /// <param name="roleid">角色ID</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public async Task<Page<SysUser>> GetRoleUserAsync(long roleid, int pageIndex, int pageSize)
        {
            var userids = await _databaseFixture.Db.SysUserRole.GetUserIdByRoleIdAsync(roleid);
            return await _databaseFixture.Db.SysUser.GetPageAsync(pageIndex, pageSize, userids);
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(List<long> ids, long userid)
        {
            using (var tran = _databaseFixture.Db.BeginTransaction())
            {
                try
                {
                    var dbuserrole = await _databaseFixture.Db.SysRole.GetListAsync(ids);
                    foreach (var item in dbuserrole)
                    {
                        item.IsDel = 1;
                        await _databaseFixture.Db.SysRole.UpdateAsync(item, tran);
                    }
                    tran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    _logJobs.ExceptionLog(userid, ex);
                    return false;
                }
            }
        }

        /// <summary>
        /// 获取角色树
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        public async Task<List<ZTree>> GetTreeAsync(long userid)
        {
            //系统列表
            var dbsys = await _databaseFixture.Db.SysSystem.FindAllAsync(m => m.IsDel == 0);
            var systemlists = dbsys.ToList();

            var rolelist = await _databaseFixture.Db.SysRole.FindAllAsync(m => m.IsDel == 0);
            var dbrole = rolelist.Select(m => new { m.RoleId, m.RoleName, m.SystemId }).ToList();
            var userrolelist = await _databaseFixture.Db.SysUserRole.FindAllAsync(m => m.UserId == userid);
            var dbuserrole = userrolelist.ToList();
            List<ZTree> trees = systemlists.Select(system => new ZTree()
            {
                id = system.SystemCode,
                name = system.SystemName,
                pId = 0
            }).ToList();
            foreach (var item in dbrole)
            {
                var system = systemlists.FirstOrDefault(m => m.SystemId == item.SystemId);
                if (system != null)
                {
                    ZTree tree = new ZTree()
                    {
                        id = item.RoleId.ToString(),
                        name = item.RoleName,
                        pId = system.SystemCode
                    };
                    var first = dbuserrole.FirstOrDefault(m => m.RoleId == item.RoleId);

                    tree.@checked = first != null;
                    trees.Add(tree);
                }
            }
            return trees;
        }

        /// <summary>
        /// 删除角色下的用户
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<bool> DeleteUserAsync(RoleToUserDto dto)
        {
            if (dto.RoleId <= 0 || !dto.UserIds.HasItems())
            {
                return false;
            }
            using (var tran = _databaseFixture.Db.BeginTransaction())
            {
                try
                {
                    var list = await _databaseFixture.Db.SysUserRole.GetRoleIdByUserIdsAsync(dto.RoleId, dto.UserIds);
                    foreach (var item in list)
                    {
                        await _databaseFixture.Db.SysUserRole.DeleteAsync(item, tran);
                    }
                    tran.Commit();
                    return true;
                }
                catch (Exception exception)
                {
                    tran.Rollback();
                    _logJobs.ExceptionLog(dto.CurrentUserId, exception);
                    return false;
                }
            }
        }

        /// <summary>
        /// 角色加入用户
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<bool> AddUserAsync(RoleToUserDto dto)
        {
            if (dto.RoleId <= 0 || !dto.UserIds.HasItems())
            {
                return false;
            }
            using (var tran = _databaseFixture.Db.BeginTransaction())
            {
                try
                {
                    var userrolelist = await _databaseFixture.Db.SysUserRole.FindAllAsync(m => m.RoleId == dto.RoleId);
                    var list = userrolelist.Select(m => m.UserId).ToList();
                    //排重 
                    List<long> addList = dto.UserIds.Where(item => !list.Contains(item)).ToList();
                    //添加
                    foreach (var item in addList)
                    {
                        SysUserRole userRole = new SysUserRole()
                        {
                            CreateTime = DateTime.Now.ToTimeStamp(),
                            RoleId = dto.RoleId,
                            UserId = item
                        };
                        await _databaseFixture.Db.SysUserRole.InsertAsync(userRole, tran);
                    }
                    tran.Commit();
                    return true;
                }
                catch (Exception exception)
                {
                    tran.Rollback();
                    _logJobs.ExceptionLog(dto.CurrentUserId, exception);
                    return false;
                }
            }
        }

        /// <summary>
        /// 获取角色树
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<List<ZTree>> GetRoleTreesAsync(List<long> ids)
        {
            var rolelist = await _databaseFixture.Db.SysRole.FindAllAsync(m => m.IsDel == 0);
            var tree = rolelist.Select(m => new ZTree
            {
                id = m.RoleId.ToString(),
                name = m.RoleName,
                open = true,
                @checked = ids.Contains(m.RoleId)
            });
            return tree.ToList();
        }
    }
}
