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

        /// <summary>
        /// 获取用户未读消息
        /// </summary>
        /// <param name="userId"用户id></param>
        /// <returns></returns>
        Task<List<ChatUserListDto>> GetUnReadListAsync(long userId);
    }
}
