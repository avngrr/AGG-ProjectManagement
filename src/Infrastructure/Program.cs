using Application.Common.Interfaces.Data;
using Application.Extensions;
using Infrastructure;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter()
    .AddSwagger()
    .AddApplicationServices()
    .AddServerServices()
    .AddRepositories()
    .AddInfrastructureMappings()
    .AddIdentity()
    .AddHttpContextAccessor()
    .AddJwtSettings(builder.Configuration)
    .AddAuthorizationPermissions()
    .AddVersioning()
    .AddControllersWithViews();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "_myAllowSpecificOrigins",
        b =>
        {
            b.WithOrigins("https://10.10.10.23:44306",
                    "https://10.10.10.23:7019",
                    "https://localhost:44306", 
                    "https://localhost:7019")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();
app.UseCors("_myAllowSpecificOrigins");

app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();
app.Run();
public partial class Program { }