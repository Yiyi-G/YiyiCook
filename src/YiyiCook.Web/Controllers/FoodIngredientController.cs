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
        [HttpPost]
        public async Task<ActionResult<FoodIngredientSourceDto>> AddOrUpdateIngredientSource([FromBody]AddOrUpdateFoodIngredientSourceInputDto input)
        {
            var source= await _FoodIngredientService.AddOrUpdateIngredientSource(input);
            return this.JsonApiResult(ErrorCode.None, new
            {
                ingredient_sources = source
            });
        }
        [HttpGet]
        public async Task<ActionResult<IngredientSourceSearchItemDto>> SearchIngredientSource([FromQuery]SearchIngredientSourceQueryDto input)
        {
            var source = await _FoodIngredientService.SearchIngredientSource(input);
            return this.JsonApiResult(ErrorCode.None, new
            {
                ingredients_sources= source
            });
        }
        [HttpPost]
        public async Task<IActionResult> AddUpdateAndDeleteFoodIngredients([FromBody]AddUpdateAndDeleteFoodIngredientsInputDto input)
        {
            await _FoodIngredientService.AddUpdateAndDeleteFoodIngredients(input);
            return this.JsonApiResult(ErrorCode.None);
        }
        [HttpGet]
        public async Task<ActionResult<FoodIngredientDto[]>> GetFoodIngredients([FromQuery]long[] fids)
        {
            var source = await _FoodIngredientService.GetFoodIngredients(fids);
            return this.JsonApiResult(ErrorCode.None, new
            {
                ingredients = source
            });
        }
    }
}