using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using YiyiCook.Web.Startup;
namespace YiyiCook.Web.Tests
{
    [DependsOn(
        typeof(YiyiCookWebModule),
        typeof(AbpAspNetCoreTestBaseModule)
        )]
    public class YiyiCookWebTestModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(YiyiCookWebTestModule).GetAssembly());
        }
    }
}