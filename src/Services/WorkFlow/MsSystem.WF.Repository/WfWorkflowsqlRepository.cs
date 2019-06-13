using Dapper;
using JadeFramework.Dapper;
using JadeFramework.Dapper.SqlGenerator;
using MsSystem.WF.IRepository;
using MsSystem.WF.Model;
using MsSystem.WF.ViewModel;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MsSystem.WF.Repository
{
    public class WfWorkflowsqlRepository : DapperRepository<WfWorkflowsql>, IWfWorkflowsqlRepository
    {
        public WfWorkflowsqlRepository(IDbConnection connection, SqlGeneratorConfig config) : base(connection, config)
        {
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
                var dbflowsql = await this.FindByIdAsync(item.Value);
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
                var res = await this.Connection.QueryAsync<int>(mysql, dbArgs);
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
