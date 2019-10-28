using Dapper;
using JadeFramework.Dapper;
using JadeFramework.Dapper.SqlGenerator;
using MsSystem.OA.IRepository;
using MsSystem.OA.Model;
using MsSystem.OA.ViewModel;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MsSystem.OA.Repository
{
    public class OaChatRepository : DapperRepository<OaChat>, IOaChatRepository
    {
        public OaChatRepository(IDbConnection connection, SqlGeneratorConfig config) : base(connection, config)
        {
        }
        public async Task<List<ChatUserListDto>> GetChatListAsync(ChatUserListSearchDto model)
        {
            int offset = model.PageSize * (model.PageIndex - 1);
            string sql = $@"SELECT * FROM oa_chat t 
                            WHERE(t.`Sender`= @sender && t.`Receiver`= @receiver) || (t.`Sender`= @receiver && t.`Receiver`= @sender)
                            ORDER BY t.`CreateTime` DESC
                            LIMIT @offset, @pageSize ";
            var list = await this.Connection.QueryAsync<ChatUserListDto>(sql, new
            {
                sender = model.Sender,
                receiver = model.Receiver,
                offset = offset,
                pageSize = model.PageSize
            });

            return list.OrderBy(m => m.CreateTime).ToList();
        }

        /// <summary>
        /// 获取用户未读消息
        /// </summary>
        /// <param name="userId"用户id></param>
        /// <returns></returns>
        public async Task<List<ChatUserListDto>> GetUnReadListAsync(long userId)
        {
            string sql = $"select * from oa_chat where IsRead=0  and id in( select max(id) from oa_chat where Receiver = {userId} GROUP BY Sender ) ";
            var list = await this.Connection.QueryAsync<ChatUserListDto>(sql);
            return list.ToList();
        }

    }
}
