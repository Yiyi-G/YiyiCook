using System;
using System.Threading.Tasks;
using Abp.TestBase;
using YiyiCook.EntityFrameworkCore;
using YiyiCook.Tests.TestDatas;

namespace YiyiCook.Tests
{
    public class YiyiCookTestBase : AbpIntegratedTestBase<YiyiCookTestModule>
    {
        public YiyiCookTestBase()
        {
            UsingDbContext(context => new TestDataBuilder(context).Build());
        }

        protected virtual void UsingDbContext(Action<YiyiCookDbContext> action)
        {
            using (var context = LocalIocManager.Resolve<YiyiCookDbContext>())
            {
                action(context);
                context.SaveChanges();
            }
        }

        protected virtual T UsingDbContext<T>(Func<YiyiCookDbContext, T> func)
        {
            T result;

            using (var context = LocalIocManager.Resolve<YiyiCookDbContext>())
            {
                result = func(context);
                context.SaveChanges();
            }

            return result;
        }

        protected virtual async Task UsingDbContextAsync(Func<YiyiCookDbContext, Task> action)
        {
            using (var context = LocalIocManager.Resolve<YiyiCookDbContext>())
            {
                await action(context);
                await context.SaveChangesAsync(true);
            }
        }

        protected virtual async Task<T> UsingDbContextAsync<T>(Func<YiyiCookDbContext, Task<T>> func)
        {
            T result;

            using (var context = LocalIocManager.Resolve<YiyiCookDbContext>())
            {
                result = await func(context);
                context.SaveChanges();
            }

            return result;
        }
    }
}
