using Abp.Modules;
using Abp.Reflection.Extensions;
using YiyiCook.Core.Localization;

namespace YiyiCook.Core
{
    public class YiyiCookCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            YiyiCookLocalizationConfigurer.Configure(Configuration.Localization);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(YiyiCookCoreModule).GetAssembly());
        }
    }
}