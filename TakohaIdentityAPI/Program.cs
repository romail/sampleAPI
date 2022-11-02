using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TakohaIdentityAPI.Data;
using TakohaIdentityAPI.Areas.Identity.Data;
using IdentityServer4;
using IdentityServer4.Test;
using TakohaIdentityAPI;

var builder = WebApplication.CreateBuilder(args);
var connectionString = "server=localhost;user=root;password=Q1w2e3r4#;database=takohaidentity_api"; //todo from appSettigs
var serverVersion = new MySqlServerVersion(new Version(8, 0, 31));

builder.Services.AddDbContext<IdentityProjectContext>(options =>
    options.UseMySql(connectionString, serverVersion));

builder.Services.AddDefaultIdentity<TakohaIdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<IdentityProjectContext>()
    .AddDefaultTokenProviders();

builder.Services.AddIdentityServer(options =>
    {
        options.Events.RaiseErrorEvents = true;
        options.Events.RaiseInformationEvents = true;
        options.Events.RaiseFailureEvents = true;
        options.Events.RaiseSuccessEvents = true;
        options.EmitStaticAudienceClaim = true;
    })
    .AddTestUsers(Config.GetUsers())
    .AddInMemoryIdentityResources(Config.IdentityResources)
    .AddInMemoryApiScopes(Config.ApiScopes)
    .AddInMemoryClients(Config.Clients)
    .AddDeveloperSigningCredential();


// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();

app.UseIdentityServer();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
