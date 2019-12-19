using Dapper;
using JadeFramework.Core.Domain.Entities;
using JadeFramework.Dapper;
using JadeFramework.Dapper.SqlGenerator;
using MsSystem.WF.IRepository;
using MsSystem.WF.Model;
using MsSystem.WF.ViewModel;
using System;
using System.Data;
using System.Threading.Tasks;

namespace MsSystem.WF.Repository
{
    public class WfWorkflowInstanceRepository : DapperRepository<WfWorkflowInstance>, IWfWorkflowInstanceRepository
    {
        public WfWorkflowInstanceRepository(IDbConnection connection, SqlGeneratorConfig config) : base(connection, config)
        {
        }

        /// <summary>
        /// 获取用户待办事项
        /// </summary>
        /// <param name="searchDto"></param>
        /// <returns></returns>
        public async Task<Page<UserWorkFlowDto>> GetUserTodoListAsync(WorkFlowTodoSearchDto searchDto)
        {
            Page<UserWorkFlowDto> page = new Page<UserWorkFlowDto>()
            {
                PageIndex = searchDto.PageIndex,
                PageSize = searchDto.PageSize
            };

            string sql = $@"SELECT * FROM (
                    SELECT wf.`FlowId`,wf.`FlowName`,ins.`InstanceId`,ins.`Code` AS InstanceCode,ins.`IsFinish`,ins.`Status`,ins.`CreateTime`,
                    ins.`CreateUserName` AS UserName ,ff.`FormName`,ff.`FormType`,ff.`FormUrl`,wif.`FormData`
                    FROM `wf_workflow_instance` ins 
                    INNER JOIN `wf_workflow` wf ON wf.`FlowId`=ins.`FlowId`
                    INNER JOIN `wf_workflow_form` ff ON ff.`FormId`=wf.`FormId`
                    INNER JOIN `wf_workflow_instance_form` wif ON wif.`InstanceId`=ins.`InstanceId`
                    WHERE ins.`MakerList` LIKE '%{searchDto.UserId},%'
                    UNION 
                    SELECT wf.`FlowId`,wf.`FlowName`,ins.`InstanceId`,ins.`Code` AS InstanceCode,ins.`IsFinish`,ins.`Status`,ins.`CreateTime`,
                    ins.`CreateUserName` AS UserName ,ff.`FormName`,ff.`FormType`,ff.`FormUrl`,wif.`FormData`
                    FROM `wf_workflow_instance` ins 
                    INNER JOIN `wf_workflow` wf ON wf.`FlowId`=ins.`FlowId`
                    INNER JOIN `wf_workflow_form` ff ON ff.`FormId`=wf.`FormId`
                    INNER JOIN `wf_workflow_instance_form` wif ON wif.`InstanceId`=ins.`InstanceId`
                    LEFT JOIN `wf_workflow_notice` wfn ON wfn.`InstanceId`=ins.`InstanceId`
                    WHERE wfn.IsTransition=1 AND wfn.IsRead=0 AND wfn.Status=1 AND wfn.Maker='{searchDto.UserId}'
                    ) t ORDER BY t.`CreateTime` DESC
            LIMIT {(searchDto.PageIndex - 1) * searchDto.PageSize},{searchDto.PageSize}";

            string countsql = $@"SELECT COUNT(1) FROM (
SELECT wf.`FlowId`,wf.`FlowName`,ins.`InstanceId`,ins.`Code` AS InstanceCode,ins.`IsFinish`,ins.`Status`,ins.`CreateTime`,
ins.`CreateUserName` AS UserName ,ff.`FormName`,ff.`FormType`,ff.`FormUrl`,wif.`FormData`
FROM `wf_workflow_instance` ins 
INNER JOIN `wf_workflow` wf ON wf.`FlowId`=ins.`FlowId`
INNER JOIN `wf_workflow_form` ff ON ff.`FormId`=wf.`FormId`
INNER JOIN `wf_workflow_instance_form` wif ON wif.`InstanceId`=ins.`InstanceId`
WHERE ins.`MakerList` LIKE '%{searchDto.UserId},%'
UNION 
SELECT wf.`FlowId`,wf.`FlowName`,ins.`InstanceId`,ins.`Code` AS InstanceCode,ins.`IsFinish`,ins.`Status`,ins.`CreateTime`,
ins.`CreateUserName` AS UserName ,ff.`FormName`,ff.`FormType`,ff.`FormUrl`,wif.`FormData`
FROM `wf_workflow_instance` ins 
INNER JOIN `wf_workflow` wf ON wf.`FlowId`=ins.`FlowId`
INNER JOIN `wf_workflow_form` ff ON ff.`FormId`=wf.`FormId`
INNER JOIN `wf_workflow_instance_form` wif ON wif.`InstanceId`=ins.`InstanceId`
LEFT JOIN `wf_workflow_notice` wfn ON wfn.`InstanceId`=ins.`InstanceId`
WHERE wfn.IsTransition=1 AND wfn.IsRead=0 AND wfn.Status=1 AND wfn.Maker='{searchDto.UserId}'
) t ORDER BY t.`CreateTime` DESC";

            page.Items = await this.Connection.QueryAsync<UserWorkFlowDto>(sql);
            page.TotalItems = await this.Connection.ExecuteScalarAsync<int>(countsql);

