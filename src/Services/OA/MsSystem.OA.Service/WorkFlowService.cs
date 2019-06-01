using Dapper;
using JadeFramework.WorkFlow;
using MsSystem.OA.IRepository;
using MsSystem.OA.IService;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MsSystem.OA.Service
{
    public class WorkFlowService : IWorkFlowService
    {
        private readonly IOaDatabaseFixture databaseFixture;

        public WorkFlowService(IOaDatabaseFixture databaseFixture)
        {
            this.databaseFixture = databaseFixture;
        }

        public async Task<bool> ChangeTableStatusAsync(WorkFlowStatusChange statusChange)
        {
            try
            {
                string sql = $" UPDATE {statusChange.TableName} SET FlowStatus='{(int)statusChange.Status}',FlowTime='{statusChange.FlowTime}' WHERE {statusChange.KeyName} = '{statusChange.KeyValue}'";
                int res = await databaseFixture.Db.Connection.ExecuteAsync(sql);
                return res > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 工作流查询数据
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task<string> WorkFlowSelectInfoAsync(string sql)
        {
            try
            {
                var res = await databaseFixture.Db.Connection.QueryAsync<string>(sql);
                var list = res.ToList();
                if (list.Count == 1)
                {
                    return Convert.ToString(list[0]);
                }
                else
                {
                    throw new Exception("SQL语句查询出错！！！只允许有且只能有一列一行");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
