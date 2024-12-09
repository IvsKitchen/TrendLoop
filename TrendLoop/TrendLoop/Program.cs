using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TrendLoop.Data;
using TrendLoop.Data.Models;
using TrendLoop.Data.Repository;
using TrendLoop.Data.Repository.Interfaces;
using TrendLoop.Data.Seed;
using TrendLoop.Services.Data;
using TrendLoop.Services.Data.Interfaces;

namespace TrendLoop
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<TrendLoopDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                // by default '@' is not allowed symbol in username, include it in order to use the email as username
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;

            }).AddRoles<IdentityRole<Guid>>()
              .AddEntityFrameworkStores<TrendLoopDbContext>();
            builder.Services.AddControllersWithViews();

            // Register repositories
            builder.Services.AddScoped<IRepository<ApplicationUser, Guid>, BaseRepository<ApplicationUser, Guid>>();
            builder.Services.AddScoped<IRepository<Product, Guid>, BaseRepository<Product, Guid>>();
            builder.Services.AddScoped<IRepository<Brand, int>, BaseRepository<Brand, int>>();
            builder.Services.AddScoped<IRepository<Category, int>, BaseRepository<Category, int>>();
            builder.Services.AddScoped<IRepository<Subcategory, int>, BaseRepository<Subcategory, int>>();
            builder.Services.AddScoped<IRepository<AttributeType, int>, BaseRepository<AttributeType, int>>();
            builder.Services.AddScoped<IRepository<AttributeValue, int>, BaseRepository<AttributeValue, int>>();

            // Register services
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IBrandService, BrandService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<ISubcategoryService, SubcategoryService>();
            builder.Services.AddScoped<IAttributeTypeService, AttributeTypeService>();
            builder.Services.AddScoped<IAttributeValueService, AttributeValueService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IPurchaseService, PurchaseService>();
            builder.Services.AddSingleton<IBlobService, AzureBlobService>();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TrendLoopDbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                // Seed the DB
                Seeder seeder = new Seeder(userManager, dbContext);
                await seeder.SeedDb(10);
            }

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

            app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");

            app.MapControllerRoute(
            name: "Errors",
            pattern: "Home/Error/{statusCode?}",
            defaults: new { controller = "Home", action = "Error" });

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
