using MsSystem.OA.Model;
using MsSystem.OA.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MsSystem.OA.IService
{
    public interface IOaChatService
    {
        Task<bool> InsertAsync(OaChat chat);
        Task<List<ChatUserListDto>> GetChatListAsync(ChatUserListSearchDto model);
    }
}
