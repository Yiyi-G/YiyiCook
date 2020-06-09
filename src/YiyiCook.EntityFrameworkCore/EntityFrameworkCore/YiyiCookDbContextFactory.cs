using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using YiyiCook.Core.Configuration;
using YiyiCook.Core.Web;
using YiyiCook.Core;

namespace YiyiCook.EntityFrameworkCore
{
    /* This class is needed to run EF Core PMC commands. Not used anywhere else */
    public class YiyiCookDbContextFactory : IDesignTimeDbContextFactory<YiyiCookDbContext>
    {
        public YiyiCookDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<YiyiCookDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            DbContextOptionsConfigurer.Configure(
                builder,
                configuration.GetConnectionString(YiyiCookConsts.ConnectionStringName)
            );

            return new YiyiCookDbContext(builder.Options);
        }
    }
}