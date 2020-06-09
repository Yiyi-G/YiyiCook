using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using YiyiCook.Core.Configuration;
using YiyiCook.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using YiyiCook.Application;
using YiyiCook.Core;

namespace YiyiCook.Web.Startup
{
    [DependsOn(
        typeof(YiyiCookApplicationModule), 
        typeof(YiyiCookEntityFrameworkCoreModule), 
        typeof(AbpAspNetCoreModule))]
    public class YiyiCookWebModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public YiyiCookWebModule(IHostingEnvironment env)
        {
            _appConfiguration = AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName);
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(YiyiCookConsts.ConnectionStringName);

            Configuration.Navigation.Providers.Add<YiyiCookNavigationProvider>();

            Configuration.Modules.AbpAspNetCore()
                .CreateControllersForAppServices(
                    typeof(YiyiCookApplicationModule).GetAssembly()
                );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(YiyiCookWebModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(YiyiCookWebModule).Assembly);
        }
    }
}