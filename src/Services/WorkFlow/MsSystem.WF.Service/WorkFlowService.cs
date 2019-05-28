using AutoMapper;
using JadeFramework.Core.Domain.Entities;
using JadeFramework.Core.Extensions;
using MsSystem.WF.IRepository;
using MsSystem.WF.IService;
using MsSystem.WF.Model;
using MsSystem.WF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MsSystem.WF.Service
{
    public class WorkFlowService : IWorkFlowService
    {
        private readonly IWFDatabaseFixture databaseFixture;
        private readonly IMapper mapper;

        public WorkFlowService(IWFDatabaseFixture databaseFixture,IMapper mapper)
        {
            this.databaseFixture = databaseFixture;
            this.mapper = mapper;
        }

        public async Task<Page<WfWorkflow>> GetPageAsync(int pageIndex, int pageSize)
        {
            return await databaseFixture.Db.Workflow.GetPageAsync(pageIndex, pageSize);
        }

        public async Task<WorkFlowDetailDto> GetByIdAsync(Guid id)
        {
            var res = await databaseFixture.Db.Workflow.FindByIdAsync(id);
            var model = mapper.Map<WfWorkflow, WorkFlowDetailDto>(res);
            var category = await databaseFixture.Db.WorkflowCategory.FindByIdAsync(res.CategoryId);
            model.CategoryName = category.Name;
            var form = await databaseFixture.Db.WorkflowForm.FindByIdAsync(res.FormId);
            model.FormName = form.FormName;
            return model;
        }

        public async Task<bool> InsertAsync(WorkFlowDetailDto workflow)
        {
            workflow.FlowId = Guid.NewGuid();
            workflow.FlowCode = DateTime.Now.ToTimeStamp() + string.Empty.CreateNumberNonce();

            //判断表单是否已被关联
            var res = await databaseFixture.Db.Workflow.IsExistFormAsync(workflow.FormId);
            if (res)
            {
                return false;
            }
            var model = mapper.Map<WorkFlowDetailDto, WfWorkflow>(workflow);
            return await databaseFixture.Db.Workflow.InsertAsync(model);
        }

        public async Task<bool> UpdateAsync(WorkFlowDetailDto workflow)
        {
            var dbflow = await databaseFixture.Db.Workflow.FindByIdAsync(workflow.FlowId);

            //判断表单是否已被关联
            var res = await databaseFixture.Db.Workflow.IsExistFormAsync(workflow.FormId, dbflow.FlowId);
            if (res)
            {
                return false;
            }

            dbflow.FormId = workflow.FormId;
            dbflow.CategoryId = workflow.CategoryId;
            dbflow.FlowName = workflow.FlowName;
            dbflow.Enable = workflow.Enable;
            dbflow.FlowContent = workflow.FlowContent;
            return await databaseFixture.Db.Workflow.UpdateAsync(dbflow);
        }

        public async Task<bool> DeleteAsync(FlowDeleteDTO dto)
        {
            using (var tran = databaseFixture.Db.BeginTransaction())
            {
                try
                {
                    await databaseFixture.Db.Workflow.DeleteAsync(dto.Ids, tran);

                    tran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return false;
                }
            }
        }

        public async Task<List<WorkFlowStartDto>> GetWorkFlowStartAsync(Guid categoryid)
        {
            return await databaseFixture.Db.Workflow.GetWorkFlowStartAsync(categoryid);
        }

        public async Task<List<WorkFlowLineDto>> GetAllLinesAsync()
        {
            var dbLines = await databaseFixture.Db.WorkflowLine.FindAllAsync(m => m.IsDel == 0);
            return dbLines.Select(m => new WorkFlowLineDto
            {
                LineId = m.Id,
                Name = m.Name
            }).ToList();
        }

        public async Task<WorkFlowLineDto> GetLineAsync(Guid lineid)
        {
            var line = await databaseFixture.Db.WorkflowLine.FindByIdAsync(lineid);
            return new WorkFlowLineDto
            {
                LineId = line.Id,
                Name = line.Name
            };
        }

    }
}
