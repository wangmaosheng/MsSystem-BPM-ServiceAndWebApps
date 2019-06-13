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
                    if (item.ToLower().Equals("userid", StringComparison.OrdinalIgnoreCase))
                    {
                        dbArgs.Add(item, model.UserId);
                    }
                    else
                    {
                        foreach (var key in model.param.Keys)
                        {
                            if (key.ToLower() == item.ToLower())
                            {
                                dbArgs.Add(item, model.param[key]);
                                break;
                            }
                        }
                    }
                }
                var res = await databaseFixture.Db.Connection.QueryAsync<string>(mysql, dbArgs);
                var list = res.Where(m => !string.IsNullOrEmpty(m)).ToList();
                if (list.Any())
                {
                    string userids = res.ToList()[0];
                    string[] array = userids.Split(',');
                    return array.Select(x => Convert.ToInt64(x)).ToList();
                }
                else
                {
                    throw new Exception("人员查询未找到！！！");
                }
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
                    if (param.ToLower().Equals("userid", StringComparison.OrdinalIgnoreCase))//当前用户ID特殊处理
                    {
                        dbArgs.Add(param, model.UserId);
                    }
                    else
                    {
                        foreach (var key in model.Param.Keys)
                        {
                            if (key.ToLower() == param.ToLower())
                            {
                                dbArgs.Add(param, model.Param[key]);
                                break;
                            }
                        }
                    }
                }
                var res = await databaseFixture.Db.Connection.QueryAsync<int>(mysql, dbArgs);
                if (res != null && res.Any())
                {
                    finalid = item.Key;
                    break;
                }
            }
            return finalid;
        }
    }
}
