using Dapper;
using MsSystem.Sys.IRepository;
using MsSystem.Sys.IService;
using MsSystem.Sys.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        public async Task<List<long>> GetFlowNodeInfo(FlowViewModel model)
        {
            try
            {
                var dbflow = await databaseFixture.Db.SysWorkflowsql.FindByIdAsync(model.sql);
                string mysql = dbflow.FlowSQL;
                var dbparamnames = dbflow.Param.Split(',');
                DynamicParameters dbArgs = new DynamicParameters();
                foreach (var item in dbparamnames)
                {
                    var dicValue = model.param[item];
                    dbArgs.Add(item, dicValue);
                }
                var res = await databaseFixture.Db.Connection.QueryAsync<string>(mysql, dbArgs);
                string userids = res.ToList()[0];
                string[] array = userids.Split(',');
                return array.Select(x => Convert.ToInt64(x)).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
