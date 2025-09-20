using Mailing;
using Mailing.MailKitImplementations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetShopMVC.DataContext;
using PetShopMVC.DataContext.Entities;
using System;

namespace PetShopMVC
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });

            builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
                options.User.RequireUniqueEmail = true;
                options.Password.RequireNonAlphanumeric = false;


                options.Lockout.MaxFailedAccessAttempts = 3;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            builder.Services.AddScoped<DataInitializer>();
            builder.Services.AddScoped<IMailService, MailKitMailService>();

            var app = builder.Build();

            using(var scope = app.Services.CreateScope())
            {
                var appDbContext=scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var dataInitializer=scope.ServiceProvider.GetRequiredService<DataInitializer>();
                await dataInitializer.SeedData();
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
            );

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            await app.RunAsync();
        }
    }
}
