﻿using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using YiyiCook.Core.Models;

namespace YiyiCook.Core.IRepositories
{
    public interface IFoodImgRepository : IRepository<FoodImg, long>
    {
    }
}
