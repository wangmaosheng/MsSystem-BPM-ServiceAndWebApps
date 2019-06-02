using Dapper;
using MsSystem.Sys.IRepository;
using MsSystem.Sys.IService;
using MsSystem.Sys.ViewModel;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MsSystem.Sys.Service
{
    public class WorkFlowService: IWorkFlowService
    {
        private readonly ISysDatabaseFixture databaseFixture;

        public WorkFlowService(ISysDatabaseFixture databaseFixture)
        {
            this.databaseFixture = databaseFixture;
        }

        /// <summary>
        /// 工作流查询数据
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task<string> WorkFlowSelectInfoAsync(FlowViewModel model)
        {
            try
            {
                var dbflow = await databaseFixture.Db.SysWorkflowsql.FindByIdAsync(model.sql);
                string mysql = dbflow.FlowSQL;
                var res = await databaseFixture.Db.Connection.QueryAsync<string>(mysql, model.param);
                return res.ToList()[0];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
