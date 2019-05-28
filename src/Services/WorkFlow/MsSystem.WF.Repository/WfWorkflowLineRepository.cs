using JadeFramework.Dapper;
using JadeFramework.Dapper.SqlGenerator;
using MsSystem.WF.IRepository;
using MsSystem.WF.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MsSystem.WF.Repository
{
    public class WfWorkflowLineRepository : DapperRepository<WfWorkflowLine>, IWfWorkflowLineRepository
    {
        public WfWorkflowLineRepository(IDbConnection connection, SqlGeneratorConfig config) : base(connection, config)
        {
        }

        /// <summary>
        /// 根据ID获取改组的全部数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<WfWorkflowLine>> GetGroupLinesByIdAsync(Guid id)
        {
            var line = await FindByIdAsync(id);
            var grouplines = await FindAllAsync(m => m.GroupId == line.GroupId && m.IsDel == 0);
            return grouplines;
        }

        public async Task<List<WfWorkflowLine>> GetByIds(List<Guid> lineids)
        {
            List<string> ids = new List<string>();
            foreach (var item in lineids)
            {
                ids.Add("'" + item.ToString() + "'");
            }
            string insql = string.Join(",", ids);
            string sql = $"SELECT * FROM `wf_workflow_line` WHERE id IN({insql})";
            var res = await QueryAsync(sql);
            return res.ToList();
        }
    }
}
