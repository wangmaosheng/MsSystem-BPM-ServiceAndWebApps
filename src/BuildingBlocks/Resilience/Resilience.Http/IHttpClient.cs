using System.Net.Http;
using System.Threading.Tasks;

namespace Resilience.Http
{
    public interface IHttpClient
    {
        /// <summary>
        /// 发送Get请求，默认读取当前上下文中的Headers Authorization 值
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="authorizationToken">覆盖默认设置的Authorization</param>
        /// <param name="authorizationMethod"></param>
        /// <returns></returns>
        Task<string> GetStringAsync(string uri, string authorizationToken = null, string authorizationMethod = "Bearer");

        /// <summary>
        /// 发送Post请求，默认读取当前上下文中的Headers Authorization 值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <param name="item"></param>
        /// <param name="authorizationToken">覆盖默认设置的Authorization</param>
        /// <param name="requestId"></param>
        /// <param name="authorizationMethod"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> PostAsync<T>(string uri, T item, string authorizationToken = null, string requestId = null, string authorizationMethod = "Bearer");

        /// <summary>
        ///   发送Delete请求，默认读取当前上下文中的Headers Authorization 值
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="authorizationToken">覆盖默认设置的Authorization</param>
        /// <param name="requestId"></param>
        /// <param name="authorizationMethod"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> DeleteAsync(string uri, string authorizationToken = null, string requestId = null, string authorizationMethod = "Bearer");

        /// <summary>
        /// 发送Put请求，默认读取当前上下文中的Headers Authorization 值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <param name="item"></param>
        /// <param name="authorizationToken">覆盖默认设置的Authorization</param>
        /// <param name="requestId"></param>
        /// <param name="authorizationMethod"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> PutAsync<T>(string uri, T item, string authorizationToken = null, string requestId = null, string authorizationMethod = "Bearer");
    }
}
