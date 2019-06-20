using JadeFramework.Dapper;
using MsSystem.OA.Model;
using MsSystem.OA.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MsSystem.OA.IRepository
{
    public interface IOaChatRepository : IDapperRepository<OaChat>
    {
        Task<List<ChatUserListDto>> GetChatListAsync(ChatUserListSearchDto model);
    }
}
