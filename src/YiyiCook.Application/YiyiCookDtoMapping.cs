using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using YiyiCook.Application.Dto.Food;
using YiyiCook.Application.Dto.Food.Input;
using YiyiCook.Application.Dto.FoodClassfy;
using YiyiCook.Application.Dto.FoodIngredient;
using YiyiCook.Application.Dto.FoodIngredient.Input;
using YiyiCook.Application.Dto.FoodOrder;
using YiyiCook.Application.Dto.FoodOrder.Input;
using YiyiCook.Application.Dto.FoodProduceProcess;
using YiyiCook.Core.Input.Food;
using YiyiCook.Core.Input.FoodIngredient;
using YiyiCook.Core.Input.FoodOrder;
using YiyiCook.Core.Input.FoodProduceProcess;
using YiyiCook.Core.Models;

namespace YiyiCook.Application
{
    public class YiyiCookDtoMapping : IDtoMapping
    {
        public void CreateMapping(IMapperConfigurationExpression mapperConfig)
        {
            //input
            mapperConfig.CreateMap<AddOrUpdateFoodInputDto, AddOrUpdateFoodInput>();
            mapperConfig.CreateMap<FoodSearchQueryDto, FoodSearchQuery>();
            mapperConfig.CreateMap<FoodSearchByKwQueryDto, FoodSearchQuery>();
            mapperConfig.CreateMap<AddOrUpdateFoodIngredientSourceInputDto, AddOrUpdateFoodIngredientSourceInput>();
            mapperConfig.CreateMap<AddUpdateAndDeleteFoodIngredientInputDto, AddUpdateAndDeleteFoodIngredientInput>();
            mapperConfig.CreateMap<SearchIngredientSourceQueryDto,SearchIngredientSourceQuery>();
            mapperConfig.CreateMap<AddUpdateAndDeleteFoodProduceProcessInputDto, AddUpdateAndDeleteFoodProduceProcessInput>();
            mapperConfig.CreateMap<AddFoodOrderItemInputDto, AddFoodOrderItemInput>();
            mapperConfig.CreateMap<AddFoodOrderInputDto, AddFoodOrderInput>();
            mapperConfig.CreateMap<SearchOrderQueryDto, SearchOrderQuery>();
            //output
            mapperConfig.CreateMap<FoodClassfy, FoodClassfyListItemDto>();
            mapperConfig.CreateMap<FoodClassfy, FoodClassfyDto>();
            mapperConfig.CreateMap<Food, FoodListItemDto>();
            mapperConfig.CreateMap<Food, SearchFoodByKwListItemDto>();
            mapperConfig.CreateMap<Food, FoodDto>();
            mapperConfig.CreateMap<FoodIngredientSource,FoodIngredientSourceDto>();
            mapperConfig.CreateMap<FoodIngredientSource,IngredientSourceSearchItemDto>();
            mapperConfig.CreateMap<FoodIngredient,FoodIngredientDto>();
            mapperConfig.CreateMap<FoodOrder, FoodOrderDto>();
            mapperConfig.CreateMap<FoodOrderItem, FoodOrderItemDto>();


        }
    }
}
