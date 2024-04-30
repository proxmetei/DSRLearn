using System.Diagnostics;
using DSRLearn.Web.Pages.Auth.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DSRLearn.Web.Handler
{
    public class AuthenticationHandler : DelegatingHandler
    {
        private readonly IServiceScopeFactory? _scopeFactory;
        public AuthenticationHandler(IServiceScopeFactory serviceScopeFactory): base(new HttpClientHandler())
        {
            _scopeFactory = serviceScopeFactory;
        }
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage req, CancellationToken cancellationToken)
        {
            Debug.WriteLine("Process request");

            var response = await base.SendAsync(req, cancellationToken);
            if (((int)response.StatusCode) == 401)
            {
                using (IServiceScope scope = _scopeFactory.CreateScope())
                {
                    IAuthService authService =
                        scope.ServiceProvider.GetRequiredService<IAuthService>();
                    var res = await authService.Refresh();
                    if (res != null)
                        req.Headers.Authorization = res;
                    response = await base.SendAsync(req, cancellationToken);
                    Debug.WriteLine("Process response");
                    return response;
                }

            }
            Debug.WriteLine("Process response");
            return response;
        }
    }
}
