using System;
using System.Collections.Generic;
using System.Text;
using YiyiCook.Core.Models;
using YiyiCook.EntityFrameworkCore;
using YiyiCook.EntityFrameworkCore.Repositories;

namespace YiyiCook.Repository
{
    public class FoodIngredientSourceRepository : YiyiCookRepositoryBase<FoodIngredientSource, long>, IFoodClassfyRepository
    {
        public FoodIngredientSourceRepository(Abp.EntityFrameworkCore.IDbContextProvider<YiyiCookDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
