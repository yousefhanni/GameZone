using Microsoft.AspNetCore.Identity;
using System;

namespace GameZone.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Register the DbContext
            builder.Services.AddDbContext<ApplicationDbContext>(opt =>
                opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Register services for DI
            builder.Services.AddScoped<IGamesService, GamesService>();
            builder.Services.AddScoped<IGameRepository, GameRepository>();

            builder.Services.AddScoped<IDevicesService, DevicesService>();
            builder.Services.AddScoped<ICategoriesService, CategoriesService>();
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

			#region  Identity and Authentication Configuration


            // Add ASP.NET Core Identity services to manage users, roles, and authentication.
            // ApplicationUser: Custom user class extending IdentityUser, representing users in your system.
            // IdentityRole: Role management system for assigning roles to users.
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(config =>
            {
                // Configure password policies for the application.
                // Require passwords to have at least one digit (e.g., 0-9).
                config.Password.RequireDigit = true;

                // Require passwords to have at least one non-alphanumeric character (e.g., !, @, #, etc.).
                config.Password.RequireNonAlphanumeric = true;

                // Require passwords to have at least one uppercase letter (e.g., A-Z).
                config.Password.RequireUppercase = true;

                // Require passwords to have at least one lowercase letter (e.g., a-z).
                config.Password.RequireLowercase = true;
            }).AddEntityFrameworkStores<ApplicationDbContext>();//to persist Identity information using ApplicationDbContext 


			// Add authentication services to the application's service collection.
			// This sets up the authentication framework (e.g., cookies, JWT, etc.) to be used in the app.
			builder.Services.AddAuthentication();

			#endregion

			// Add controllers with views
			builder.Services.AddControllersWithViews(); // Ensure controllers are added

            var app = builder.Build();

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

            app.UseAuthorization(); // Ensure this is added after UseRouting()

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Register}/{id?}");

            app.Run();
        }
    }
}
