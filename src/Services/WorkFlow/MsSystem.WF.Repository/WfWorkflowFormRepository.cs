using Dapper;
using JadeFramework.Core.Domain.Entities;
using JadeFramework.Dapper;
using JadeFramework.Dapper.SqlGenerator;
using MsSystem.WF.IRepository;
using MsSystem.WF.Model;
using MsSystem.WF.ViewModel;
using System.Data;
using System.Threading.Tasks;

namespace MsSystem.WF.Repository
{
    public class WfWorkflowFormRepository: DapperRepository<WfWorkflowForm>, IWfWorkflowFormRepository
    {
        public WfWorkflowFormRepository(IDbConnection connection, SqlGeneratorConfig config) : base(connection, config)
        {
        }

        public async Task<Page<FormPageDto>> GetPageAsync(int pageIndex, int pageSize)
        {
            Page<FormPageDto> page = new Page<FormPageDto>
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            int offset = pageSize * (pageIndex - 1);
            string sql = $"SELECT ff.FormId,ff.FormName,t.`FlowName`,ff.FormType,ff.CreateTime FROM `wf_workflow_form` ff  " +
                $"LEFT JOIN(SELECT DISTINCT wf.FlowId, wf.FlowName, wf.FormId FROM `wf_workflow` wf WHERE wf.Enable= 1 ) t ON t.FormId = ff.`FormId` LIMIT @offset, @pageSize";
            page.Items = await this.Connection.QueryAsync<FormPageDto>(sql, new { offset = offset, pageSize = pageSize });
            page.TotalItems = await this.Connection.ExecuteScalarAsync<int>("SELECT COUNT(1) FROM `wf_workflow_form` ff LEFT JOIN(SELECT DISTINCT wf.FlowId, wf.FlowName, wf.FormId FROM `wf_workflow` wf WHERE wf.Enable = 1 ) t ON t.FormId = ff.`FormId` ");
            return page;
        }
    }
}
