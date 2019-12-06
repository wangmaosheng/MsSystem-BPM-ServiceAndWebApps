using Dapper;
using JadeFramework.Core.Domain.Entities;
using JadeFramework.Dapper;
using JadeFramework.Dapper.SqlGenerator;
using MsSystem.OA.IRepository;
using MsSystem.OA.Model;
using MsSystem.OA.ViewModel;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MsSystem.OA.Repository
{
    public class OaMessageRepository : DapperRepository<OaMessage>, IOaMessageRepository
    {
        public OaMessageRepository(IDbConnection connection, SqlGeneratorConfig config) : base(connection, config)
        {
        }

        public async Task<Page<OaMessage>> GetPageAsync(int pageIndex, int pageSize)
        {
            Page<OaMessage> page = new Page<OaMessage>()
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            int offset = (pageIndex - 1) * pageSize;
            string sql = $"SELECT * FROM oa_message WHERE isdel=0 LIMIT @offset,@pageSize";
            page.Items = await this.QueryAsync(sql, new { offset = offset, pageSize = pageSize });
            page.TotalItems = await this.ExecuteScalarAsync<int>($"SELECT COUNT(1) FROM oa_message WHERE isdel=0");
            return page;
        }

        /// <summary>
        /// 目前只取面向全部人员类型
        /// TODO 后期添加其他类型
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public async Task<Page<OaMessageMyList>> MyListAsync(OaMessageMyListSearch search)
        {
            Page<OaMessageMyList> page = new Page<OaMessageMyList>()
            {
                PageIndex = search.PageIndex,
                PageSize = search.PageSize
            };
            string sqlwhere = " ";
            if (search.IsRead == 1)
            {
                sqlwhere += "AND omu.IsRead=1 ";
            }
            else if (search.IsRead == 0)
            {
                sqlwhere += "AND omu.IsRead=0 ";
            }

            string sql = $@"SELECT om.`Id`,om.`MsgType`,om.`TargetType`,om.`Title`,om.`IsLocal`,om.`Link`,om.`CreateTime`,omu.IsRead FROM oa_message_user omu
INNER JOIN oa_message om ON om.Id=omu.MessageId
WHERE omu.Id in(
SELECT MAX(omu2.Id) Id FROM oa_message_user omu2 WHERE omu2.UserId=@userid GROUP BY omu2.MessageId
)
{sqlwhere}
ORDER BY om.CreateTime DESC
LIMIT @offset,@pageSize
";


            page.Items = await this.Connection.QueryAsync<OaMessageMyList>(sql, new { userid = search.UserId, offset = search.OffSet(), pageSize = search.PageSize });

            page.TotalItems = await this.ExecuteScalarAsync<int>($@"SELECT COUNT(omu.Id) FROM oa_message_user omu
                    INNER JOIN oa_message om ON om.Id=omu.MessageId
                    WHERE omu.Id in(
                    SELECT MAX(omu2.Id) Id FROM oa_message_user omu2 WHERE omu2.UserId=@userid GROUP BY omu2.MessageId
                    )
                    {sqlwhere}", new { userid = search.UserId });

            return page;
        }

        /// <summary>
        /// 根据id集合获取可用消息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<IEnumerable<OaMessage>> GetByIds(List<long> ids)
        {
            string str = string.Join(",", ids);
            string sql = $"SELECT * FROM oa_message WHERE isdel=0 AND id in(" + str + ")";
            return await this.QueryAsync(sql);
        }

    }
}
