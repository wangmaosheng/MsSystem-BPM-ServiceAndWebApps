using Dapper;
using JadeFramework.WorkFlow;
using MsSystem.OA.IRepository;
using MsSystem.OA.IService;
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
            string sql = $" UPDATE {statusChange.TableName} SET FlowStatus='{(int)statusChange.Status}',FlowTime='{statusChange.FlowTime}' WHERE {statusChange.KeyName} = '{statusChange.KeyValue}'";
            int res = await databaseFixture.Db.Connection.ExecuteAsync(sql);
            return res > 0;
        }
    }
}
