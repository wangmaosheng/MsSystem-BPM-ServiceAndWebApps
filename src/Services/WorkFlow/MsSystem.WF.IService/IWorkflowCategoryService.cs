using JadeFramework.Core.Domain.Entities;
using MsSystem.WF.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MsSystem.WF.IService
{
    public interface IWorkflowCategoryService
    {
        /// <summary>
        /// 获取树状列表
        /// </summary>
        /// <returns></returns>
        Task<List<CategoryTreeListDto>> GetTreeListAsync();

        /// <summary>
        /// 获取流程分类树
        /// </summary>
        /// <returns></returns>
        Task<List<ZTree>> GetCategoryTreeAsync();
        Task<CategoryDetailDto> GetCategoryDetailAsync(Guid id);
        Task<bool> InsertAsync(CategoryDetailDto model);
        Task<bool> UpdateAsync(CategoryDetailDto model);
        Task<bool> DeleteAsync(CategoryDeleteDto model);
    }
}
