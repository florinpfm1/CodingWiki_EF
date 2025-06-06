using CodingWiki_DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace CodingWiki_Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //We registered the DbContext on "ApplicationDbContext" in the DI container, we pass the options where we configure the connection string
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                //this makes the DbContext by default in all aplication to not track the entities, which is useful for read-only retrieve operations
                //options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                
                //builder.Configuration.GetConnectionString is a helper method that looks for connection string in appsettings.json->ConnectionStrings key
                options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
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
