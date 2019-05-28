using JadeFramework.Core.Domain.Entities;
using JadeFramework.Dapper;
using MsSystem.WF.Model;
using MsSystem.WF.ViewModel;
using System.Threading.Tasks;

namespace MsSystem.WF.IRepository
{
    public interface IWfWorkflowFormRepository: IDapperRepository<WfWorkflowForm>
    {
        Task<Page<FormPageDto>> GetPageAsync(int pageIndex, int pageSize);
    }
}
