using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using porductsApp.Data;
using porductsApp.Models;
using porductsApp.Seeds;

namespace porductsApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
                //.AddDefaultUI()
                //.AddDefaultTokenProviders();

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            #region Default seeds

            using var scope = app.Services.CreateScope();
            
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            DefaultCatalogs.SeedCatalogs(context);

            #endregion

            using var userScope = app.Services.CreateAsyncScope();

            var roleManger = userScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManger = userScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            DefaultRoles.SeedAsync(roleManger);
            DefaultUsers.SeedAdminUserAsync(userManger);

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}