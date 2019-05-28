using Dapper;
using JadeFramework.Core.Domain.Entities;
using JadeFramework.Core.Extensions;
using JadeFramework.Dapper;
using JadeFramework.Dapper.SqlGenerator;
using MsSystem.Sys.IRepository;
using MsSystem.Sys.Model;
using MsSystem.Sys.ViewModel;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MsSystem.Sys.Repository
{
    public class SysUserRepository : DapperRepository<SysUser>, ISysUserRepository
    {
        public SysUserRepository(IDbConnection connection, SqlGeneratorConfig config) : base(connection, config)
        {
        }

        public async Task<Page<SysUser>> GetPageAsync(UserIndexSearch search)
        {
            Page<SysUser> page = new Page<SysUser>()
            {
                PageIndex = search.PageIndex,
                PageSize = search.PageSize
            };

            string sqlWhere = " WHERE 1=1 ";
            if (!search.UserName.IsNullOrEmpty())
            {
                sqlWhere += $@" AND username LIKE '%{search.UserName.TrimBlank()}%' ";
            }
            if (search.IsDel != -1)
            {
                sqlWhere += $@" AND IsDel = {search.IsDel} ";
            }
            string sql = $"SELECT * FROM sys_user {sqlWhere} LIMIT {search.OffSet()},{search.PageSize}";
            page.Items = await this.QueryAsync(sql);
            page.TotalItems = await this.ExecuteScalarAsync<int>($"SELECT COUNT(1) FROM sys_user {sqlWhere} ");
            return page;
        }
        public async Task<Page<SysUser>> GetPageAsync(int pageIndex, int pageSize, List<int> userids)
        {
            Page<SysUser> page = new Page<SysUser>()
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            int offset = pageSize * (pageIndex - 1);
            string sqlwhere = "";
            if (userids.Any())
            {
                sqlwhere = $"AND userid IN({userids.Join()})";
            }
            string sql = $"SELECT * FROM sys_user WHERE isdel=0 {sqlwhere} LIMIT {offset},{pageSize}";
            page.Items = await this.QueryAsync(sql);
            page.TotalItems = await this.ExecuteScalarAsync<int>($"SELECT COUNT(1) FROM sys_user WHERE isdel=0 {sqlwhere}");
            return page;
        }

        /// <summary>
        /// 修改用户头像
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="imgUrl">头像地址</param>
        /// <returns></returns>
        public async Task<bool> ModifyUserHeadImgAsync(long userid, string imgUrl)
        {
            var dbuser = await this.FindByIdAsync(userid);
            dbuser.HeadImg = imgUrl;
            return await this.UpdateAsync(dbuser);
        }

        /// <summary>
        /// 根据角色ID获取用户ID集合
        /// </summary>
        /// <param name="roleids"></param>
        /// <returns></returns>
        public async Task<List<long>> GetUserIdsByRoleIdsAsync(List<long> roleids)
        {
            string sql = $@"SELECT su.`UserId` FROM `sys_user` su
LEFT JOIN `sys_user_role` sur ON sur.`UserId`=su.`UserId`
INNER JOIN `sys_role` sr ON sr.`RoleId`=sur.`RoleId`
WHERE sr.`IsDel`=0 AND su.`IsDel`=0 AND sr.`RoleId` IN({roleids.Join()})";
            var res = await this.Connection.QueryAsync<long>(sql);
            return res.ToList();
        }
    }
}
