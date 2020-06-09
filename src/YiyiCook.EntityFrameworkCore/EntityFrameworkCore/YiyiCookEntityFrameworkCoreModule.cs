using Abp.EntityFrameworkCore;
using Abp.Modules;
using Abp.Reflection.Extensions;
using YiyiCook.Core;

namespace YiyiCook.EntityFrameworkCore
{
    [DependsOn(
        typeof(YiyiCookCoreModule), 
        typeof(AbpEntityFrameworkCoreModule))]
    public class YiyiCookEntityFrameworkCoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(YiyiCookEntityFrameworkCoreModule).GetAssembly());
        }
    }
}