            return page;
        }


        /// <summary>
        /// 获取用户流程操作历史记录
        /// </summary>
        /// <param name="searchDto"></param>
        /// <returns></returns>
        public async Task<Page<WorkFlowOperationHistoryDto>> GetUserOperationHistoryAsync(WorkFlowOperationHistorySearchDto searchDto)
        {
            Page<WorkFlowOperationHistoryDto> page = new Page<WorkFlowOperationHistoryDto>
            {
                PageIndex = searchDto.PageIndex,
                PageSize = searchDto.PageSize
            };
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取用户发起的流程
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<Page<UserWorkFlowDto>> GetUserWorkFlowPageAsync(int pageIndex, int pageSize, string userId)
        {
            Page<UserWorkFlowDto> page = new Page<UserWorkFlowDto>
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            string sql = $@"SELECT wf.`FlowId`,wf.`FlowName`,wfin.`Code` AS InstanceCode,wfin.`InstanceId`,wfin.`IsFinish`,wfin.`Status`,wfin.`CreateTime` ,
wfin.`CreateUserName` AS UserName,ff.`FormName`,ff.`FormType`,ff.`FormUrl`,wif.`FormData`
FROM `wf_workflow_instance` wfin
INNER JOIN `wf_workflow` wf ON wf.`FlowId`= wfin.`FlowId`
INNER JOIN `wf_workflow_form` ff ON ff.`FormId`=wf.`FormId`
INNER JOIN `wf_workflow_instance_form` wif ON wif.`InstanceId`=wfin.`InstanceId`
WHERE  wfin.`CreateUserId`={userId} ORDER BY wfin.`CreateTime` DESC LIMIT {(pageIndex - 1) * pageSize} , {pageSize}";

            page.Items = await this.Connection.QueryAsync<UserWorkFlowDto>(sql, null);

            string countsql = $@"SELECT COUNT(1) FROM `wf_workflow_instance` wfin
        INNER JOIN `wf_workflow` wf ON wf.`FlowId`= wfin.`FlowId`
        INNER JOIN `wf_workflow_form` ff ON ff.`FormId`=wf.`FormId`
        INNER JOIN `wf_workflow_instance_form` wif ON wif.`InstanceId`=wfin.`InstanceId`
        WHERE wfin.`CreateUserId`={userId} ";

            page.TotalItems = await this.Connection.ExecuteScalarAsync<int>(countsql);

            return page;
        }

        /// <summary>
        /// 获取我的审批历史记录
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<Page<UserWorkFlowDto>> GetMyApprovalHistoryAsync(int pageIndex, int pageSize, string userId)
        {
            Page<UserWorkFlowDto> page = new Page<UserWorkFlowDto>
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            string sql = $@"SELECT DISTINCT wf.`FlowId`,wf.`FlowName`,ins.`InstanceId`,ins.`Code` AS InstanceCode,ins.`IsFinish`,ins.`Status` ,a.`CreateTime`,ins.`CreateUserName` AS UserName,
ff.`FormName`,ff.`FormType`,ff.`FormUrl`,wif.`FormData`
FROM wf_workflow_operation_history a 
INNER JOIN `wf_workflow_instance` ins ON ins.`InstanceId`=a.`InstanceId`
INNER JOIN `wf_workflow` wf ON wf.`FlowId`=ins.`FlowId`
INNER JOIN `wf_workflow_form` ff ON ff.`FormId`=wf.`FormId`
INNER JOIN `wf_workflow_instance_form` wif ON wif.`InstanceId`=ins.`InstanceId`
INNER JOIN (
SELECT MAX(b.`CreateTime`) AS CreateTime FROM `wf_workflow_operation_history` b  WHERE b.`CreateUserId` = {userId} GROUP BY b.`InstanceId`
)c ON c.CreateTime=a.`CreateTime` 
WHERE  a.`CreateUserId`= {userId} ORDER BY a.`CreateTime` DESC 
LIMIT  {(pageIndex - 1) * pageSize} , {pageSize}" ;
            page.Items = await this.Connection.QueryAsync<UserWorkFlowDto>(sql);

            string sqlcount = $@"SELECT COUNT(1) FROM ( SELECT DISTINCT wf.`FlowId`,wf.`FlowName`,ins.`InstanceId`,ins.`Code` AS InstanceCode,ins.`IsFinish` ,ins.`Status`,a.`CreateTime`,ins.`CreateUserName` AS UserName,ff.`FormName`,ff.`FormType`,ff.`FormUrl`,wif.`FormData`
FROM wf_workflow_operation_history a 
INNER JOIN `wf_workflow_instance` ins ON ins.`InstanceId`=a.`InstanceId`
INNER JOIN `wf_workflow` wf ON wf.`FlowId`=ins.`FlowId`
INNER JOIN `wf_workflow_form` ff ON ff.`FormId`=wf.`FormId`
INNER JOIN `wf_workflow_instance_form` wif ON wif.`InstanceId`=ins.`InstanceId`
INNER JOIN (
SELECT MAX(b.`CreateTime`) AS CreateTime FROM `wf_workflow_operation_history` b  WHERE b.`CreateUserId` = {userId} GROUP BY b.`InstanceId`
)c ON c.CreateTime=a.`CreateTime` 
WHERE  a.`CreateUserId`= {userId} ORDER BY a.`CreateTime` DESC ) A";
            page.TotalItems = await this.Connection.ExecuteScalarAsync<int>(sqlcount);

            return page;
        }
    }
}
