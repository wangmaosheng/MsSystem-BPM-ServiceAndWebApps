using JadeFramework.Core.Extensions;
using JadeFramework.WorkFlow;
using MsSystem.Web.Areas.WF.ViewModel;
using System;
using System.Web;

namespace MsSystem.Web.Areas.WF.Infrastructure
{
    public static class API
    {
        public static class Config
        {
            public static string GetRoleTreesAsync(string baseUri) => $"{baseUri}/Role/GetRoleTreesAsync";
            public static string GetUserTreeAsync(string baseUri) => $"{baseUri}/User/GetUserTreeAsync";
        }
        public static class Form
        {
            public static string GetPageAsync(string baseUri, int pageIndex, int pageSize) => $"{baseUri}/Form/GetPageAsync?pageIndex={pageIndex}&pageSize={pageSize}";
            public static string GetFormDetailAsync(string baseUri, Guid id) => $"{baseUri}/Form/GetFormDetailAsync?id={id.ToString()}";
            public static string GetFormTreeAsync(string baseUri) => $"{baseUri}/Form/GetFormTreeAsync";
            public static string InsertAsync(string baseUri) => $"{baseUri}/Form/InsertAsync";
            public static string UpdateAsync(string baseUri) => $"{baseUri}/Form/UpdateAsync";
        }
        public static class Category
        {
            public static string GetTreeListAsync(string baseUri) => $"{baseUri}/Category/GetTreeListAsync";
            public static string GetCategoryTreeAsync(string baseUri) => $"{baseUri}/Category/GetCategoryTreeAsync";
            public static string GetCategoryDetailAsync(string baseUri,Guid id) => $"{baseUri}/Category/GetCategoryDetailAsync?id="+ id;
            public static string InsertAsync(string baseUri) => $"{baseUri}/Category/InsertAsync";
            public static string UpdateAsync(string baseUri) => $"{baseUri}/Category/UpdateAsync";
            public static string DeleteAsync(string baseUri) => $"{baseUri}/Category/DeleteAsync";
        }
        public static class WorkFlowInstance
        {
            public static string CreateInstanceAsync(string baseUri) => $"{baseUri}/WorkFlowInstance/CreateInstanceAsync";
            public static string AddOrUpdateCustomFlowFormAsync(string baseUri) => $"{baseUri}/WorkFlowInstance/AddOrUpdateCustomFlowFormAsync";
            public static string GetFlowApprovalAsync(string baseUri, WorkFlowProcessTransition model)
            {
                return $"{baseUri}/WorkFlowInstance/GetFlowApprovalAsync?{model.ToUrlParam()}";
            }
            public static string GetMyApprovalHistoryAsync(string baseUri, int pageIndex, int pageSize, string userId)
                => $"{baseUri}/WorkFlowInstance/GetMyApprovalHistoryAsync?pageIndex={pageIndex}&pageSize={pageSize}&userId={userId}";

            public static string GetProcessAsync(string baseUri, WorkFlowProcess process) => $"{baseUri}/WorkFlowInstance/GetProcessAsync?{process.ToUrlParam()}";
            public static string GetProcessForSystemAsync(string baseUri, SystemFlowDto model)
                => $"{baseUri}/WorkFlowInstance/GetProcessForSystemAsync?PageId=" + model.PageId + "&UserId=" + model.UserId + "&FormUrl=" + HttpUtility.UrlEncode(model.FormUrl);

            public static string GetUserOperationHistoryAsync(string baseUri, WorkFlowOperationHistorySearchDto searchDto) 
                => $"{baseUri}/WorkFlowInstance/GetUserOperationHistoryAsync?{searchDto.ToUrlParam()}";

            public static string GetUserTodoListAsync(string baseUri, WorkFlowTodoSearchDto searchDto) 
                => $"{baseUri}/WorkFlowInstance/GetUserTodoListAsync?{searchDto.ToUrlParam()}";

            public static string GetUserWorkFlowPageAsync(string baseUri, int pageIndex, int pageSize, string userId)
                => $"{baseUri}/WorkFlowInstance/GetUserWorkFlowPageAsync?pageIndex={pageIndex}&pageSize={pageSize}&userId={userId}";
            public static string ProcessTransitionFlowAsync(string baseUri) => $"{baseUri}/WorkFlowInstance/ProcessTransitionFlowAsync";
            public static string GetFlowImageAsync(string baseUri, Guid flowid, Guid? instanceId)
                => $"{baseUri}/WorkFlowInstance/GetFlowImageAsync?flowid={flowid.ToString()}&InstanceId={instanceId?.ToString()}";

            public static string UrgeAsync(string baseUri) => $"{baseUri}/WorkFlowInstance/UrgeAsync";

        }
        public static class WorkFlow
        {
            public static string GetPageAsync(string baseUri, int pageIndex, int pageSize) => $"{baseUri}/WorkFlow/GetPageAsync?pageIndex={pageIndex}&pageSize={pageSize}";
            public static string GetByIdAsync(string baseUri, Guid id) => $"{baseUri}/WorkFlow/GetByIdAsync?id={id.ToString()}";
            public static string GetWorkFlowStartAsync(string baseUri, Guid categoryid) => $"{baseUri}/WorkFlow/GetWorkFlowStartAsync?categoryid={categoryid.ToString()}";
            public static string InsertAsync(string baseUri) => $"{baseUri}/WorkFlow/InsertAsync";
            public static string UpdateAsync(string baseUri) => $"{baseUri}/WorkFlow/UpdateAsync";
            public static string DeleteAsync(string baseUri) => $"{baseUri}/WorkFlow/DeleteAsync";
            public static string GetAllLinesAsync(string baseUri) => $"{baseUri}/WorkFlow/GetAllLinesAsync";
            public static string GetLineAsync(string baseUri, Guid lineid) => $"{baseUri}/WorkFlow/GetLineAsync?lineid={lineid.ToString()}";
            public static string NewVersionAsync(string baseUri) => $"{baseUri}/WorkFlow/NewVersionAsync";

        }
    }
}
