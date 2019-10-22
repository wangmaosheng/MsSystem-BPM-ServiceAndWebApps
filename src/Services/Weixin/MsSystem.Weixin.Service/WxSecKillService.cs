using MsSystem.Weixin.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MsSystem.Weixin.Model;
using JadeFramework.Core.Extensions;

namespace MsSystem.Weixin.Service
{
    public class WxSecKillService
    {
        private readonly IWeixinDatabaseFixture databaseFixture;

        public WxSecKillService(IWeixinDatabaseFixture databaseFixture)
        {
            this.databaseFixture = databaseFixture;
        }


        public void UpdateRedisAmount(long secKillId, long userId)
        {
            

        }


        /// <summary>
        /// 将Redis 数据同步到 数据库中
        /// </summary>
        /// <returns></returns>
        public async Task RedisSecKillToDbAsync()
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// 更新数据库库存数量
        /// 延时更新
        /// </summary>
        /// <param name="secKillId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task UpdateDbAmountAsync(long secKillId, long userId)
        {
            using (var tran = databaseFixture.Db.BeginTransaction())
            {
                try
                {
                    string updateSql = $"update wx_seckill set amount=amount-1 where seckillid={secKillId} and amount > 1";

                    int res = await databaseFixture.Db.Connection.ExecuteScalarAsync<int>(updateSql, tran);

                    WxSecKillRecord record = new WxSecKillRecord
                    {
                        SecKillId = secKillId,
                        State = 0,
                        UserId = userId,
                        CreateTime = DateTime.Now.ToTimeStamp()
                    };

                    bool recordRes = await databaseFixture.Db.WxSecKillRecordRepository.InsertAsync(record, tran);

                    if (res > 0 && recordRes)
                    {
                        tran.Commit();
                    }

                }
                catch (Exception ex)
                {

                    tran.Rollback();

                }
            }
        }
    }
}
