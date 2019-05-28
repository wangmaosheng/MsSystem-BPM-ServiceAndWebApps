using JadeFramework.Dapper;
using MsSystem.WF.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MsSystem.WF.IRepository
{
    public interface IWfWorkflowLineRepository : IDapperRepository<WfWorkflowLine>
    {
        /// <summary>
        /// 根据ID获取改组的全部数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IEnumerable<WfWorkflowLine>> GetGroupLinesByIdAsync(Guid id);

        Task<List<WfWorkflowLine>> GetByIds(List<Guid> lineids);
    }
}
