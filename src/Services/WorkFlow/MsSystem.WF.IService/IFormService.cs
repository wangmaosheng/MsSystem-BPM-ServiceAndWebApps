using JadeFramework.Core.Domain.Entities;
using JadeFramework.Core.Extensions;
using MsSystem.WF.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MsSystem.WF.IService
{
    public interface IFormService: IAutoDenpendencyScoped
    {
        Task<Page<FormPageDto>> GetPageAsync(int pageIndex, int pageSize);
        Task<FormDetailDto> GetFormDetailAsync(Guid id);
        Task<bool> InsertAsync(FormDetailDto model);
        Task<bool> UpdateAsync(FormDetailDto model);
        Task<List<ZTree>> GetFormTreeAsync();
    }
}
