using System;
using System.Collections.Generic;
using System.Text;
using YiyiCook.Core.Models;
using YiyiCook.EntityFrameworkCore;
using YiyiCook.EntityFrameworkCore.Repositories;

namespace YiyiCook.Core.IRepositories
{
    public class FoodIngredientRepository : YiyiCookRepositoryBase<FoodIngredient, long>, IFoodIngredientRepository
    {
        public FoodIngredientRepository(Abp.EntityFrameworkCore.IDbContextProvider<YiyiCookDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
