using Application.Common.Interfaces.Authentication;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using WebUI.Authentication;
using WebUI.Managers.Identity.Authentication;

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
            .AddScoped(sp =>
            {
                return sp.GetRequiredService<IHttpClientFactory>().CreateClient("Infrastructure");
            })
            .AddHttpClient("Infrastructure", client => client.BaseAddress = new Uri("https://localhost:7010"))
            .AddHttpMessageHandler<AuthenticationHeaderHandler>();

        return builder;
    }
}