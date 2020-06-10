using System;
using System.Collections.Generic;
using System.Text;
using YiyiCook.Core.Models;
using YiyiCook.EntityFrameworkCore;
using YiyiCook.EntityFrameworkCore.Repositories;

namespace YiyiCook.Core.IRepositories
{
    public class FoodImgRepository : YiyiCookRepositoryBase<FoodImg, long>, IFoodImgRepository
    {
        public FoodImgRepository(Abp.EntityFrameworkCore.IDbContextProvider<YiyiCookDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
