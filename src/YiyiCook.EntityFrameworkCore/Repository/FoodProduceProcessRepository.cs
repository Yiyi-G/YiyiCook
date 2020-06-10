using System;
using System.Collections.Generic;
using System.Text;
using YiyiCook.Core.Models;
using YiyiCook.EntityFrameworkCore;
using YiyiCook.EntityFrameworkCore.Repositories;

namespace YiyiCook.Core.IRepositories
{
    public class FoodProduceProcessRepository : YiyiCookRepositoryBase<FoodProduceProcess, long>, IFoodProduceProcessRepository
    {
        public FoodProduceProcessRepository(Abp.EntityFrameworkCore.IDbContextProvider<YiyiCookDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
