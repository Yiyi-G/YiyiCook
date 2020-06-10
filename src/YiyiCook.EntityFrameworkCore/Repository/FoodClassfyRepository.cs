using System;
using System.Collections.Generic;
using System.Text;
using YiyiCook.Core.Models;
using YiyiCook.EntityFrameworkCore;
using YiyiCook.EntityFrameworkCore.Repositories;

namespace YiyiCook.Core.IRepositories
{
    public class FoodClassfyRepository: YiyiCookRepositoryBase<FoodClassfy, long>, IFoodClassfyRepository
    {
        public FoodClassfyRepository(Abp.EntityFrameworkCore.IDbContextProvider<YiyiCookDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
