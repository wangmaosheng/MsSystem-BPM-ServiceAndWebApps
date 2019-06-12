using JadeFramework.Dapper;
using MsSystem.WF.Model;
using MsSystem.WF.ViewModel;
using System;
using System.Threading.Tasks;

namespace MsSystem.WF.IRepository
{
    public interface IWfWorkflowsqlRepository : IDapperRepository<WfWorkflowsql>
    {
        /// <summary>
        /// 获取最终的节点ID
        /// </summary>
        /// <param name="data">连线条件字典集合</param>
        /// <returns></returns>
        Task<Guid?> GetFinalNodeId(FlowLineFinalNodeDto model);
    }
}
