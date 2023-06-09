global using diarioAlimentar.Shared;
using System.Security.Claims;

using diarioAlimentar.Server.Controllers;
using diarioAlimentar.Server.Data;
using diarioAlimentar.Server.Models;

using IdentityModel;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace diarioAlimentar
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? builder.Configuration["ConnectionStrings:DefaultConnection"] ?? builder.Configuration["DefaultConnection"]  ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
            builder.Services.AddSingleton<IAlimentoProvider, AlimentosJSON>();
            builder.Services.AddScoped<ApplicationUserClaimsPrincipalFactory>();
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddClaimsPrincipalFactory<ApplicationUserClaimsPrincipalFactory>();

            builder.Services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>(options =>
                {
                    options.IdentityResources["openid"].UserClaims.Add(ClaimTypes.DateOfBirth);
                    options.ApiResources.Single().UserClaims.Add(ClaimTypes.DateOfBirth);

                    options.IdentityResources["openid"].UserClaims.Add(ClaimTypes.Gender);
                    options.ApiResources.Single().UserClaims.Add(ClaimTypes.Gender);

                    options.IdentityResources["openid"].UserClaims.Add("peso");
                    options.ApiResources.Single().UserClaims.Add("peso");

                    options.IdentityResources["openid"].UserClaims.Add("altura");
                    options.ApiResources.Single().UserClaims.Add("altura");

                    options.IdentityResources["openid"].UserClaims.Add("atividade");
                    options.ApiResources.Single().UserClaims.Add("atividade");
                });

            builder.Services.AddAuthentication()
                .AddIdentityServerJwt();

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            builder.Services.AddAuthorizationCore();
            builder.Services.AddOptions();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseWebSockets();
            app.MapRazorPages();
            app.MapControllers();
            app.MapFallbackToFile("index.html");

            app.Run();
        }
    }
}