using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MsSystem.Sys.IService;
using MsSystem.Sys.ViewModel;
using System.Threading.Tasks;

namespace MsSystem.Sys.API.Controllers
{
    [Authorize]
    [Route("api/Dept/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class DeptController : ControllerBase
    {
        public ISysDeptService _deptService;
        public DeptController(ISysDeptService deptService)
        {
            _deptService = deptService;
        }

        [HttpGet]
        [ActionName("GetTreeAsync")]
        public async Task<DeptIndexViewModel> GetTreeAsync()
        {
            return await _deptService.GetTreeAsync();
        }

        [HttpGet]
        [ActionName("GetDeptAsync")]
        public async Task<DeptShowViewModel> GetDeptAsync(long deptid)
        {
            return await _deptService.GetDeptAsync(deptid);
        }

        [HttpPost]
        [ActionName("AddAsync")]
        public async Task<bool> AddAsync([FromBody]DeptShowDto dto)
        {
            return await _deptService.AddAsync(dto);
        }

        [HttpPost]
        [ActionName("UpdateAsync")]
        public async Task<bool> UpdateAsync([FromBody]DeptShowDto dto)
        {
            return await _deptService.UpdateAsync(dto);
        }

        [HttpPost]
        [ActionName("DeleteAsync")]
        public async Task<bool> DeleteAsync([FromBody]DeptDeleteDTO dto)
        {
            return await _deptService.DeleteAsync(dto.Ids, dto.UserId);
        }
    }
}