using MsSystem.Web.Areas.OA.ViewModel;
using JadeFramework.Core.Extensions;

namespace MsSystem.Web.Areas.OA.Infrastructure
{
    public static class API
    {
        public static class OaLeave
        {
            public static string GetPageAsync(string baseUri, int pageIndex, int pageSize, long userid)
                => $"{baseUri}/Leave/GetPageAsync?pageIndex=" + pageIndex + "&pageSize=" + pageSize + "&userid=" + userid;
            public static string GetAsync(string baseUri, long id) => $"{baseUri}/Leave/GetAsync?id=" + id;
            public static string InsertAsync(string baseUri) => $"{baseUri}/Leave/InsertAsync";
            public static string UpdateAsync(string baseUri) => $"{baseUri}/Leave/UpdateAsync";
        }
        public static class OaMessage
        {
            public static string GetPageAsync(string baseUri, int pageIndex, int pageSize) => $"{baseUri}/Message/GetPageAsync?pageIndex={pageIndex}&pageSize={pageSize}";
            public static string GetByIdAsync(string baseUri, long id) => $"{baseUri}/Message/GetByIdAsync?id={id}";
            public static string InsertAsync(string baseUri) => $"{baseUri}/Message/InsertAsync";
            public static string UpdateAsync(string baseUri) => $"{baseUri}/Message/UpdateAsync";
            public static string DeleteAsync(string baseUri) => $"{baseUri}/Message/DeleteAsync";
            public static string EnableMessageAsync(string baseUri) => $"{baseUri}/Message/EnableMessageAsync";
            public static string MyListAsync(string baseUri, OaMessageMyListSearch search) => $"{baseUri}/Message/MyListAsync?"+ search.ToUrlParam();
            public static string MyListDetailAsync(string baseUri, long id, long userid) => $"{baseUri}/Message/MyListDetailAsync?id=" + id + "&userid=" + userid;
            public static string ReadMessageAsync(string baseUri) => $"{baseUri}/Message/ReadMessageAsync";

        }
        public static class OaChat
        {
            public static string GetChatUserAsync(string baseUri) => $"{baseUri}/Chat/GetChatUserAsync";
            public static string GetChatListAsync(string baseUri, ChatUserListSearchDto model) => $"{baseUri}/Chat/GetChatListAsync?" + model.ToUrlParam();
        }
    }
}
