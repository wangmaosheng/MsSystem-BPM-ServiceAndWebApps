using AutoMapper;
using JadeFramework.Core.Domain.Entities;
using JadeFramework.Core.Domain.Result;
using JadeFramework.Core.Extensions;
using MsSystem.OA.IRepository;
using MsSystem.OA.IService;
using MsSystem.OA.Model;
using MsSystem.OA.ViewModel;
using System;
using System.Threading.Tasks;

namespace MsSystem.OA.Service
{
    public class OaLeaveService : IOaLeaveService
    {
        private readonly IOaDatabaseFixture _databaseFixture;
        private readonly IMapper _mapper;

        public OaLeaveService(IOaDatabaseFixture databaseFixture, IMapper mapper)
        {
            this._databaseFixture = databaseFixture;
            this._mapper = mapper;
        }

        public async Task<OaLeaveShowDto> GetAsync(long id)
        {
            var entity = await _databaseFixture.Db.OaLeaveRepository.FindByIdAsync(id);
            var res = _mapper.Map<OaLeave, OaLeaveShowDto>(entity);
            res.StartTime = entity.StartTime.ToDateTime();
            res.EndTime = entity.EndTime.ToDateTime();
            return res;
        }

        public async Task<Page<OaLeaveDto>> GetPageAsync(int pageIndex, int pageSize, long userid)
        {
            return await _databaseFixture.Db.OaLeaveRepository.GetPageAsync(pageIndex, pageSize, userid);
        }

        public async Task<AjaxResult> InsertAsync(OaLeaveShowDto entity)
        {
            try
            {
                var dbentity = _mapper.Map<OaLeaveShowDto, OaLeave>(entity);
                dbentity.CreateTime = DateTime.Now.ToTimeStamp();
                dbentity.LeaveCode = DateTime.Now.ToTimeStamp() + string.Empty.CreateNumberNonce();
                dbentity.StartTime = entity.StartTime.ToTimeStamp();
                dbentity.EndTime = entity.EndTime.ToTimeStamp();
                int id = await _databaseFixture.Db.OaLeaveRepository.InsertReturnIdAsync(dbentity);
                return AjaxResult.Success(data: id);
            }
            catch (Exception ex)
            {
                return AjaxResult.Error(ex.Message);
            }
        }

        public async Task<AjaxResult> UpdateAsync(OaLeaveShowDto entity)
        {
            try
            {
                var dbdata = await _databaseFixture.Db.OaLeaveRepository.FindByIdAsync(entity.Id);
                dbdata.Title = entity.Title;
                dbdata.AgentId = entity.AgentId;
                dbdata.LeaveType = entity.LeaveType;
                dbdata.Reason = entity.Reason;
                dbdata.Days = entity.Days;
                dbdata.StartTime = entity.StartTime.ToTimeStamp();
                dbdata.EndTime = entity.EndTime.ToTimeStamp();
                await _databaseFixture.Db.OaLeaveRepository.UpdateAsync(dbdata);
                return AjaxResult.Success(data: dbdata.Id);
            }
            catch (Exception ex)
            {
                return AjaxResult.Error(ex.Message);
            }
        }
    }
}
