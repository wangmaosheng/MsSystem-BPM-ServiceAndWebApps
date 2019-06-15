namespace MsSystem.Web.Areas.Weixin.Infrastructure
{
    public static class API
    {
        public static class Account
        {
            public static string GetPageAsync(string baseUri, int pageIndex, int pageSize) => $"{baseUri}/Account/GetPageAsync?pageIndex={pageIndex}&pageSize={pageSize}";
        }
        public static class Rule
        {
            public static string GetRulePageAsync(string baseUri, int pageIndex, int pageSize) => $"{baseUri}/Rule/GetRulePageAsync?pageIndex={pageIndex}&pageSize={pageSize}";
            public static string GetRuleReplyAsync(string baseUri, int id) => $"{baseUri}/Rule/GetRuleReplyAsync?id={id}";
            public static string AddAsync(string baseUri) => $"{baseUri}/Rule/AddAsync";
            public static string UpdateAsync(string baseUri) => $"{baseUri}/Rule/UpdateAsync";
        }
        public static class WxMenu
        {
            public static string GetTreesAsync(string baseUri) => $"{baseUri}/Menu/GetTreesAsync";
        }
    }
}
