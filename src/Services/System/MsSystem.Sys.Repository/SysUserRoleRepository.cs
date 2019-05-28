using Dapper;
using JadeFramework.Core.Extensions;
using JadeFramework.Dapper;
using JadeFramework.Dapper.SqlGenerator;
using MsSystem.Sys.IRepository;
using MsSystem.Sys.Model;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MsSystem.Sys.Repository
{

    public class SysUserRoleRepository : DapperRepository<SysUserRole>, ISysUserRoleRepository
    {
        public SysUserRoleRepository(IDbConnection connection, SqlGeneratorConfig config) : base(connection, config)
        {
        }

        /// <summary>
        /// 根据角色ID获取用户ID
        /// </summary>
        /// <param name="roleid">角色ID</param>
        /// <returns></returns>
        public async Task<List<int>> GetUserIdByRoleIdAsync(long roleid)
        {
            string sql = $"SELECT userid FROM sys_user_role WHERE roleid=@roleid";
            var res = await this.Connection.QueryAsync<int>(sql, new {roleid = roleid});
            return res.ToList();
        }

        /// <summary>
        /// 根据用户ID获取角色ID
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        public async Task<IEnumerable<int>> GetRoleIdByUserIdAsync(long userid)
        {
            string sql = $"SELECT roleid FROM sys_user_role WHERE userid = @userid";
            return await this.Connection.QueryAsync<int>(sql,new { userid = userid });
        }

        /// <summary>
        /// 根据用户ID集合获取
        /// </summary>
        /// <param name="roleid">角色ID</param>
        /// <param name="userids">用户ID集合</param>
        /// <returns></returns>
        public async Task<IEnumerable<SysUserRole>> GetRoleIdByUserIdsAsync(long roleid, List<long> userids)
        {
            if (userids.Any())
            {
                string sql = $"SELECT * FROM sys_user_role WHERE  roleid=@roleid AND userid  IN ({userids.Join()}) ";
                return await this.QueryAsync(sql,new { roleid = roleid });
            }
            else
            {
                throw new NoNullAllowedException();
            }
        }
    }
}
