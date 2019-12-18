using JadeFramework.Core.Extensions;
using MsSystem.OA.IRepository;
using MsSystem.OA.IService;
using MsSystem.OA.Model;
using MsSystem.OA.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MsSystem.OA.Service
{
    public class OaChatService : IOaChatService
    {
        private readonly IOaDatabaseFixture _databaseFixture;

        public OaChatService(IOaDatabaseFixture databaseFixture)
        {
            this._databaseFixture = databaseFixture;
        }

        public async Task<bool> InsertAsync(OaChat chat)
        {
            chat.CreateTime = DateTime.Now.ToTimeStamp();
            bool res = await _databaseFixture.Db.OaChat.InsertAsync(chat);
            return res;
        }

        public async Task<List<ChatUserListDto>> GetChatListAsync(ChatUserListSearchDto model)
        {
            return await _databaseFixture.Db.OaChat.GetChatListAsync(model);
        }

        /// <summary>
        /// 获取用户未读消息
        /// </summary>
        /// <param name="userId"用户id></param>
        /// <returns></returns>
        public async Task<List<ChatUserListDto>> GetUnReadListAsync(long userId)
        {
            return await _databaseFixture.Db.OaChat.GetUnReadListAsync(userId);
        }

    }
}
