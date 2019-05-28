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
    public class SysResourceRepository : DapperRepository<SysResource>, ISysResourceRepository
    {
        public SysResourceRepository(IDbConnection connection, SqlGeneratorConfig config) : base(connection, config)
        {
        }

        public async Task<List<SysResource>> GetListAsync(IEnumerable<long> resids)
        {
            string sql = $"SELECT * FROM sys_resource WHERE isshow=1 AND isbutton=0 AND isdel=0 AND resourceid IN({resids.Join()})";
            var list = await this.QueryAsync(sql);
            return list.ToList();
        }

        public async Task<List<SysResource>> GetAllListAsync(IEnumerable<long> resids,byte? isdel = 0)
        {
            string sql;
            if (isdel == 1)
            {
                sql = $"SELECT * FROM sys_resource WHERE isdel=1 AND resourceid IN({resids.Join()})";
            }
            else if(isdel == 0)
            {
                sql = $"SELECT * FROM sys_resource WHERE isdel=0 AND resourceid IN({resids.Join()})";
            }
            else
            {
                sql = $"SELECT * FROM sys_resource WHERE resourceid IN({resids.Join()})";
            }
            var list = await this.QueryAsync(sql);
            return list.ToList();
        }

        public async Task<IEnumerable<SysResource>> GetAllListAsync(IEnumerable<long> resids)
        {
            string sql = $"SELECT * FROM sys_resource WHERE isdel=0 AND resourceid IN({resids.Join()})";
            return await this.QueryAsync(sql);
        }

        /// <summary>
        /// 根据用户ID获取该用户可用的菜单
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        public async Task<IEnumerable<SysResource>> GetListByUserIdAsync(long userid)
        {

            string sql = $@"SELECT res.* FROM sys_resource res
LEFT JOIN sys_role_resource srr ON srr.ResourceId=res.ResourceId
LEFT JOIN sys_role sr ON sr.RoleId = srr.RoleId
LEFT JOIN sys_user_role sur ON sur.RoleId=sr.RoleId
LEFT JOIN sys_user su ON su.UserId=sur.UserId
WHERE res.IsDel=0 AND res.IsButton=0 AND res.IsShow=1 AND sr.IsDel=0 AND su.IsDel=0 AND su.UserId={userid}  ORDER BY sort ASC";

            return await this.QueryAsync(sql);
        }

        /// <summary>
        /// 获取菜单按钮
        /// </summary>
        /// <param name="parentid"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SysResource>> GetChildButtonsAsync(long parentid)
        {
            string sql = $@"SELECT * FROM sys_resource sr WHERE sr.ParentId={parentid} AND sr.IsButton=1";
            return await this.QueryAsync(sql);
        }
    }
}
