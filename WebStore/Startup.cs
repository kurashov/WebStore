using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebStore.DAL.Contexts;
using WebStore.Data;
using WebStore.Infrastructure.Interfaces;
using WebStore.Infrastructure.Services.InDataBase;
using WebStore.Infrastructure.Services.InMemory;

namespace WebStore
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //add DbContext
            services.AddDbContext<WebStoreDbContext>( opt =>
                 opt.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

            //add MVC infrastructure 
            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();

            //add services in DI container
            //services.AddSingleton<IEmployeesData, InMemoryEmployeesData>();
            //services.AddSingleton<IProductData, InMemoryProductData>();
            services.AddScoped<IProductData, InDataBaseProductData>();
            services.AddScoped<IEmployeesData, InDataBaseEmployeesData>();
            services.AddTransient<DbInitializer>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DbInitializer dbInitializer)
        {
            dbInitializer.Initialize();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }

            //use static files from wwwroot
            app.UseStaticFiles();
            app.UseDefaultFiles();

            app.UseRouting();

            //app.UseWelcomePage( "/Welcome" );

            app.UseEndpoints(endpoints =>
            {
                //config routing
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
