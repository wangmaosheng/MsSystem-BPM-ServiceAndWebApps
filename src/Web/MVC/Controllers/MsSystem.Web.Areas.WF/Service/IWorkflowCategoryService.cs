using JadeFramework.Core.Domain.Entities;
using JadeFramework.Core.Extensions;
using Microsoft.Extensions.Configuration;
using MsSystem.Web.Areas.WF.Infrastructure;
using MsSystem.Web.Areas.WF.ViewModel;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MsSystem.Web.Areas.WF.Service
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
    public class WorkflowCategoryService : IWorkflowCategoryService
    {
        private readonly HttpClient _apiClient;
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl;

        public WorkflowCategoryService(HttpClient httpClient, IConfiguration configuration)
        {
            _apiClient = httpClient;
            _configuration = configuration;
            _baseUrl = configuration["MsApplication:url"] + "/api/wf";
        }

        public async Task<List<ZTree>> GetCategoryTreeAsync()
        {
            var uri = API.Category.GetCategoryTreeAsync(_baseUrl);
            var responseString = await _apiClient.GetStringAsync(uri);
            return responseString.ToObject<List<ZTree>>();
        }

        public async Task<List<CategoryTreeListDto>> GetTreeListAsync()
        {
            var uri = API.Category.GetTreeListAsync(_baseUrl);
            var responseString = await _apiClient.GetStringAsync(uri);
            return responseString.ToObject<List<CategoryTreeListDto>>();
        }
        public async Task<CategoryDetailDto> GetCategoryDetailAsync(Guid id)
        {
            var uri = API.Category.GetCategoryDetailAsync(_baseUrl, id);
            var responseString = await _apiClient.GetStringAsync(uri);
            return responseString.ToObject<CategoryDetailDto>();
        }

        public async Task<bool> InsertAsync(CategoryDetailDto model)
        {
            var uri = API.Category.InsertAsync(_baseUrl);
            return await _apiClient.PostBooleanAsync(uri, model);
        }

        public async Task<bool> UpdateAsync(CategoryDetailDto model)
        {
            var uri = API.Category.UpdateAsync(_baseUrl);
            return await _apiClient.PostBooleanAsync(uri, model);
        }

        public async Task<bool> DeleteAsync(CategoryDeleteDto model)
        {
            var uri = API.Category.DeleteAsync(_baseUrl);
            return await _apiClient.PostBooleanAsync(uri, model);
        }
    }
}
