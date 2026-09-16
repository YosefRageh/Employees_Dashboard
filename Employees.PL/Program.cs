using Employees.DAL.DataBase;
using Employees.DAL.Entities;
using Employees.PL.Filters;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Employees.PL.Filters;

namespace Employees.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add<ThemeActionFilter>();
            });

            builder.Services.AddDbContext<EmployeesDbContext>(options =>
                options.UseSqlServer(
                    "Server=localhost;Database=EmployeesDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true"));

            // Add Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<EmployeesDbContext>()
                .AddDefaultTokenProviders();

            // Add Session
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSession();

            app.UseAuthentication();

            app.UseAuthorization();

            // Map middleware: branches the pipeline for /admin and never continues to MVC
            app.Map("/admin", adminApp =>
            {
                adminApp.Run(async context =>
                {
                    await context.Response.WriteAsync(
                        "Employee Admin Dashboard (Map Middleware)");
                });
            });

            // Run middleware: terminal delegate, handled directly, no controller involved
            app.Map("/hello", helloApp =>
            {
                helloApp.Run(async context =>
                {
                    await context.Response.WriteAsync(
                        "Hello from Middleware");
                });
            });

            app.MapStaticAssets();

            app.MapControllerRoute(
     name: "default",
     pattern: "{controller=Account}/{action=Login}/{id?}")
     .WithStaticAssets();

            app.Run();
        }
    }
}