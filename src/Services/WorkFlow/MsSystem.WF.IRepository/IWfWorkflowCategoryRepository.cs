using JadeFramework.Dapper;
using MsSystem.WF.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MsSystem.WF.IRepository
{
    public interface IWfWorkflowCategoryRepository: IDapperRepository<WfWorkflowCategory>
    {
        Task<IEnumerable<WfWorkflowCategory>> GetCategoriesAsync(List<Guid> ids);
    }
}
