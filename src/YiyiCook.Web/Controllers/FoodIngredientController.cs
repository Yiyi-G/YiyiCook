using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TgnetAbp.Api;
using YiyiCook.Application.Abstractions;
using YiyiCook.Application.Dto.FoodIngredient;
using YiyiCook.Application.Dto.FoodIngredient.Input;

namespace YiyiCook.Web.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class FoodIngredientController : YiyiCookControllerBase
    {
        private readonly IFoodIngredientService _FoodIngredientService;
        public FoodIngredientController(IFoodIngredientService foodIngredientService)
        {
            _FoodIngredientService = foodIngredientService;
        }

        public async Task<ActionResult<FoodIngredientSourceDto>> AddOrUpdateIngredientSource(AddOrUpdateFoodIngredientSourceInputDto input)
        {
            var source= await _FoodIngredientService.AddOrUpdateIngredientSource(input);
            return this.JsonApiResult(ErrorCode.None, new
            {
                ingredient_sources = source
            });
        }
        public async Task<ActionResult<IngredientSourceSearchItemDto>> SearchIngredientSource(SearchIngredientSourceQueryDto input)
        {
            var source = await _FoodIngredientService.SearchIngredientSource(input);
            return this.JsonApiResult(ErrorCode.None, new
            {
                ingredients_sources= source
            });
        }
        public async Task<IActionResult> AddUpdateAndDeleteFoodIngredients(AddUpdateAndDeleteFoodIngredientsInputDto input)
        {
            await _FoodIngredientService.AddUpdateAndDeleteFoodIngredients(input);
            return this.JsonApiResult(ErrorCode.None);
        }
        public async Task<ActionResult<FoodIngredientDto[]>> GetFoodIngredients(long[] fids)
        {
            var source = await _FoodIngredientService.GetFoodIngredients(fids);
            return this.JsonApiResult(ErrorCode.None, new
            {
                ingredients = source
            });
        }
    }
}