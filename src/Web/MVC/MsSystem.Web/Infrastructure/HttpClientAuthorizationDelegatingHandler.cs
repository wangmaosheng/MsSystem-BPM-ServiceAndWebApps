using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace MsSystem.Web.Infrastructure
{
    public class HttpClientAuthorizationDelegatingHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccesor;
        private readonly TokenClient tokenClient;

        public HttpClientAuthorizationDelegatingHandler(IHttpContextAccessor httpContextAccesor, TokenClient tokenClient)
        {
            _httpContextAccesor = httpContextAccesor;
            this.tokenClient = tokenClient;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var authorizationHeader = _httpContextAccesor.HttpContext
                .Request.Headers["Authorization"];

            if (!string.IsNullOrEmpty(authorizationHeader))
            {
                request.Headers.Add("Authorization", new List<string>() { authorizationHeader });
            }

            var token = await GetToken();

            if (token != null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }

        async Task<string> GetToken()
        {
            return await tokenClient.GetToken();
        }
    }

    public class HttpClientRequestIdDelegatingHandler : DelegatingHandler
    {

        public HttpClientRequestIdDelegatingHandler()
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Method == HttpMethod.Post || request.Method == HttpMethod.Put)
            {
                if (!request.Headers.Contains("x-requestid"))
                {
                    request.Headers.Add("x-requestid", Guid.NewGuid().ToString());
                }
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
