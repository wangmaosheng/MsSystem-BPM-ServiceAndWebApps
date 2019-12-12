using JadeFramework.Core.Domain.Entities;
using MsSystem.Web.Areas.Sys.Model;
using System.Collections.Generic;

namespace MsSystem.Web.Areas.Sys.ViewModel
{
    public class LoginDTO
    {
        public string Account { get; set; }
        public string Password { get; set; }
    }
    public class UserLoginDto
    {
        public string username { get; set; }
        public string password { get; set; }
        public string validatecode { get; set; }
    }
    /// <summary>
    /// 用户
    /// </summary>
    public class UserIndexViewModel
    {
        public UserIndexSearch Search { get; set; }
        public Page<SysUser> Users { get; set; }
    }

    public class UserIndexSearch : BaseSearch
    {
        /// <summary>
        /// 是否多选
        /// </summary>
        public byte More { get; set; } = 1;
        public string UserName { get; set; }
        public override string ToString()
        {
            return $"PageIndex={PageIndex}&PageSize={PageSize}&UserName={UserName}";
        }
    }

    public class UserShowDto
    {
        public SysUser User { get; set; }
    }

    public class RoleBoxDto
    {
        public long UserId { get; set; }

        public List<long> RoleIds { get; set; }

        public long CreateUserId { get; set; }
    }

    public class DataPrivilegesViewModel
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// 系统ID
        /// </summary>
        public long SystemId { get; set; }
        /// <summary>
        /// 部门树
        /// </summary>
        public List<ZTree> ZTrees { get; set; }
    }


    /// <summary>
    /// 数据权限View
    /// </summary>
    public class DataPrivilegesDto
    {
        /// <summary>
        /// 系统ID
        /// </summary>
        public long SystemId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 选中的部门
        /// </summary>
        public List<long> Depts { get; set; }
    }

    public class UserDeptViewModel
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// 部门ID
        /// </summary>
        public long DeptId { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 可选部门
        /// </summary>
        public List<SelectListItem> Depts { get; set; }
    }

    public class UserDeptDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        public long DeptId { get; set; }
    }

    public class UserDeleteDTO
    {
        public List<long> Ids { get; set; }
        public long UserId { get; set; }
    }
    public class ModifyUserHeadImgDTO
    {
        public long UserId { get; set; }
        public string ImgUrl { get; set; }
    }
}
