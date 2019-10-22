using JadeFramework.Core.Domain.CodeBuilder.MySQL;
using JadeFramework.Core.Domain.Enum;
using JadeFramework.Core.Extensions;
using MsSystem.Web.Areas.Sys.ViewModel;

namespace MsSystem.Web.Areas.Sys.Infrastructure
{
    public static class API
    {
        public static class SysDept
        {
            public static string GetTreeAsync(string baseUri) => $"{baseUri}/Dept/GetTreeAsync";
            public static string GetDeptAsync(string baseUri, long deptid) => $"{baseUri}/Dept/GetDeptAsync?deptid={deptid}";
            public static string AddAsync(string baseUri) => $"{baseUri}/Dept/AddAsync";
            public static string UpdateAsync(string baseUri) => $"{baseUri}/Dept/UpdateAsync";
            public static string DeleteAsync(string baseUri) => $"{baseUri}/Dept/DeleteAsync";
        }
        public static class SysLog
        {
            public static string GetPageAsync(string baseUri, LogSearchDto model) => $"{baseUri}/Log/GetPageAsync?" + model.ToUrlParam();
            public static string GetChartsAsync(string baseUri, LogLevel level) => $"{baseUri}/Log/GetChartsAsync?level={(int)level}";
        }
        public static class SysReleaseLog
        {
            public static string GetPageAsync(string baseUri, int pageIndex, int pageSize) => $"{baseUri}/Log/GetPageAsync?pageIndex={pageIndex}&pageSize={pageSize}";
        }
        public static class SysResource
        {
            public static string GetLeftTreeAsync(string baseUri, long userid) => $"{baseUri}/Resource/GetLeftTreeAsync?userid={userid}";
            //public static string GetUserPermission(string baseUri) => $"{baseUri}/Resource/GetUserPermission";
            public static string GetUserPermissionAsync(string baseUri, long userid) => $"{baseUri}/Resource/GetUserPermissionAsync?userid={userid}";
            public static string GetTreeAsync(string baseUri, long systemId) => $"{baseUri}/Resource/GetTreeAsync?systemId={systemId}";
            public static string GetResourceAsync(string baseUri, long id, long systemid) => $"{baseUri}/Resource/GetResourceAsync?id={id}&systemid={systemid}";
            public static string AddAsync(string baseUri) => $"{baseUri}/Resource/AddAsync";
            public static string UpdateAsync(string baseUri) => $"{baseUri}/Resource/UpdateAsync";
            public static string DeleteAsync(string baseUri) => $"{baseUri}/Resource/DeleteAsync";
            public static string GetBoxTreeAsync(string baseUri, long roleid) => $"{baseUri}/Resource/GetBoxTreeAsync?roleid={roleid}";
            public static string BoxSaveAsync(string baseUri) => $"{baseUri}/Resource/BoxSaveAsync";
            public static string GetTimeAsync(string baseUri) => $"{baseUri}/Resource/GetTimeAsync";
        }
        public static class SysRole
        {
            public static string GetListAsync(string baseUri, RoleIndexSearch search) => $"{baseUri}/Role/GetListAsync?{search.ToUrlParam()}";
            public static string GetAsync(string baseUri, long roleid) => $"{baseUri}/Role/GetAsync?roleid={roleid}";
            public static string GetRoleUserAsync(string baseUri, long roleid, int pageIndex, int pageSize) => $"{baseUri}/Role/GetRoleUserAsync?roleid={roleid}&pageIndex={pageIndex}&pageSize={pageSize}";
            public static string GetTreeAsync(string baseUri, long userid) => $"{baseUri}/Role/GetTreeAsync?userid={userid}";
            public static string AddAsync(string baseUri) => $"{baseUri}/Role/AddAsync";
            public static string UpdateAsync(string baseUri) => $"{baseUri}/Role/UpdateAsync";
            public static string DeleteAsync(string baseUri) => $"{baseUri}/Role/DeleteAsync";
            public static string DeleteUserAsync(string baseUri) => $"{baseUri}/Role/DeleteUserAsync";
            public static string AddUserAsync(string baseUri) => $"{baseUri}/Role/AddUserAsync";

        }
        public static class SysSystem
        {
            public static string GetByIdAsync(string baseUri, long id) => $"{baseUri}/System/GetByIdAsync?id={id}";
            public static string ListAsync(string baseUri) => $"{baseUri}/System/ListAsync";
            public static string GetPageAsync(string baseUri, int pageIndex, int pageSize) => $"{baseUri}/System/GetPageAsync?pageIndex={pageIndex}&pageSize={pageSize}";
            public static string InsertAsync(string baseUri) => $"{baseUri}/System/InsertAsync";
            public static string UpdateAsync(string baseUri) => $"{baseUri}/System/UpdateAsync";
            public static string DeleteAsync(string baseUri) => $"{baseUri}/System/DeleteAsync";

        }
        public static class SysUser
        {
            public static string GetUserPageAsync(string baseUri, UserIndexSearch search) => $"{baseUri}/User/GetUserPageAsync?{search.ToUrlParam()}";
            public static string GetAsync(string baseUri, long userid) => $"{baseUri}/User/GetAsync?userid={userid}";
            public static string GetUserDeptAsync(string baseUri, long userid) => $"{baseUri}/User/GetUserDeptAsync?userid={userid}";
            public static string LoginAsync(string baseUri) => $"{baseUri}/User/LoginAsync";
            public static string ScanningLoginAsync(string baseUri) => $"{baseUri}/User/ScanningLoginAsync";
            public static string AddAsync(string baseUri) => $"{baseUri}/User/AddAsync";
            public static string UpdateAsync(string baseUri) => $"{baseUri}/User/UpdateAsync";
            public static string SaveUserRoleAsync(string baseUri) => $"{baseUri}/User/SaveUserRoleAsync";
            public static string DeleteAsync(string baseUri) => $"{baseUri}/User/DeleteAsync";
            public static string SaveDataPrivilegesAsync(string baseUri) => $"{baseUri}/User/SaveDataPrivilegesAsync";
            public static string SaveUserDeptAsync(string baseUri) => $"{baseUri}/User/SaveUserDeptAsync";
            public static string ModifyUserHeadImgAsync(string baseUri) => $"{baseUri}/User/ModifyUserHeadImgAsync";

        }

        public class CodeBuilder
        {
            public static string GetTablesAsync(string baseUri, TableSearch search) => $"{baseUri}/CodeBuilder/GetTablesAsync?{search.ToUrlParam()}";
            public static string GetTableColumnsAsync(string baseUri, TableSearch search) => $"{baseUri}/CodeBuilder/GetTableColumnsAsync?{search.ToUrlParam()}";
        }

        public static class Schedule
        {
            public static string GetPageListAsync(string baseUri, int pageIndex, int pageSize) => $"{baseUri}/schedule/GetPageListAsync?pageIndex={pageIndex}&pageSize={pageSize}";
            public static string AddOrUpdateAsync(string baseUri) => $"{baseUri}/schedule/AddOrUpdateAsync";
            public static string GetScheduleAsync(string baseUri, long id) => $"{baseUri}/schedule/GetScheduleAsync?id={id}";
            public static string StartAsync(string baseUri) => $"{baseUri}/schedule/StartAsync";
            public static string StopAsync(string baseUri) => $"{baseUri}/schedule/StopAsync";
            public static string ExecuteJobAsync(string baseUri) => $"{baseUri}/schedule/ExecuteJobAsync";
            public static string SuspendAsync(string baseUri) => $"{baseUri}/schedule/SuspendAsync";
        }
    }
}
