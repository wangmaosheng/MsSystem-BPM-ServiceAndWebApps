using Dapper;
using JadeFramework.Core.Domain.Entities;
using JadeFramework.Dapper;
using JadeFramework.Dapper.SqlGenerator;
using MsSystem.OA.IRepository;
using MsSystem.OA.Model;
using MsSystem.OA.ViewModel;
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
                sqlwhere += "AND om.IsRead=1 ";
            }
            else if (search.IsRead == 0)
            {
                sqlwhere += "AND om.IsRead=0 ";
            }

            string sql = $@"SELECT * FROM ( SELECT om.`Id`,om.`MsgType`,om.`TargetType`,om.`Title`,om.`IsLocal`,om.`Link`,om.`CreateTime`,(omur.`Id` > 0) AS IsRead  FROM oa_message om
LEFT JOIN oa_message_user_read omur ON omur.`MessageId`=om.`Id` AND omur.`UserId`= @userid
WHERE om.`IsEnable`=1 AND om.`IsDel`=0 AND om.`FaceUserType`=0 ) om WHERE 1=1  {sqlwhere}
ORDER BY om.`CreateTime` DESC LIMIT @offset,@pageSize";


            page.Items = await this.Connection.QueryAsync<OaMessageMyList>(sql, new { userid = search.UserId, offset = search.OffSet(), pageSize = search.PageSize });

            page.TotalItems = await this.ExecuteScalarAsync<int>($@"SELECT COUNT(1) FROM 
( SELECT om.`Id`,om.`MsgType`,om.`TargetType`,om.`Title`,om.`IsLocal`,om.`Link`,om.`CreateTime`,(omur.`Id` > 0) AS IsRead  FROM oa_message om
LEFT JOIN oa_message_user_read omur ON omur.`MessageId`=om.`Id` AND omur.`UserId`= @userid
WHERE om.`IsEnable`=1 AND om.`IsDel`=0 AND om.`FaceUserType`=0 ) om WHERE 1=1  {sqlwhere}", new { userid = search.UserId });

            return page;
        }
    }
}
