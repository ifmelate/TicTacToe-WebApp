using System.Globalization;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.WebEncoders;
using TicTacToe.Data.EF;
using TicTacToe.Repositories;
using TicTacToe.Repositories.Interfaces;
using TicTacToe.Services;

namespace TicTacToe.Web
{
    public class Startup
    {
        private const string DefaultCultureName = "en";
        private const string SecondCultureName = "ru-RU";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           
            //data layer
            services.AddDbContext<DataContext>(
                options => options.UseInMemoryDatabase("TestInMemoryDb"));

            //other
            services.AddHttpContextAccessor();

            //repositories
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IMoveRepository, MoveRepository>();
            services.AddScoped<ICellRepository, CellRepository>();

            //services
            services.AddScoped<IPlayerService, PlayerService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<IGameCellSelectorService, GameCellSelectorService>();
            services.AddScoped<IGameCellService, GameCellService>();
            services.AddScoped<IVictoryCheckService, VictoryCheckService>();
            services.AddScoped<IResultsService, ResultsService>();

        
          

          

            services.Configure<RequestLocalizationOptions>(
                opts =>
                {
                    var supportedCultures = new[]
                    {
                        new CultureInfo(SecondCultureName),
                        new CultureInfo(DefaultCultureName)
                    };
                    opts.DefaultRequestCulture = new RequestCulture(DefaultCultureName, DefaultCultureName);
                    opts.SupportedCultures = supportedCultures;
                    opts.SupportedUICultures = supportedCultures;

                });
            services.AddMvc(options => options.MaxModelValidationErrors = 500)
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                        factory.Create(typeof(Resources.GameStrings));
                })
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                ;
            services.AddLocalization();
            services.Configure<WebEncoderOptions>(options => options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All));
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseExceptionHandler("/Game/Error");
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
