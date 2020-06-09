using YiyiCook.EntityFrameworkCore;

namespace YiyiCook.Tests.TestDatas
{
    public class TestDataBuilder
    {
        private readonly YiyiCookDbContext _context;

        public TestDataBuilder(YiyiCookDbContext context)
        {
            _context = context;
        }

        public void Build()
        {
            //create test data here...
        }
    }
}