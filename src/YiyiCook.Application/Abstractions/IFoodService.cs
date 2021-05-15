using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TgnetAbp.Data;
using YiyiCook.Application.Dto;
using YiyiCook.Application.Dto.Food;
using YiyiCook.Application.Dto.Food.Input;

namespace YiyiCook.Application.Abstractions
{
    public interface IFoodService : Abp.Application.Services.IApplicationService
    {
        Task<PageModel<FoodListItemDto>> GetPageFoodList(FoodSearchQueryDto query);
        Task<IEnumerable<SearchFoodByKwListItemDto>> SearchFoodByKw(FoodSearchByKwQueryDto query);
        Task<FoodDto> GetFood(long fid);
        Task<IEnumerable<ImageDto>> GetFoodImg(long fid);
        Task<IEnumerable<FoodImageListItemDto>> GetFoodImg(long[] fids);
        Task<long> AddOrUpdateFood(AddOrUpdateFoodInputDto input);
    }
}
