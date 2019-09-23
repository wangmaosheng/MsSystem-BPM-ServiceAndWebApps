using JadeFramework.Core.Domain.CodeBuilder.MySQL;
using JadeFramework.Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MsSystem.Sys.IService
{
    public interface ICodeBuilderService
    {
        Task<List<Table>> GetTablesAsync(TableSearch search);
        Task<List<TableColumn>> GetTableColumnsAsync(TableSearch search);
    }
}
