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
    public class SysRoleRepository : DapperRepository<SysRole>, ISysRoleRepository
    {
        public SysRoleRepository(IDbConnection connection, SqlGeneratorConfig config) : base(connection, config)
        {
        }

        public async Task<List<SysRole>> GetListAsync(IEnumerable<long> roleids)
        {
            string strin = roleids.Join();
            string sql = $"select * from sys_role where isdel=0 and roleid in({strin})";
            var list = await this.QueryAsync(sql);
            return list.ToList();
        }

        public async Task<Page<SysRole>> GetPageAsync(RoleIndexSearch search)
        {
            Page<SysRole> page = new Page<SysRole>()
            {
                PageIndex = search.PageIndex,
                PageSize = search.PageSize
            };

            string sqlWhere = $@"systemid = {search.SystemId} ";
            if (!search.RoleName.IsNullOrEmpty())
            {
                sqlWhere += $@" AND RoleName LIKE '%{search.RoleName}%' ";
            }
            if (search.IsDel != -1)
            {
                sqlWhere += $@" AND IsDel = {search.IsDel} ";
            }

            string sql = $"SELECT * FROM sys_role WHERE {sqlWhere} LIMIT {search.OffSet()},{search.PageSize}";
            page.Items = await this.QueryAsync(sql);
            page.TotalItems = await this.Connection.ExecuteScalarAsync<int>($"SELECT COUNT(1) FROM sys_role WHERE {sqlWhere}");
            return page;
        }


        /// <summary>
        /// 根据用户ID获取该用户的角色
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        public async Task<IEnumerable<SysRole>> GetByUserIdAsync(long userid)
        {
            string sql = $@"SELECT sr.* FROM sys_role sr 
                        LEFT JOIN sys_user_role sur ON sur.RoleId=sr.RoleId
                        WHERE sr.IsDel=0 AND sur.UserId=@userid";
            return await this.QueryAsync(sql, new {userid = userid});
        }

    }
}
