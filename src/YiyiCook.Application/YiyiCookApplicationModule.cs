using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Castle.MicroKernel.Registration;
using System.Reflection;
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
            //注册IDtoMapping
            IocManager.IocContainer.Register(
            Classes.FromAssembly(Assembly.GetExecutingAssembly())
                   .IncludeNonPublicTypes()
                   .BasedOn<IDtoMapping>()
                   .WithService.Self()
                   .WithService.DefaultInterfaces()
                   .LifestyleTransient()
                    );

            //解析依赖，并进行映射规则创建
            Configuration.Modules.AbpAutoMapper().Configurators.Add(mapper =>
            {
                var mappers = IocManager.IocContainer.ResolveAll<IDtoMapping>();
                foreach (var dtomap in mappers)
                    dtomap.CreateMapping(mapper);
            });
        }
    }
}