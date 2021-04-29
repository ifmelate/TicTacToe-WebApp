using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TicTacToe.Data.EF;

namespace TicTacToe.Web
{
    public class Startup
    {
        private const string DefaultCultureName = "ru";
        private const string SecondCultureName = "en";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //data layer
            var connectionString = Configuration["ConnectionStrings:DefaultConnectionString"];
            services.AddDbContext<DataContext>(
                options => options.UseSqlServer(connectionString, x => x.MigrationsAssembly("TicTacToe.Data.EF")));

            var supportedCultures = new[]
            {
                new CultureInfo(SecondCultureName),
                new CultureInfo(DefaultCultureName)
            };

            services.Configure<RequestLocalizationOptions>(
                opts =>
                {
                    opts.DefaultRequestCulture = new RequestCulture(DefaultCultureName, DefaultCultureName);
                    opts.SupportedCultures = supportedCultures;
                    opts.SupportedUICultures = supportedCultures;

                });
            services.AddMvc(options => options.MaxModelValidationErrors = 500)
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .SetCompatibilityVersion(CompatibilityVersion.Latest)
                ;
            services.AddLocalization();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Game/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRequestLocalization();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Game}/{action=Index}/{id?}");
            });
        }
    }
}
