using Dapper;
using JadeFramework.Core.Domain.Entities;
using JadeFramework.Dapper;
using JadeFramework.Dapper.SqlGenerator;
using MsSystem.WF.IRepository;
using MsSystem.WF.Model;
using MsSystem.WF.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MsSystem.WF.Repository
{
    public class WfWorkflowRepository : DapperRepository<WfWorkflow>, IWfWorkflowRepository
    {
        public WfWorkflowRepository(IDbConnection connection, SqlGeneratorConfig config) : base(connection, config)
        {
        }

        public async Task<Page<WfWorkflow>> GetPageAsync(int pageIndex, int pageSize)
        {
            Page<WfWorkflow> page = new Page<WfWorkflow>
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            int offset = pageSize * (pageIndex - 1);
            string sql = "SELECT * FROM `wf_workflow` ORDER BY FlowCode DESC,IsOld ASC LIMIT @offset,@pageSize ";
            page.Items = await this.QueryAsync(sql, new { offset = offset, pageSize = pageSize });
            page.TotalItems = await this.Connection.ExecuteScalarAsync<int>("SELECT COUNT(1) FROM wf_workflow");
            return page;
        }

        public async Task<bool> IsExistFormAsync(Guid formid)
        {
            string sql = $"SELECT COUNT(1) FROM `wf_workflow` wf WHERE wf.`FormId`='{formid.ToString()}' AND wf.`Enable`=1 ";
            int count = await this.Connection.ExecuteScalarAsync<int>(sql);
            return count > 0;
        }

        public async Task<bool> IsExistFormAsync(Guid formid, Guid excludFlowid)
        {
            string sql = $"SELECT COUNT(1) FROM `wf_workflow` wf WHERE wf.`FormId`='{formid.ToString()}' AND wf.`Enable`=1 AND wf.`FlowId`<>'{excludFlowid.ToString()}' ";
            int count = await this.Connection.ExecuteScalarAsync<int>(sql);
            return count > 0;
        }

        public async Task<bool> DeleteAsync(List<Guid> ids, IDbTransaction transaction)
        {
            List<string> inList = new List<string>();
            foreach (var item in ids)
            {
                inList.Add("'" + item.ToString() + "'");
            }
            string sql = $"UPDATE `wf_workflow` wf SET wf.`Enable`=0 WHERE wf.`FlowId` IN({string.Join(",", inList)})";
            int res = await this.Connection.ExecuteAsync(sql, null, transaction);
            return res > 0;
        }

        /// <summary>
        /// 根据分类获取表单信息发起流程
        /// </summary>
        /// <param name="categoryid"></param>
        /// <returns></returns>
        public async Task<List<WorkFlowStartDto>> GetWorkFlowStartAsync(Guid categoryid)
        {
            string sql = $"SELECT wf.`FlowId`,wf.`FlowCode`,wf.`FlowName`,wfc.`Id` AS CategoryId,wfc.`Name` AS CategoryName,wff.`FormId`,wff.`FormName`,wff.`FormType`,wff.`FormUrl` " +
                $"FROM `wf_workflow` wf " +
                $"INNER JOIN `wf_workflow_category` wfc ON wfc.`Id`= wf.`CategoryId` " +
                $"INNER JOIN `wf_workflow_form` wff ON wff.`FormId`= wf.`FormId` " +
                $"WHERE wf.`Enable`= 1";
            if (categoryid != default(Guid))
            {
                sql += $" AND wf.`CategoryId`='{categoryid.ToString()}' ";
            }
            var list = await this.Connection.QueryAsync<WorkFlowStartDto>(sql);
            return list.ToList();
        }
    }
}
