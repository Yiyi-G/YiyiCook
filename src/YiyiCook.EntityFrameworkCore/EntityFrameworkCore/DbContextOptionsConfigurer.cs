using Microsoft.EntityFrameworkCore;

namespace YiyiCook.EntityFrameworkCore
{
    public static class DbContextOptionsConfigurer
    {
        public static void Configure(
            DbContextOptionsBuilder<YiyiCookDbContext> dbContextOptions, 
            string connectionString
            )
        {
            /* This is the single point to configure DbContextOptions for YiyiCookDbContext */
            dbContextOptions.UseSqlServer(connectionString);
        }
    }
}
