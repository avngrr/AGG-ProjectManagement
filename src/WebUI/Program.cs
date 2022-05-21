using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WebUI;
using WebUI.Extensions;

var builder = WebAssemblyHostBuilder
    .CreateDefault(args)
    .AddRootComponents()
    .AddServices();

await builder.Build().RunAsync();