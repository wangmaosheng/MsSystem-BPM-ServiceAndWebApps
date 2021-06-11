﻿using JadeFramework.Dapper.DbContext;
using JadeFramework.Core.Extensions;

namespace MsSystem.Sys.IRepository
{
    public interface ISysDbContext : IDapperDbContext, IAutoDenpendencyScoped
    {
        ISysButtonRepository SysButton { get; }
        ISysSystemRepository SysSystem { get; }
        ISysDeptRepository SysDept { get; }
        ISysReleaseLogRepository SysReleaseLog { get; }
        ISysResourceRepository SysResource { get; }
        ISysRoleRepository SysRole { get; }
        ISysRoleResourceRepository SysRoleResource { get; }
        ISysUserRepository SysUser { get; }
        ISysUserDeptRepository SysUserDept { get; }
        ISysUserRoleRepository SysUserRole { get; }
        ISysDataPrivilegesRepository SysDataPrivileges { get; }
        ISysLeaderRepository SysLeader { get; }
        ISysDeptLeaderRepository SysDeptLeader { get; }
        ISysWorkflowsqlRepository SysWorkflowsql { get; }
        ISysScheduleRepository SysSchedule { get; }
    }
    public interface ISysLogDbContext : IDapperDbContext, IAutoDenpendencyScoped
    {
        ISysLogRepository SysLog { get; }
    }
}
