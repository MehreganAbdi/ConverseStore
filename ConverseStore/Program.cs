using Application.CloudinarySetUp;
using Application.Interfaces;
using Application.Repositories;
using Application.Services;
using Context.Data;
using Context.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ConverseStore
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<ISneakerRepository, SneakerRepository>();
            builder.Services.AddScoped<ISocksRepository, SocksRepository>();
            builder.Services.AddScoped<IPhotoService , PhotoService>();

            builder.Services.Configure<CloudinarySetUp>(builder.Configuration.GetSection("CloudinarySetup"));


            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<StoreDbContext>();

            builder.Services.AddMemoryCache();

            builder.Services.AddSession();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
            
            
            var app = builder.Build();


            //if (args.Length == 1 && args[0].ToLower() == "seeddata")
            //{
            //    //
            //    //Seed.SeedData(app);
            //    await SeedData.SeedUsersAndRolesAsync(app);
            //}



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
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}