using Dapper;
using JadeFramework.WorkFlow;
using MsSystem.OA.IRepository;
using MsSystem.OA.IService;
using MsSystem.OA.ViewModel;
using System;
using System.Collections.Generic;
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
        public async Task<List<long>> GetFlowNodeInfo(FlowViewModel model)
        {
            try
            {
                var dbflow = await databaseFixture.Db.OaWorkflowsql.FindByIdAsync(model.sql);
                string mysql = dbflow.FlowSQL;
                var dbparamnames = dbflow.Param.Split(',');
                DynamicParameters dbArgs = new DynamicParameters();
                foreach (string item in dbparamnames)
                {
                    if (item.Equals("userid", StringComparison.OrdinalIgnoreCase))
                    {
                        dbArgs.Add(item, model.UserId);
                    }
                    else
                    {
                        dbArgs.Add(item, model.param[item]);
                    }
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

        /// <summary>
        /// 获取最终的节点ID
        /// </summary>
        /// <param name="data">连线条件字典集合</param>
        /// <returns></returns>
        public async Task<Guid?> GetFinalNodeId(FlowLineFinalNodeDto model)
        {
            Guid? finalid = null;
            foreach (var item in model.Data)
            {
                var dbflowsql = await databaseFixture.Db.OaWorkflowsql.FindByIdAsync(item.Value);
                string mysql = dbflowsql.FlowSQL;
                var dbparamnames = dbflowsql.Param.Split(',');
                DynamicParameters dbArgs = new DynamicParameters();
                foreach (string param in dbparamnames)
                {
                    if (param.Equals("userid", StringComparison.OrdinalIgnoreCase))//当前用户ID特殊处理
                    {
                        dbArgs.Add(param, model.UserId);
                    }
                    else
                    {
                        dbArgs.Add(param, model.Param[param]);
                    }
                }
                var res = await databaseFixture.Db.Connection.QueryAsync<int>(mysql, dbArgs);
                if (res != null && res.ToList()[0] == 1)
                {
                    finalid = item.Key;
                    break;
                }
            }
            return finalid;
        }
    }
}
