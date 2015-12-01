using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace WebAPIBookStore2.Handlers
{
    public class BasicAuthenticationDelegatingHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            AuthenticationHeaderValue authHeader = request.Headers.Authorization;
            if (authHeader != null && authHeader.Scheme == "Basic")
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, "Alberto"),
                    new Claim(ClaimTypes.Email, "alberto@codiceplastico.com"),
                    new Claim(ClaimTypes.Country, "Italy"),
                    new Claim(ClaimTypes.Role, "Administrator"),
                    new Claim(ClaimTypes.Role, "User")
                };
                var identity = new ClaimsIdentity(claims, "Basic");

                HttpContext.Current.User = new ClaimsPrincipal(identity);
            }



            // Call the inner handler.
            var response = await base.SendAsync(request, cancellationToken);
            return response;
        }

        //private Task<HttpResponseMessage> Unauthorized(HttpRequestMessage request)
        //{
        //    var response = request.CreateResponse(HttpStatusCode.Unauthorized);
        //    response.Headers.Add("WWW-Authenticate", "Basic");
        //    TaskCompletionSource<HttpResponseMessage> task = new TaskCompletionSource<HttpResponseMessage>();
        //    task.SetResult(response);
        //    return task.Task;
        //}
    }
}