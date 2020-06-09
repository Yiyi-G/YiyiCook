using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using YiyiCook.Core;

namespace YiyiCook.Application
{
    [DependsOn(
        typeof(YiyiCookCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class YiyiCookApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(YiyiCookApplicationModule).GetAssembly());
        }
    }
}