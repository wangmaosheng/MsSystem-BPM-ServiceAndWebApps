using JadeFramework.Core.Extensions;
using MsSystem.OA.IRepository;
using MsSystem.OA.IService;
using MsSystem.OA.Model;
using System;
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
    }
}
