using System.Net.Http.Headers;
using Application.Common.Interfaces.Authentication;
using Toolbelt.Blazor;

namespace WebUI.Managers.Interceptor;

public class HttpInterceptorService
{
    private readonly HttpClientInterceptor _interceptor;
    private readonly IAuthenticationManager _authManager;
    public HttpInterceptorService(HttpClientInterceptor interceptor, IAuthenticationManager authManager)
    {
        _interceptor = interceptor;
        _authManager = authManager;
    }
    public void RegisterEvent() => _interceptor.BeforeSendAsync += InterceptBeforeHttpAsync;
    public async Task InterceptBeforeHttpAsync(object sender, HttpClientInterceptorEventArgs e)
    {
        var absPath = e.Request.RequestUri.AbsolutePath; 
        if (!absPath.Contains("token") && !absPath.Contains("accounts"))
        {
            var token = await _authManager.TryRefreshToken();
            if(!string.IsNullOrEmpty(token))
            {
                e.Request.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);
            }
        }
    }
    public void DisposeEvent() => _interceptor.BeforeSendAsync -= InterceptBeforeHttpAsync;
}