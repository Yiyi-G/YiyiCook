using System;
using Abp.AspNetCore;
using Abp.Castle.Logging.Log4Net;
using Abp.EntityFrameworkCore;
using YiyiCook.EntityFrameworkCore;
using Castle.Facilities.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Reflection;
using System.IO;
using System.Net;

namespace YiyiCook.Web.Startup
{
    public class Startup
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //Configure DbContext
            services.AddAbpDbContext<YiyiCookDbContext>(options =>
            {
                DbContextOptionsConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
            });

            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
            services.AddControllersWithViews(options =>
            {
                // options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            }).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("http://localhost", "http://localhost:3000", "http://fe.tgnet.com", "http://fe.tgnet.com:3000", "http://club.tgnet.com.cn", "http://club.tgnet.com")
                         .AllowAnyHeader()
                         .AllowAnyMethod()
                         .AllowCredentials();
                    });
            });

            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "YiyiCookAPI接口文档", Version = "v1" });
            //    var controllerXmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //    var dtoXmlFile = "YiyiCook.Application.xml";
            //    var controllerXmlPath = Path.Combine(AppContext.BaseDirectory, controllerXmlFile);
            //    var dtoXmlPath = Path.Combine(AppContext.BaseDirectory, dtoXmlFile);
            //    c.IncludeXmlComments(controllerXmlPath);
            //    c.IncludeXmlComments(dtoXmlPath);
            //});
            services.AddSession();
            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(1);
                options.ExcludedHosts.Add("yiyimao.xyz");
            });

            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = (int)HttpStatusCode.TemporaryRedirect;
                options.HttpsPort = 10101;
            });
            //Configure Abp and Dependency Injection
            return services.AddAbp<YiyiCookWebModule>(options =>
            {
                //Configure Log4Net logging
                options.IocManager.IocContainer.AddFacility<LoggingFacility>(
                    f => f.UseAbpLog4Net().WithConfig("log4net.config")
                );
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseAbp(); //Initializes ABP framework.
            app.UseCors();
            app.UseCookiePolicy();
            app.UseSession();
            //app.UseSwagger();
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "yiyi.cook.com");
            //});
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Activity}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                    name: "areas", "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            });


        }
    }
}
