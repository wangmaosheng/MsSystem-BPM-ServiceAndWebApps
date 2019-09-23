using JadeFramework.Core.Domain.CodeBuilder.MySQL;
using JadeFramework.Core.Domain.Entities;
using JadeFramework.Core.Extensions;
using Microsoft.Extensions.Configuration;
using MsSystem.Web.Areas.Sys.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MsSystem.Web.Areas.Sys.Service
{
    public interface ICodeBuilderService
    {
        Task<List<Table>> GetTablesAsync(TableSearch search);
        Task<List<TableColumn>> GetTableColumnsAsync(TableSearch search);
    }

    public class CodeBuilderService : ICodeBuilderService
    {
        private readonly HttpClient _apiClient;
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl;

        public CodeBuilderService(HttpClient httpClient, IConfiguration configuration)
        {
            _apiClient = httpClient;
            _configuration = configuration;
            _baseUrl = configuration["MsApplication:url"] + "/api/sys";
        }

        public async Task<List<Table>> GetTablesAsync(TableSearch search)
        {
            var uri = API.CodeBuilder.GetTablesAsync(_baseUrl, search);
            var responseString = await _apiClient.GetStringAsync(uri);
            return responseString.ToObject<List<Table>>();
        }

        public async Task<List<TableColumn>> GetTableColumnsAsync(TableSearch search)
        {
            var uri = API.CodeBuilder.GetTableColumnsAsync(_baseUrl, search);
            var responseString = await _apiClient.GetStringAsync(uri);
            return responseString.ToObject<List<TableColumn>>();
        }

    }
}
