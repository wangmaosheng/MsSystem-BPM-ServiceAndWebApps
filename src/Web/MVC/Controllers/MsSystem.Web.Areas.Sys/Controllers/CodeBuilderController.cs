using JadeFramework.Core.Domain.CodeBuilder.MySQL;
using JadeFramework.Core.Domain.Entities;
using JadeFramework.Core.Extensions;
using JadeFramework.Core.Mvc;
using Microsoft.AspNetCore.Mvc;
using MsSystem.Utility.Filters;
using MsSystem.Web.Areas.Sys.Infrastructure;
using MsSystem.Web.Areas.Sys.Service;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSystem.Web.Areas.Sys.Controllers
{
    [Area("Sys")]
    public class CodeBuilderController : BaseController
    {
        private readonly ICodeBuilderService _codeBuilderService;

        public CodeBuilderController(ICodeBuilderService codeBuilderService)
        {
            this._codeBuilderService = codeBuilderService;
        }

        [HttpGet]
        [Permission("/Sys/CodeBuilder/Index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [ActionName("GetTablesAsync")]
        public async Task<IActionResult> GetTablesAsync([FromQuery]TableSearch search)
        {
            var tables = await _codeBuilderService.GetTablesAsync(search);
            var res = tables.Select(m => new ZTree
            {
                id = m.TABLE_NAME,
                name = m.TABLE_NAME
            }).ToList();
            return Ok(res);
        }


        [HttpGet]
        [ActionName("GetTableColumnsAsync")]
        public async Task<IActionResult> GetTableColumnsAsync([FromQuery]TableSearch search)
        {
            var list = await _codeBuilderService.GetTableColumnsAsync(search);
            return Ok(list);
        }


        [HttpGet]
        [ActionName("CreateFileAsync")]
        public async Task<IActionResult> CreateFileAsync([FromQuery]TableSearch search)
        {
            search.CreateUser = UserIdentity.UserName;
            var tables = await _codeBuilderService.GetTablesAsync(search);

            var table = tables.First(m => m.TABLE_NAME == search.TableName);

            var list = await _codeBuilderService.GetTableColumnsAsync(search);

            StringBuilder stringbuilder = new StringBuilder();
            string filename = search.TableName.ToHump();
            switch (search.Type)
            {
                case 101:
                    stringbuilder = CodeStringBuild.GetModel(search, table, list);
                    break;
                case 102:
                    stringbuilder = CodeStringBuild.GetRepository(search, table);
                    filename += "Repository";
                    break;
                case 103:
                    stringbuilder = CodeStringBuild.GetIRepository(search, table);
                    filename = "I" + filename + "Repository";
                    break;
            }

            filename += ".cs";
            byte[] bytes = Encoding.UTF8.GetBytes(stringbuilder.ToString());
            return File(bytes, "text/plain", filename);
        }
    }
}
