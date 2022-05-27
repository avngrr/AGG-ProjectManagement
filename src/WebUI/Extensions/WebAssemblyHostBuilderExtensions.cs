using Application.Common.Interfaces.Authentication;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using WebUI.Authentication;
using WebUI.Managers.Identity.Authentication;
using WebUI.Managers.Interceptor;
using WebUI.Managers.Projects;

namespace WebUI.Extensions;

public static class WebAssemblyHostBuilderExtensions
{
    public static WebAssemblyHostBuilder AddRootComponents(this WebAssemblyHostBuilder builder)
    {
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");
        return builder;
    }
    public static WebAssemblyHostBuilder AddServices(this WebAssemblyHostBuilder builder)
    {
        builder.Services
            .AddBlazoredLocalStorage()
            .AddAuthorizationCore()
            .AddMudServices()
            .AddTransient<AuthenticationHeaderHandler>()
            .AddScoped<AuthenticationStateProvider, AuthStateProvider>()
            .AddScoped<IAuthenticationManager, AuthenticationManager>()
            .AddTransient<IProjectManager, ProjectManager>()
            .AddTransient<ITicketManager, TicketManager>()
            .AddScoped<HttpInterceptorService>()
            .AddScoped(sp =>
                sp.GetRequiredService<IHttpClientFactory>().CreateClient("Infrastructure").EnableIntercept(sp))
            .AddHttpClient("Infrastructure", client => client.BaseAddress = new Uri("https://localhost:7010"))
            .AddHttpMessageHandler<AuthenticationHeaderHandler>();
        builder.Services.AddHttpClientInterceptor();

        return builder;
    }
}