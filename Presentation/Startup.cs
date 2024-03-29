using Application;
using ExternalAPI;
using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Middlewares;
using Application.Adapter;
using Infrastructure.Adapter;
using System.Text;
using System.Globalization;
using Infrastructure.Implementation.DataBase;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Implementation.BackgroundJob;
using Mapster;
using System.Reflection;
using MapsterMapper;
using Common.Settings;

namespace Presentation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //MapsterMappings.Configure();
            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
            ApplicationMapsterMappings.Configure();
            InfrastructureMapsterMappings.Configure();
            services.AddDbContextPool<BechatsrosSatmarEntityContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddOptions();
            services.AddControllersWithViews();
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.Configure<MailSetting>(Configuration.GetSection("Mail"));
            services.Configure<CardknoxSetting>(Configuration.GetSection("CardKnox"));
            services.AddApplication();
            services.AddInfrastructure(Configuration);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddExternalAPI();
            services.AddMvc().AddSessionStateTempDataProvider().AddRazorRuntimeCompilation();          
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromHours(10);
            });
            services.AddHostedService<LongRunningService>();
            services.AddSingleton<BackgroundWorkerQueue>();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
            services.AddSignalR();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            services.AddSingleton<IMapper, Mapper>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var culture = CultureInfo.CreateSpecificCulture("en-US");
            var dateformat = new DateTimeFormatInfo
            {
                ShortDatePattern = "yyyy-MM-dd",
                LongDatePattern = "yyyy-MM-dd hh:mm:ss tt"
            };
            culture.DateTimeFormat = dateformat;
            app.UseMiddleware<CustomExceptionMiddleware>();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
            app.UseCookiePolicy();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                 name: "AdminPanel",
                 areaName: "AdminPanel",
                 pattern: "AdminPanel/{controller=Setting}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Authorize}/{action=Login}/{id?}"                   
                    );

            });
        }
    }
}
