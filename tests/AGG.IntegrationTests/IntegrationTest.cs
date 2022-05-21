using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Application.Identity.Commands;
using Application.Routes.Identity;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AGG.IntegrationTests;

public class IntegrationTest
{
    protected readonly HttpClient _testClient;
    protected IntegrationTest()
    {
        var appFactory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.RemoveAll(typeof(ApplicationDbContext));
                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("testDb");
                    });
                });
            });

        _testClient = appFactory.CreateClient();
    }

    protected async Task AuthenticateAsync()
    {
        _testClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetJwtAsync());
    }

    private async Task<string> GetJwtAsync()
    {
        var response = await _testClient.PostAsJsonAsync(AuthenticationEndpoints.Register, new RegisterCommand()
        {
            Email = "enzodesmidt@gmail.com",
            Password = "COM123puter!",
            PasswordConfirm = "COM123puter!",
            UserName = "avngr",
            FirstName = "Enzo",
            LastName = "Desmidt"    
        });
        return "";
    }
}