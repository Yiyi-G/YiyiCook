using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TgnetAbp.Api;
using TgnetAbp.Data;
using YiyiCook.Application.Abstractions;
using YiyiCook.Application.Dto;
using YiyiCook.Application.Dto.Food;
using YiyiCook.Application.Dto.Food.Input;

namespace YiyiCook.Web.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class FoodController : YiyiCookControllerBase
    {
        private readonly IFoodService _FoodService;
        public FoodController(IFoodService foodService)
        {
            _FoodService = foodService;
        }

        public async Task<ActionResult<PageModel<FoodListItemDto>>> GetPageFoodList(FoodSearchQueryDto query)
        {
            var source = await _FoodService.GetPageFoodList(query);
            return this.JsonApiResult(ErrorCode.None, new
            {
                foods = source
            });
        }
        public async Task<ActionResult<IEnumerable<SearchFoodByKwListItemDto>>> SearchFoodByKw(FoodSearchByKwQueryDto query)
        {
            var source = await _FoodService.SearchFoodByKw(query);
            return this.JsonApiResult(ErrorCode.None, new
            {
                foods = source
            });
        }
        public async Task<ActionResult<FoodDto>> GetFood(long fid)
        {
            var source = await _FoodService.GetFood(fid);
            return this.JsonApiResult(ErrorCode.None, new
            {
                food = source
            });
        }
        public async Task<ActionResult<IEnumerable<ImageDto>>> GetFoodImg(long fid)
        {
            var source = await _FoodService.GetFoodImg(fid);
            return this.JsonApiResult(ErrorCode.None, new
            {
                food_imgs = source
            });
        }
        public async Task<ActionResult<IEnumerable<FoodImageListItemDto>>> GetFoodImg(long[] fids)
        {
            var source = await _FoodService.GetFoodImg(fids);
            return this.JsonApiResult(ErrorCode.None, new
            {
                food_imgs = source
            });
        }

        public async Task<IActionResult> AddOrUpdateFood(AddOrUpdateFoodInputDto input)
        {
            await _FoodService.AddOrUpdateFood(input);
            return this.JsonApiResult(ErrorCode.None);
        }
    }
}