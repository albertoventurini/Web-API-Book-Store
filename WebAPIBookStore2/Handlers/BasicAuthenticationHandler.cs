using System;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using WebAPIBookStore2.Services;

namespace WebAPIBookStore2.Handlers
{
    public class BasicAuthenticationHandler : DelegatingHandler
    {

        private static readonly AuthenticationService _authenticationService = new AuthenticationService();
        private static readonly ClaimsIdentityFactory _claimsIdentityFactory = new ClaimsIdentityFactory();

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var authHeader = request.Headers.Authorization;
            if (authHeader != null && authHeader.Scheme == "Basic" && authHeader.Parameter != null)
            {
                var encodedCredentials = authHeader.Parameter;
                var credentialBytes = Convert.FromBase64String(encodedCredentials);
                string[] credentials = Encoding.ASCII.GetString(credentialBytes).Split(':');

                var username = credentials[0];
                var password = credentials[1];

                if(!_authenticationService.Authenticate(username, password))
                {
                    var response = request.CreateResponse(HttpStatusCode.Unauthorized);
                    var task = new TaskCompletionSource<HttpResponseMessage>();
                    task.SetResult(response);
                    return task.Task;
                }

                var identity = _claimsIdentityFactory.Create(username);
                HttpContext.Current.User = new ClaimsPrincipal(identity);
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}