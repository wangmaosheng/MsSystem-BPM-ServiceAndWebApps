using JadeFramework.Core.Domain.CodeBuilder.MySQL;
using JadeFramework.Core.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MsSystem.Sys.IService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MsSystem.Sys.API.Controllers
{
    [Authorize]
    [Route("api/CodeBuilder/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class CodeBuilderController : ControllerBase
    {
        private readonly ICodeBuilderService _codeBuilderService;

        public CodeBuilderController(ICodeBuilderService codeBuilderService)
        {
            this._codeBuilderService = codeBuilderService;
        }

        [HttpGet]
        [ActionName("GetTablesAsync")]
        public async Task<List<Table>> GetTablesAsync([FromQuery]TableSearch search)
        {
            var res = await _codeBuilderService.GetTablesAsync(search);
            return res;
        }

        [HttpGet]
        [ActionName("GetTableColumnsAsync")]
        public async Task<List<TableColumn>> GetTableColumnsAsync([FromQuery]TableSearch search)
        {
            var res = await _codeBuilderService.GetTableColumnsAsync(search);
            return res;
        }
    }
}
