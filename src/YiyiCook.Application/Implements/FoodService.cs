using Abp.ObjectMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TgnetAbp.Data;
using YiyiCook.Application.Abstractions;
using YiyiCook.Application.Dto;
using YiyiCook.Application.Dto.Food;
using YiyiCook.Application.Dto.Food.Input;
using YiyiCook.Core.Input.Food;
using YiyiCook.Core.Services;

namespace YiyiCook.Application.Implements
{
    public class FoodService :  IFoodService
    {
        private readonly IObjectMapper _objectMapper;
        private readonly IFoodClassfyDomainService _FoodClassfyDomainService;
        private readonly IFoodDomainService _FoodDomainService;
        public FoodService(
            IObjectMapper objectMapper,
            IFoodClassfyDomainService foodClassfyDomainService,
            IFoodDomainService foodDomainService)
        {
            _objectMapper = objectMapper;
            _FoodClassfyDomainService = foodClassfyDomainService;
            _FoodDomainService = foodDomainService;
        }

        public async Task<PageModel<FoodListItemDto>> GetPageFoodList(FoodSearchQueryDto query)
        {
            var foodSoures = await _FoodDomainService.Search(_objectMapper.Map<FoodSearchQuery>(query));
            var foods = foodSoures.Models.Select(p => _objectMapper.Map<FoodListItemDto>(p)).ToArray();
            var fids = foods.Select(p => p.Id).ToArray();
            var imgSource = await _FoodDomainService.GetFoodImgs(fids);
            foods = foods.Select(p =>
            {
                p.ImgIds = imgSource.Where(i => i.Fid == p.Id).Select(i => i.FileId).ToArray();
                return p;
            }).ToArray();
            var models = new PageModel<FoodListItemDto>() { Count = foodSoures.Count, Models = foods };
            return models;
        }
        
        public async Task<IEnumerable<SearchFoodByKwListItemDto>> SearchFoodByKw(FoodSearchByKwQueryDto query)
        {
            var foods = await _FoodDomainService.Search(_objectMapper.Map<FoodSearchQuery>(query));
            return _objectMapper.Map<IEnumerable<SearchFoodByKwListItemDto>>(foods);
        }
        public async Task<FoodDto> GetFood(long fid)
        {
            var food = await _FoodDomainService.Get(fid);
            var foodDto = _objectMapper.Map<FoodDto>(food);
            var imgSources = await _FoodDomainService.GetFoodImgs(fid);
            foodDto.ImgIds = imgSources.Select(p => p.FileId).ToArray();
            return foodDto;
        }
        public async Task<IEnumerable<ImageDto>> GetFoodImg(long fid)
        {
            var imgs = await _FoodDomainService.GetFoodImgs(fid);
            return imgs.Select(p=>new ImageDto() { Url = p.Url});
        }
        public async Task<IEnumerable<FoodImageListItemDto>> GetFoodImg(long[] fids)
        {
            var imgs = await _FoodDomainService.GetFoodImgs(fids);
            return imgs.GroupBy(p=>p.Fid).Select(p => new FoodImageListItemDto() { Fid = p.Key,Images =p.Select(i=> new ImageDto() { Url = i.Url }).ToArray()});
        }
        public async Task<long> AddOrUpdateFood(AddOrUpdateFoodInputDto input)
        {
           var food =  await _FoodDomainService.AddOrUpdateFood(_objectMapper.Map<AddOrUpdateFoodInput>(input));
            return food.Id;
        }
    }
}
