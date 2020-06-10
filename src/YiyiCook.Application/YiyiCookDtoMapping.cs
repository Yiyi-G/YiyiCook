using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using YiyiCook.Application.Dto.Food.Input;
using YiyiCook.Core.Input.Food;

namespace YiyiCook.Application
{
    public class YiyiCookDtoMapping : IDtoMapping
    {
        public void CreateMapping(IMapperConfigurationExpression mapperConfig)
        {
            mapperConfig.CreateMap<AddOrUpdateFoodInputDto, AddOrUpdateFoodInput>();
            mapperConfig.CreateMap<FoodSearchQueryDto, FoodSearchQuery>();
        }
    }
}
