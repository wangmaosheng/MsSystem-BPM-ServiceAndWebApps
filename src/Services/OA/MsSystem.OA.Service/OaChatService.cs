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
            return await _databaseFixture.Db.OaChat.InsertAsync(chat);
        }

        public async Task<List<ChatUserListDto>> GetChatListAsync(ChatUserListSearchDto model)
        {
            return await _databaseFixture.Db.OaChat.GetChatListAsync(model);
        }

    }
}
